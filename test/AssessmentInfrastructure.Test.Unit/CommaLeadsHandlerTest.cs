using FluentAssertions;
using Infrastructure.Services;
using Xunit;

namespace AssessmentInfrastructure.Test.Unit
{
    public class CommaLeadsHandlerTest
    {
        private readonly CommaLeadsHandler _sut = new CommaLeadsHandler();
        
        [Fact]
        public void FetchLeads_ShouldReturnRecords_WhenPathIsNotPassed()
        {
            var leads =  _sut.FetchLeads(string.Empty);

            leads.Should().Contain(",");
        }
        
        [Fact]
        public void FetchLeads_ShouldSpace_WhenDelimiterIsInvoked()
        {
            var delimiter =  _sut.DelimiterCharacter;

            delimiter.Should().Be(',');
        }
    }
}