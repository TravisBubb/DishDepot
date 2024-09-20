using BSS.DishDepot.Domain.Entities;
using BSS.DishDepot.Domain.Foundation;
using BSS.DishDepot.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BSS.DishDepot.Presentation.Middleware;

public class IdentityContextMiddleware 
{
    private readonly RequestDelegate _next;

    public IdentityContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUnitOfWork uow, IIdentityContextAccessor accessor)
    {
        if (context.User?.Claims?.Any(c => c.Type == "UserId") ?? false)
        {
            var claim = context.User.Claims.First(c => c.Type == "UserId");
            if (Guid.TryParse(claim.Value, out var userId))
            {
                var user = await uow.Query<User>().FirstOrDefaultAsync(u => u.Id == userId);
                if (user is not null)
                {
                    accessor.IdentityContext = new IdentityContext
                    {
                        UserId = userId,
                        UserEmail = user.Email
                    };
                }
            }
        }

        await _next(context);
    }
}
