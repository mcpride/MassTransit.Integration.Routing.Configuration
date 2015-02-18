// Copyright � Marco Stolze 2015
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Magnum.Reflection;
using MassTransit.Services.Routing.Configuration;

namespace MassTransit.Integration.Routing.Configuration
{
    public class AppConfigMessageRouting : MessageRoutingConfiguration
    {
        private readonly RoutingConfigurator _routingConfigurator;
        private readonly string _configurationKey;
        private const string ConfigurationGroupName = "masstransit.routing";

        public AppConfigMessageRouting(RoutingConfigurator routingConfigurator, string configurationKey)
        {
            _routingConfigurator = routingConfigurator;
            _configurationKey = configurationKey;
        }

        public override void Configure()
        {
            IDictionary<Type, IList<Uri>> routedTypes = new Dictionary<Type, IList<Uri>>();
            IDictionary<Assembly, IList<Type>> cachedAssemblyTypes = new Dictionary<Assembly, IList<Type>>();
            var section = (MessageRoutingSettings)ConfigurationManager.GetSection(string.Format(@"{0}/{1}", ConfigurationGroupName, _configurationKey));
            if (section == null) return;
            if (section.Routes == null) return;
            var routes = (from MessageTypeCollection route in section.Routes let uri = new Uri(route.Uri) from MessageTypeElement messageType in route 
                          where !string.IsNullOrWhiteSpace(messageType.Type) select new Tuple<Uri, string>(uri, messageType.Type)).ToList();
            foreach (var routingElement in routes)
            {
                if (!routingElement.Item2.IsTypeNameFilter())
                {
                    var type = Type.GetType(routingElement.Item2, false, true);
                    if (type == null) continue;
                    RouteTo(type, routingElement.Item1, routedTypes);
                }
                else
                {
                    string typeNameFilter;
                    string assemblyName;
                    if (!TrySplitFullTypeNameFilter(routingElement.Item2, out typeNameFilter, out assemblyName)) continue;
                    Assembly assembly;
                    if (!TryLoadAssembly(assemblyName, out assembly)) continue;
                    var assemblyTypes = TypesOfAssembly(assembly, cachedAssemblyTypes);
                    foreach (var type in assemblyTypes)
                    {
                        if (type.FullName.MatchesTypeNameFilter(typeNameFilter)) RouteTo(type, routingElement.Item1, routedTypes);
                    }

                }
            }
        }

        private void RouteTo(Type type, Uri uri, IDictionary<Type, IList<Uri>> routedTypes)
        {
            if (IsRouted(type, uri, routedTypes)) return;
            var route = _routingConfigurator.FastInvoke<RoutingConfigurator, RouteTo>(new[] {type}, "Route");
            route.To(uri);
        }

        private static bool TryLoadAssembly(string assemblyName, out Assembly assembly)
        {
            assembly = null;
            try
            {
                assembly = Assembly.Load(assemblyName);
                return (assembly != null);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool TrySplitFullTypeNameFilter(string fullTypeNameFilter, out string typeNameFilter, out string assemblyName)
        {
            typeNameFilter = null;
            assemblyName = null;
            var splitIdx = fullTypeNameFilter.IndexOf(',');
            if (splitIdx < 1) return false;
            typeNameFilter = fullTypeNameFilter.Substring(0, splitIdx).Trim();
            assemblyName = fullTypeNameFilter.Substring(splitIdx + 1).Trim();
            return true;
        }

        private static bool IsRouted(Type type, Uri uri, IDictionary<Type, IList<Uri>> routedTypes)
        {
            if (routedTypes.ContainsKey(type))
            {
                var uriList = routedTypes[type];
                if (uriList.Contains(uri)) return true;
                uriList.Add(uri);
            }
            else
            {
                routedTypes[type] = new List<Uri> { uri };
            }
            return false;
        }

        private static IList<Type> TypesOfAssembly(Assembly assembly,
            IDictionary<Assembly, IList<Type>> cachedAssemblyTypes)
        {
            IList<Type> assemblyTypes;
            if (cachedAssemblyTypes.TryGetValue(assembly, out assemblyTypes)) return assemblyTypes;
            assemblyTypes = assembly.GetTypes().Where(t => (t.IsClass && !(t.IsAbstract || t.IsArray || t.IsInterface || t.IsEnum || t.IsGenericType || t.IsGenericTypeDefinition))).ToList();
            cachedAssemblyTypes[assembly] = assemblyTypes;
            return assemblyTypes;
        }
    }
}