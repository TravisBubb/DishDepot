﻿using BSS.DishDepot.Domain.Entities;
using BSS.DishDepot.Domain.Foundation;
using MediatR;

namespace BSS.DishDepot.Application.Cqrs.Recipes
{
    public sealed record GetRecipesQuery : IRequest<Result<List<Recipe>>>;
}
