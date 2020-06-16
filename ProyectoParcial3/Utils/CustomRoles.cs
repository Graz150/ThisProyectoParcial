 

public static class CustomRoles
{

    public const string Administrador = "Administrador";
    public const string Docente = "Docente";
    public const string Estudiante = "Alumno";
    public const string AdministratorOrTeacher = Administrador + "," + Docente;
    public const string AdministratorOrStudent = Administrador + "," + Estudiante;
}