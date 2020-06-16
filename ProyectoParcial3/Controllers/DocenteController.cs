using System.Web.Mvc;

namespace ProyectoParcial3.Controllers
{
    [Authorize(Roles = CustomRoles.AdministratorOrTeacher)]

    public class DocenteController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}