using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ETL.API.Services
{
    public interface IHtmlExtractor
    {
        Task<string> Extract(string url);
    }

    public class HtmlExtractor : IHtmlExtractor
    {
        public async Task<string> Extract(string url)
        {
            var htmlDocument = new HtmlWeb();
            var document = htmlDocument.Load(url);
            try
            {
                await SavePageContent(document);
                await GetNextPage(document);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "Unable to extract records from a page";
            }

            return "Successfully extracted records from page.";
        }

        private async Task SavePageContent(HtmlDocument document)
        {
            var filePath = Environment.CurrentDirectory + @"\webpage.txt";

            if (File.Exists(filePath))
                await File.AppendAllTextAsync(filePath, document.Text);
            else
                await File.WriteAllTextAsync(filePath, document.Text);
        }

        private async Task GetNextPage(HtmlDocument document)
        {
            try
            {
                var nextPage = document?.DocumentNode?.SelectNodes("//link")
                    .FirstOrDefault(x => x.OuterHtml.Contains("next"));
                string nextPageUrl = null;

                if (nextPage != null)
                    nextPageUrl = nextPage?.Attributes[1].Value;

                if (nextPageUrl != null)
                    await Extract(nextPageUrl);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("error occured");
            }
        }
    }
}