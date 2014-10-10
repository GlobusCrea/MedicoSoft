using DAL;
using MVCMedicoSoft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCMedicoSoft.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]        
        public ActionResult LoginForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginForm(string txtLogin, string txtPass)
        {
            Utilisateur u = Utilisateur.AuthentifieMoi(txtLogin, txtPass);
            if(u == null)
            {
                ViewBag.Error = "Try again!";
                return View();
            }
            else
            {
                // 1. Stocker en session le login utilisateur
                MySession.Login = u.Login;
                MySession.User = u;
                // 2. Rediriger vers Home/Index (Page d'accueil)
                return RedirectToRoute(new { Controller="Home", action = "Index" });
            }
        }

        [HttpGet]
        public RedirectToRouteResult LogOut()
        {
            MySession.Login = null;
            Session.Abandon();
            return RedirectToRoute (new { controller = "Home", action = "Index" });
        }
	}
}