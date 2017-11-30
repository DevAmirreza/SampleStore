using AYadollahibastani_C50_A03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AYadollahibastani_C50_A03.Controllers
{
    public class ShoppingCartRestController : ApiController
    {
        // GET: api/ShoppingCartRest
        [Route("~/api/ShoppingCartRest")]
        public IEnumerable<Product> Get()
        {
            return ShoppingCartList.Instance.GetList(); 
        }

        // GET: api/ShoppingCartRest/5
        public Product Get(int id)
        {
            return ShoppingCartList.Instance.GetList().FirstOrDefault(o => o.Id == id);
        }

        // POST: api/ShoppingCartRest
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ShoppingCartRest/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ShoppingCartRest/5
        public void Delete(int id)
        {
            Models.ShoppingCartList.Instance.RemoveProduct(id);
        }
    }
}
