using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplicacionWebProyecto3.Models
{
    public class AlergiaModel
    {
        public int IDAlergia { get; set; }
        public string CedulaPaciente { get; set; }
        public string NombreAlergia { get; set; }
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "{0} es requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
        [Display(Name = "Fecha del diagnostico")]
        public string FechaDiagnostico { get; set; }
        public string Medicamentos { get; set; }
        public AlergiaModel()
        {

        }//constructor vacio

        public AlergiaModel(int iDAlergia, string cedulaPaciente, string nombreAlergia, string descripcion, string fechaDiagnostico, string medicamentos)
        {
            IDAlergia = iDAlergia;
            CedulaPaciente = cedulaPaciente;
            NombreAlergia = nombreAlergia;
            Descripcion = descripcion;
            FechaDiagnostico = fechaDiagnostico;
            Medicamentos = medicamentos;
        }
    }//fin class
}//fin namespace
