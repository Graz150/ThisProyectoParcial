﻿using System.Web.Mvc;

namespace ProyectoParcial3.Controllers
{

    [Authorize(Roles = CustomRoles.AdministratorOrStudent)]
    

    public class EstudianteController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}