using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace EventFlowAPI.Extensions
{
    public static class BuilderExtenstionsIdentity
    {
        public static void AddIdentity(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<User, Role>(options =>
            {
                options.Password = GetIdentityPasswordOptions();
            })
            .AddEntityFrameworkStores<APIContext>();
        }
        public static void AddAuthentication(this WebApplicationBuilder builder,
            string jwtSettingsSection, string googleAuthSection, string facebookAuthSection)
        {
            var jwtSettings = builder.Configuration.GetSection(jwtSettingsSection);
            var googleSettings = builder.Configuration.GetSection(googleAuthSection);
            var facebookSettings = builder.Configuration.GetSection(facebookAuthSection);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = GetJWTTokenOptions(jwtSettings);
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                       /* foreach (var header in context.Request.Headers)
                        {
                            Console.WriteLine($"{header.Key}: {header.Value}");
                        }*/

                        var token = context.Request.Cookies["EventFlowJWTCookie"];
                       // Log.Information($"Token {token}");
                        if (!string.IsNullOrEmpty(token))
                        {
                            context.Token = token;
                        }
                        return Task.CompletedTask;
                    }
                };
            })/*.AddCookie("CookieAuthentication", options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.Name = "jwt";
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            })*/.AddGoogle(options =>
            {
                options.GetGoogleOptions(googleSettings);
            }) 
            .AddFacebook(options =>
             {
                 options.GetFacebookOptions(facebookSettings);
             });
        }
        
        private static TokenValidationParameters GetJWTTokenOptions(IConfigurationSection jwtSettingsSection)
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettingsSection["validIssuer"],
                ValidAudience = jwtSettingsSection["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(jwtSettingsSection.GetSection("securityKey").Value!)),
                RoleClaimType = "userRoles"
            };
        }
        private static PasswordOptions GetIdentityPasswordOptions()
        {
            return new PasswordOptions
            {
                RequireNonAlphanumeric = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
                RequiredUniqueChars = 0,
                RequiredLength = 5
            };
        }
        private static FacebookOptions GetFacebookOptions(this FacebookOptions options, IConfigurationSection facebookSection)
        {
            options.ClientId = facebookSection["AppId"]!;
            options.ClientSecret = facebookSection["AppSecret"]!;
            return options;
        }
        private static GoogleOptions GetGoogleOptions(this GoogleOptions options, IConfigurationSection googleSection)
        {
            options.ClientId = googleSection["clientId"]!;
            options.ClientSecret = googleSection["clientSecret"]!;
            options.SaveTokens = true;
            //options.CallbackPath = "/api/auth/google-login";
            return options;
        }
    }
}
