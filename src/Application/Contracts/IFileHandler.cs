namespace Application.Contracts
{
    public interface IFileHandler
    {
        string FetchLeads(string filepath);
        
        char DelimiterCharacter { get;  }
        bool AddToLeads(string lead);
    }
}