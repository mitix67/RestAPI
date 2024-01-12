using Microsoft.AspNetCore.Authorization;
using RestAPI.Migrations;
using System.Security.Cryptography.X509Certificates;

namespace RestAPI.Authorization
{
    public class AtLeast2RestaurantsRequierement : IAuthorizationRequirement
    {
        public int MinimumRestaurantsCreated { get; }
        public AtLeast2RestaurantsRequierement(int minimumRestaurantsCreated)
        {
            MinimumRestaurantsCreated = minimumRestaurantsCreated;
        }
    }
}
