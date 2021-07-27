using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplicacionWebProyecto3.Models
{
    public class CitasModel
    {
        [Display(Name = "ID Cita")]
        public int ID_Citas { get; set; }

        [Required(ErrorMessage = "La {0} es requerido")]
        [DisplayFormat(DataFormatString = "{000000000}", ApplyFormatInEditMode = true)]
        [StringLength(9, ErrorMessage = "La {0} de tener al menos {2} digitos", MinimumLength = 9)]
        [Display(Name = "Cedula")]
        public string CedulaPaciente { get; set; }

        [Display(Name ="Centro de Salud")]
        public int CentroSalud { get; set; }

        [Display(Name = "Especialidad Requerida")]
        public int EspecialidadRequerida { get; set; }

        [Required(ErrorMessage = "{0} es requerida")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de la cita")]
        public string Fecha { get; set; }

        [Required(ErrorMessage = "{0} es requerida")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hora de la cita")]
        public string Hora { get; set; }
        public string Descipcion { get; set; }
        public CitasModel()
        {

        }//constructor vaci

        public CitasModel(int iD_Citas, string cedulaPaciente, int centroSalud, int especialidadRequerida, string fecha, string hora, string descipcion)
        {
            ID_Citas = iD_Citas;
            CedulaPaciente = cedulaPaciente;
            CentroSalud = centroSalud;
            EspecialidadRequerida = especialidadRequerida;
            Fecha = fecha;
            Hora = hora;
            Descipcion = descipcion;
        }
    }//fin class
}//fin namespace
