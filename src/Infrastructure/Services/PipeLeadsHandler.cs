using Application.Contracts;

namespace Infrastructure.Services
{
    public class PipeLeadsHandler : IFileHandler
    {
        private static string pipeLeads;
        public PipeLeadsHandler()
        {
            pipeLeads = @"James|Bob|House|Industrial|2022-03-11|+14155550132
                     Allen|Bey|Condo|Construction|2022-1-8|+14155550444
                     Brad|Shawn|Trailer|MarlitePanels-(FED)|2022-9-10|+14155550777";
        }
   
        public string FetchLeads(string filepath)
        {
            return pipeLeads;
        }

        public bool AddToLeads(string lead)
        {
            if (!lead.Contains(DelimiterCharacter))
                return false;
            
            if (lead.Split(DelimiterCharacter).Length != 6)
                return false;
            
            pipeLeads += $"\n{lead}";
            return true;
        }
        public char DelimiterCharacter { get; } = '|';
    }
}