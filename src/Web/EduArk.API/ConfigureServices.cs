using EduArk.API.Services;
using EduArk.Application.Common.Interfaces;
using EduArk.Infrastructure.Master.Data;
using EduArk.Infrastructure.Tenant.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddEduArkWebAPIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddHttpContextAccessor();

          
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddSingleton<IDateTimeService, DateTimeService>();

            services.AddHealthChecks()
               .AddDbContextCheck<MasterDbContext>();

            services.AddHealthChecks()
              .AddDbContextCheck<TenantDbContext>();

            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Edu ARK CDAP. - Web API",
                    Version = "v1",
                    Description = "",
                    TermsOfService = new Uri("https://example.com/terms")
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                    }
                });
            });

            var context = services.BuildServiceProvider()
                 .GetService<MasterDbContext>();

            var tenants = context.Tenants.Where(x=>x.IsActive == true).ToList();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.RequireHttpsMetadata = false;
                  options.SaveToken = true;
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = configuration["Tokens:Issuer"],
                      ValidAudiences = new List<string>
                      {
                          "webapp"
                      },

                      IssuerSigningKeyResolver = (string token, SecurityToken securityToken, string kid, TokenValidationParameters validationParameters) =>
                      {
                          List<SecurityKey> keys = new List<SecurityKey>();

                          var school = tenants.FirstOrDefault(t => t.APIKey.ToString().ToUpper().Trim() == kid.ToUpper().Trim());
                          if (school != null)
                          {
                              keys.Add(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(school.SecretKey.ToString())));
                          }


                          return keys;
                      },
                      ClockSkew = TimeSpan.Zero
                  };
              });



            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = long.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
                o.ValueCountLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddMvc(options =>
            {
                options.MaxModelBindingCollectionSize = int.MaxValue;
            });

            var allowedOrigins = new List<string>();

            var allowOrigins = configuration.GetValue<string>("AllowedOrigins")
                .Split(",");

            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicy",
                          builder => builder.WithOrigins(allowOrigins)
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
            });

            return services;
        }
    }
}
