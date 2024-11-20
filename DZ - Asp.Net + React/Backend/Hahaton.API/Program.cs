using Hahaton.Application;
using Hahaton.Application.Auth;
using Hahaton.Application.ServiceInterfaces;
using Hahaton.Application.Services;
using Hahaton.Core.Interfaces;
using Hahaton.Data;
using Hahaton.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        conBuilder =>
        {
            conBuilder.WithOrigins("http://localhost:5173") 
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials(); 
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuerSigningKey = true,
         IssuerSigningKey = new SymmetricSecurityKey(
             "augduiagsugduigsajdhoiwuigaugsgduagsdowguoqidsdsf13313"u8.ToArray()),
         ValidateIssuer = false, 
         ValidateAudience = false, 
         ValidateLifetime = false,
     };

     options.Events = new JwtBearerEvents()
     {
         OnMessageReceived = context =>
         {
             context.Token = context.Request.Cookies["tasty"];

             return Task.CompletedTask;
         }
     };
 });

builder.Services.AddScoped<IMissionRepository, MissionRepository>();
builder.Services.AddScoped<IMissionService, MissionService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Urls.Add("http://localhost:5047");
app.Urls.Add("http://26.249.179.3:5047");

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
