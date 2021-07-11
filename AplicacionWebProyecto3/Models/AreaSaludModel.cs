using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplicacionWebProyecto3.Models
{
    public class AreaSaludModel
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public AreaSaludModel()
        {

        }
        public AreaSaludModel(int iD, string nombre)
        {
            ID = iD;
            Nombre = nombre;
        }
    }
}
