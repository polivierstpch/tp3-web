// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer
{
    public class SeedData
    {
        public static async Task EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseNpgsql(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            using var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            await context.Database.MigrateAsync();

            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await SeedRoles(roleMgr);
                    
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            await SeedUsers(userMgr);
        }
        
        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("admin");
            if (adminRole is null)
            {
                adminRole = new IdentityRole("admin");
                var result = await roleManager.CreateAsync(adminRole);
                
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }

            var managerRole = await roleManager.FindByNameAsync("manager");
            if (managerRole is null)
            {
                managerRole = new IdentityRole("manager");
                var result = await roleManager.CreateAsync(managerRole);
                
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }

        private static async Task SeedUsers(UserManager<ApplicationUser> userManager)
        {
            var adminUser = await userManager.FindByEmailAsync("admin@cegeplimoilou.ca");
            if (adminUser is null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin@cegeplimoilou.ca",
                    Email = "admin@cegeplimoilou.ca",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Pass123$");
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = await userManager.AddToRoleAsync(adminUser, "admin");

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
            
            var managerUser = await userManager.FindByEmailAsync("manager@email.com");
            if (managerUser is null)
            {
                managerUser = new ApplicationUser
                {
                    UserName = "manager@email.com",
                    Email = "manager@email.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(managerUser, "Pass123$");
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = await userManager.AddToRoleAsync(managerUser, "manager");

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }
    }
}
