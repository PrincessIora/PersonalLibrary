using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using PersonalLibrary.Data;
using PersonalLibrary.Repositories;
using PersonalLibrary.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

builder.Services.AddDbContext<LibraryContext>(
options =>
options.UseSqlite(
"Data Source=library.db"));

builder.Services.AddScoped<
IBookRepository,
BookRepository>();

builder.Services.AddScoped<
BookService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.MapControllers();

app.Run();