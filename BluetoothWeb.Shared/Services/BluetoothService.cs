using Blazm.Bluetooth;

using BluetoothWeb.DataModel;
using Masa.Blazor;
using System.Collections.ObjectModel;
using System.Reflection.PortableExecutable;

namespace BluetoothWeb.Shared.Services
{
    public class BluetoothService : IBluetoothService
    {
        private readonly IBluetoothNavigator _bluetoothNavigator;
        private readonly IPopupService _popupService;

        public event Action<DeviceInforModel> OnDeviceChanged;
        public event Action<byte[]> OnDataReceive;

        private DeviceFilter _deviceFilter { get; set; } = new DeviceFilter();

        private Device device { get; set; }
        private Device Device
        {
            get => device;
            set
            {
                //记录日志
                if (value != null)
                {
                    Logs.Add("已配对：" + (string.IsNullOrWhiteSpace(value.Name) ? value.Id : value.Name));
                }
                else if (device != null)
                {
                    Logs.Add("已断开：" + (string.IsNullOrWhiteSpace(device.Name) ? device.Id : device.Name));
                }
                device = value;
                DeviceChanged();

            }
        }

        public BluetoothService(IBluetoothNavigator bluetoothNavigator, IPopupService popupService)
        {
            _bluetoothNavigator = bluetoothNavigator;
            _popupService = popupService;
        }

        public ObservableCollection<string> Logs { get; set; } = new ObservableCollection<string>
        {
            "Logs:"
        };

        public void DeviceChanged()
        {
            OnDeviceChanged?.Invoke(GetConnectedDeviceInfor());
        }

        public DeviceInforModel GetConnectedDeviceInfor()
        {
            return Device == null
                ? null
                : new DeviceInforModel
                {
                    Id = Device.Id,
                    Name = Device.Name,
                    Connected = true
                };
        }

        public async Task RequestDevice(DeviceFilter filter)
        {
            try
            {
                Device = null;

                RequestDeviceQuery query = new() { AcceptAllDevices = filter.AllowAllDevices };

                if (!filter.AllowAllDevices)
                {
                    query.Filters = new List<Filter>
                    {
                        new Filter
                        {
                            Name = string.IsNullOrWhiteSpace(filter.DeviceName)
                                ? null
                                : filter.DeviceName,
                            NamePrefix = string.IsNullOrWhiteSpace(filter.DeviceNamePrefix)
                                ? null
                                : filter.DeviceNamePrefix,
                        }
                    };
                }

                if (!string.IsNullOrEmpty(filter.ServiceUuid))
                {
                    query.OptionalServices = filter.ServiceUuid.Replace("，", ",").Replace("、", ",").Split(',').ToList();
                }
                //请求配对
                Device = await _bluetoothNavigator.RequestDeviceAsync(query);
                //开启消息通知
                await _bluetoothNavigator.SetupNotifyAsync(Device, filter.ServiceUuid, filter.TXuuid);
                Device.Notification += Device_Notification;
                //保持配置
                _deviceFilter = filter;
            }
            catch (System.Exception ex)
            {
                await ProcError("请求配对失败", ex);
            }

        }

        public void RemoveDevice()
        {
            Device.Notification-= Device_Notification;
            Device = null;
        }

        public async Task SendBytes(byte[] bytes)
        {
            await _bluetoothNavigator.WriteValueAsync(device.Id, _deviceFilter.ServiceUuid, _deviceFilter.RXuuid, bytes);
        }

        public async Task SendString(string str)
        {
            await SendBytes(System.Text.Encoding.UTF8.GetBytes(str));
        }
       
        public Task<bool> GetAvailability()
        {
            return Task.FromResult(true);
        }

        public async Task ProcError(string message, Exception ex)
        {
            Logs.Add($"Exception: {ex.Message}");
            await _popupService.ToastErrorAsync(message);
        }

        private async void Device_Notification(object sender, CharacteristicEventArgs e)
        {
            var data = e.Value.ToArray();
            await Task.Run(() => OnDataReceive?.Invoke(data));
        }
    }

}
