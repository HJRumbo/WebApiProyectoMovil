using System.ComponentModel.DataAnnotations;
using Entity;

namespace WebApiProyectoDotNet.Models
{
    public class UsuarioViewModel
    {
        [Required( ErrorMessage="El correo es requerido")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
        public string Correo { get; set; }
        [Required( ErrorMessage="La contraseña es requerida")]
        public string Contraseña { get; set; }
        public UsuarioViewModel()
        {

        }
        public UsuarioViewModel(Usuario usuario)
        {
            Correo = usuario.Correo;
        }
    }
}