using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application;
using Application.Leads.Queries;
using Application.Leads.Queries.DTOs;
using Domain;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Assessement.Application.Test.Unit
{
    public class FetchLeadsByPropertyTypeHandlerTest
    {
        private readonly FetchLeadsByPropertyTypeHandler _sut;
        private readonly IParseLeads _parseLeads = Substitute.For<IParseLeads>();
        
        public FetchLeadsByPropertyTypeHandlerTest()
        {
            _sut = new FetchLeadsByPropertyTypeHandler(_parseLeads);
        }

        [Fact]
        public async Task Handle_ShouldReturnSortedPropertyType_WhenInvoked()
        {
            var parsedleads = new List<Lead>
            {
                new()
                {
                    FirstName = "Bey",
                    LastName = "Allen",
                    PhoneNumber = "+14155550444",
                    Project = "Construction",
                    PropertyType = PropertyType.Condo,
                    StartDate = "1/8/2022"
                },
            };
            
            var expected = new LeadsBySortedParameters()
            {
                Leads = parsedleads,
                ResponseDescription = $"PropertyType Leads retrieval completed successfully",
                ResponseCode = "00"
            };

            _parseLeads.FetchProcessedLeads().Returns(parsedleads);
            
            var response = await _sut.Handle(new FetchLeadsByPropertyType(), new CancellationToken());

            response.Should().BeEquivalentTo(expected);
        }
    }
}