using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Producto
    {
        [Key]
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Talla { get; set; }
        public double Precio { get; set; }
        public string Imagen { get; set; }
    }
}