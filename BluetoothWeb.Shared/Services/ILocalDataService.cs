using BluetoothWeb.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothWeb.Shared.Services
{
    public interface ILocalDataService
    {
        LocalDataModel Data { get; set; }

        bool IsApp { get; set; }

        Task Save();

        Task Load();
    }
}
