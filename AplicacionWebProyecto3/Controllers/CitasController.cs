using AplicacionWebProyecto3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
