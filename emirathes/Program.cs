using emirathes.Extensions;
using emirathes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using emirathes.IRepository;


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

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(40);
    options.Cookie.IsEssential = true;
});


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true; //R?q?m t?l?b edin
    options.Password.RequireLowercase = false; //Kiçik h?rf t?l?b edin
    options.Password.RequireUppercase = false; //Böyük h?rf t?l?b edin
    options.Password.RequiredLength = 8; //T?l?b olunan uzunluq...
    options.Password.RequireNonAlphanumeric = false; //@ * ! ve.s kimi simvollar olmalidi
    options.Lockout.MaxFailedAccessAttempts = 50; //5 giri?ten sonra bloklanir 
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMilliseconds(5); //bloklamndiqdan 5deq sonra acilir
    options.Lockout.AllowedForNewUsers = true; //yeni qeydiyyat userdirse passwordu unuda biler.bir nece yazdiqda bloklamaya bilersiz
                                               //   options.User.AllowedUserNameCharacters =
                                               //"abcdefghijklmnopqrstuvwxyz0123456789._";//olmas?n? istediyiniz vacib karaterleri yazin
    options.User.RequireUniqueEmail = true; //unique email adresleri olsun (1emaille bir qeydiyyat)
    options.SignIn.RequireConfirmedEmail = false; //qeydiyyat etdikden sonra email ile token gönderecek 
    options.SignIn.RequireConfirmedPhoneNumber = false; //telefon do?rulamas?
});

builder.Services.AddScoped<IEmailService, EmailService>();


//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = FacebookDefaults.AuthenticationScheme;
//})

//    .AddCookie()

//.AddFacebook(opt =>
// {
//     opt.AppId = "415431347994155";
//     opt.AppSecret = "757550bb5850a889982b88cf93614832";
// });

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();
app.UseSession();

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

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
app.UseStatusCodePagesWithReExecute("/Error/NotFound", "?statusCode={0}");

app.UseAuthorization();


app.MapControllerRoute(
    name: "Admin",
      pattern: "{area:exists}/{controller=DashBoard}/{action=Index}/{id?}");

app.MapControllerRoute(

     name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}");
   




app.Run();

//app.Use(async (context, next) =>
//{
//    if (!context.User.Identity.IsAuthenticated)
//    {
//        context.Response.Redirect("/Account/Login");
//        return;
//    }

//    await next();
//});
