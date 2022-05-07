using Application.Contracts;
using Assessment.API.Common;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xunit;

namespace Assessment.API.Test.Unit
{
    public class CustomConfigurationManagerTest
    {
        private readonly CustomConfigurationManager _sut;
        private readonly IConfiguration _configuration = Substitute.For<IConfiguration>();
        
        public CustomConfigurationManagerTest()
        {
            _sut = new CustomConfigurationManager(_configuration);
        }

        [Fact]
        public void RetrieveAppSettingsValue_ShouldReturnVariable_WhenInvoked()
        {
            var variableSettingsName = "test";
            _configuration[variableSettingsName].Returns("testconfig");

            var response = _sut.RetrieveAppSettingsValue(variableSettingsName);

            response.Should().Be("testconfig");
        }
    }
}