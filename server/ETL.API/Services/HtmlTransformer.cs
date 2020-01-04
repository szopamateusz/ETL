using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ETL.API.Models;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace ETL.API.Services
{
    public interface IHtmlTransformer
    {
        Task<string> Transform();
    }

    public class HtmlTransformer : IHtmlTransformer
    {
        public async Task<string> Transform()
        {
            try
            {
                var doc = new HtmlDocument();
                doc.Load(Environment.CurrentDirectory + @"\webpage.txt");
                var divs = doc.DocumentNode.SelectNodes("//div");

                var usedNodes = divs
                    .Where(x => x.Id.Contains("customer_review-"))
                    .ToList();

                var reviews = new List<Review>();
                foreach (var usedNode in usedNodes)
                {
                    var reviewDate = usedNode.ChildNodes
                        .FirstOrDefault(x => x.OuterHtml.Contains("data-hook=\"review-date\""))?.InnerText;
                    var reviewerName = usedNode.ChildNodes
                        .FirstOrDefault(x => x.OuterHtml.Contains("class=\"a-profile-name\""))?.InnerText;
                    var productRating = usedNode.ChildNodes
                        .FirstOrDefault(x => x.OuterHtml.Contains("class=\"a-icon-alt\""))?.FirstChild.InnerText;
                    var reviewTitle = usedNode.ChildNodes
                        .FirstOrDefault(x => x.OuterHtml.Contains("data-hook=\"review-title\""))?.ChildNodes[2]
                        .InnerText;
                    var reviewText = usedNode.ChildNodes
                        .FirstOrDefault(x => x.OuterHtml.Contains("data-hook=\"review-body\""))?.InnerText;

                    reviews.Add(new Review
                    {
                        ReviewDate = reviewDate,
                        ReviewerName = reviewerName,
                        ProductRating = productRating,
                        ReviewTitle = reviewTitle,
                        ReviewText = reviewText
                    });
                }

                var serializedData = JsonConvert.SerializeObject(reviews);

                await File.WriteAllTextAsync(Environment.CurrentDirectory + @"\transformed.txt", serializedData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "There was an issue during transforming the reviews";
            }

            return "Successfully transformed the reviews.";
        }
    }
}