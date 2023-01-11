#nullable disable
using Business.Services;
using DataAccess.Contexts;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

#region Culture
List<CultureInfo> cultures = new List<CultureInfo>()
{
    new CultureInfo("en-US")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name);
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});
#endregion

#region Authentication and sessýon 
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.LoginPath = "/Account/Users/Login";
        config.AccessDeniedPath = "/Account/Users/AccessDenied";
        config.ExpireTimeSpan = TimeSpan.FromMinutes(50);
        config.SlidingExpiration = true;
    });
#endregion

string connectionString = builder.Configuration.GetConnectionString("StudentKayitDb");
#region IoC Container (Inversion of Control Container)
builder.Services.AddDbContext<StudentKayitContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<StudentRepoBase, StudentRepo>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ClassRepoBase, ClassRepo>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<LessonRepoBase, LessonRepo>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<UserRepoBase, UserRepo>();
builder.Services.AddScoped<IReportService, ReportService>();
#endregion
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
#region Culture
app.UseRequestLocalization(options =>
{
    options.DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name);
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});
#endregion
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 

app.UseAuthorization(); 

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
