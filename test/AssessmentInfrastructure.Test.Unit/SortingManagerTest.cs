using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using FluentAssertions;
using Infrastructure.Utility;
using Xunit;

namespace AssessmentInfrastructure.Test.Unit
{
    public class SortingManagerTest
    {
        [Fact]
        public void SortedByPropertyAndProjectParams_ShouldReturnTheOrder_WhenInvoked()
        {
            var actual = new List<Lead>
            {
                new Lead()
                {
                    FirstName = "Bey",
                    LastName = "Allen",
                    PhoneNumber = "+14155550444",
                    Project = "Construction",
                    PropertyType  = PropertyType.Condo,
                    StartDate = "1/8/2022"
                },
                new Lead()
                {
                    FirstName = "Shawn",
                    LastName = "Brad",
                    PhoneNumber = "+14155550777",
                    Project = "MarlitePanels-(FED)",
                    PropertyType  = PropertyType.Trailer,
                    StartDate = "9/10/2022"
                },
                new Lead()
                {
                    FirstName = "Bob",
                    LastName = "James",
                    PhoneNumber = "+14155550132",
                    Project = "Industrial",
                    PropertyType  = PropertyType.House,
                    StartDate = "3/11/2022"
                },
              
            };

            var leads = actual.SortedByPropertyAndProjectParams().ToList();

            leads.Should().HaveCount(3);
            leads.Should().BeInAscendingOrder(l => l.PropertyType);
            // .And.BeInAscendingOrder(l => l.Project);

        }
        
        [Fact]
        public void SortedByLastNameDesc_ShouldReturnTheOrder_WhenInvoked()
        {
            var actual = new List<Lead>
            {
                new Lead()
                {
                    FirstName = "Bey",
                    LastName = "Allen",
                    PhoneNumber = "+14155550444",
                    Project = "Construction",
                    PropertyType  = PropertyType.Condo,
                    StartDate = "1/8/2022"
                },
                new Lead()
                {
                    FirstName = "Shawn",
                    LastName = "Brad",
                    PhoneNumber = "+14155550777",
                    Project = "MarlitePanels-(FED)",
                    PropertyType  = PropertyType.Trailer,
                    StartDate = "9/10/2022"
                },
                new Lead()
                {
                    FirstName = "Bob",
                    LastName = "James",
                    PhoneNumber = "+14155550132",
                    Project = "Industrial",
                    PropertyType  = PropertyType.House,
                    StartDate = "3/11/2022"
                },
              
            };

            var leads = actual.SortedByLastNameDesc().ToList();

            leads.Should().HaveCount(3);
            leads.Should().BeInDescendingOrder(l => l.LastName);

        }
        
        [Fact]
        public void SortedByStartDateAsc_ShouldReturnTheOrder_WhenInvoked()
        {
            var actual = new List<Lead>
            {
                new Lead()
                {
                    FirstName = "Bey",
                    LastName = "Allen",
                    PhoneNumber = "+14155550444",
                    Project = "Construction",
                    PropertyType  = PropertyType.Condo,
                    StartDate = "1/8/2022"
                },
                new Lead()
                {
                    FirstName = "Shawn",
                    LastName = "Brad",
                    PhoneNumber = "+14155550777",
                    Project = "MarlitePanels-(FED)",
                    PropertyType  = PropertyType.Trailer,
                    StartDate = "9/10/2022"
                },
                new Lead()
                {
                    FirstName = "Bob",
                    LastName = "James",
                    PhoneNumber = "+14155550132",
                    Project = "Industrial",
                    PropertyType  = PropertyType.House,
                    StartDate = "3/11/2022"
                },
              
            };

            var leads = actual.SortedByStartDateAsc().ToList();

            leads.Should().HaveCount(3);
            leads.Should().BeInAscendingOrder(l => l.StartDate);

        }
        
        [Fact]
        public void SortedByProject_ShouldReturnTheOrder_WhenInvoked()
        {
            var actual = new List<Lead>
            {
                new()
                {
                    FirstName = "Bey",
                    LastName = "Allen",
                    PhoneNumber = "+14155550444",
                    Project = "Construction",
                    PropertyType  = PropertyType.Condo,
                    StartDate = "1/8/2022"
                },
                new()
                {
                    FirstName = "Shawn",
                    LastName = "Brad",
                    PhoneNumber = "+14155550777",
                    Project = "MarlitePanels-(FED)",
                    PropertyType  = PropertyType.Trailer,
                    StartDate = "9/10/2022"
                },
                new()
                {
                    FirstName = "Bob",
                    LastName = "James",
                    PhoneNumber = "+14155550132",
                    Project = "Industrial",
                    PropertyType  = PropertyType.House,
                    StartDate = "3/11/2022"
                },
              
            };

            var leads = actual.SortedByProjects().ToList();

            leads.Should().HaveCount(3);
            leads.Should().BeInAscendingOrder(l => l.Project);

        }
    }
}