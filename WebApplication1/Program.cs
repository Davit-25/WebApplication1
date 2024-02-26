using Microsoft.EntityFrameworkCore;
using WebApplication1;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Data;
using WebApplication1.Mapping;
using WebApplication1.Areas.Identity.Data;
using WebApplication1.Iterfaces;
using WebApplication1.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.Build.Execution;
using System.Security.Cryptography;
using Microsoft.AspNetCore.StaticFiles;
using WebApplication1.Models;
using WebApplication1.Entities;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Configuration;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Microsoft.AspNetCore.Mvc.ApplicationModels;



// Add services to the container.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddControllers(
options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFilmListService, FilmListService>();
builder.Services.AddScoped<IFilmListMapping<WebApplication1.Entities.FilmsList, WebApplication1.Models.ModelFilmList>, MapperFilmList>();

builder.Services.AddDbContext<CinemaTableContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("CinemaTableDB")));
builder.Services.AddDbContext<FilmApplicDBContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("FilmApplicDbUser")));
builder.Services.AddDefaultIdentity<ApplicationUser>(option=>option.SignIn.RequireConfirmedAccount=true).AddEntityFrameworkStores<FilmApplicDBContext>();



var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    using var dbContext = scope.ServiceProvider.GetRequiredService<CinemaTableContext>();
    dbContext.Database.Migrate();
}
using (var scope = app.Services.CreateScope())
{
    using var dbContext = scope.ServiceProvider.GetRequiredService<FilmApplicDBContext>();
    dbContext.Database.Migrate();
}

app.UseDefaultFiles();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapRazorPages();
app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllerRoute(
       name: "default",
       pattern: "{controller=Home}/{action=Index}/{id?}");
// Configure the HTTP request pipeline.
app.Run();
