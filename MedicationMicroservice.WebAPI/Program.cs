using MedicationMicroservice.Application;
using MedicationMicroservice.BusinessLogic.Extensions;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using WebAPI.Middlewares;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
        
        builder.Services.AddStackExchangeRedisCache(options =>
            options.Configuration = builder.Configuration.GetConnectionString("RedisCache"));
        
        builder.Services.AddApplicationServices(); 
        builder.Services.AddControllers();
        builder.Services.AddRequestValidations();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        var app = builder.Build();
        app.UseMiddleware<ExceptionMiddleware>();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(); 
            app.UseSwaggerUI();
            app.ApplyMigrations();
        }
        
        app.UseHttpsRedirection();
        app.UseRouting();
        
        app.UseHttpMetrics();
        app.MapMetrics();
        
        app.MapControllers();
        app.Run();
    }
}