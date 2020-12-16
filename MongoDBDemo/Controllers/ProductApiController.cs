using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBDemo.Models;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoDBDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase, IProductApiController
    {
        private readonly IDistributedCache _distributedCache;
        public ProductApiController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        IMongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
        // GET: api/<ProductApiController>
        [HttpGet]
        public async Task<IEnumerable<Products>> Get()
        
        {
            IEnumerable<Products> productResults;
            string serializedProducts;
           // await _distributedCache.RemoveAsync("");
            var encodedProducts = await _distributedCache.GetAsync("");
            if (encodedProducts != null)
            {
                serializedProducts = Encoding.UTF8.GetString(encodedProducts);
                productResults = JsonConvert.DeserializeObject<IEnumerable<Products>>(serializedProducts);
            }
            else
            {

                var database = mongoClient.GetDatabase("ecommerce");
                var collection = database.GetCollection<Products>("products");
                productResults = collection.Find<Products>(a => true).ToList();
                serializedProducts = JsonConvert.SerializeObject(productResults);
                encodedProducts = Encoding.UTF8.GetBytes(serializedProducts);
                var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5)).SetAbsoluteExpiration(DateTime.Now.AddHours(6));
                await _distributedCache.SetAsync("", encodedProducts, option);
               
            }
            return (IEnumerable<Products>)productResults;
        }

        // GET api/<ProductApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductApiController>
        [HttpPost]
        public void Post([FromBody] Products products)
        {
            var database = mongoClient.GetDatabase("ecommerce");
            var collection = database.GetCollection<Products>("products");
            collection.InsertOne(products);
        }

        // PUT api/<ProductApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
