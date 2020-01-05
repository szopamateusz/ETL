namespace ETL.API.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string ReviewDate { get; set; }
        public string ReviewerName { get; set; }
        public string ProductRating { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewText { get; set; }
        public string ProductName { get; set; }
    }
}