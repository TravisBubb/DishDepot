﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BSS.DishDepot.Domain.Entities;

[Table("Users", Schema = "DishDepot")]
public class User : ETagCreatedDateEntity
{
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
}