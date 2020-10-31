using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Usuario
    {
        [Key]
        public string Correo {get;set;}
        public string Contraseña {get;set;}
    }
}