using System;
using Application;
using Application.Contracts;
using Application.Interfaces;
using Assessment.API.Common;
using Assessment.API.Filters;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Utility;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Services.BuildServiceProvider()
    .GetRequiredService<IConfiguration>();

// Add services to the container.
builder.Services.AddControllers(c => c.Filters.Add<ExceptionFilters>());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var assembly = AppDomain.CurrentDomain.Load("Application");
builder.Services.AddMediatR(assembly);

builder.Services.AddTransient<IConfigurationManager, CustomConfigurationManager>();
builder.Services.AddTransient<IParseLeads, PipeLeads>();
builder.Services.AddSingleton<IFileHandler, PipeLeadsHandler>();
builder.Services.AddTransient<ILeadConverter, LeadsConverter>();
builder.Services.AddScoped(typeof(IAPICalls<>), typeof(CustomAPICalls<>));

builder.Services.AddHttpClient("LeadBase", httpClient =>
{
    httpClient.BaseAddress = new Uri(configuration["LeadsAPIURL"]);
                
    httpClient.DefaultRequestHeaders.Add(
        "Accept", "application/json");
    httpClient.DefaultRequestHeaders.Add(
        "UserAgent", "QualityLead");
    httpClient.DefaultRequestHeaders.Add(
        "X-Api-Key", configuration["LeadsAPIkey"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();