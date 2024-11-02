using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TopLearn.Core.Convertors;
using TopLearn.Core.Services;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

#region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
    options.SlidingExpiration = true;
});

#endregion

#region DataBase Context

builder.Services.AddDbContext<TopLearnContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TopLearnConnection"));
});

#endregion

#region Ioc

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IViewRenderService, RenderViewToString>();
builder.Services.AddTransient<IPermissionService, PermissionService>();
//builder.Services.AddTransient<ICourseService, CourseService>();
//builder.Services.AddTransient<IOrderService, OrderService>();
//builder.Services.AddTransient<IForumService, ForumService>();

#endregion

var app = builder.Build();

#region Middleware

//// Middleware 404
//app.Use(async (context, next) =>
//{
//    await next();
//    if (context.Response.StatusCode == 404)
//    {
//        context.Response.Redirect("/Home/Error404");
//    }
//});

//// Middleware coursefilesonline
//app.Use(async (context, next) =>
//{
//    if (context.Request.Path.Value!.ToLower().StartsWith("/coursefilesonline"))
//    {
//        var callingUrl = context.Request.Headers["Referer"].ToString();
//        if (!string.IsNullOrEmpty(callingUrl) &&
//            (callingUrl.StartsWith("https://localhost:44349") || callingUrl.StartsWith("https://localhost:44349")))
//        {
//            await next.Invoke();
//        }
//        else
//        {
//            context.Response.Redirect("/Login");
//        }
//    }
//    else
//    {
//        await next.Invoke();
//    }
//});

#endregion

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

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapRazorPages();

app.Run();
