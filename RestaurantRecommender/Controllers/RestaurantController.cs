using Microsoft.AspNetCore.Mvc;
using RestaurantRecommender.Data;
using RestaurantRecommender.Models;

namespace RestaurantRecommender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RestaurantController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetRestaurants()
        {
            var restaurants = _context.Restaurants.ToList();
            return Ok(restaurants);
        }

        [HttpPost]
        public IActionResult AddRestaurant(Restaurant restaurant)
        {
            if (restaurant == null)
                return BadRequest("Invalid restaurant data.");

            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetRestaurants), new { id = restaurant.Id }, restaurant);
        }

        [HttpGet("recommendations")]
        public IActionResult GetRecommendations(
     string? cuisine = null,
     double? minRating = null,
     double? maxRating = null,
     double? minCost = null,
     double? maxCost = null,
     string? location = null)
        {
            // Получение всех ресторанов из базы
            var query = _context.Restaurants.AsQueryable();

            // Фильтрация по типу кухни
            if (!string.IsNullOrEmpty(cuisine))
            {
                query = query.Where(r => r.Cuisine.ToLower().Contains(cuisine.ToLower()));
            }

            // Фильтрация по минимальному рейтингу
            if (minRating.HasValue)
            {
                query = query.Where(r => r.Rating >= minRating);
            }

            // Фильтрация по максимальному рейтингу
            if (maxRating.HasValue)
            {
                query = query.Where(r => r.Rating <= maxRating);
            }

            // Фильтрация по минимальной стоимости
            if (minCost.HasValue)
            {
                query = query.Where(r => r.AverageCost >= minCost);
            }

            // Фильтрация по максимальной стоимости
            if (maxCost.HasValue)
            {
                query = query.Where(r => r.AverageCost <= maxCost);
            }

            // Фильтрация по местоположению
            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(r => r.Location.ToLower().Contains(location.ToLower()));
            }

            // Получение результата
            var recommendations = query.ToList();

            // Если ничего не найдено
            if (!recommendations.Any())
            {
                return NotFound("No restaurants match the criteria.");
            }

            return Ok(recommendations);
        }

            [HttpGet("recommendations/user/{userId}")]
            public IActionResult GetRecommendationsForUser(int userId)
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var query = _context.Restaurants.AsQueryable();

                // Учитываем предпочтения пользователя
                if (!string.IsNullOrEmpty(user.FavoriteCuisine))
                {
                    query = query.Where(r => r.Cuisine.ToLower().Contains(user.FavoriteCuisine.ToLower()));
                }

                if (user.MinRating.HasValue)
                {
                    query = query.Where(r => r.Rating >= user.MinRating);
                }

                if (user.MaxCost.HasValue)
                {
                    query = query.Where(r => r.AverageCost <= user.MaxCost);
                }

                var recommendations = query.ToList();

                if (!recommendations.Any())
                {
                    return NotFound("No recommendations found for this user.");
                }

                return Ok(recommendations);
            }

        }
    }

