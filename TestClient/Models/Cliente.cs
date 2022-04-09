using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestClient.Models
{
    public class Cliente
    {
        public int numeroCliente { get; set; }
        public int numeroCola { get; set; }
        public string id { get; set; }
        public string nombre { get; set; }
        public DateTime horaRegistro { get; set; }
        public string estado { get; set; }
    }
}