using Application.Contracts;

namespace Infrastructure.Services
{
    public class SpaceLeadsHandler : IFileHandler
    {
        public string FetchLeads(string filepath)
        {
            return @"Mercy Grant House Industrial 2022-09-29 +14143550132
                     Cooper Mike Condo Epoxy-Flooring 4/12/2022 +14775550444
                     Silly Lim Trailer Commercial 2022-02-17 +14895550777";
        }

        public char DelimiterCharacter { get; } = ' ';
        
        public bool AddToLeads(string lead)
        {
            throw new System.NotImplementedException();
        }
    }
}