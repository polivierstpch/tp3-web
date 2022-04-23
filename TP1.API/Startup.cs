using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json.Serialization;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TP1.API.Data;
using TP1.API.Data.Repository;
using TP1.API.Filters;
using TP1.API.Interfaces;
using TP1.API.Services;

namespace TP1.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<ICategorieRepository, CategorieRepository>();
            services.AddScoped<IParticipationRepository, ParticipationRepository>();
            services.AddScoped<IEvenementRepository, EvenementRepository>();
            services.AddScoped<IVilleRepository, VilleRepository>();

            services.AddScoped<IValidationParticipation, MockValidationParticipation>();
            services.AddScoped<IVillesService, VillesService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IEvenementsService, EvenementsService>();
            services.AddScoped<IParticipationsService, ParticipationsService>();
            
            services.AddControllers(options =>
            {
                options.AllowEmptyInputInBodyModelBinding = true;
                options.Filters.Add<HttpExceptionActionFilter>();
            }).ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
              .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            
            var identityUrl = Configuration.GetValue<string>("IdentityUrl");
            
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = identityUrl;
                    options.Audience = "Web2Api";
                    options.TokenValidationParameters.ValidTypes = new[] {"at+jwt"};
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true
                    };
                });

            const string apiScope = "web2ApiScope";
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "RequireApiScope",
                    pb => pb.RequireClaim(JwtClaimTypes.Scope, apiScope)
                );
                options.AddPolicy(
                    "RequireAdminRole",
                    pb => pb.RequireClaim(ClaimTypes.Role, IdentityRole.Admin)
                );
                options.AddPolicy(
                    "RequireManagerRole",
                    pb => pb.RequireClaim(ClaimTypes.Role, IdentityRole.Manager)
                );
                options.DefaultPolicy = options.GetPolicy("RequireApiScope")!;
            });
            
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{identityUrl}/connect/authorize"),
                            TokenUrl = new Uri($"{identityUrl}/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { apiScope, "Demo API - scope web2Api" }
                            }
                        }
                    }
                });
                
                c.OperationFilter<AuthorizeCheckOperationFilter>();
                
                c.SwaggerDoc(
                    "v1", 
                    new OpenApiInfo 
                    { 
                        Title = "TP1.API",
                        Version = "v1",
                        Description = "Prout TP1",
                        Contact = new OpenApiContact
                        {
                            Name = "Pier-Olivier St-Pierre-Chouinard & Caroline Marissal-Rousseau",
                            Email = "yolo.swaggins@wow.com",
                            Url = new Uri("https://github.com/CMarou/TP1.API")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "MIT"
                        }
                    }
                );

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(c =>
                {
                    c.WithOrigins("http://localhost:8080");
                    c.AllowAnyHeader();
                    c.AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {   
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TP1.API v1"));
            }

            app.UseCors();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
