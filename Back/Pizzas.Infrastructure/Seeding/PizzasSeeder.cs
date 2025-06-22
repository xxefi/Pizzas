using Microsoft.EntityFrameworkCore;
using Pizzas.Core.Entities.Main;
using Pizzas.Infrastructure.Context;
using static NanoidDotNet.Nanoid;
using static BCrypt.Net.BCrypt;

namespace Pizzas.Infrastructure.Seeding;

public class PizzasSeeder(PizzasContext context)
{
    public async Task SeedAsync()
    {
        await context.Database.MigrateAsync();
        
        if (!await context.Roles.AnyAsync())
        {
            var roles = new[]
            {
                new RoleEntity { Id = Generate(size: 24), Name = "Admin" },
                new RoleEntity { Id = Generate(size: 24), Name = "SuperAdmin" },
                new RoleEntity { Id = Generate(size: 24), Name = "Customer" }
            };
            await context.Roles.AddRangeAsync(roles);
            await context.SaveChangesAsync();
        }

        var superAdminRole = await context.Roles
            .Include(r => r.Permissions) 
            .FirstAsync(r => r.Name == "SuperAdmin");
        
        var permissionNames = new[]
        {
            "role_view",
            "role_permission_view",
            "role_check",
            "role_user_list",
            "role_permission_check",
            "role_permission_add",
            "role_create",
            "role_update",
            "role_delete",
            "permission_create",
            "permission_update",
            "permission_delete",
            "permission_view",
            "category_create",
            "category_update",
            "category_delete",
        };
        
        var existingPermissions = await context.Permissions
            .Where(p => permissionNames.Contains(p.Name))
            .ToListAsync();

        var missingPermissions = permissionNames
            .Except(existingPermissions.Select(p => p.Name))
            .ToList();

        var newPermissions = missingPermissions
            .Select(name => new PermissionEntity
            {
                Id = Generate(size: 24),
                Name = name
            })
            .ToList();

        if (newPermissions.Any())
        {
            await context.Permissions.AddRangeAsync(newPermissions);
            await context.SaveChangesAsync();
            existingPermissions.AddRange(newPermissions);
        }
        
        foreach (var permission in existingPermissions)
            if (!superAdminRole.Permissions.Any(p => p.Id == permission.Id))
                superAdminRole.Permissions.Add(permission);

        await context.SaveChangesAsync();
        
        bool superAdminExists = await context.Users.AnyAsync(u => u.RoleId == superAdminRole.Id);
        if (!superAdminExists)
        {
            var superAdmin = new UserEntity
            {
                Id = Generate(size: 24),
                Username = "expert",
                FirstName = "Super",
                LastName = "Admin",
                Email = "magsudluefgan@example.com",
                Password = HashPassword("ChangeMe123!"),
                RoleId = superAdminRole.Id,
                Verified = true
            };
            await context.Users.AddAsync(superAdmin);
            await context.SaveChangesAsync();
        }
    }
}