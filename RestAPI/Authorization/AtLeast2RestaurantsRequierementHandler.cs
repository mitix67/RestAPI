using Microsoft.AspNetCore.Authorization;
using RestAPI.Entities;
using System.Security.Claims;

namespace RestAPI.Authorization
{
    public class AtLeast2RestaurantsRequierementHandler : AuthorizationHandler<AtLeast2RestaurantsRequierement>
    {
        private readonly ILogger<AtLeast2RestaurantsRequierementHandler> _logger;
        private readonly RestaurandDbContext _context;
        public AtLeast2RestaurantsRequierementHandler(ILogger<AtLeast2RestaurantsRequierementHandler> logger, RestaurandDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AtLeast2RestaurantsRequierement requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var createdUserRestaurants = _context
                .Restaurants
                .Count(r => r.CreatedById == userId);

            _logger.LogInformation($"User has {createdUserRestaurants} restaurants");

            if(createdUserRestaurants >= requirement.MinimumRestaurantsCreated)
            {
                _logger.LogInformation($"Authorization succeded");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation($"Authorization failed");
            }
            return Task.CompletedTask;
        }
    }
}
