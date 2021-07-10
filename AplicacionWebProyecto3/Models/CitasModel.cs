using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplicacionWebProyecto3.Models
{
    public class CitasModel
    {
        public int IDCitas { get; set; }
        public string CedulaPaciente { get; set; }
        public int CentroSalud { get; set; }
        public int EspecialidadRequerida { get; set; }
        public string Fecha { get; set; }
        public CitasModel()
        {

        }//constructor vacio

        public CitasModel(int iDCitas, string cedulaPaciente, int centroSalud, int especialidadRequerida, string fecha)
        {
            IDCitas = iDCitas;
            CedulaPaciente = cedulaPaciente;
            CentroSalud = centroSalud;
            EspecialidadRequerida = especialidadRequerida;
            Fecha = fecha;
        }
    }//fin class
}//fin namespace
