using Datos;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logica
{
    public class ClienteService
    {
        private readonly TiendaContext _context;

        public ClienteService(TiendaContext context){
            _context=context;
        }

        public GuardarClienteResponse Guardar(Cliente cliente)
        {
            try
            {
                var clienteBuscado = _context.Clientes.Find(cliente.Identificacion);
                if(clienteBuscado != null ){
                    return new GuardarClienteResponse($"El cliente con la identificacion {cliente.Identificacion}, ya se encuentra registrado"); 
                }
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
                return new GuardarClienteResponse(cliente);
            }
            catch (Exception e)
            {
                return new GuardarClienteResponse($"Error de la Aplicacion: {e.Message}");
            }
            
        }

        public List<Cliente> ConsultarTodos()
        {
            List<Cliente> clientes = _context.Clientes.ToList();
            return clientes;
        }

        public Cliente BuscarXIdentificacion(string identificacion){
            Cliente cliente = _context.Clientes.Find(identificacion);
            return cliente;
        }

        public string Eliminar(string identificacion){
            try{
                var cliente = BuscarXIdentificacion(identificacion);
                if(cliente!=null){
                    _context.Clientes.Remove(cliente);
                    _context.SaveChanges();
                    return cliente.Nombre+" fué eliminado/a correctamente. ";
                }else{
                    return "Lo sentimos, el cliente con la identificación "+identificacion +", no está registrada. ";
                }

            }catch(Exception e){
                return $"Error de la aplicación {e.Message}";
            }
        }

        public string Modificar(Cliente clienteNuevo){
                
            try{
                var clienteViejo = _context.Clientes.Find(clienteNuevo.Identificacion);
                if(clienteViejo!=null){
                    clienteViejo.Identificacion = clienteNuevo.Identificacion;
                    clienteViejo.Nombre = clienteNuevo.Nombre;
                    clienteViejo.Apellido = clienteNuevo.Apellido;
                    clienteViejo.Genero = clienteNuevo.Genero;
                    clienteViejo.FechaNacimiento = clienteNuevo.FechaNacimiento;
                    _context.Update(clienteViejo);
                    _context.SaveChanges();
                    return ($"El registro {clienteNuevo.Nombre} se ha modificado satisfactoriamente.");
                }else{

                    return ($"El registro {clienteNuevo.Nombre} no fué encontrado.");
                }
            }catch(Exception e){
                return ($"Error de la aplicación: {e.Message}.");
            }
        }
    }

    public class GuardarClienteResponse 
    {
        public GuardarClienteResponse(Cliente cliente)
        {
            Error = false;
            Cliente = cliente;
        }
        public GuardarClienteResponse(string mensaje)
        {
            Error = true;
            Mensaje = mensaje;
        }
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public Cliente Cliente { get; set; }
    }
}
