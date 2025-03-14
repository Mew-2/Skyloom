using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo2.Services
{
    /// <summary>
    /// URL请求参数
    /// </summary>
    public class BaseRequest
    {
        public Method Method { get; set; }
        public string Route { get; set; }

        public string ContentType { get; set; } = "application/json";
        public object Parameter { get; set; }
    }
}