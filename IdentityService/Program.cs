using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityService.Configurations;
using IdentityService.Data;
using IdentityService.Interfaces;
using IdentityService.Middleware;
using IdentityService.Repositories;
using IdentityService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using IdentityService.Common;

namespace IdentityService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),

                    // Retry Mechanism For Temporary Failures
                      sqloptions =>
                      {
                          sqloptions.EnableRetryOnFailure(
                              maxRetryCount: 5,
                              maxRetryDelay : TimeSpan.FromSeconds(30),
                              errorNumbersToAdd : null
                              );
                      }
                    );
            });

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            //For Creating Refresh Token CRUD
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            //For Generating Refresh Token using RandomGenerator and Cryptography
            builder.Services.AddScoped<IRefreshTokenService,RefreshTokenService>();

            //Password-Hashing Service
            builder.Services.AddSingleton<IPasswordService,PasswordService>();

            //JWT Configuration
            builder.Services.Configure<JwtSettings>(
                builder.Configuration.GetSection("Jwt")
                );

            //JWT Service For Generating Token
            builder.Services.AddScoped<IJwtService, JwtService>();

           
            //Get All Jwt Section 
            var jwtsettings = builder.Configuration
                .GetSection("Jwt")
                .Get<JwtSettings>();

            //Authentication Configuration

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtsettings!.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwtsettings.Audience,

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtsettings.Key)
                            ),

                        ClockSkew = TimeSpan.Zero //Remove Delay of Token when expire
                    };

            });

            builder.Services.AddControllers();

            //Fluent Validation
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    //Collect all The Error From Fluent Validation Model.IsInvalid
                    var errors = context.ModelState
                    .Values
                    .SelectMany(x => x.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                    //Generate Generic Response
                    var response = ApiResponse<object>.FailureResponse(
                        "Validation Failed",
                        errors
                        );

                    return new BadRequestObjectResult(response);
                };
            });

            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //Swagger Configuration For JWT Authentication
            builder.Services.AddSwaggerGen(c =>
            {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Identity Service API"




            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter JWT Token"
            });


            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });


            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
          
            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
