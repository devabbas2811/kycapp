
using Kyc.Validators;
using FluentValidation.AspNetCore;
using System.Reflection;
using FluentValidation;
using Kyc.Entities;
using Kyc.Services;

var builder = WebApplication.CreateBuilder(args);
var _policyName = "CorsPolicy";

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IValidator<Aadhar>, AadharValidator>();
builder.Services.AddCors(opt =>
        {
            opt.AddPolicy(name: _policyName, builder =>
            {
                builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
            });
        });

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<CheckSumService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policyName: _policyName );

app.UseAuthorization();

app.MapControllers();

app.Run();
