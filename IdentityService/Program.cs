using IdentityService.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var dataSourceBuilder = new NpgsqlDataSourceBuilder("Host=host.docker.internal;Database=identity;Username=usr;Password=pwd")
    .EnableDynamicJson();
var dataSource = dataSourceBuilder.Build();
builder.Services.AddDbContext<UsersContext>(options => options.UseNpgsql(dataSource));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(identity =>
        {
            identity.Password.RequiredLength = 1;
            identity.Password.RequireNonAlphanumeric = false;
            identity.Password.RequireUppercase = false;
            identity.Password.RequireLowercase = false;
            identity.Password.RequireDigit = false;
        })
    .AddEntityFrameworkStores<UsersContext>();
builder.Services.AddAuthorization();


//builder.Services.AddIdentityApiEndpoints<IdentityUser>()
//    .AddEntityFrameworkStores<UsersContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapIdentityApi<IdentityUser>();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
