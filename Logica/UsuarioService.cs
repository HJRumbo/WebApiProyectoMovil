using Datos;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logica
{
    public class UsuarioService
    {
        private readonly TiendaContext _context;

        public UsuarioService(TiendaContext context){
            _context=context;
        }

        public GuardarUsuarioResponse Guardar(Usuario usuario)
        {
            try
            {
                var usuarioBuscado = _context.Clientes.Find(usuario.Correo);
                if(usuarioBuscado != null ){
                    return new GuardarUsuarioResponse($"El usuario con el correo {usuario.Correo}, ya se encuentra registrado"); 
                }
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return new GuardarUsuarioResponse(usuario);
            }
            catch (Exception e)
            {
                return new GuardarUsuarioResponse($"Error de la Aplicacion: {e.Message}");
            }
            
        }
        public List<Usuario> ConsultarTodos()
        {
            List<Usuario> usuarios = _context.Usuarios.ToList();
            return usuarios;
        }
        public Usuario BuscarXCorreo(string correo){
            Usuario usuario = _context.Usuarios.Find(correo);
            return usuario;
        }

        public string Eliminar(string correo){
            try{
                var usuario = BuscarXCorreo(correo);
                if(usuario!=null){
                    _context.Usuarios.Remove(usuario);
                    _context.SaveChanges();
                    return usuario.Correo+" fué eliminado/a correctamente. ";
                }else{
                    return "Lo sentimos, el usuario con el correo "+correo +", no está registrada. ";
                }

            }catch(Exception e){
                return $"Error de la aplicación {e.Message}";
            }
        }

        public string Modificar(Usuario usuarioNuevo){
                
            try{
                var usuarioViejo = _context.Usuarios.Find(usuarioNuevo.Correo);
                if(usuarioViejo!=null){
                    usuarioViejo.Correo = usuarioNuevo.Correo;
                    usuarioViejo.Contraseña = usuarioNuevo.Contraseña;
                    _context.Update(usuarioViejo);
                    _context.SaveChanges();
                    return ($"El registro {usuarioNuevo.Correo} se ha modificado satisfactoriamente.");
                }else{

                    return ($"El registro {usuarioNuevo.Correo} no fué encontrado.");
                }
            }catch(Exception e){
                return ($"Error de la aplicación: {e.Message}.");
            }
        }
    }

    public class GuardarUsuarioResponse 
    {
        public GuardarUsuarioResponse(Usuario usuario)
        {
            Error = false;
            Usuario = usuario;
        }
        public GuardarUsuarioResponse(string mensaje)
        {
            Error = true;
            Mensaje = mensaje;
        }
        public bool Error { get; set; }
        public string Mensaje { get; set; }
        public Usuario Usuario { get; set; }
    }
}