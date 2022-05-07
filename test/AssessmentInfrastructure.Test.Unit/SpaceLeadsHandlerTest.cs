using FluentAssertions;
using Infrastructure.Services;
using Xunit;

namespace AssessmentInfrastructure.Test.Unit.TestResults
{
    public class SpaceLeadsHandlerTest
    {
        private readonly SpaceLeadsHandler _sut = new SpaceLeadsHandler();
        
        [Fact]
        public void FetchLeads_ShouldReturnRecords_WhenPathIsNotPassed()
        {
            var leads =  _sut.FetchLeads(string.Empty);
            
            leads.Should().Contain(" ");
            leads.Should().NotBeEmpty();
        }
        
        [Fact]
        public void FetchLeads_ShouldSpace_WhenDelimiterIsInvoked()
        {
            var delimiter =  _sut.DelimiterCharacter;

            delimiter.Should().Be(' ');
        }
    }
}