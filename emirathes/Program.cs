using DocuSign.eSign.Model;
using emirathes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Core;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContent>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
    });

builder.Services.AddIdentity<ProgramUsers, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContent>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true; //R?q?m t?l?b edin
    options.Password.RequireLowercase = false; //Kiçik h?rf t?l?b edin
    options.Password.RequireUppercase = false; //Böyük h?rf t?l?b edin
    options.Password.RequiredLength = 8; //T?l?b olunan uzunluq...
    options.Password.RequireNonAlphanumeric = false; //@ * ! ve.s kimi simvollar olmalidi
    options.Lockout.MaxFailedAccessAttempts = 5; //5 giri?ten sonra bloklanir 
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMilliseconds(5); //bloklamndiqdan 5deq sonra acilir
    options.Lockout.AllowedForNewUsers = true; //yeni qeydiyyat userdirse passwordu unuda biler.bir nece yazdiqda bloklamaya bilersiz
                                               //   options.User.AllowedUserNameCharacters =
                                               //"abcdefghijklmnopqrstuvwxyz0123456789._";//olmas?n? istediyiniz vacib karaterleri yazin
    options.User.RequireUniqueEmail = true; //unique email adresleri olsun (1emaille bir qeydiyyat)
    options.SignIn.RequireConfirmedEmail = false; //qeydiyyat etdikden sonra email ile token gönderecek 
    options.SignIn.RequireConfirmedPhoneNumber = false; //telefon do?rulamas?
});

//builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection(""));

//builder.Services.AddScoped<IEmailService, EmailService>();


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

app.UseAuthorization();


app.MapControllerRoute(
    name: "Admin",
      pattern: "{area:exists}/{controller=DashBoard}/{action=Index}/{id?}");

app.MapControllerRoute(

     name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}");
   




app.Run();
