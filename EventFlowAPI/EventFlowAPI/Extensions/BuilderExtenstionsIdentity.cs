using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
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
        public static void AddJWTTokenAuth(this WebApplicationBuilder builder, string jwtSettingsSection)
        {
            var jwtSettings = builder.Configuration.GetSection(jwtSettingsSection);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = GetJWTTokenOptions(jwtSettings);
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
                    .GetBytes(jwtSettingsSection.GetSection("securityKey").Value!))
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
    }
}
