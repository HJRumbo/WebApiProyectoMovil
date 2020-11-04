using System.ComponentModel.DataAnnotations;
using Entity;

namespace WebApiProyectoDotNet.Models {
    public class ProductoInputModel {
        [Required(ErrorMessage = "El codigo es requerido")]
        public int Codigo { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripcion es requerido")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La talla es requerido")]
        public string Talla { get; set; }
        [Required(ErrorMessage = "El precio es requerido")]
        public double Precio { get; set; }
        [Required(ErrorMessage = "La imagen es requerida")]
        public string Imagen { get; set; }
    }

    public class ProductoViewModel: ProductoInputModel{
        public ProductoViewModel()
        {
            
        }
        public ProductoViewModel(Producto producto)
        {
            Codigo=producto.Codigo;
            Nombre=producto.Nombre;
            Descripcion=producto.Descripcion;
            Talla=producto.Talla;
            Precio=producto.Precio;
            Imagen=producto.Imagen;
        }
    }

    public class ProductoUpdateModel{
         [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripcion es requerido")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La talla es requerido")]
        public string Talla { get; set; }
        [Required(ErrorMessage = "El precio es requerido")]
        public double Precio { get; set; }
        [Required(ErrorMessage = "La imagen es requerida")]
        public string Imagen { get; set; }

    }
}