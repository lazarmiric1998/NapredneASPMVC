using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Napredne_Aplikacija.Models;
using Napredne_Aplikacija.Security;

namespace Napredne_Aplikacija.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataProtectionProvider dataProtectionProvider;
        private readonly EnkripcijaPodataka enkripcijaPodataka;
        private readonly IDataProtector protector;

        public HomeController(ILogger<HomeController> logger ,
            IDataProtectionProvider dataProtectionProvider, EnkripcijaPodataka enkripcijaPodataka)
        {
            _logger = logger;
            this.dataProtectionProvider = dataProtectionProvider;
            this.enkripcijaPodataka = enkripcijaPodataka;
            protector = dataProtectionProvider.CreateProtector(enkripcijaPodataka.KorinsikPin);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
