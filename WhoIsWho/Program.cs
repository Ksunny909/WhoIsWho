using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WhoIsWho.Data;
using WhoIsWho.Models.Entities;
using WhoIsWho.Mappings;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("default"), cfg => cfg.CommandTimeout(300)));

builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(cfg =>
{
    cfg.Password.RequiredLength = 3;
    cfg.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opts => {
      
});

builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(edpt => { edpt.MapDefaultControllerRoute(); edpt.MapControllers(); });

await SeedDB.Initialize(app.Services.CreateScope());

app.Run();
