using System;
using System.Collections.Generic;
using System.Text;

namespace BluetoothWeb.DataModel
{
    public class LocalDataModel
    {
        public DeviceFilter Filter { get; set; } = new DeviceFilter();

        public bool DisplayReceiveText { get; set; }

        public bool DisplayReceiveDataChart { get; set; }

        public bool AppendToStringEnd { get; set; } = true;

    }
}
