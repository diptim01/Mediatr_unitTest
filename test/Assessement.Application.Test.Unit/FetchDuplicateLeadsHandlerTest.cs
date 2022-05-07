using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application;
using Application.Interfaces;
using Application.Leads.Queries;
using Application.Leads.Queries.DTOs;
using Domain;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Assessement.Application.Test.Unit
{
    public class FetchDuplicateLeadsHandlerTest
    {
        private readonly FetchDuplicateLeadsHandler _sut;
        private readonly IParseLeads _parseLeads = Substitute.For<IParseLeads>();
        private readonly IAPICalls<APiLead> _apiCalls = Substitute.For<IAPICalls<APiLead>>();
        private readonly IConfigurationManager _configurationManager = Substitute.For<IConfigurationManager>();

        public FetchDuplicateLeadsHandlerTest()
        {
            _sut = new FetchDuplicateLeadsHandler(_parseLeads, _apiCalls, _configurationManager);
        }

        [Fact]
        public async Task Handle_ShouldReturnDuplicateLeads_WhenInvoked()
        {
            var apiLeads = new List<APiLead>
            {
                new()
                {
                    FirstName = "Bey",
                    LastName = "Allen",
                    Phone = "+14155550444",
                    Project = "Construction",
                    PropertyType = "Condo",
                    StartDate = "1/8/2022"
                },
            };

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
                ResponseDescription = $"{parsedleads.Count} Duplicate(s) found",
                ResponseCode = "00"
            };

            _apiCalls.GetAll(Arg.Any<string>()).Returns(apiLeads);
            _parseLeads.FetchProcessedLeads().Returns(parsedleads);

            var response = await _sut.Handle(new FetchDuplicateLeads(),
                new CancellationToken());

            response.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task Handle_ShouldReturnNoLeads_WhenInvoked()
        {
            var apiLeads = new List<APiLead>
            {
                new()
                {
                    FirstName = "Bey",
                    LastName = "Allen",
                    Phone = "+14155550455",
                    Project = "Construction",
                    PropertyType = "Condo",
                    StartDate = "1/8/2022"
                },
            };

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
                ResponseDescription = $"No Duplicate found",
                ResponseCode = "00"
            };

            _apiCalls.GetAll(Arg.Any<string>()).Returns(apiLeads);
            _parseLeads.FetchProcessedLeads().Returns(parsedleads);

            var response = await _sut.Handle(new FetchDuplicateLeads(),
                new CancellationToken());

            response.Should().BeEquivalentTo(expected);
        }
    }
}
