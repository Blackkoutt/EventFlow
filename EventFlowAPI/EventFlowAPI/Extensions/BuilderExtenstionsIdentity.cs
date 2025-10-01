﻿using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Enums;
using EventFlowAPI.Logic.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
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
            AuthConfiguration jwtSettingsSection, AuthConfiguration googleAuthSection, AuthConfiguration facebookAuthSection)
        {
            var jwtSettings = builder.Configuration.GetSection(jwtSettingsSection.ToString());
            var googleSettings = builder.Configuration.GetSection(googleAuthSection.ToString());
            var facebookSettings = builder.Configuration.GetSection(facebookAuthSection.ToString());

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
                        var token = context.Request.Cookies[Cookie.EventFlowJWTCookie.ToString()];
                        if (!string.IsNullOrEmpty(token))
                        {
                            context.Token = token;
                        }
                        return Task.CompletedTask;
                    }
                };
            }).AddGoogle(options =>
            {
                options.GetOAuthOptions(googleSettings);
                options.SaveTokens = true;
            }) 
            .AddFacebook(options =>
            {
                options.GetOAuthOptions(facebookSettings);
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

        private static OAuthOptions GetOAuthOptions(this OAuthOptions options, IConfigurationSection section)
        {
            options.ClientId = section["AppId"]!;
            options.ClientSecret = section["AppSecret"]!;
            return options;
        }
    }
}
