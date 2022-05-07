using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using FluentAssertions;
using Infrastructure;
using Infrastructure.Utility;
using Xunit;

namespace AssessmentInfrastructure.Test.Unit
{
    public class LeadsConverterTest
    {
        private readonly LeadsConverter _sut;

        public LeadsConverterTest()
        {
            _sut = new LeadsConverter();
        }

        [Fact]
        public async Task ParseRawLead_ReturnLeadsObjects_WhenRawLeadsArePassed()
        {
            var rawLead =  @"James|Bob|House|Industrial|2022-03-11|+14155550132
                        Allen|Bey|Condo|Construction|2022-1-8|+14155550444
                        Brad|Shawn|Trailer|MarlitePanels-(FED)|2022-9-10|+14155550777";

            var processedLeads = new Lead
            {
                FirstName = "Shawn",
                LastName = "Brad",
                PhoneNumber = "+14155550777",
                PropertyType = PropertyType.Trailer,
                Project = "MarlitePanels-(FED)"
            };

            var leads = await _sut.ParseRawLead(rawLead, '|');

            var enumerable = leads as Lead[] ?? leads.ToArray();
            enumerable.Should().HaveCount(3);
            enumerable.Should().Contain(l => l.FirstName == "Shawn");
        }
        
        [Fact]
        public async Task ParseRawLead_ReturnThrowException_WhenDelimiterIsIncorrect()
        {
            const string rawLead = @"James|Bob|House|Industrial|2022-03-11|+14155550132
                        Allen|Bey|Condo|Construction|2022-1-8|+14155550444
                        Brad|Shawn|Trailer|MarlitePanels-(FED)|2022-9-10|+14155550777";
            
            Func<Task> leads = () => _sut.ParseRawLead(rawLead, ',');
            await leads.Should().ThrowAsync<Exception>().WithMessage("Index was outside the bounds of the array.");
        }
    }
}