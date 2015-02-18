using Microsoft.VisualStudio.TestTools.UnitTesting;
using MS.QualityTools.UnitTestFramework.Specifications;

namespace MassTransit.Integration.Routing.Configuration.Tests.Specs
{
    [TestClass]
    [SpecificationDescription("Type name question mark wildcard matching specifications")]
    public class TypeNameQuestionMarkWildcardMatchExtensionMethodsSpecs : Specification
    {
        [TestMethod]
        [ScenarioDescription("Valid type name filter with question mark matches")]
        public void MatchesValidTypeNameFilterWithQuestionMark()
        {
            Given("A valid type full name", scx => { })
            .And("a valid type name filter with an question mark included", scx => { })
            .Then("MatchesTypeNameFilter method should match",
                    scx => "MassTransit.Integration.Routing.Configuration.MessageRoutingConfiguration"
                        .MatchesTypeNameFilter("MassTransit.Integration.Routing.Configuration.Message?outingConfiguration"));
        }

        [TestMethod]
        [ScenarioDescription("Non valid type name filter with question mark doesn't match")]
        public void NotMatchesNonValidTypeNameFilterWithQuestionMark()
        {
            Given("A valid type full name", scx => { })
            .And("a non valid type name filter with an question mark included", scx => { })
            .Then("MatchesTypeNameFilter method should not match",
                    scx => !"MassTransit.Integration.Routing.Configuration.MessageRoutingConfiguration"
                        .MatchesTypeNameFilter("MassTransit.Integration.Routing.Configuration.Message?utingConfiguration"));
        }

        [TestMethod]
        [ScenarioDescription("Valid type name filter with two question marks matches")]
        public void MatchesValidTypeNameFilterWith2QuestionMarks()
        {
            Given("A valid type full name", scx => { })
            .And("a valid type name filter with 2 question marks included", scx => { })
            .Then("MatchesTypeNameFilter method should match",
                    scx => "MassTransit.Integration.Routing.Configuration.MessageRoutingConfiguration"
                        .MatchesTypeNameFilter("MassTransit.Integration.Routing.Con?iguration.?essageRoutingConfiguration"));
        }

        [TestMethod]
        [ScenarioDescription("Valid type name filter with two following question marks matches")]
        public void MatchesValidTypeNameFilterWith2FollowingQuestionMarks()
        {
            Given("A valid type full name", scx => { })
            .And("a valid type name filter with 2 following question marks included", scx => { })
            .Then("MatchesTypeNameFilter method should match",
                    scx => "MassTransit.Integration.Routing.Configuration.MessageRoutingConfiguration"
                        .MatchesTypeNameFilter("MassTransit.Integration.Routing.Configuration.??ssageRoutingConfiguration"));
        }

        [TestMethod]
        [ScenarioDescription("Valid type name filter with question mark at the beginning matches")]
        public void MatchesValidTypeNameFilterWithQuestionMarkAtBeginning()
        {
            Given("A valid type full name", scx => { })
            .And("a valid type name filter with a question mark at the beginning included", scx => { })
            .Then("MatchesTypeNameFilter method should match",
                    scx => "MassTransit.Integration.Routing.Configuration.MessageRoutingConfiguration"
                        .MatchesTypeNameFilter("?assTransit.Integration.Routing.Configuration.MessageRoutingConfiguration"));
        }

        [TestMethod]
        [ScenarioDescription("Valid type name filter with question mark at the end matches")]
        public void MatchesValidTypeNameFilterWithQuestionMarkAtEnd()
        {
            Given("A valid type full name", scx => { })
            .And("a valid type name filter with a question mark at the end included", scx => { })
            .Then("MatchesTypeNameFilter method should match",
                    scx => "MassTransit.Integration.Routing.Configuration.MessageRoutingConfiguration"
                        .MatchesTypeNameFilter("MassTransit.Integration.Routing.Configuration.MessageRoutingConfiguratio?"));
        }
    }
}
