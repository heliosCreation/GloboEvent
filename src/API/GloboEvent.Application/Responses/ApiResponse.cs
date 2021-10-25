using System.Collections.Generic;
using System.Net;

namespace GloboEvent.Application.Responses
{
    public class ApiResponse<T> : BaseResponse where T : class
    {

        public T Data { get; set; }

        public List<T> DataList { get; set; }


        public ApiResponse<T> setNotFoundResponse()
        {
            Succeeded = false;
            StatusCode = (int)HttpStatusCode.NotFound;
            return this;
        }
    }
}