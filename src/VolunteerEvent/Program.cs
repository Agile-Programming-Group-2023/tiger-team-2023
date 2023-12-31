// <copyright file="Program.cs" company="Adairsville High School">
//  Copyright © {copyrightYearText} Adairsville High School. All rights reserved.
//  This product is protected by copyright and intellectual property laws in the United States and other countries as well as by international treaties.</copyright>

using Microsoft.AspNetCore.Mvc;
using VolunteerEvent.Services;
using VolunteerScheduler.Models.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
builder.Services.AddSingleton<IVolunteerEventStorageService, VolunteerEventStorageService>();

builder.Services.Configure<JsonOptions>(options =>
{
    //// options.JsonSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
    options.JsonSerializerOptions.Converters.Add(new CalendarConverter());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
