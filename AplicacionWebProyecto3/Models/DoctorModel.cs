using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplicacionWebProyecto3.Models
{
    public class DoctorModel
    {

        public string Cedula { get; set; }
        public int CodigoMedico { get; set; }
        public string Pass { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }

        public DoctorModel()
        {
            this.Cedula = "";
            this.CodigoMedico = 0;
            this.Pass = "";
            this.Nombre = "";
            this.Apellidos = "";
        }// fin constructor

        public DoctorModel(string cedula, int codigoMedico, string pass, string nombre, string apellidos)
        {
            Cedula = cedula;
            CodigoMedico = codigoMedico;
            Pass = pass;
            Nombre = nombre;
            Apellidos = apellidos;
        } // fin constructor
    }// fin class
}//fin namespace
