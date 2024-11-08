using Microsoft.EntityFrameworkCore;
using Biblioteka.DataBaseContext;
using Biblioteka.Interfaces;
using Biblioteka.Services;
using ProxyKit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TestApiDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestDbString")), ServiceLifetime.Scoped);

builder.Services.AddScoped<IBookInterface, BookService>();
builder.Services.AddScoped<IGenreInterface, GenreService>();
builder.Services.AddScoped<IReaderInterface, ReaderService>();
builder.Services.AddScoped<IRentalInterface, RentalService>();
builder.Services.AddScoped<IPhotoInterface, PhotoService>();

builder.Services.AddProxy();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseWhen(context => context.Request.Path.Value.Contains("/api/Books"),
    applicationBuilder => applicationBuilder.RunProxy(context =>
        context.ForwardTo("https://localhost:7159/").AddXForwardedHeaders().Send()));
app.UseWhen(context => context.Request.Path.Value.Contains("/api/Reader"),
applicationBuilder => applicationBuilder.RunProxy(context =>
context.ForwardTo("https://localhost:7230/").AddXForwardedHeaders().Send()));
app.UseWhen(context => context.Request.Path.Value.Contains("/api/Photo"),
applicationBuilder => applicationBuilder.RunProxy(context =>
context.ForwardTo("https://localhost:7196/").AddXForwardedHeaders().Send()));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
