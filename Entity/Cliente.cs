using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Cliente
    {
        [Key]
        public string Identificacion {get;set;}
        public string Nombre {get;set;}
        public string Apellido {get;set;}
        public string TipoDocumento {get;set;}
        public string FechaNacimiento {get;set;}
        public string Genero {get;set;}

        [ForeignKey("Usuario")]
        public string Correo {get;set;}
    }
}
