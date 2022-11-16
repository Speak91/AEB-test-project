using AebTestProject.Models.Request;
using AebTestProject.Models.Validator;
using AebTestProject.Services;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace AebTestProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = builder.Configuration;
            var connectionString = configuration.GetConnectionString("TaskDatabase");

            // Add services to the container.
            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddFluentValidationAutoValidation();
            services.AddScoped<IValidator<CreateTask>, CreateTaskValidator>();
            services.AddScoped<IValidator<UpdateTask>, UpdateTaskValidator>();
            builder.Services.AddControllers();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}