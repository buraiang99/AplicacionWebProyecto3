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
    public class VacunaController : Controller
    {
        public IConfiguration Configuration { get; set; }

        public VacunaController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registrar(VacunaModel vacunaModel)
        {
            if (ModelState.IsValid)
            {
                string conexionString = Configuration["ConnectionStrings:DB_Connection_Turrialba"];
                var connection = new SqlConnection(conexionString);

                string sqlQuery = $"exec sp_insertarVacuna @param_CEDULA_PACIENTE = '{vacunaModel.CedulaPaciente}', " +
                    $"@param_NOMBRE_VACUNA = '{vacunaModel.NombreVacuna}', " +
                    $"@param_DESCRIPCION = '{vacunaModel.Descripcion}', " +
                    $"@param_FECHA_APLICACION = '{vacunaModel.FechaAplicacion}', @param_FECHA_PROXIMA = '{vacunaModel.FechaProxima}'";
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
    }
}
