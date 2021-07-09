using AplicacionWebProyecto3.Models;
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
    public class DoctorController : Controller
    {

        public IConfiguration Configuration { get; set; }

        public DoctorController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarDoctor(DoctorModel doctorModel)
        {
            if (ModelState.IsValid)
            {
                string conexionString = Configuration["ConnectionStrings:DB_Conection"];
                var connection = new SqlConnection(conexionString);

                string sqlQuery = $"exec sp_insertarDoctor @param_CEDULA = '{doctorModel.Cedula}', " +
                    $"@param_CODIGO_MEDICO = '{doctorModel.CodigoMedico}', " +
                    $"@param_PASS = '{doctorModel.Pass}', " +
                    $"@param_NOMBRE = '{doctorModel.Nombre}', @param_APELLIDOS = '{doctorModel.Apellidos}'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    command.ExecuteReader();
                    connection.Close();
                };
            }
            return View();
        }
    }
}
