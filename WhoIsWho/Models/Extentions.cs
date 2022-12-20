using System.Security.Claims;

namespace WhoIsWho.Models
{
    public static class UsersExtentions
    {
        public static int Id(this ClaimsPrincipal user) => int.Parse(user.Claims.FirstOrDefault(c => c.Type == "Id")?.Value ?? "0");
    }
}
