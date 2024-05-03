using Common.MassTransit;
using Common.Web;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using Serilog;
using Serilog.Events;
using System.Text;
using TA.Data;
using TA.Data.Repositories;
using TA.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var dataSourceBuilder = new NpgsqlDataSourceBuilder("Host=host.docker.internal;Database=ta;Username=usr;Password=pwd")
    .EnableDynamicJson();
var dataSource = dataSourceBuilder.Build();
builder.Services.AddDbContext<AssignedTestsContext>(options => options.UseNpgsql(dataSource));
builder.Services.AddScoped<ITestAssignerService, TestAssignerService>();
builder.Services.AddScoped<IStudentAnswersRepository, StudentAnswersRepository>();
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

builder.Services.AddMassTransit(x =>
{
    x.AddRequestClient<GetGroupInfoByIdRequestMessage>();
    x.AddRequestClient<GetGroupsForUserRequestMessage>();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", 5672, "/", host =>
        {
            host.Username("admin");
            host.Password("admin");
        });

        cfg.ConfigureEndpoints(ctx);
    });
});

builder.Host.UseSerilog((host, log) =>
{
    if (host.HostingEnvironment.IsProduction())
        log.MinimumLevel.Information();
    else
        log.MinimumLevel.Verbose();

    log.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
    log.MinimumLevel.Override("Quartz", LogEventLevel.Information);
    log.WriteTo.Console();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

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
