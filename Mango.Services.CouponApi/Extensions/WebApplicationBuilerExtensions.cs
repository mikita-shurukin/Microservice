﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mango.Services.CouponApi.Extensions
{
    public static class WebApplicationBuilerExtensions
    {
        public static WebApplicationBuilder AddAppAuthentification(this WebApplicationBuilder builder)
        {

            var settingsSection = builder.Configuration.GetSection("ApiSettings");

            var secret = builder.Configuration.GetValue<string>("ApiSettings:Secret");
            var issuer = builder.Configuration.GetValue<string>("ApiSettings:Issuer");
            var audience = builder.Configuration.GetValue<string>("ApiSettings:Audience");

            var key = Encoding.ASCII.GetBytes(secret);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateAudience = true,

                };
            });

            return builder;
        }
    }
}
