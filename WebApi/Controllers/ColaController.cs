using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using TestData;

namespace WebApi.Controllers
{
    public class ColaController : ApiController
    {
        private Entities contex = new Entities();
        
        [HttpGet]
        public IEnumerable<Cola> Get()
        {
            using (Entities db = new Entities())
            {                
                return db.Colas.ToList();
            }
        }


        [HttpPost]
        public IHttpActionResult Post([FromBody] Cola cola)
        {
            if (ModelState.IsValid)
            {
                contex.Colas.Add(cola);
                contex.SaveChanges();
                return Ok(cola);
            }
            else 
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IHttpActionResult Put(int numeroCola, [FromBody] Cola cola)
        {
            if (ModelState.IsValid)
            {
                if (contex.Colas.Count(x => x.numeroCola == numeroCola) > 0)
                {
                    contex.Entry(cola).State = EntityState.Modified;
                    contex.SaveChanges();
                    return Ok(cola);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int numeroCola)
        {
            var cola = contex.Colas.Find(numeroCola);

            if (cola != null)
            {
                contex.Colas.Remove(cola);
                contex.SaveChanges();

                return Ok(cola);
            }
            else
            {
                return NotFound();
            }
        
        }
    }
}