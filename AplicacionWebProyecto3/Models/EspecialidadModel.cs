using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplicacionWebProyecto3.Models
{
    public class EspecialidadModel
    {
        public int ID_Especialidad { get; set; }
        public string Nombre { get; set; }
        public EspecialidadModel()
        {

        }
        public EspecialidadModel(int iD_Especialidad, string nombre)
        {
            ID_Especialidad = iD_Especialidad;
            Nombre = nombre;
        }
    }
}
