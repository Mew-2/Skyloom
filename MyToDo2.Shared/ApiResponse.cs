using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyToDo2.Shared
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }

        public ApiResponse()
        {
        }

        public ApiResponse(string message, bool status = false)
        {
            this.Status = status;
            this.Message = message;
        }

        public ApiResponse(bool status, object result)
        {
            this.Status = status;
            this.Result = result;
        }
    }

    public class ApiResponse<T>
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public T Result { get; set; }
    }
}