using Microsoft.AspNetCore.Mvc;
using Logica;
using System.Collections.Generic;
using Datos;
using WebApiProyectoDotNet.Models;
using System.Linq;
using Entity;
using Microsoft.AspNetCore.Http;

namespace WebApiProyectoDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;
        private readonly UsuarioService _usuarioService;
        public ClienteController(TiendaContext context)
        {
            _clienteService = new ClienteService(context);
            _usuarioService = new UsuarioService(context);
        }

        [HttpGet]
        public IEnumerable<ClienteViewModel> Gets()
        {
            var clientes = _clienteService.ConsultarTodos().Select(c=> new ClienteViewModel(c));
            return clientes;
        }

        
        [HttpPost]
        public ActionResult<ClienteViewModel> Post(ClienteInputModel clienteInput)
        {
            Usuario usuario = MapearUsuario(clienteInput);
            var responseUser = _usuarioService.Guardar(usuario);
            Cliente clienteOk;
            if(responseUser.Error){
                ModelState.AddModelError("Guardar Persona", responseUser.Mensaje);
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                return BadRequest(problemDetails);
            }else{
                Cliente cliente = MapearPersona(clienteInput);
                var response = _clienteService.Guardar(cliente);
                if (response.Error) 
                {
                    ModelState.AddModelError("Guardar Persona", response.Mensaje);
                    var problemDetails = new ValidationProblemDetails(ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest,
                    };
                    return BadRequest(problemDetails);
                }
                clienteOk = response.Cliente;
            }
        
            return Ok(clienteOk);
            
        }
        
        private Usuario MapearUsuario(ClienteInputModel clienteInput){
            var usuario = new Usuario{
                Correo = clienteInput.Correo,
                Contraseña = clienteInput.Contraseña
            };

            return usuario;
        }
        private Cliente MapearPersona(ClienteInputModel clienteInput)
        {
            var cliente = new Cliente
            {
                Identificacion = clienteInput.Identificacion,
                Nombre = clienteInput.Nombre,
                Apellido = clienteInput.Apellido,
                Genero = clienteInput.Genero,
                TipoDocumento = clienteInput.TipoDocumento,
                FechaNacimiento = clienteInput.FechaNacimiento,
                Correo = clienteInput.Correo
            };

            return cliente;
        }

        // GET: api/Persona/5
        [HttpGet("{identificacion}")]
        public ActionResult<ClienteViewModel> Get(string identificacion)
        {
            var cliente = _clienteService.BuscarXIdentificacion(identificacion);
            if (cliente == null) return NotFound();
            var clienteViewModel = new ClienteViewModel(cliente);
            return clienteViewModel;
        }

        // DELETE: api/Persona/5
        /*[HttpDelete("{identificacion}")]
        public ActionResult<string> Delete(string identificacion)
        {
            string mensaje = _personaService.Eliminar(identificacion);
            return Ok(mensaje);
        }*/

        [HttpPut("{identificacion}")]
        public ActionResult<string> Put(string identificacion, ClienteUpdateModel clienteUpdate)
        {
            var id=_clienteService.BuscarXIdentificacion(identificacion);
            if(id==null){
                return BadRequest("No encontrado");
            }
            Cliente cliente = MapearPersonaUpdate(clienteUpdate, identificacion, id.Correo);
            Usuario usuario = MapearUsuarioUpdate(clienteUpdate, id.Correo);
            var mensaje=_clienteService.Modificar(cliente);
            var mensajeUsuario=_usuarioService.Modificar(usuario);
            return Ok(mensaje+", "+mensajeUsuario);
        }

        private Usuario MapearUsuarioUpdate(ClienteUpdateModel clienteUpdate, string correo){
            var usuario = new Usuario{
                Correo = correo,
                Contraseña = clienteUpdate.Contraseña
            };
            return usuario;
        }
        private Cliente MapearPersonaUpdate(ClienteUpdateModel clienteUpdate, string identificacion, string correo){
            
            var cliente = new Cliente
            {
                Identificacion = identificacion,
                Nombre = clienteUpdate.Nombre,
                Apellido = clienteUpdate.Apellido,
                Genero = clienteUpdate.Genero,
                FechaNacimiento = clienteUpdate.FechaNacimiento,
                Correo = correo
            };
            return cliente;
        }
    }
}