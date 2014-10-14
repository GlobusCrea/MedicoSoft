using DAL;
using MVCMedicoSoft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCMedicoSoft.Controllers
{
    public class UsersController : Controller
    {
        
        public RedirectToRouteResult Index()
        {
            return RedirectToRoute(new { Controller = "Login", Action = "LoginForm" });
        }

        public ActionResult usersList()
        {
            if((MySession.Login == null) && (MySession.Login != null))
            {
                return RedirectToRoute(new { Controller = "Login", Action = "LoginForm" });
            }
            else
            {
                List<Utilisateur> usersList = Utilisateur.getInfos();
                return View(usersList);
            }
        }
	}
}