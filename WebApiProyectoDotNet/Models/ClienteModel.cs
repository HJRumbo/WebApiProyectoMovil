using System.ComponentModel.DataAnnotations;
using Entity;

namespace WebApiProyectoDotNet.Models
{
    public class ClienteInputModel
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es requerido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El tipo de documento es requerido")]
        [TipoDocumentoValidacion( ErrorMessage="El tipo de documento debe ser CC, CE o Pasaporte")]
        public string TipoDocumento { get; set; }
        [Required(ErrorMessage = "La identificacion es requerida")]
        public string Identificacion { get; set; }
        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        public string FechaNacimiento { get; set; }
        [Required(ErrorMessage = "El género es requerido")]
        [GeneroValidacion( ErrorMessage="El género debe ser F o M")]
        public string Genero { get; set; }
        [Required( ErrorMessage="El correo es requerido")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
        public string Correo { get; set; }
        [Required( ErrorMessage="La contraseña es requerida")]
        public string Contraseña { get; set; }
    }

    public class ClienteViewModel : ClienteInputModel
    {
        public ClienteViewModel()
        {

        }
        public ClienteViewModel(Cliente cliente)
        {
            Nombre = cliente.Nombre;
            Apellido = cliente.Apellido;
            Identificacion = cliente.Identificacion;
            TipoDocumento = cliente.TipoDocumento;
            FechaNacimiento = cliente.FechaNacimiento;
            Correo = cliente.Correo;
            Genero = cliente.Genero;
        }
    }

    public class ClienteUpdateModel
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es requerido")]
        public string Apellido { get; set; }
        
        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        public string FechaNacimiento { get; set; }
        [Required(ErrorMessage = "El género es requerido")]
        [GeneroValidacion( ErrorMessage="El género debe ser F o M")]
        public string Genero { get; set; }
        [Required( ErrorMessage="La contraseña es requerida")]
        public string Contraseña { get; set; }
    }

    public class GeneroValidacion : ValidationAttribute{
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((value.ToString().ToUpper() == "M") || (value.ToString().ToUpper() == "F"))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }
    }

    public class TipoDocumentoValidacion : ValidationAttribute{
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((value.ToString().ToUpper() == "CC") || (value.ToString().ToUpper() == "CE") 
            || (value.ToString().ToUpper() == "Pasaporte"))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }
    }
}