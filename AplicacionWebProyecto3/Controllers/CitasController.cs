using AplicacionWebProyecto3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AplicacionWebProyecto3.Controllers
{
    public class CitasController : Controller
    {
        public IConfiguration Configuration { get; set; }

        public CitasController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegistrarCita()
        {

            ViewBag.AreaSalud = ItemsAreaSalud();
            ViewBag.Especialidad = ItemsEspecialidad();
            return View();
        }

        private List<SelectListItem> ItemsAreaSalud()
        {
            List<AreaSaludModel> areaSaludList = new List<AreaSaludModel>();
            if (ModelState.IsValid)
            {
                string conexionString = Configuration["ConnectionStrings:DB_Connection_Turrialba"];
                //var connection = new SqlConnection(conexionString);
                using (SqlConnection connection = new SqlConnection(conexionString))
                {
                    string sqlQuery = $"exec sp_getAllCentroSalud";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        SqlDataReader sqlDataReader = command.ExecuteReader();
                        while (sqlDataReader.Read())
                        {
                            AreaSaludModel areaSalud = new AreaSaludModel();
                            areaSalud.ID = Int32.Parse(sqlDataReader["ID"].ToString());
                            areaSalud.Nombre = sqlDataReader["NOMBRE"].ToString();
                            areaSaludList.Add(areaSalud);
                        }
                        connection.Close();
                    }
                }
                List<SelectListItem> selectListItems = areaSaludList.ConvertAll(areaSalud =>
                {
                    return new SelectListItem()
                    {
                        Text = areaSalud.Nombre.ToString(),
                        Value = areaSalud.ID.ToString(),
                        Selected = false
                    };
                });
                return selectListItems;
            }
            return null;
        }
        private List<SelectListItem> ItemsEspecialidad()
        {
            List<EspecialidadModel> especialidadList = new List<EspecialidadModel>();
            if (ModelState.IsValid)
            {
                string conexionString = Configuration["ConnectionStrings:DB_Connection_Turrialba"];
                //var connection = new SqlConnection(conexionString);
                using (SqlConnection connection = new SqlConnection(conexionString))
                {
                    string sqlQuery = $"exec sp_getAllEspecialidad";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        SqlDataReader sqlDataReader = command.ExecuteReader();
                        while (sqlDataReader.Read())
                        {
                            EspecialidadModel especialidadModel = new EspecialidadModel();
                            especialidadModel.ID_Especialidad = Int32.Parse(sqlDataReader["ID"].ToString());
                            especialidadModel.Nombre = sqlDataReader["NOMBRE"].ToString();
                            especialidadList.Add(especialidadModel);
                        }
                        connection.Close();
                    }
                }
                List<SelectListItem> selectListItems = especialidadList.ConvertAll(especialidadModel =>
                {
                    return new SelectListItem()
                    {
                        Text = especialidadModel.Nombre.ToString(),
                        Value = especialidadModel.ID_Especialidad.ToString(),
                        Selected = false
                    };
                });
                 return selectListItems;
            }
            return null;
        }

        [HttpPost]
        public IActionResult RegistrarCita(CitasModel citasModel)
        {
            Console.WriteLine("Hora: "+citasModel.Hora+" Fecha: "+citasModel.Fecha);
            //if (ModelState.IsValid)
            //{
            //    string conexionString = Configuration["ConnectionStrings:DB_Conection_Turrialba"];
            //    var connection = new SqlConnection(conexionString);

            //    string sqlQuery = $"exec sp_insertarCita @param_CEDULA_PACIENTE = '{citasModel.CedulaPaciente}'," +
            //        $"@param_ID_CENTRO_SALUD = '{citasModel.CedulaPaciente}'";

            //}
            return View();
        }
    }
}
