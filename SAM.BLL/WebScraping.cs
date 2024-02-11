using HtmlAgilityPack;
using SAM.Domain;

namespace SAM.Application
{
    public class WebScraping : IWebScraping
    {
        private const string PlayStoreTopUrl = "https://www.appbrain.com/stats/google-play-rankings";
        private const string PlayStoreTopUrlPaid = "https://www.appbrain.com/stats/google-play-rankings/top_paid/all/us";
        private const string NameXPath = "//div[8]/table/tbody/tr[{0}]/td[4]";
        private const string DescriptionXPath = "//div/section/div/div";
        private const string RatingXPath = "//div[8]/table/tbody/tr[{0}]/td[6]";
        private const string CategoryXPath = "//div[8]/table/tbody/tr[{0}]/td[5]";
        private const string ImagePathXPath = "/html[1]/body[1]/div[2]/div[3]/div[1]/div[1]/div[5]/div[2]/div[1]/div[2]/div[1]/div[8]/table[1]/tbody[1]/tr[{0}]/td[3]/a[1]/img[1][1]";
        private const string DownloadsStringXPath = "//div[8]/table/tbody/tr[{0}]/td[7]";

        public ApplicationModel CallUrl(string fullUrl)
        {
            return new ApplicationModel();
        }

        public async Task<IEnumerable<ApplicationModel>> CallTOP(int number)
        {
            return await GetTopApps(PlayStoreTopUrl, number);
        }

        public async Task<IEnumerable<ApplicationModel>> CallTopPaid(int number)
        {
            return await GetTopApps(PlayStoreTopUrlPaid, number);
        }

        private async Task<IEnumerable<ApplicationModel>> GetTopApps(string url, int number)
        {
            HtmlWeb web = new HtmlWeb();
            var htmlContent = await web.LoadFromWebAsync(url);
            var list = new List<ApplicationModel>();

            for (int i = 2; i < number + 2; i++)
            {
                var name = GetInnerText(htmlContent, string.Format(NameXPath, i));
                if (!string.IsNullOrEmpty(name))
                {
                    list.Add(new ApplicationModel
                    {
                        Name = name,
                        Description = GetInnerText(htmlContent, DescriptionXPath),
                        Rating = GetInnerText(htmlContent, string.Format(RatingXPath, i)),
                        Category = GetInnerText(htmlContent, string.Format(CategoryXPath, i)),
                        ImagePath = GetInnerImmage(htmlContent, string.Format(ImagePathXPath, i)),
                        DownloadsString = GetInnerText(htmlContent, string.Format(DownloadsStringXPath, i))
                    });
                }
            }

            return list;
        }

        private static string GetInnerText(HtmlDocument document, string xpath)
        {
            var node = document.DocumentNode.SelectSingleNode(xpath);
            return node?.InnerText.Trim() ?? string.Empty;
        }

        private static string GetInnerImmage(HtmlDocument document, string xpath)
        {
            var metaTag = document.DocumentNode.SelectSingleNode(xpath);
            return metaTag?.GetAttributeValue("src", "").Replace("&#x3D;", "=") ?? string.Empty;
        }
    }
}
