using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace MongoDBDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public async Task<Task<string>> Get()
        {
            HttpClient client = new HttpClient();
            var matches =  client.GetAsync("https://jsonmock.hackerrank.com/api/football_matches?year=2011&team1=Barcelona").Result;
            return matches.Content.ReadAsStringAsync();




        }
    }
}
