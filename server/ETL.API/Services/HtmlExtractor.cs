﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ETL.API.Services
{
    public interface IHtmlExtractor
    {
        Task Extract(string url);
    }

    public class HtmlExtractor : IHtmlExtractor
    {
        public async Task Extract(string url)
        {
            var htmlDocument = new HtmlWeb();
            var document = htmlDocument.Load(url);

            await SavePageContent(document);

            await GetNextPage(document);
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
            var nextPageUrl = document.DocumentNode.SelectNodes("//link").FirstOrDefault(x => x.OuterHtml.Contains("next"))
                ?.Attributes[1]
                .Value;

            if (nextPageUrl != null)
                await Extract(nextPageUrl);
        }
    }
}