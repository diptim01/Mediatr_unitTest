using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Domain;
using FluentAssertions;
using Infrastructure;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AssessmentInfrastructure.Test.Unit
{
    public class PipeLeadsTest
    {
        private readonly PipeLeads _sut;
        private readonly IFileHandler _fileHandler = Substitute.For<IFileHandler>();
        private readonly ILeadConverter _leadsConverter = Substitute.For<ILeadConverter>();
        
        public PipeLeadsTest()
        {
            _sut = new PipeLeads(_fileHandler, _leadsConverter);
        }
        
        [Fact]
        public async Task FetchProcessedLeads_ShouldReturnProcessedLeads_WhenLeadsExist()
        {
            var lead = new Lead
            {
                FirstName = "Kenny",
                LastName = "Makinde",
                PhoneNumber = "+23418687077",
                PropertyType = PropertyType.House,
                Project = "Marlite_Panels(FED)"
            };

            var tmpLeads = new List<Lead>(){lead};
            
            _leadsConverter.ParseRawLead(Arg.Any<string>(), _fileHandler.DelimiterCharacter)
                .Returns(tmpLeads);
            
            var leads =  await _sut.FetchProcessedLeads();
            var enumerable = leads as Lead[] ?? leads.ToArray();
            
            enumerable.Should().ContainEquivalentOf(lead);
            enumerable.Should().HaveCount(1);
            enumerable.Should().Contain(x => x.FirstName == "Kenny");
            enumerable.Should().NotBeNull();
        }
        
        [Fact]
        public  async Task FetchProcessedLeads_ShouldThrowAnException_WhenLeadsIsWrong()
        {
            var exception = new ArgumentException("Something is wrong with the leads");
            
            _leadsConverter.ParseRawLead(Arg.Any<string>(), _fileHandler.DelimiterCharacter)
                .Throws(exception);
            
            Func<Task> leads = () => _sut.FetchProcessedLeads(); 
            
            await leads.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Something is wrong with the leads");
        }
    }
}