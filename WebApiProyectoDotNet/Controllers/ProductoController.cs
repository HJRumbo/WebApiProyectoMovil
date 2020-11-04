using System.Collections.Generic;
using System.Linq;
using Datos;
using Entity;
using Logica;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiProyectoDotNet.Models;

namespace WebApiProyectoDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController: ControllerBase
    {
        private readonly ProductoService _service;
        public ProductoController(TiendaContext context)
        {
            _service=new ProductoService(context);
        }
        [HttpGet]
        public IEnumerable<ProductoViewModel> Gets(){
            var productos=_service.ConsultarProductos().Select(p=>new ProductoViewModel(p));
            return productos;
        }

        [HttpPost]
        public ActionResult<ProductoViewModel> Post(ProductoInputModel productoInput){
            Producto producto=MapearProducto(productoInput);
            Producto productoOk;
            var response=_service.GuardarProdcuto(producto);
            if(response.Error){
                ModelState.AddModelError("Guardar producto", response.Mensaje);
                var problemDetails=new ValidationProblemDetails(ModelState){
                    Status = StatusCodes.Status400BadRequest,
                };
                return BadRequest(problemDetails);
            }
            productoOk=response.Producto;
            return Ok(productoOk);
        }

     
        [HttpGet("{codigo}")]
        public ActionResult<ProductoViewModel> Get(int codigo){
            var producto=_service.BuscarXCodigo(codigo);
            if(producto==null)return NotFound();
            var productoViewModel=new ProductoViewModel(producto);
            return productoViewModel;
        }
        [HttpDelete("{codigo}")]
        public ActionResult<string> Delete(int codigo){
            string mensaje=_service.Eliminar(codigo);
            return Ok(mensaje);
        }
        [HttpPut("{codigo}")]
        public ActionResult<string> Put(int codigo, ProductoUpdateModel productoUpdate){
            var id=_service.BuscarXCodigo(codigo);
            if(id==null){
                return BadRequest("No encontrado");
            }
            Producto producto=MapearProductoUpdate(productoUpdate,codigo);
            var mensaje=_service.Modificar(producto);
            return Ok(mensaje);
        }
        [NonAction]
        public Producto MapearProductoUpdate(ProductoUpdateModel productoUpdate,int codigo){
            var producto=new Producto{
                Codigo=codigo,
                Nombre=productoUpdate.Nombre,
                Descripcion=productoUpdate.Descripcion,
                Talla=productoUpdate.Talla,
                Precio=productoUpdate.Precio,
                Imagen=productoUpdate.Imagen
            };
            return producto;
        }
        [NonAction]
           public Producto MapearProducto(ProductoInputModel productoInput){
            var producto=new Producto{
                Codigo=productoInput.Codigo,
                Nombre=productoInput.Nombre,
                Descripcion=productoInput.Descripcion,
                Talla=productoInput.Talla,
                Precio=productoInput.Precio,
                Imagen=productoInput.Imagen
            };
            return producto;
        }
    }
}