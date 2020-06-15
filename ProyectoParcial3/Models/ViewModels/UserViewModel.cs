using Microsoft.AspNet.Identity;
using PagedList;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;


namespace ProyectoParcial3.Models.ViewModels
{
    public class UserViewModel
    {
        public string UserId{ get; set; }
        public string Correo { get; set; }
        public IEnumerable<ApplicationUser> ListaUsuarios { get; set; }
        public IPagedList<ApplicationUser> ListaUsuariosP { get; set; }
        public ApplicationUser Usuario { get; set; }
        public string Message { get; set; }
        
        // Trae todos los usuarios que correspondan a un rol
       

        public Dictionary<string, string> RolesByUsuario { get; set; } = new Dictionary<string, string>();

        public UserViewModel(ApplicationDbContext db, string role)
        {
            this.ListaUsuarios = getUsersByRoleId(role, db);
        }
        // 
        // crea y lista  los usuarios
    
        public UserViewModel(ApplicationDbContext db)
        {
            //Para crear y listar todos los usuarios
            Message = null;
            ListaUsuarios = db.Users.ToList();
        }
     
        public UserViewModel()
        {
            
        }


 
        // trae a los usuarios cuyo rol sea especificado
    
        private IEnumerable<ApplicationUser> getUsersByRoleId(string role, ApplicationDbContext db)
        {
            var usuarios = db.Users.Where(user => user.Roles.All(urm => urm.RoleId == role));
            return usuarios;
        }
     
        // Ordena a los usuarios que tengan cierto rol  
        public UserViewModel(ApplicationDbContext db, string role, string sortOrder, string searchString, int? page)
        {
            this.ListaUsuariosP = getUsersByRoleId(role, db, sortOrder, searchString, page); 
        }

     
        //Lista de usuarios paginados
        private IPagedList<ApplicationUser> getUsersByRoleId(string role, ApplicationDbContext db, string sortOrder, string searchString, int? page)
        {
            var usuarios = db.Users.Where(user => user.Roles.All(urm => urm.RoleId == role));


            if (!string.IsNullOrEmpty(searchString))
            {
                usuarios = usuarios.Where(s => s.Nombres.Contains(searchString) || s.Apellidos.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Nombre":
                    usuarios = usuarios.OrderBy(s => s.Nombres);
                    break;
                case "Nombres_desc":
                    usuarios = usuarios.OrderByDescending(s => s.Nombres);
                    break;
                case "Apellido":
                    usuarios = usuarios.OrderBy(s => s.Apellidos);
                    break;
                case "Apellidos_desc":
                    usuarios = usuarios.OrderByDescending(s => s.Apellidos);
                    break;
                case "Email":
                    usuarios = usuarios.OrderBy(s => s.Email);
                    break;
                case "Email_desc":
                    usuarios = usuarios.OrderByDescending(s => s.Email);
                    break;
                case "Username":
                    usuarios = usuarios.OrderBy(s => s.UserName);
                    break;
                case "Username_desc":
                    usuarios = usuarios.OrderByDescending(s => s.UserName);
                    break;
                default:
                    usuarios = usuarios.OrderBy(s => s.Id);
                    break;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return usuarios.ToPagedList(pageNumber, pageSize);
        }
    }
}