using Microsoft.Win32;
using School_Manager.Core.Services.Implemetations;
using School_Manager.Core.Services.Interfaces;
using School_Manager.IOC;
using School_Manger.Class;
using School_Manger.Models;
using School_Manger.PaymentService;
using SMS.Base;
using SMS.TempLinkService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var container = new Container();
builder.Services.RegisterServices(builder.Configuration);
//Container.Register(builder.Services);
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.Cookie.Name = "SchoolManger";
        options.LoginPath = "/Home/Login";
        options.AccessDeniedPath = "/Home/AccessDenied";
    });
builder.Services.AddSession();
builder.Services.AddSingleton<IAppConfigService>(new AppConfigService(builder.Configuration));
builder.Services.AddSingleton<ITempLink>(provider=> new TempLinkService(provider.GetRequiredService<IAppConfigService>()));
builder.Services.AddSingleton<ISMSService>(provider =>
{
    string apiKey = builder.Configuration["Sms:ApiKey"];
    return new SMSService(apiKey,provider.GetRequiredService<IAppConfigService>());
});

builder.Services.AddSingleton<IPayment>(new PaymentService());
builder.Services.AddSingleton<IZarinPalService>(new ZarinPalService(builder.Configuration["ZarinPal:Token"]));
builder.Services.AddHostedService<PaymentControl>(provider =>
    new PaymentControl(
        provider.GetRequiredService<IPayment>(),
        provider,
        provider.GetRequiredService<ILogger<PaymentControl>>()
    )
);
builder.Services.AddSingleton<ISmsQueue, SmsQueue>();
builder.Services.AddHostedService<SmsBackgroundService>();

//Container.Register(builder.Services);
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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Parent}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=SelectPage}/{id?}")
    .WithStaticAssets();


app.Run();
