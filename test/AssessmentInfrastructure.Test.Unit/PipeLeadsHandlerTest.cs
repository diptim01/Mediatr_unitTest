using FluentAssertions;
using Infrastructure.Services;
using Xunit;

namespace AssessmentInfrastructure.Test.Unit
{
    public class PipeLeadsHandlerTest
    {
        private readonly PipeLeadsHandler _sut = new PipeLeadsHandler();
        
        [Fact]
        public void FetchLeads_ShouldReturnRecords_WhenPathIsNotPassed()
        {
            var leads =  _sut.FetchLeads(string.Empty);

            leads.Should().Contain("|");
        }
        
        [Fact]
        public void FetchLeads_ShouldSpace_WhenDelimiterIsInvoked()
        {
            var delimiter =  _sut.DelimiterCharacter;

            delimiter.Should().Be('|');
        }

        [Fact]
        public void AddToLeads_ShouldIncludeNewLeads_WhenSupplied()
        {
            var newLead = "Test|MAKX|Trailer|MarlitePanels-(FED)|2018-2-1|+14155440777";

            var isAdded = _sut.AddToLeads(newLead);

            isAdded.Should().BeTrue();
        }
        
        [Fact]
        public void AddToLeads_ShouldReturnFalse_WhenWrongLeadsIsSupplied()
        {
            var newLead = "";

            var isAdded = _sut.AddToLeads(newLead);

            isAdded.Should().BeFalse();
        }

        [Fact]
        public void AddToLeads_ShouldReturnFalse_WhenInAccurateleadsAreSupplied()
        {
            var newLead = "Test|MAKX|Trailer|MarlitePanels-(FED)|2018-2-1";

            var isAdded = _sut.AddToLeads(newLead);

            isAdded.Should().BeFalse();
        }
    }
}