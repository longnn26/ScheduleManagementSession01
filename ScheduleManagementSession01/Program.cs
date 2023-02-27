using Microsoft.Extensions.DependencyInjection;
using ScheduleManagementSession01.Extensions;
using Services.Mapping;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddAutoMapper();
builder.Services.ConfigIdentityService();

builder.Services.AddBussinessService();
builder.Services.ConfigureSwagger();
builder.Services.AddJWTAuthentication(builder.Configuration["Jwt:Key"], builder.Configuration["Jwt:Issuer"]);


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

app.Services.ApplyPendingMigrations();



app.Run();
