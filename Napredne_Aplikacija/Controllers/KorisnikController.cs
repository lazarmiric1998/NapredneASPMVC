using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Napredne_Aplikacija.Models;
using Napredne_Aplikacija.Security;

namespace Napredne_Aplikacija.Controllers
{
    
    public class KorisnikController : Controller
    {

        public KorisnikController(UserManager<ExtendedKorisnik> userManager,
            SignInManager<ExtendedKorisnik> signInManager, KorisnikContext context,
             IDataProtectionProvider dataProtectionProvider, EnkripcijaPodataka enkripcijaPodataka)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
            this.dataProtectionProvider = dataProtectionProvider;
            this.enkripcijaPodataka = enkripcijaPodataka;
            protector = dataProtectionProvider.CreateProtector(enkripcijaPodataka.KorinsikPin);
        }
        private readonly IDataProtector protector;
        private readonly SignInManager<ExtendedKorisnik> signInManager;
        private readonly UserManager<ExtendedKorisnik> userManager;
        private readonly KorisnikContext _context;
        private readonly IDataProtectionProvider dataProtectionProvider;
        private readonly EnkripcijaPodataka enkripcijaPodataka;

        public async Task<IActionResult> Index()
        {
            return View(await _context.Korisnici.ToListAsync());
        }

        // GET: Korisnik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Korisnici
                .FirstOrDefaultAsync(m => m.KorisnikID == id);
            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }
        // GET: Korisnik/Creat
        [HttpGet]
        public async Task<IActionResult> LoginKorisnika(string returnUrl)
        {
            LoginModel model = new LoginModel
            {
                ReturnUrl = returnUrl,
                ExternalLogin = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("LoginKorisnika", "Korisnik");
        }


        // POST: Korisnik/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginKorisnika(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var reasult = await signInManager.PasswordSignInAsync(loginModel.KorisnickoIme, loginModel.Sifra, loginModel.rememberMe, false);
                if (reasult.Succeeded)
                {

                    return RedirectToAction("index", "korisnik");
                }

                ModelState.AddModelError("", "Neuspesna prijava");

            }
            return View(loginModel);
        }



        [HttpGet]

        public IActionResult RegistracijaKorisnika()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> RegistracijaKorisnika([Bind("KorisnikID,Ime,Prezime,Email,Pol,KorisnickoIme,Sifra,PotvrdiSifru,Grad,Pin")] Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                // _context.Add(korisnik);
                // await _context.SaveChangesAsync();

                var user = new ExtendedKorisnik { UserName = korisnik.KorisnickoIme, Email = korisnik.Email, Pin = protector.Protect(korisnik.Pin), City = korisnik.Grad };
                var reasult = await userManager.CreateAsync(user, korisnik.Sifra);
                //dekripcija stringa
                string dekriptovanPin = protector.Unprotect(user.Pin);
                //ViewBag.SuccessMessage = "Uspesna registracija!";
                if (reasult.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "korisnik");
                }

                foreach (var error in reasult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                //return RedirectToAction(nameof(LoginKorisnika));

            }

            // ModelState.Clear();
            return View(korisnik);
        }

        // GET: Korisnik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Korisnici.FindAsync(id);
            if (korisnik == null)
            {
                return NotFound();
            }
            return View(korisnik);
        }

        // POST: Korisnik/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KorisnikID,Ime,Prezime,Email,Pol,KorisnickoIme,Sifra,PotvrdiSifru,Grad,Pin")] Korisnik korisnik)
        {
            if (id != korisnik.KorisnikID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(korisnik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KorisnikExists(korisnik.KorisnikID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(korisnik);
        }

        // GET: Korisnik/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Korisnici
                .FirstOrDefaultAsync(m => m.KorisnikID == id);
            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        // POST: Korisnik/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var korisnik = await _context.Korisnici.FindAsync(id);
            _context.Korisnici.Remove(korisnik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KorisnikExists(int id)
        {
            return _context.Korisnici.Any(e => e.KorisnikID == id);
        }

        
        [HttpGet]
        public IActionResult DodajKorisnika()
        {
            return View();
        }


        
        [HttpPost]
        public async Task<IActionResult> DodajKorisnika([Bind("KorisnikID,Ime,Prezime,Email,Pol,KorisnickoIme,Sifra,PotvrdiSifru,Grad,Pin")] Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                _context.AddAsync(korisnik);
                await _context.SaveChangesAsync();
            }



            return View(korisnik);
        }

        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Korisnik", new { ReturnUrl = returnUrl });

            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            //ako je null incijalizuem sa rut url
            returnUrl = returnUrl ?? Url.Content("~/");
            LoginModel model = new LoginModel
            {
                ReturnUrl = returnUrl,
                ExternalLogin = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            if (remoteError != null)
            {
                ModelState.AddModelError("", $"Error provajder:{remoteError}");
                return View("LoginKorisnika", model);
            }

            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError("", "Google error");
                return View("LoginKorisnika", model);
            }
            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (email != null)
                {
                    var user = await userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new ExtendedKorisnik
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                        };
                        await userManager.CreateAsync(user);
                    }
                        await userManager.AddLoginAsync(user, info);
                        await signInManager.SignInAsync(user, isPersistent: false);

                        return LocalRedirect(returnUrl);
                    }
                    ViewBag.ErrorTitle = $"Email error :{info.LoginProvider}";
                    ViewBag.ErrorMessage = "Greska!!!";
                    return View("LoginKorisnika", model);
                }
            }
        }

    
}
