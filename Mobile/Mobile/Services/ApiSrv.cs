using Mobile.Abstract;
using Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Mobile.Services
{
    public abstract class ApiSrv:IApiSrv
    {
        protected readonly HttpClient client;
        //protected string Url { get; set; } = "https://pracainzynierska.azurewebsites.net/api/";
        protected string Url { get; set; } = "http://10.0.2.2:52239/api/";
        public ApiSrv()
        {
            client = new HttpClient();
        }
    }
}
