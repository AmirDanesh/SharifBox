using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace JwtAuthorization
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddAuth(
           this IServiceCollection services,
           JwtSettings jwtSettings)
        {
            services
                .AddAuthorization()
                .AddAuthentication(options =>
                                    {
                                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                                    }
                ).AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.FromMinutes(3),
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                });

            return services;
        }

        public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();

            app.UseAuthorization();

            return app;
        }
    }
}