using testTerraformNet.Service.impl;
using testTerraformNet.Service;
using Microsoft.EntityFrameworkCore;
using testTerraformNet;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Allow Angular app's origin
              .AllowAnyMethod()                    // Allow any HTTP method (GET, POST, etc.)
              .AllowAnyHeader()                    // Allow any header
              .AllowCredentials();                 // Allow credentials (cookies, authorization headers)
    });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString =
   builder.Configuration.GetConnectionString("ProdConnection")
       ?? throw new InvalidOperationException("Connection string"
       + "'DefaultConnection' not found.");

builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddScoped<ICarService, CarService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
