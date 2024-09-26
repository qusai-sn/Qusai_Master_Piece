using MasterPiece.Models;
using MasterPiece.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Swagger/OpenAPI setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS policy allowing all origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Configure SQL Server connection for the DbContext
builder.Services.AddDbContext<MasterPieceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MasterPiece_Connection_string")));

// Service injection for EventService
builder.Services.AddScoped<IEventService, EventService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Serve default static files (for other assets like JS, CSS, etc.)
app.UseStaticFiles();

// Serve files from the "Media" folder for event banners and other media files
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Media")),
    RequestPath = "/Media"  // Maps the "Media" folder to "/media" in the URL
});

app.UseRouting();

// Apply CORS policy
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
