using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PersonalLibrary.Data;
using PersonalLibrary.Repositories;
using PersonalLibrary.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlite("Data Source=library.db"));

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<BookService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "📚 Royal Library API",
        Version = "v1",
        Description = "A graceful personal library system."
    });
});

var app = builder.Build();

app.UseStaticFiles();
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Royal Library API v1");

    c.InjectStylesheet("/swagger/custom.css");

    c.DocumentTitle = "📚 Royal Library";

    c.DefaultModelsExpandDepth(-1);
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();