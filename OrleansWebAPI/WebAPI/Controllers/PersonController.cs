using IGrains;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class PersonController : ApiController
    {
        [HttpGet]
        public string SayHello(string name)
        {
            var grain = GrainClient.GrainFactory.GetGrain<IPersonGrain>(name);
            grain.SayHelloAsync();
            return "success";
        }
    }
}
