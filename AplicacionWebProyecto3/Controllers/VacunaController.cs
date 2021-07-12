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
        public IActionResult VerVacunas()
        {
            List<VacunaModel> vacunas = new List<VacunaModel>();
            if (ModelState.IsValid)
            {
                string connectionString = Configuration["ConnectionStrings:DB_Connection_Turrialba"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = $"exec sp_getAllVacunas";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        SqlDataReader productosReader = command.ExecuteReader();
                        while (productosReader.Read())
                        {
                            VacunaModel temp = new VacunaModel();
                            temp.IDVacuna = Int32.Parse(productosReader["ID"].ToString());
                            temp.CedulaPaciente = productosReader["CEDULA_PACIENTE"].ToString();
                            temp.NombreVacuna = productosReader["NOMBRE_VACUNA"].ToString();
                            temp.Descripcion = productosReader["DESCRIPCION"].ToString();
                            temp.FechaAplicacion = productosReader["FECHA_APLICACION"].ToString();
                            temp.FechaProxima = productosReader["FECHA_PROXIMA"].ToString();
                            vacunas.Add(temp);
                        } // while
                        connection.Close();
                    }
                }
            }
            ViewBag.Vacunas = vacunas;
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
        [HttpPost]
        public IActionResult EliminarVacuna(VacunaModel vacunaModel)
        {
            Console.WriteLine(vacunaModel.IDVacuna);
            string conexionString = Configuration["ConnectionStrings:DB_Connection_Turrialba"];
            var connection = new SqlConnection(conexionString);

            string sqlQuery = $"exec sp_eliminarVacuna @param_ID = '{vacunaModel.IDVacuna}'";
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
        public IActionResult ActualizarVacuna(VacunaModel vacunaModel)
        {
            Console.WriteLine(vacunaModel.IDVacuna);
            string conexionString = Configuration["ConnectionStrings:DB_Connection_Turrialba"];
            var connection = new SqlConnection(conexionString);

            string sqlQuery = $"exec sp_eliminarVacuna @param_ID = '{vacunaModel.IDVacuna}'";
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();
                command.ExecuteReader();
                connection.Close();
            };
            return View("Index");
        }
    }
}
