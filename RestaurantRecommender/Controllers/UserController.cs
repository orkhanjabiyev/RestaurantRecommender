using Microsoft.AspNetCore.Mvc;
using RestaurantRecommender.Data;
using RestaurantRecommender.Models;

namespace RestaurantRecommender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // Получение всех пользователей
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        // Добавление нового пользователя
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if (user == null)
                return BadRequest("Invalid user data.");

            _context.Users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }
    }
}
