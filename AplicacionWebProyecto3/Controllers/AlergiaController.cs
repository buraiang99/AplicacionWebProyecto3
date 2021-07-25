using AplicacionWebProyecto3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AplicacionWebProyecto3.Controllers
{
    public class AlergiaController : Controller
    {
        public IConfiguration Configuration { get; set; }

        public AlergiaController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Registrar()
        {
            if (HttpContext.Session.GetString("Cedula") is null)
            {
                Console.WriteLine("entro" + HttpContext.Session.GetString("Cedula"));
                ViewData["Mensaje"] = "Debes de iniciar sesion para continuar";
                return View("Index");
            }
            else
            {
                return View();
            }
                
        }
        public IActionResult VerAlergias()
        {
            if (HttpContext.Session.GetString("Cedula") is null)
            {
                Console.WriteLine("entro" + HttpContext.Session.GetString("Cedula"));
                ViewData["Mensaje"] = "Debes de iniciar sesion para continuar";
                return View("Index");
            }
            else
            {
                List<AlergiaModel> alergias = new List<AlergiaModel>();
                if (ModelState.IsValid)
                {
                    string connectionString = Configuration["ConnectionStrings:DB_Connection_Turrialba"];
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string sqlQuery = $"exec sp_getAllAlergias";
                        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                        {
                            command.CommandType = CommandType.Text;
                            connection.Open();
                            SqlDataReader productosReader = command.ExecuteReader();
                            while (productosReader.Read())
                            {
                                AlergiaModel temp = new AlergiaModel();
                                temp.IDAlergia = Int32.Parse(productosReader["ID"].ToString());
                                temp.CedulaPaciente = productosReader["CEDULA_PACIENTE"].ToString();
                                temp.NombreAlergia = productosReader["NOMBRE_ALERGIA"].ToString();
                                temp.Descripcion = productosReader["DESCRIPCION"].ToString();
                                temp.FechaDiagnostico = productosReader["FECHA_DIAGNOSTICO"].ToString();
                                temp.Medicamentos = productosReader["MEDICAMENTOS"].ToString();
                                alergias.Add(temp);
                            } // while
                            connection.Close();
                        }
                    }
                }
                ViewBag.Alergias = alergias;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Registrar(AlergiaModel alergiaModel)
        {
            if (ModelState.IsValid)
            {
                string conexionString = Configuration["ConnectionStrings:DB_Connection_Turrialba"];
                var connection = new SqlConnection(conexionString);

                string sqlQuery = $"exec sp_insertarAlergia @param_CEDULA_PACIENTE = '{alergiaModel.CedulaPaciente}', " +
                    $"@param_NOMBRE_ALERGIA = '{alergiaModel.NombreAlergia}', " +
                    $"@param_DESCRIPCION = '{alergiaModel.Descripcion}', " +
                    $"@param_FECHA_DIAGNOSTICO = '{alergiaModel.FechaDiagnostico}', @param_MEDICAMENTOS = '{alergiaModel.Medicamentos}'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    command.ExecuteReader();
                    connection.Close();
                };
            }
            return View("Index");
        }
        [HttpPost]
        public IActionResult EliminarAlergia(AlergiaModel alergiaModel)
        {
            string conexionString = Configuration["ConnectionStrings:DB_Connection_Turrialba"];
            var connection = new SqlConnection(conexionString);

            string sqlQuery = $"exec sp_eliminarAlergia @param_ID = '{alergiaModel.IDAlergia}'";
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();
                command.ExecuteReader();
                connection.Close();
            };
            return View("Index");
        }
        [HttpPost]
        public IActionResult Update(AlergiaModel alergiaModel)
        {
            if (ModelState.IsValid)
            {
                string conexionString = Configuration["ConnectionStrings:DB_Connection_Turrialba"];
                var connection = new SqlConnection(conexionString);

                string sqlQuery = $"exec sp_updateAlergia @param_ID = '{alergiaModel.IDAlergia}', " +
                    $"@param_NOMBRE_ALERGIA = '{alergiaModel.NombreAlergia}', " +
                    $"@param_DESCRIPCION = '{alergiaModel.Descripcion}', " +
                    $"@param_FECHA_DIAGNOSTICO = '{alergiaModel.FechaDiagnostico}', @param_MEDICAMENTOS = '{alergiaModel.Medicamentos}'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    command.ExecuteReader();
                    connection.Close();
                };
            }
            return View("Index");
        }
        [HttpPost]
        public String ActualizarAlergia(AlergiaModel alergiaModel)
        {
            string conexionString = Configuration["ConnectionStrings:DB_Connection_Turrialba"];
            var connection = new SqlConnection(conexionString);

            string sqlQuery = $"exec sp_getAlergia @param_ID = '{alergiaModel.IDAlergia}'";
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();
                SqlDataReader vacunaReader = command.ExecuteReader();
                AlergiaModel temp = new AlergiaModel();
                if (vacunaReader.Read())
                {
                    temp.IDAlergia = Int32.Parse(vacunaReader["ID"].ToString());
                    temp.CedulaPaciente = vacunaReader["CEDULA_PACIENTE"].ToString();
                    temp.NombreAlergia = vacunaReader["NOMBRE_ALERGIA"].ToString();
                    temp.Descripcion = vacunaReader["DESCRIPCION"].ToString();
                    temp.FechaDiagnostico = vacunaReader["FECHA_DIAGNOSTICO"].ToString();
                    temp.Medicamentos = vacunaReader["MEDICAMENTOS"].ToString();
                }
                connection.Close();
                return "<div class='row'>"
                    + "<div class='col-md-3' id='divid'>"
            + "<label for= 'inputEmail4' class='form-label'>ID</label>"
            + "<input asp-for='IDAlergia' type= 'text' class='form-control text-center' value='" + temp.IDAlergia + "' readonly='readonly' required id = 'IDAlergia' name='IDAlergia' placeholder='ID de la alergia'>"
            + "</div>"
            + "<div class='col-md-3'>"
                + "<label for='inputEmail4' class='form-label'>Cedula Paciente</label>"
                + "<input asp-for='CedulaPaciente' type= 'text' class='form-control text-center' value='" + temp.CedulaPaciente + "' readonly='readonly' required id = 'CedulaPaciente' name='CedulaPaciente' placeholder='Cedula del paciente'>"
            + "</div>"
            + "<div class='col-md-3'>"
                + "<label for='inputEmail4' class='form-label'>Nombre</label>"
                + "<input asp-for='NombreAlergia' type='text' class='form-control text-center' value='" + temp.NombreAlergia + "' required id = 'NombreAlergia' name='NombreAlergia' placeholder='Nombre de la alergia'>"
            + "</div>"
            + "<div class='col-md-3'>"
                + "<label for='inputEmail4' class='form-label'>Descripcion</label>"
                + "<input asp-for='Descripcion' type='text' class='form-control text-center' value='" + temp.Descripcion + "' required id = 'Descripcion' name='Descripcion' placeholder='Descripcion de la alergia'>"
            + "</div>"
            + "</div>"
            + "<div class='row'>"
            + "<div class='form-group col-md-6'>"
                + "<label for='inputEmail4' class='form-label'>Formato de la fecha: yyyy-mm-dd</label>"
                + "<input asp-for='FechaDiagnostico' type='text' class='form-control text-center' value='" + temp.FechaDiagnostico + "' required id = 'FechaDiagnostico' name='FechaDiagnostico' placeholder='Fecha Diagnostico de alergia'>"
            + "</div>"
            + "<div class='form-group col-md-6'>"
                + "<label for='inputEmail4' class='form-label'>Medicamentos</label>"
                + "<input asp-for='Medicamentos' type='text' class='form-control text-center' value='" + temp.Medicamentos + "' required id = 'Medicamentos' name='Medicamentos' placeholder='Medicamentos para la alergia'>"
            + "</div>"
            + "</div>"
            + "<div class='col-12 w-auto mx-3 text-center my-3'>"
            + "<button type = 'submit' class='btn btn-primary text-center mt-3'>Registrar</button>"
            + "</div>";
            };
        }
    }
}
