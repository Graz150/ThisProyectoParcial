﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ProyectoParcial3.Models;
using ProyectoParcial3.Models.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoParcial3.Controllers
{
    public class AdministradorController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationRoleManager _roleManager;
        private ApplicationUserManager _userManager;

        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private readonly string adminRole = "1";
        private readonly string alumnoRole = "3";
        private readonly string docenteRole = "2";
        private IEnumerable<ApplicationUser> getUsersByRoleId(string role)
        {
            var usuarios = db.Users.Where(user => user.Roles.All(urm => urm.RoleId == role));
            return usuarios;
        }
        public ActionResult RedirectToRegister()
        {
            return RedirectToAction("Register", "Account");
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public List<SelectListItem> ConsultarRoles()
        {
            List<SelectListItem> listaRoles = new List<SelectListItem>();
            foreach (var role in db.Roles)
            {
                listaRoles.Add(new SelectListItem() { Value = role.Name, Text = role.Name });
            }
            return listaRoles;
        }


        // GET: Administrador
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //Ordenamiento por columnas
            ViewBag.CurrentSort = sortOrder;
            //Se le asigna al viewbag el valor dependiendo del valor que traiga para alternal el ordenamiento entre ascendente y descendente
            ViewBag.NombreSortParm = sortOrder == "Nombre" ? "Nombres_desc" : "Nombre";
            ViewBag.ApellidosSortParm = sortOrder == "Apellido" ? "Apellidos_desc" : "Apellido";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.UserNameSortParm = sortOrder == "Username" ? "Username_desc" : "Username";
            //condicional para saber si se tiene algun valor de textbox de busqueda

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            //Trae todos los usuarios que sean admin ordenados y paginados
            //Se le pasa la base de datos el tipo de rol de los usuarios que se quiere traer, lo que contiene el cuadro de busqueda, la pagina y el ordenamiento
            UserViewModel adminUsers = new UserViewModel(db, adminRole, sortOrder, searchString, page);

            //-----------------------------------------var roles = UserManager.IsInRole();

            Dictionary<string, string> RolesByUsuario = new Dictionary<string, string>();
            foreach (var i in db.Users.ToList())
            {
                foreach (var j in db.Roles.ToList())
                {
                    if (UserManager.IsInRole(i.Id, j.Name))
                    {
                        RolesByUsuario.Add(i.Id, j.Name);
                    }
                }
            }
            adminUsers.RolesByUsuario = RolesByUsuario;
            //return View(db.Users.ToList());
            return View(adminUsers);

        }





        public ActionResult IndexAlumnos(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NombreSortParm = sortOrder == "Nombres" ? "Nombres_desc" : "Nombres";
            ViewBag.ApellidosSortParm = sortOrder == "Apellidos" ? "Apellidos_desc" : "Apellidos";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.UserNameSortParm = sortOrder == "Username" ? "Username_desc" : "Username";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            UserViewModel alumnoUsers = new UserViewModel(db, alumnoRole, sortOrder, searchString, page);

            return View(alumnoUsers);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public ActionResult Delete(UserViewModel usuario)
        //{
        //var ciudad = db.ciudades.Where(x => x.Nombre.Contains("B"));
        //ApplicationUser applicationUser = db.Users.Find(usuario.Usuario.Id);
        //db.Users.Remove(applicationUser);
        //db.SaveChanges();
        //return RedirectToAction("Index");
        //}
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (UserViewModel applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser.Usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(applicationUser);


        }


        public ActionResult Delete(UserViewModel obj)
        {   
            ApplicationUser applicationUser = db.Users.Where(x => x.Email == obj.Correo).First();
            db.Users.Remove(applicationUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}