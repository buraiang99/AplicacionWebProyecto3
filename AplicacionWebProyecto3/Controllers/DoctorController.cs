using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegistrarDoctor()
        {
            return View();
        } 

        [HttpPost]
        public IActionResult RegistrarDoctor()
        {

        }
    }
}
