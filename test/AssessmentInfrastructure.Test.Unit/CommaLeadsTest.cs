using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Domain;
using FluentAssertions;
using Infrastructure;
using Infrastructure.Utility;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AssessmentInfrastructure.Test.Unit
{
    public class CommaLeadsTest
    {
        private readonly CommaLeads _sut;
        private readonly IFileHandler _fileHandler = Substitute.For<IFileHandler>();
        private readonly ILeadConverter _leadsConverter = Substitute.For<ILeadConverter>();
        
        public CommaLeadsTest()
        {
            _sut = new CommaLeads(_fileHandler, _leadsConverter);
        }
        
        [Fact]
        public async Task FetchProcessedLeads_ShouldReturnProcessedLeads_WhenLeadsExist()
        {
            var lead = new Lead
            {
                FirstName = "Olu",
                LastName = "Makinde",
                PhoneNumber = "+23418687077",
                PropertyType = PropertyType.House,
                Project = "Marlite_Panels(FED)",
                StartDate = "09/9/2022"
            };

            var tmpLeads = new List<Lead>(){lead};
            
            _leadsConverter.ParseRawLead(Arg.Any<string>(), _fileHandler.DelimiterCharacter)
                .Returns(tmpLeads);
            
            var leads =  await _sut.FetchProcessedLeads();
            var enumerable = leads as Lead[] ?? leads.ToArray();
            
            enumerable.Should().ContainEquivalentOf(lead);
            enumerable.Should().HaveCount(1);
            enumerable.Should().Contain(x => x.FirstName == "Olu" &&
                                             x.LastName == "Makinde" && x.PhoneNumber == "+23418687077" && x.PropertyType == PropertyType.House
                                             && x.Project == "Marlite_Panels(FED)" && x.StartDate == "09/9/2022");
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