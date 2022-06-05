using AutoMapper;
using Demo.Db.DataContext;
using DEMOWebApi.MapperProfile;
using Microsoft.EntityFrameworkCore;
using Lamar.Microsoft.DependencyInjection;
using DEMOWebApi.Lamar;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLamar();

//builder.Host.UseLamar((context, registry) =>
//{
//    registry.AddControllers();
//});


//var config = new MapperConfiguration(cfg =>
//{
//    cfg.AddProfile(new AppMapProfile());
//});

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLamar(new ApplicationRegistry());
builder.Services.AddDbContext<AppDbContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddAutoMapper(typeof(AppMapProfile));


var app = builder.Build();

app.UseCors(options=>
    options
        //.AllowAnyOrigin()
        .WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader());

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
