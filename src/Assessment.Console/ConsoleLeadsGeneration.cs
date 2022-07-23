using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Domain;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Utility;
using Newtonsoft.Json;

namespace Assessment.Console
{
    [ExcludeFromCodeCoverage]
    public class ConsoleLeadsGeneration
    {
        public async Task Process()
        {
            try
            {
                System.Console.WriteLine("\n \n ++++++ PIPE ++++++");
                await ProcessRequest(new PipeLeads(new PipeLeadsHandler(),new LeadsConverter()));
                System.Console.WriteLine("\n \n ++++++ COMMA ++++++");
                await ProcessRequest(new CommaLeads(new CommaLeadsHandler(), new LeadsConverter()));
                System.Console.WriteLine("\n \n ++++++ SPACE ++++++");
                await ProcessRequest(new SpaceLeads(new SpaceLeadsHandler(), new LeadsConverter()));
            }
            catch (Exception exx)
            {
                System.Console.WriteLine(exx.Message);
            }
        }
        
        private async Task ProcessRequest(IParseLeads leads)
        {
            var getQualityLeads = await new QualityLeadsManager(leads).GetQualityLeads();
            SortedLeads(getQualityLeads.ToList());
        }

        private void SortedLeads(IReadOnlyCollection<Lead> qualityLeads)
        {
            System.Console.WriteLine("**** By property and Project");
            Print(qualityLeads.SortedByPropertyAndProjectParams());
            System.Console.WriteLine("**** By StartDate");
            Print(qualityLeads.SortedByStartDateAsc());
            System.Console.WriteLine("**** By LastName Desc");
            Print(qualityLeads.SortedByLastNameDesc());
        }

        private void Print(IEnumerable<Lead> leads)
        {
            foreach (var item in leads)
            {
                System.Console.WriteLine(JsonConvert.SerializeObject(item));
            } 
        }
    }
    
    
}