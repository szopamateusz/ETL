using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ETL.API.Data;
using ETL.API.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ETL.API.Services
{
    public interface IHtmlLoader
    {
        Task<string> Load();
    }

    public class HtmlLoader : IHtmlLoader
    {
        private readonly ReviewsDbContext _reviewsDbContext;

        public HtmlLoader(ReviewsDbContext reviewsDbContext)
        {
            _reviewsDbContext = reviewsDbContext;
        }

        public async Task<string> Load()
        {
            try
            {
                var fileContent = await File.ReadAllTextAsync(Environment.CurrentDirectory + @"\transformed.txt");

                List<Review> reviews = JsonConvert.DeserializeObject<List<Review>>(fileContent);

                foreach (var review in reviews)
                {
                    var exists = await _reviewsDbContext.Reviews.AnyAsync(x => x.ProductRating.Equals(review.ProductRating) && x.ReviewDate.Equals(review.ReviewDate)
                                                                                                                            && x.ReviewText.Equals(review.ReviewText) && x.ReviewTitle.Equals(review.ReviewTitle) && x.ReviewerName.Equals(review.ReviewerName));

                    if (exists)
                        continue;

                    await _reviewsDbContext.Reviews.AddAsync(review);
                    await _reviewsDbContext.SaveChangesAsync();
                }

                //File.Delete(Environment.CurrentDirectory + @"\transformed.txt");
                //File.Delete(Environment.CurrentDirectory + @"\webpage.txt");
                return $"Successfully loaded {reviews.Count} reviews";

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "Error occured during loading the reviews.";
            }
        }
    }
}
