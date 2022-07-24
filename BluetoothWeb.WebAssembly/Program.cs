using Blazm.Bluetooth;
using Blazored.LocalStorage;
using BluetoothWeb.Shared;
using BluetoothWeb.Shared.Services;
using BluetoothWeb.WebAssembly;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// ���Masa.Blazor
builder.Services.AddMasaBlazor(options => {
    options.UseTheme(themeOptions => {
        themeOptions.Primary = "purple";
    });
});
//�����������
builder.Services.AddBlazmBluetooth();
//��ӱ��ش���
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(typeof(IBluetoothService), typeof(BluetoothService));
builder.Services.AddScoped(typeof(ILocalDataService), typeof(LocalDataService));

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
