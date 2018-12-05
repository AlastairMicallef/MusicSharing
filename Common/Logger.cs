using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    class Logger
    {
        public static void Log(string user, string method, string message)
        {
            Trace.WriteLine(String.Format("Date: {0}, User: {1}, Method: {2}, Message: {3}"
                , DateTime.Now.ToString("dd/MM/yyyy hh:ss:ff"), user, method, message
                ));


        }
    }
}
