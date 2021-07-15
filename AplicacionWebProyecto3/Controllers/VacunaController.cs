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
        public IActionResult Update(VacunaModel vacunaModel)
        {
            if (ModelState.IsValid)
            {
                string conexionString = Configuration["ConnectionStrings:DB_Connection_Turrialba"];
                var connection = new SqlConnection(conexionString);

                string sqlQuery = $"exec sp_updateVacuna @param_ID = '{vacunaModel.IDVacuna}', " +
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
        public String ActualizarVacuna(VacunaModel vacunaModel)
        {
            string conexionString = Configuration["ConnectionStrings:DB_Connection_Turrialba"];
            var connection = new SqlConnection(conexionString);

            string sqlQuery = $"exec sp_getVacuna @param_ID = '{vacunaModel.IDVacuna}'";
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();
                SqlDataReader vacunaReader = command.ExecuteReader();
                VacunaModel temp = new VacunaModel();
                if (vacunaReader.Read())
                {
                    temp.IDVacuna=Int32.Parse(vacunaReader["ID"].ToString());
                    temp.CedulaPaciente = vacunaReader["CEDULA_PACIENTE"].ToString();
                    temp.NombreVacuna = vacunaReader["NOMBRE_VACUNA"].ToString();
                    temp.Descripcion = vacunaReader["DESCRIPCION"].ToString();
                    temp.FechaAplicacion = vacunaReader["FECHA_APLICACION"].ToString();
                    temp.FechaProxima = vacunaReader["FECHA_PROXIMA"].ToString();
                }
                connection.Close();
                return "<div class='row'>"
                    +"<div class='col-md-3' id='divid'>"
            + "<label for= 'inputEmail4' class='form-label'>Cedula Paciente</label>"
            + "<input asp-for='IDVacuna' type= 'text' class='form-control text-center' value='" + temp.IDVacuna + "' readonly='readonly' required id = 'IDVacuna' name='IDVacuna' placeholder='ID de la Vacuna'>"
            + "</div>"
            + "<div class='col-md-3'>"
                + "<label for='inputEmail4' class='form-label'>Cedula Paciente</label>"
                + "<input asp-for='CedulaPaciente' type= 'text' class='form-control text-center' value='" + temp.CedulaPaciente + "' readonly='readonly' required id = 'CedulaPaciente' name='CedulaPaciente' placeholder='Cedula del paciente'>"
            + "</div>"
            + "<div class='col-md-3'>"
                + "<label for='inputEmail4' class='form-label'>Nombre</label>"
                + "<input asp-for='NombreVacuna' type='text' class='form-control text-center' value='" + temp.NombreVacuna + "' required id = 'NombreVacuna' name='NombreVacuna' placeholder='Nombre de la vacuna'>"
            + "</div>"
            + "<div class='col-md-3'>"
                + "<label for='inputEmail4' class='form-label'>Descripcion</label>"
                + "<input asp-for='Descripcion' type='text' class='form-control text-center' value='" + temp.Descripcion + "' required id = 'Descripcion' name='Descripcion' placeholder='Descripcion de la vacuna'>"
            + "</div>"
            + "</div>"
            + "<div class='row'>"
            + "<div class='form-group col-md-6'>"
                + "<label for='inputEmail4' class='form-label'>Formato de la fecha: yyyy-mm-dd</label>"
                + "<input asp-for='FechaAplicacion' type='text' class='form-control text-center' value='" + temp.FechaAplicacion + "' required id = 'FechaAplicacion' name='FechaAplicacion' placeholder='Fecha Aplicacion de la vacuna'>"
            + "</div>"
            + "<div class='form-group col-md-6'>"
                + "<label for='inputEmail4' class='form-label'>Formato de la fecha: yyyy-mm-dd</label>"
                + "<input asp-for='FechaProxima' type='text' class='form-control text-center' value='" + temp.FechaProxima + "' required id = 'FechaProxima' name='FechaProxima' placeholder='Fecha Proxima de la vacuna'>"
            + "</div>"
            + "</div>"
            + "<div class='col-12 w-auto mx-3 text-center my-3'>"
            + "<button type = 'submit' class='btn btn-primary text-center mt-3'>Registrar</button>"
            + "</div>";
            };
        }
    }
}
