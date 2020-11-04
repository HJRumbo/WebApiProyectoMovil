using System;
using System.Collections.Generic;
using System.Linq;
using Datos;
using Entity;

namespace Logica
{
    public class ProductoService
    {
        private readonly TiendaContext _context;
        public ProductoService(TiendaContext context)
        {
            _context=context;
        }
        public GuardarProdcutoResponse GuardarProdcuto(Producto producto){
            try
            {
                var productoBuscado=_context.Productos.Find(producto.Codigo);
                if(productoBuscado!=null){
                    return new GuardarProdcutoResponse($"El producto con el codigo {producto.Codigo}, ya esta registrado");
                }
                _context.Productos.Add(producto);
                _context.SaveChanges();
                return new GuardarProdcutoResponse(producto);
            }
            catch (Exception e)
            {
                return new GuardarProdcutoResponse($"Error de la Aplicacion: {e.Message}");
            }
        }

        public List<Producto> ConsultarProductos(){
            List<Producto> productos=_context.Productos.ToList();
            return productos;
        }

        public Producto BuscarXCodigo(int codigo){
            Producto producto=_context.Productos.Find(codigo);
            return producto;
        }

        public string Eliminar(int codigo){
            try
            {
                var producto=BuscarXCodigo(codigo);
                if(producto!=null){
                    _context.Productos.Remove(producto);
                    _context.SaveChanges();
                    return producto.Nombre+" fue eliminado correctamente.";
                }else{
                    return "Lo sentimos, el producto con el codigo "+producto.Codigo+", no esta registrado.";
                }
            }
            catch (Exception e) 
            {
                
                return $"Error de la aplicación {e.Message}";
            }
        }

        public string Modificar(Producto productoNuevo){
            try
            {
                var productoViejo=_context.Productos.Find(productoNuevo.Codigo);
                if(productoViejo!=null){
                    productoViejo.Codigo=productoNuevo.Codigo;
                    productoViejo.Descripcion=productoNuevo.Descripcion;
                    productoViejo.Nombre=productoNuevo.Nombre;
                    productoViejo.Precio=productoNuevo.Precio;
                    productoViejo.Talla=productoNuevo.Talla;
                    productoViejo.Imagen=productoNuevo.Imagen;
                    _context.Update(productoViejo);
                    _context.SaveChanges();
                    return ($"El registro {productoNuevo.Nombre} se ha modificado satisfactoriamente.");
                }else{
                    return ($"El registro {productoNuevo.Nombre} no fué encontrado.");
                }
            }
            catch (System.Exception e)
            {
                
                return ($"Error de la aplicación: {e.Message}.");
            }
        }

    }

    public class GuardarProdcutoResponse{
        public GuardarProdcutoResponse(Producto producto)
        {
            Error=false;
            Producto=producto;   
        }
        public GuardarProdcutoResponse(string mensaje)
        {
            Error=true;
            Mensaje=mensaje;
        }
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public Producto Producto { get; set; }
    }
}