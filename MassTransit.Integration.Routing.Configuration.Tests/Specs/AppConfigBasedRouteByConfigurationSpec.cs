using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using MassTransit.Services.Routing.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MS.QualityTools.UnitTestFramework.Specifications;

namespace MassTransit.Integration.Routing.Configuration.Tests.Specs
{
    [TestClass]
    [SpecificationDescription("App.config based RouteByConfiguration specification")]
    public class AppConfigBasedRouteByConfigurationSpec : Specification
    {
        [TestMethod]
        [ScenarioDescription("Initialize App.config based message routing")]
        public void Initialize_AppConfig_based_message_routing()
        {
            IServiceBus serviceBus = null;
            Given("a msmq initialized service bus with 4 well known message routes and 1 unknown message from app.config",
                sc =>
                {
                    serviceBus = ServiceBusFactory.New(sbc =>
                    {
                        sbc.UseMsmq(mqc =>
                        {
                            mqc.UseMulticastSubscriptionClient();
                            mqc.VerifyMsmqConfiguration();
                        });
                        sbc.ReceiveFrom("msmq://localhost/AppConfigRoutingConfigTestQueue");
                        // ReSharper disable CSharpWarnings::CS0618
                        sbc.ConfigureService<RoutingConfigurator>(BusServiceLayer.Session, routingConfigurator =>
                        // ReSharper restore CSharpWarnings::CS0618
                        {
                            routingConfigurator.RouteByConfiguration("myMessageRoutings");
                            routingConfigurator.RouteByConfiguration("otherMessageRoutings");
                            routingConfigurator.RouteByConfiguration("unknownSection");
                        });
                    });
                })
            .Then("4 defined routes should be available in the diagnostic probe entry with key zz.mt.outbound_pipeline", sc =>
            {
                var outboundConfigFound = false;
                var probe = serviceBus.Probe();
                foreach (var entry in probe.Entries.Where(entry => entry.Key == "zz.mt.outbound_pipeline"))
                {
                    outboundConfigFound = true;
                    //Console.WriteLine(entry.Value);
                    var matches = Regex.Matches(entry.Value, @"\b(Send).*$", RegexOptions.ExplicitCapture | RegexOptions.Multiline);
                    matches.Count.Should().Be(4);
                    break;
                }
                return outboundConfigFound;
            });
        }
    }
}
