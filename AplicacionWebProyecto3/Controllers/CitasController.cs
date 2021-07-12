using AplicacionWebProyecto3.Models;
using Microsoft.AspNetCore.Http;
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
            List<EspecialidadModel> especialidadList = ListarEspecialidad();
            List<SelectListItem> selectListItemsEspec = especialidadList.ConvertAll(especialidadModel =>
            {
                return new SelectListItem()
                {
                    Text = especialidadModel.Nombre.ToString(),
                    Value = especialidadModel.ID_Especialidad.ToString(),
                    Selected = false
                };
            });

            List<AreaSaludModel> areaSaludList = ListarAreaSalud();
            List<SelectListItem> selectListItems = areaSaludList.ConvertAll(areaSalud =>
            {
                return new SelectListItem()
                {
                    Text = areaSalud.Nombre.ToString(),
                    Value = areaSalud.ID.ToString(),
                    Selected = false
                };
            });


            ViewBag.AreaSalud = selectListItems;
            ViewBag.Especialidad = selectListItemsEspec;
            return View();
        }  // Fin RegistrarCita

        [HttpPost]
        public IActionResult RegistrarCita(CitasModel citasModel)
        {
            Console.WriteLine("Cedula: " + citasModel.CedulaPaciente);
            Console.WriteLine(citasModel.Fecha + " " + citasModel.Hora);
            Console.WriteLine("comboEspecialidad" + Request.Form["listaEspecialidad"].ToString());
            Console.WriteLine("comboArea" + Request.Form["listaAreaSalud"].ToString());
            if (ModelState.IsValid)
            {
                string conexionString = Configuration["ConnectionStrings:DB_Connection_Turrialba"];
                var connection = new SqlConnection(conexionString);

                var fechatemp = citasModel.Fecha + " " + citasModel.Hora;

                Console.WriteLine(fechatemp);
                string sqlQuery = $"exec sp_insertarCita @param_CEDULA_PACIENTE = '{citasModel.CedulaPaciente}'," +
                    $"@param_ID_CENTRO_SALUD = '{Convert.ToInt32(Request.Form["listaAreaSalud"].ToString())}', " +
                    $"@param_FECHA_HORA_CITA = '{fechatemp}'," +
                    $"@param_ESPECIALIDAD = '{Convert.ToInt32(Request.Form["listaEspecialidad"].ToString())}'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    command.ExecuteReader();
                    connection.Close();
                };

            }
            return View("Index");
        }// fin RegistrarCita
        private List<AreaSaludModel> ListarAreaSalud()
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
                return areaSaludList;
            }
            return null;
        }
        private List<EspecialidadModel> ListarEspecialidad()
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
                 return especialidadList;
            }
            return especialidadList;
        }// Fin ItemsEspecialidad

        public IActionResult Listar()
        {
            List<CitasModel> listaCitas = new List<CitasModel>();
            if (ModelState.IsValid)
            {
                string connectionString = Configuration["ConnectionStrings:DB_Connection_Turrialba"];

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = $"exec sp_getAllCitas";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        SqlDataReader sqlDataReader = command.ExecuteReader();
                        while (sqlDataReader.Read())
                        {
                            CitasModel citas = new CitasModel();
                            citas.ID_Citas = Int32.Parse(sqlDataReader["ID"].ToString());
                            citas.CedulaPaciente = sqlDataReader["CEDULA_PACIENTE"].ToString();
                            citas.Fecha = sqlDataReader["FECHA"].ToString();
                            //Console.WriteLine("----------------"+sqlDataReader["FECHA"].ToString());
                            citas.Hora = sqlDataReader["HORA"].ToString();
                            citas.CentroSalud = Int32.Parse(sqlDataReader["ID_CENTRO_SALUD"].ToString());
                            citas.EspecialidadRequerida = Int32.Parse(sqlDataReader["ESPECIALIDAD"].ToString());
                            citas.Descipcion = sqlDataReader["DESCRIPCION_DETALLADA"].ToString();
                            listaCitas.Add(citas);
                        }
                    }
                }

            }
            ViewBag.Especialidades = ListarEspecialidad();
            ViewBag.AreasSalud = ListarAreaSalud();
            ViewBag.Citas = listaCitas;
            return View();
        }
    }
}
