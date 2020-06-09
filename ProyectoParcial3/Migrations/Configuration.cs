namespace ProyectoParcial3.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProyectoParcial3.Models.ApplicationDbContext>
    {
        public Configuration()
        { 
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        //Metodo seed , llenado predeterminado de la base de datos
        protected override void Seed(ProyectoParcial3.Models.ApplicationDbContext context)
        {

            //Llenado de tabla AspNetRoles
            context.Roles.AddOrUpdate(x => x.Id,
                 new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Id = "1", Name = "Administrador" },
                 new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Id = "2", Name = "Docente" },
                 new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Id = "3", Name = "Alumno" }
                 );


            //Llenado de la tabla ciudad
            context.Ciudad.AddOrUpdate(

              new Models.Ciudad { ID = 1, Nombre = "Bogota" },
              new Models.Ciudad { ID = 2, Nombre = "Bucaramanga" },
              new Models.Ciudad { ID= 3,  Nombre= "Cucuta"},
              new Models.Ciudad { ID= 4, Nombre= "Medellin"},
              new Models.Ciudad { ID= 5, Nombre= "Barrancabermeja"},
              new Models.Ciudad { ID= 6, Nombre= "Villavicencio"}
                );


            //Llenado de pregunta afirmacion



            context.Afirmacions.AddOrUpdate(
                
                
                new Models.Afirmacion { ID= 1 , Nombre= "Cuanto es 2+2"}
                             
                                                               
                
                );





        }
    }
}
 