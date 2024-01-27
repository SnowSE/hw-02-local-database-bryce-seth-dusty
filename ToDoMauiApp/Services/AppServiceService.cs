using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoMauiApp.Services
{
    public class AppServiceService
    {
        public NetworkAccess GetConnectionStatus()
        {
            NetworkAccess connection = Connectivity.Current.NetworkAccess;
            return connection;
        }
    }
}
