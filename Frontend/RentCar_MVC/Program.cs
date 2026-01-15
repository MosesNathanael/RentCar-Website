using RentCar_MVC;
using RentCar_MVC.Controllers;
using RentCar_MVC.Services;
using System.Globalization;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IDisplayCarService, HomePageService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5271/");
});
builder.Services.AddHttpClient<IAccountService, AccountService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5271/");
});
builder.Services.AddHttpClient<IRentalCarService, RentalCarServices>(client =>
{
     client.BaseAddress = new Uri("http://localhost:5271/");
});

builder.Services.AddHttpClient<IPaymentService, PaymentServices>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5271/");
});

builder.Services.AddHttpClient<ITransactionHistoryService, TransactionHistoryService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5271/");
});


builder.Services.AddHttpClient("RentCarApi", client => 
{ 
    client.BaseAddress = new Uri("http://localhost:5271/");
});




builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();



var app = builder.Build();

var cultureInfo = new CultureInfo("id-ID"); 
CultureInfo.DefaultThreadCurrentCulture = cultureInfo; 
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();




app.Run();
