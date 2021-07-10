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
                ViewBag.Especialidad = selectListItems;
            }

            //ViewBag.Especialidad = selectListItems;
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarCita(CitasModel citasModel)
        {
            if (ModelState.IsValid)
            {
                string conexionString = Configuration["ConnectionStrings:DB_Conection_Turrialba"];
                var connection = new SqlConnection(conexionString);

            }
            return View();
        }
    }
}
