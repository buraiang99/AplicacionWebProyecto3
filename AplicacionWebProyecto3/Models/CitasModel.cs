using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplicacionWebProyecto3.Models
{
    public class CitasModel
    {
        [Required(ErrorMessage = "La {0} es requerido")]
        [DisplayFormat(DataFormatString = "{000000000}", ApplyFormatInEditMode = true)]
        [StringLength(9, ErrorMessage = "La {0} de tener al menos {2} digitos", MinimumLength = 9)]
        [Display(Name = "Cedula")]
        public string CedulaPaciente { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Area de salud")]
        public int CentroSalud { get; set; }

        [Required(ErrorMessage = "La {0} es requerida")]
        [Display(Name = "Especialidad")]
        public int EspecialidadRequerida { get; set; }

        [Required(ErrorMessage = "{0} es requerida")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de la cita")]
        public string Fecha { get; set; }

        [Required(ErrorMessage = "{0} es requerida")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hora de la cita")]
        public string Hora { get; set; }
        public CitasModel()
        {

        }//constructor vaci
        public CitasModel(string cedulaPaciente, int centroSalud, int especialidadRequerida, string fecha, string hora)
        {
            CedulaPaciente = cedulaPaciente;
            CentroSalud = centroSalud;
            EspecialidadRequerida = especialidadRequerida;
            Fecha = fecha;
            Hora = hora;
        }
    }//fin class
}//fin namespace
