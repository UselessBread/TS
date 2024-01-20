using Microsoft.EntityFrameworkCore;
using Npgsql;
using TS.Data;
using TS.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Have to use NpgsqlDataSourceBuilder in order to enable JSON mapping.
var dataSourceBuilder = new NpgsqlDataSourceBuilder("Host=host.docker.internal;Database=ts;Username=usr;Password=pwd")
    .EnableDynamicJson();
var dataSource = dataSourceBuilder.Build();
builder.Services.AddDbContext<TestsContext>(options => options.UseNpgsql(dataSource));
builder.Services.AddScoped<ITestsRepository, TestsRepository>();

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