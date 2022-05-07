using Application.Contracts;

namespace Infrastructure.Services
{
    public class CommaLeadsHandler : IFileHandler
    {
        public string FetchLeads(string filepath)
        {
            return @"Alex,Chase,House,Industrial,2022-7-21,+14150450132
                     Peace,Ken,Condo,Construction,2022-2-18,+14159850444
                     Susan,Brain,Trailer,Masonry,2022-09-10,+1415588877";
        }

        public char DelimiterCharacter { get; } = ',';
        
        public bool AddToLeads(string lead)
        {
            throw new System.NotImplementedException();
        }
    }
}