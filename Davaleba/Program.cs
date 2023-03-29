using Davaleba.Helpers;
using Davaleba.Interface;
using Davaleba.Models;
using Davaleba.Repository;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DavalebaContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("sqlConnection")));
builder.Services.AddScoped<IUsers, UserRepository>();
builder.Services.AddScoped<IJWTTokenServices, JWTServiceManage>();
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWKToekn:key"],
            ValidAudience = builder.Configuration["JWKToekn:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWTToken:key"]))
        };
    });

var app = builder.Build();

app.UseCors(options => options.WithOrigins("*")
.AllowAnyMethod()
.AllowAnyHeader()
.AllowAnyOrigin()
);



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<MiddlewareErrorHandler>();

app.MapControllers();

app.Run();
