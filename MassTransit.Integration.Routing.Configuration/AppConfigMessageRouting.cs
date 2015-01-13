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
using System.Configuration;
using System.Linq;
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
            var section = (MessageRoutingSettings)ConfigurationManager.GetSection(string.Format(@"{0}/{1}", ConfigurationGroupName, _configurationKey));
            if (section == null) return;
            if (section.Routes == null) return;
            var routes = (from MessageTypeCollection route in section.Routes let uri = new Uri(route.Uri) 
                          from MessageTypeElement messageType in route let type = Type.GetType(messageType.Type, true, true) 
                          select new Tuple<Uri, Type>(uri, type)).ToList();
            foreach (var routingElement in routes)
            {
                var route = _routingConfigurator.FastInvoke<RoutingConfigurator, RouteTo>(new[] { routingElement.Item2 }, "Route");
                route.To(routingElement.Item1);
            }
        }
    }
}