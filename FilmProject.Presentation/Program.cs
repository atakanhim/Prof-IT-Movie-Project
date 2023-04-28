using FilmProject.Infrastructure.Data;
using FilmProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FilmProject.Application.Interfaces;
using FilmProject.Application.Services;
using FilmProject.Infrastructure.Repository.Abstract;
using FilmProject.Infrastructure.Repository.Concrete;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.VisualStudio.Web.CodeGeneration;
using QRCoder;
using Serilog;

using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Mappings;
using FilmProject.Presentation.Mappings;

using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;
using System.Reflection;
using Microsoft.AspNetCore.Localization.Routing;
using FluentValidation.AspNetCore;
using FluentValidation;
using FilmProject.Presentation.Models;
using FilmProject.Presentation.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//Serilog Configuration has been added
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

//auto mapper 
builder.Services.AddAutoMapper(typeof(MovieProfile));
builder.Services.AddAutoMapper(typeof(PresentationMovieProfile));

builder.Services.AddControllersWithViews()
    .AddViewLocalization();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("InspiniaPolicy", policy =>
    {
        policy.RequireRole("Admin");
        
    });
});

#region connection-helpers
//services
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IMovieCategoryMapService, MovieCategoryMapService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IRoleService, RoleService>();

//repositories
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IMovieCategoryMapRepository, MovieCategoryMapRepository>();
builder.Services.AddScoped<IEmailService, EmailSender>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddSingleton(new QRCodeService(new QRCodeGenerator()));
#endregion 

#region Localization
builder.Services.AddLocalization(opt => { opt.ResourcesPath = "Resource"; });

builder.Services.Configure<RequestLocalizationOptions>(opt =>
{
    //Supported Language packets were created 
    var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("tr-TR"),
    };

    //Localization configurations were declared
    opt.DefaultRequestCulture = new RequestCulture("tr-TR");
    opt.SupportedCultures = supportedCultures;
    opt.SupportedUICultures = supportedCultures;

    //Localization passing ways were created
    opt.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider(),
        new AcceptLanguageHeaderRequestCultureProvider()
    };

    //    opt.RequestCultureProviders = new[]
    //{
    //            new RouteDataRequestCultureProvider()
    //            {
    //                Options=opt
    //            }
    //    };
});
#endregion


// fluent vlaidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddTransient<IValidator<CategoryViewModel>,CategoryValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);

app.UseEndpoints(endpoints =>
{
   
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapAreaControllerRoute(
     name: "Inspinia",
     areaName: "Inspinia",
     pattern: "Inspinia/{controller=AdminMetrics}/{action=Metrics}"
 ).RequireAuthorization("InspiniaPolicy"); ;
});
app.MapRazorPages();

app.Run();
