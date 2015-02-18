using Microsoft.VisualStudio.TestTools.UnitTesting;
using MS.QualityTools.UnitTestFramework.Specifications;

namespace MassTransit.Integration.Routing.Configuration.Tests.Specs
{
    [TestClass]
    [SpecificationDescription("Type name asterisk wildcard matching specifications")]
    public class TypeNameAsteriskWildcardMatchExtensionMethodsSpecs: Specification
    {
        [TestMethod]
        [ScenarioDescription("Valid type name filter with asterisk matches")]
        public void MatchesValidTypeNameFilterWithAsterisk()
        {
            Given("A valid type full name", scx => { })
            .And("a valid type name filter with an asterisk included", scx => { })
            .Then("MatchesTypeNameFilter method should match", 
                    scx => "MassTransit.Integration.Routing.Configuration.MessageRoutingConfiguration"
                        .MatchesTypeNameFilter("MassTransit.Integration.Routing.Configuration.*Configuration"));
        }

        [TestMethod]
        [ScenarioDescription("Non valid type name filter with asterisk doesn't match")]
        public void NotMatchesNonValidTypeNameFilterWithAsterisk()
        {
            Given("A valid type full name", scx => { })
            .And("a non valid type name filter with an asterisk included", scx => { })
            .Then("MatchesTypeNameFilter method should not match",
                    scx => !"MassTransit.Integration.Routing.Configuration.MessageRoutingConfiguration"
                        .MatchesTypeNameFilter("MassTransit.Configuration.*Configuration"));
        }

        [TestMethod]
        [ScenarioDescription("Valid type name filter with two asterisks matches")]
        public void MatchesValidTypeNameFilterWith2Asterisks()
        {
            Given("A valid type full name", scx => { })
            .And("a valid type name filter with 2 asterisks included", scx => { })
            .Then("MatchesTypeNameFilter method should match",
                    scx => "MassTransit.Integration.Routing.Configuration.MessageRoutingConfiguration"
                        .MatchesTypeNameFilter("MassTransit.Integration.Routing.*.*Configuration"));
        }

        [TestMethod]
        [ScenarioDescription("Valid type name filter with asterisk at the beginning matches")]
        public void MatchesValidTypeNameFilterWithAsteriskAtBeginning()
        {
            Given("A valid type full name", scx => { })
            .And("a valid type name filter with an asterisk at the beginning included", scx => { })
            .Then("MatchesTypeNameFilter method should match",
                    scx => "MassTransit.Integration.Routing.Configuration.MessageRoutingConfiguration"
                        .MatchesTypeNameFilter("*.Integration.Routing.Configuration.MessageRoutingConfiguration"));
        }

        [TestMethod]
        [ScenarioDescription("Valid type name filter with asterisk at the end matches")]
        public void MatchesValidTypeNameFilterWithAsteriskAtEnd()
        {
            Given("A valid type full name", scx => { })
            .And("a valid type name filter with an asterisk at the end included", scx => { })
            .Then("MatchesTypeNameFilter method should match",
                    scx => "MassTransit.Integration.Routing.Configuration.MessageRoutingConfiguration"
                        .MatchesTypeNameFilter("MassTransit.Integration.Routing.Configuration*"));
        }

        [TestMethod]
        [ScenarioDescription("Valid type name filter with just an asterisk matches")]
        public void MatchesValidTypeNameFilterWithJustAsterisk()
        {
            Given("A valid type full name", scx => { })
            .And("a valid type name filter with just an asterisk", scx => { })
            .Then("MatchesTypeNameFilter method should match",
                    scx => "MassTransit.Integration.Routing.Configuration.MessageRoutingConfiguration"
                        .MatchesTypeNameFilter("*"));
        }
    }
}
