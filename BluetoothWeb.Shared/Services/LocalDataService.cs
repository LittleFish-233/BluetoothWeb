using Blazored.LocalStorage;
using BluetoothWeb.DataModel;

namespace BluetoothWeb.Shared.Services
{
    public class LocalDataService : ILocalDataService
    {
        public LocalDataModel Data { get; set; } = new LocalDataModel();
        private readonly ILocalStorageService _localStorage;

        public LocalDataService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            _ = Load();
        }

        public async Task Save()
        {
            await _localStorage.SetItemAsync("LocalData", Data);
        }

        public async Task Load()
        {
            LocalDataModel data = await _localStorage.GetItemAsync<LocalDataModel>("LocalData");
            if (data != null)
            {
                Data = data;
            }
        }
    }
}
