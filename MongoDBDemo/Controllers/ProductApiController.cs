using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBDemo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoDBDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        IMongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
        // GET: api/<ProductApiController>
        [HttpGet]
        public IEnumerable<Products> Get()
        {
           var database = mongoClient.GetDatabase("ecommerce");
            var collection = database.GetCollection<Products>("products");
            var productResults = collection.Find<Products>(a => true).ToList();
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
