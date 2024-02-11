namespace SAM.Domain;

public interface IWebScraping
{
    Task<IEnumerable<ApplicationModel>> CallTOP(int number);
    Task<IEnumerable<ApplicationModel>> CallTopPaid(int number);
    ApplicationModel CallUrl(string fullUrl);
}