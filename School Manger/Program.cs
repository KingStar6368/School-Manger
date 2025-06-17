using School_Manager.Core.Services.Implemetations;
using School_Manager.Core.Services.Interfaces;
using School_Manager.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var container = new Container();

container.Register(builder.Services);
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization();

builder.Services.AddScoped<IBillService, BillService>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IParentService,ParentService>();
builder.Services.AddScoped<IChildService,ChildService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<ISchoolService,SchoolService>();

var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
