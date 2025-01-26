namespace RestaurantRecommender.Models
{
    public class Restaurant
    {
        public int Id { get; set; } 

        public string Name { get; set; } = string.Empty; 

        public string Cuisine { get; set; } = string.Empty;

        public double AverageCost { get; set; } 

        public double Rating { get; set; } 

        public string Location { get; set; } = string.Empty; 
    }
}
