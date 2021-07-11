using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "{0} es requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
        [Display(Name = "Fecha de la cita")]
        public string Fecha { get; set; }

        [Required(ErrorMessage = "{0} es requerido")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = false)]
        [Display(Name = "Hora de la cita")]
        public string Hora { get; set; }
        public CitasModel()
        {

        }//constructor vaci
        public CitasModel(int iDCitas, string cedulaPaciente, int centroSalud, int especialidadRequerida, string fecha, string hora)
        {
            IDCitas = iDCitas;
            CedulaPaciente = cedulaPaciente;
            CentroSalud = centroSalud;
            EspecialidadRequerida = especialidadRequerida;
            Fecha = fecha;
            Hora = hora;
        }
    }//fin class
}//fin namespace
