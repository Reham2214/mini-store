using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using mini_store.Data;
using mini_store;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options => 
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(SharedResource));
    });

 builder.Services.AddDbContext<AppDbContext>(Options=>Options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
 ));
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var supportedCultures = new[] { "ar", "en-US" };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
options.SetDefaultCulture(supportedCultures[0]);
options.AddSupportedCultures(supportedCultures);
options.AddSupportedUICultures(supportedCultures);
// إزالة مزود لغة المتصفح
var browserLanguageProvider = options.RequestCultureProviders.OfType<Microsoft.AspNetCore.Localization.AcceptLanguageHeaderRequestCultureProvider>()
.FirstOrDefault();
if (browserLanguageProvider != null)
{
options.RequestCultureProviders.Remove(browserLanguageProvider);
}
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => 
{
    // تخفيف شروط كلمات المرور لتسهيل التجربة
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

var app = builder.Build();


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

var localizationOptions =
    app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(localizationOptions.Value);


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");


app.Run();