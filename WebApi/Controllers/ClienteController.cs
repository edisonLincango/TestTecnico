using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using TestData ;

namespace WebApi.Controllers
{
    public class ClienteController : ApiController
    {         
        
        private Entities contex = new Entities();

        [HttpGet]
        public IEnumerable<Cliente> Get()
        {
            using (Entities db = new Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Clientes;
            }
        }

        [HttpGet]
        public Cliente Get(int numeroCliente)
        {
            using (Entities db = new Entities())
            {                
                return db.Clientes.FirstOrDefault(x => x.numeroCliente == numeroCliente);
            }
        }

        [HttpGet]
        public Cliente Get(int numeroCola, string estado)
        {
            using (Entities db = new Entities())
            {
                return db.Clientes.FirstOrDefault(x => x.numeroCola == numeroCola && x.estado == estado);
            }
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.horaRegistro = DateTime.Now;
                cliente.numeroCola = ObtenerNumeroCola();
                cliente.estado = "PEN";
                contex.Clientes.Add(cliente);
                contex.SaveChanges();
                return Ok(cliente);
            }
            else
            {
                return BadRequest();
            }
        }

        private int ObtenerNumeroCola() {

            using (Entities db = new Entities())
            {
                List<Cliente> pendientes = db.Clientes.Where(x => x.estado == "PEN").ToList();

                List<Cola> colas = db.Colas.ToList();

                Cola cola1 = colas.FirstOrDefault(x => x.numeroCola == 1);
                Cola cola2 = colas.FirstOrDefault(x => x.numeroCola == 2);

                List<Cliente> clientes1 = pendientes.Where(x => x.numeroCola == cola1.numeroCola).ToList();
                List<Cliente> clientes2 = pendientes.Where(x => x.numeroCola == cola2.numeroCola).ToList();

                if ((clientes1.Count * cola1.minutosAtencion) < (clientes2.Count * cola2.minutosAtencion))
                {
                    return cola1.numeroCola;
                }
                else 
                {
                    return cola2.numeroCola;
                }
            }           
        }

        [HttpPut]
        public IHttpActionResult Put(int numeroCliente, [FromBody] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if (contex.Clientes.Count(x => x.numeroCliente == numeroCliente) > 0)
                {
                    contex.Entry(cliente).State = EntityState.Modified;
                    contex.SaveChanges();
                    return Ok(cliente);
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
        public IHttpActionResult Delete(int numeroCliente)
        {
            var cliente = contex.Clientes.Find(numeroCliente);

            if (cliente != null)
            {
                contex.Clientes.Remove(cliente);
                contex.SaveChanges();

                return Ok(cliente);
            }
            else
            {
                return NotFound();
            }

        }
    }
}