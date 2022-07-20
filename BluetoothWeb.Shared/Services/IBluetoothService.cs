using BluetoothWeb.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothWeb.Shared.Services
{
    public interface IBluetoothService
    {
        ObservableCollection<string> Logs { get; set; }

        event Action<DeviceInforModel> OnDeviceChanged;

        event Action<byte[]> OnDataReceive;

        Task SendString(string str);

        DeviceInforModel GetConnectedDeviceInfor();

        Task RequestDevice(DeviceFilter filter);


        Task<bool> GetAvailability();

        void RemoveDevice();
    }
}
