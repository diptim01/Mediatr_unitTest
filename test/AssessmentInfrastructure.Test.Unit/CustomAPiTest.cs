using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Contracts;
using Domain;
using FluentAssertions;
using Infrastructure;
using Infrastructure.Services;
using NSubstitute;
using Xunit;

namespace AssessmentInfrastructure.Test.Unit
{
    public class CustomAPiTest
    {
        private readonly CustomAPICalls<Lead> _sut;
        private readonly IHttpClientFactory _httpClientFactory = Substitute.For<IHttpClientFactory>();

        
        public CustomAPiTest()
        {
            _sut = new CustomAPICalls<Lead>(_httpClientFactory);
        }

        [Fact]
        public async Task Get_ReturnLeads_WhenURLIsPassed()
        {
            var httpClient = new HttpClient(new FakeHttpMessageHandler()) { BaseAddress=new Uri("https://localhost")};
            _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);
            
            var response = await _sut.GetAll("");

            response.Should().NotBeNull();
            response.Should().HaveCount(l => l > 0);
        }
        
        [Fact]
        public async Task GetAll_ThrowError_WhenCallIsUnSuccessful()
        {
            var httpClient = new HttpClient(new FakeHttpMessageHandler.ErrorHttpMessageHandler()) { BaseAddress=new Uri("https://localhost")};
            _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);
            
            Func<Task> response = () =>  _sut.GetAll("");

            await response.Should().ThrowAsync<Exception>();
        }
    }
}