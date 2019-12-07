using System;
using System.Collections.Generic;

namespace ETL.API.Models
{
    public class Review
    {
        public DateTime ReviewDate { get; set; }
        public string ReviewerName { get; set; }
        public string ProductRating { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewText { get; set; }
        public string ReviewRating { get; set; }
        public List<ReviewComment> ReviewComments { get; set; }
    }
}
