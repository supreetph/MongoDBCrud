using Microsoft.AspNetCore.Mvc;
using MongoDBDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDBDemo.Controllers
{
    public interface IProductApiController
    {
        void Delete(int id);
       Task< IEnumerable<Products>> Get();
        string Get(int id);
        void Post([FromBody] Products products);
        void Put(int id, [FromBody] string value);
    }
}