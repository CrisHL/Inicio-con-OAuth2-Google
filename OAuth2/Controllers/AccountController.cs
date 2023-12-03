using OAuth2.Services;
using OAuth2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OAuth2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService = null;
        private readonly StateService _stateService = null;
        public AccountController()
        {
            _userService = new UserService();
            _stateService = new StateService();
        }
        [HttpGet]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.IdState = new SelectList(_stateService.GetAllStates(), "IdState", "StateName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserVM userVM)
        {
            ViewBag.IdState = new SelectList(_stateService.GetAllStates(), "IdState", "StateName");

            if (!ModelState.IsValid)
            {
                return View(userVM);
            }

            var existingUser = await _userService.ValidateExistingUser(userVM.UserName);
            if (existingUser)
            {
                ModelState.AddModelError("", "El nombre de usuario ya está en uso.");
                return View(userVM);
            }

            var existingEmail = await _userService.ValidateExistingEmail(userVM.Email);
            if (existingEmail)
            {
                ModelState.AddModelError("", "El correo ya está en uso.");
                return View(userVM);
            }

            if (!await _userService.Saveuser(userVM))
            {
                ModelState.AddModelError("", "No se pudo registrar el usuario.");
                return View(userVM);
            }

            return RedirectToAction("AccountNoVerified");
        }


        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            UserVM model = new UserVM();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([Bind(Include = "UserName, Password")] UserVM model)
        {
            try
            {
                bool validCredentials = await _userService.ValidateCredentialsAsync(model.UserName, model.Password);

                if (!validCredentials)
                {
                    ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos.");
                    return View("Login", model);
                }

                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error durante el inicio de sesión. Por favor, inténtalo de nuevo más tarde.");
                return View("Login", model);
            }
        }
    }
}