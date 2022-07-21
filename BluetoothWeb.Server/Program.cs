using Blazm.Bluetooth;
using Blazored.LocalStorage;
using BluetoothWeb.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
// 添加Masa.Blazor
builder.Services.AddMasaBlazor(options=>{
    options.UseTheme(themeOptions=>{
        themeOptions.Primary = "purple";
    });
});
//添加蓝牙服务
builder.Services.AddBlazmBluetooth();
//添加本地储存
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(typeof(IBluetoothService), typeof(BluetoothService));
builder.Services.AddScoped(typeof(ILocalDataService), typeof(LocalDataService));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
