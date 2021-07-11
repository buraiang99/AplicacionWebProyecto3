using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplicacionWebProyecto3.Models
{
    public class VacunaModel
    {
        public int IDVacuna { get; set; }
        public string CedulaPaciente { get; set; }
        public string NombreVacuna { get; set; }
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "{0} es requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
        [Display(Name = "Fecha de la vacuna")]
        public string FechaAplicacion { get; set; }
        [Required(ErrorMessage = "{0} es requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
        [Display(Name = "Fecha de la proxima vacuna")]
        public string FechaProxima { get; set; }
        public VacunaModel()
        {

        }//constructor vacio

        public VacunaModel(int iDVacuna, string cedulaPaciente, string nombreVacuna,string descripcion,string fechaAplicacion,string fechaProxima)
        {
            IDVacuna = iDVacuna;
            CedulaPaciente = cedulaPaciente;
            NombreVacuna = nombreVacuna;
            Descripcion = descripcion;
            FechaAplicacion = fechaAplicacion;
            FechaProxima = fechaProxima;
        }
    }//fin class
}//fin namespace
