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
    public class FetchLeadsByProjectHandlerTest
    {
        private readonly FetchLeadsByProjectHandler _sut;
        private readonly IParseLeads _parseLeads = Substitute.For<IParseLeads>();
        
        public FetchLeadsByProjectHandlerTest()
        {
            _sut = new FetchLeadsByProjectHandler(_parseLeads);
        }

        [Fact]
        public async Task Handle_ShouldReturnSortedProject_WhenInvoked()
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
                ResponseDescription = $"Sorted Project Leads retrieval completed successfully",
                ResponseCode = "00"
            };

            _parseLeads.FetchProcessedLeads().Returns(parsedleads);
            
            var response = await _sut.Handle(new FetchLeadsByProject(), new CancellationToken());

            response.Should().BeEquivalentTo(expected);
        }
    }
}