using System;
using System.Collections.Generic;
using System.Text;

namespace BluetoothWeb.DataModel
{

    public class DeviceFilter
    {
        public string DeviceName { get; set; } = "HC-02";

        public string DeviceNamePrefix { get; set; }

        public bool AllowAllDevices { get; set; } 

        public string ServiceUuid { get; set; } = "49535343-fe7d-4ae5-8fa9-9fafd205e455";

        public string TXuuid { get; set; } = "49535343-1E4D-4BD9-BA61-23C647249616";
        public string RXuuid { get; set; } = "49535343-8841-43F4-A8D4-ECBE34729BB3";


    }
}
