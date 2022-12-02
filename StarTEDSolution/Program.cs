using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarTEDSystem;
using StarTEDSystem.BLL;
using StarTEDSystem.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//  Setup the connection string service for the applciation.
//  1)  Retreive the connection string information from your appsettings.json
var connectionString = builder.Configuration.GetConnectionString("StarTEDDb");

//  2)  Register any "services" you wish to use.
//      In our solution, our service will be created (coded) in the class library WestWindSystem.
//      One of these services will be the setup of the database context connection.
//      Another service will be created as the application requires.

//  This setup can be done here locally.
//  Or, this setup can aslo be done elsewheere and called from this location ****

builder.Services.AddBackendDependencies(options => options.UseSqlServer(connectionString));
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
  