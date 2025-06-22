using Microsoft.EntityFrameworkCore;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Auth;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Infrastructure.Context;

public class PizzasContext : DbContext
{
    public PizzasContext(DbContextOptions<PizzasContext> options) : base(options) { }
    
    public DbSet<PizzaEntity> Pizzas { get; set; } = null!;
    public DbSet<AddressEntity> Addresses { get; set; } = null!;
    public DbSet<CategoryEntity> Categories { get; set; } = null!;
    public DbSet<FavoriteEntity> Favorites { get; set; } = null!;
    //public DbSet<PizzaIngredientEntity> PizzasIngredients { get; set; } = null!;
    public DbSet<IngredientEntity> Ingredients { get; set; } = null!;
    public DbSet<PizzaPriceEntity> PizzaPrices { get; set; } = null!;
    public DbSet<ReviewEntity> Reviews { get; set; } = null!;
    public DbSet<OrderEntity> Orders { get; set; } = null!;
    public DbSet<OrderItemEntity> OrderItems { get; set; } = null!;
    public DbSet<DeliveryInfoEntity> DeliveryInfos { get; set; } = null!;
    public DbSet<BasketEntity> Baskets { get; set; } = null!;
    public DbSet<BasketItemEntity> BasketItems { get; set; } = null!;
    public DbSet<PermissionEntity> Permissions { get; set; } = null!;
    public DbSet<RoleEntity> Roles { get; set; } = null!;
    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<BlackListedEntity> BlackListeds { get; set; } = null!;
    public DbSet<UserActiveSessionsEntity> UserActiveSessions { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) 
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(PizzasContext).Assembly);
}