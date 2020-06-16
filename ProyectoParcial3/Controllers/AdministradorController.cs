using Microsoft.AspNet.Identity;
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

    [Authorize(Roles = "Administrador")]



    public class AdministradorController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationRoleManager _roleManager;
        private ApplicationUserManager _userManager;

        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private readonly string adminRole = "1";
          private readonly string docenteRole = "2";
        private readonly string alumnoRole = "3";
 
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

        /*Esta parte de consulta de rol , iba a ser usada para la creacion de un nuevo administrador, porque como podemos observar
        no esta el if que me excluye el rol administrador de la consulta
         */
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
            //El valor del viewbag altera el ordenamiento sea ASC o DESC en este caso , es descendente
            ViewBag.NombreSortParm = sortOrder == "Nombre" ? "Nombres_desc" : "Nombre";
            ViewBag.ApellidosSortParm = sortOrder == "Apellido" ? "Apellidos_desc" : "Apellido";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.UserNameSortParm = sortOrder == "Username" ? "Username_desc" : "Username";
                                                                                                      

            //Aqui le pedimos que solo traiga aquellos que sean admins de la base de datos
            UserViewModel adminUsers = new UserViewModel(db, adminRole , sortOrder, searchString, page);
            
           

            /*Diccionario usado para traernos los roles de cada uno de los usuarios (en este caso solo el de administrador porque solo estamos trayendo administradores
            y mostrarlos acordes al ID de usuario*/ 

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




         //Esta seria el index de la lista de los alumnos
        public ActionResult ListaEstudiantes(string sortOrder, string currentFilter, string searchString, int? page)
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
         
        //Aqui decimos que de la bd nos traiga solo los alumnos 

            UserViewModel alumnoUsers = new UserViewModel(db, alumnoRole, sortOrder, searchString, page);

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
            alumnoUsers.RolesByUsuario = RolesByUsuario;

            return View(alumnoUsers);
        }


        public ActionResult ListaDocentes(string sortOrder, string currentFilter, string searchString, int? page)
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

            //Aqui decimos que de la bd nos traiga solo los alumnos 

            UserViewModel docenteUsers = new UserViewModel(db, docenteRole, sortOrder, searchString, page);

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
            docenteUsers.RolesByUsuario = RolesByUsuario;

            return View(docenteUsers);
        }

        /*
           
        //Intento de llamarlo por id de usuario
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
         
        /*Metodo para eliminar el administrador, funciona, pero no hace logout cuando nos eliminamos a nosotros mismos,
         de hecho, eso no deberia ni de poder ocurrir.

        ToDo^
         
         */

        //Edit que no funciona, deberia, pero no
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel applicationUser)
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