using System.ComponentModel.DataAnnotations;

namespace RestaurantRecommender.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? FavoriteCuisine { get; set; }

        public double? MinRating { get; set; }
        public double? MaxCost { get; set; }
    }
}
