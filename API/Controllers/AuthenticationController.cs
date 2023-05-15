using System.Data;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly Aerolinea_DesarrolloContext _context;
        private readonly EmailService _emailService;


        public AuthenticationController(Aerolinea_DesarrolloContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> Login(Usuarios usuarios)
        {
            if (usuarios == null)
            {
                return BadRequest("Datos Invalidos");
            }
            
            var user = _context.Usuarios
                            .Where(x =>
                            x.Username == usuarios.Username
                            && x.Password == usuarios.Password
                            && x.Active == true)
                            .Join(_context.Roles,
                            u => u.idRol,
                            r => r.idRol,
                            (u, r) => new
                            {
                                u.idUsuario,
                                u.Username,
                                u.Password,
                                u.idRol,
                                u.Active,
                                r.Rol
                            })
                            .FirstOrDefault();
            if (user == null)
            {
                return NotFound("Datos Invalidos");
            }
            if (user.Rol.Equals("Usuario"))
            {
                //Esto cambiarlo por la libreria la cual generara los token
                var token = new Tokens()
                {
                    idUsuario = user.idUsuario,
                    Token = Convert.ToString(Guid.NewGuid()),
                    CreateAt = DateTime.Now,
                    Expiration = DateTime.Now.AddMinutes(15),
                    Active = true
                };
                _context.Tokens.Add(token);
                await _context.SaveChangesAsync();
                var ToEmail = await _context.Clientes.Where(x => x.idUsuario == user.idUsuario).Select(x => x.Email).FirstOrDefaultAsync();
                await _emailService.SendEmailAsync("Token para login", $"Su token es :{token.Token} y expira :{token.Expiration}", ToEmail);
                return Ok(user);
            }
            else
            {
                return Ok(user);
            }
        }
        [Route("login-2Auth")]
        [HttpPost]
        public async Task<ActionResult> ValidacionToken(Tokens modelToken)
        {
            if (modelToken == null)
            {
                return BadRequest("Datos Invalidos");
            }
            var tokenValidate = _context.Tokens.Where(x => x.Token == modelToken.Token
                                            && x.idUsuario == modelToken.idUsuario).FirstOrDefault();
            if (tokenValidate == null || tokenValidate.Active == false)
            {
                return BadRequest("Datos Invalidos");
            }
            if (DateTime.Now > tokenValidate.Expiration)
            {
                tokenValidate.Active = false;
                _context.Entry(tokenValidate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return BadRequest("El token Ya Expiro");
            }
            var token = _context.Tokens.Where(x => x.Token == modelToken.Token
                                            && x.idUsuario == modelToken.idUsuario
                                            && x.Active == true).FirstOrDefault();
            token.Active = false;
            _context.Entry(token).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var dataUser =await _context.Usuarios.Join(_context.Clientes,
                                                    u => u.idUsuario,
                                                    c => c.idUsuario,
                                                    (u, c) => new
                                                    {
                                                        u.idUsuario,
                                                        u.Username,
                                                        c.idCliente
                                                    }).FirstOrDefaultAsync();
            return Ok(dataUser);
        }
        //Client
        [Route("registryUser")]
        [HttpPost]
        public async Task<ActionResult> Registrousuario(RegistryRequest usuarios)
        {
            if (usuarios == null)
            {
                return BadRequest("Datos Invalidos");
            }
            var exist = _context.Usuarios.Where(x => x.Username == usuarios.Username).FirstOrDefault();
            if (exist != null)
            {
                return BadRequest("El nombre de usuario ya esta en uso");
            }
            var user = new Usuarios()
            {
                Username = usuarios.Username,
                Password = usuarios.Password,
                idRol = 2,
                Active = false
            };
            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();
            var client = new Clientes()
            {
                idUsuario = user.idUsuario,
                Nombres = usuarios.Nombres,
                Apellidos = usuarios.Apellidos,
                DPI = usuarios.DPI,
                Telefono = usuarios.Telefono,
                Email = usuarios.Email,
                FechaRegistro = DateTime.Now
            };
            _context.Clientes.Add(client);
            await _context.SaveChangesAsync();
            var token = new Tokens()
            {
                idUsuario = user.idUsuario,
                Token = Convert.ToString(Guid.NewGuid()),
                CreateAt = DateTime.Now,
                Expiration = DateTime.Now.AddMinutes(15),
                Active = true
            };
            var response = new
            {
                idUsuario = client.idUsuario,
                idCliente=client.idCliente,
                username=user.Username
            };
            _context.Tokens.Add(token);
            await _context.SaveChangesAsync();
            var ToEmail = await _context.Clientes.Where(x => x.idUsuario == user.idUsuario).Select(x => x.Email).FirstOrDefaultAsync();
            await _emailService.SendEmailAsync("Token para Activar su cuenta", $"Su token es :{token.Token} y expira :{token.Expiration}",ToEmail);
            return Ok(response);
        }
        //Client
        [Route("registryUserActivate")]
        [HttpPost]
        public async Task<ActionResult> Activacionusuario(Tokens modelToken)
        {
            if (modelToken == null)
            {
                return BadRequest("Datos Invalidos");
            }
            var tokenValidate = _context.Tokens.Where(x => x.Token == modelToken.Token
                                            && x.idUsuario == modelToken.idUsuario).FirstOrDefault();
            if (tokenValidate == null || tokenValidate.Active == false)
            {
                return BadRequest("Datos Invalidos");
            }
            if (DateTime.Now > tokenValidate.Expiration)
            {
                tokenValidate.Active = false;
                _context.Entry(tokenValidate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return BadRequest("El token Ya Expiro");
            }
            var token = _context.Tokens.Where(x => x.Token == modelToken.Token
                                            && x.idUsuario == modelToken.idUsuario
                                            && x.Active == true).FirstOrDefault();
            token.Active = false;
            _context.Entry(token).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var user = _context.Usuarios.Where(x => x.idUsuario == modelToken.idUsuario).FirstOrDefault();
            user.Active = true;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        // For Clients
        [Route("RequestResetPassword")]
        [HttpPost]
        public async Task<ActionResult> RequestResetPassword(RegistryRequest usuarios)
        {
            if (usuarios == null)
            {
                return BadRequest("Datos Invalidos");
            }
            var exist = _context.Usuarios.Join(_context.Clientes,
                u => u.idUsuario,
                c => c.idUsuario,
                (u, c) => new { u, c })
                .Where(x => x.c.Email == usuarios.Email).FirstOrDefault();
            if (exist == null)
            {
                return BadRequest("Email Incorrecto");
            }
            var token = new Tokens()
            {
                idUsuario = exist.u.idUsuario,
                Token = Convert.ToString(Guid.NewGuid()),
                CreateAt = DateTime.Now,
                Expiration = DateTime.Now.AddMinutes(15),
                Active = true
            };
            var response = new
            {
                idUsuario = exist.u.idUsuario,
                idCliente = exist.c.idCliente,
                username = exist.u.Username
            };
            _context.Tokens.Add(token);
            await _context.SaveChangesAsync();
            await _emailService.SendEmailAsync("Token para Reset Password", $"Su token es :{token.Token} y expira :{token.Expiration}",exist.c.Email);
            return Ok(response);
        }
        [Route("ValidateTokenResetPassword")]
        [HttpPost]
        public async Task<ActionResult> ValidateTokenResetPassword(Tokens modelToken)
        {
            if (modelToken == null)
            {
                return BadRequest("Datos Invalidos");
            }
            var tokenValidate = _context.Tokens.Where(x => x.Token == modelToken.Token
                                            && x.idUsuario == modelToken.idUsuario).FirstOrDefault();
            if (tokenValidate == null || tokenValidate.Active == false)
            {
                return BadRequest("Datos Invalidos");
            }
            if (DateTime.Now > tokenValidate.Expiration)
            {
                tokenValidate.Active = false;
                _context.Entry(tokenValidate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return BadRequest("El token Ya Expiro");
            }
            var token = _context.Tokens.Where(x => x.Token == modelToken.Token
                                            && x.idUsuario == modelToken.idUsuario
                                            && x.Active == true).FirstOrDefault();
            token.Active = false;
            _context.Entry(token).State = EntityState.Modified;
            return Ok();
        }
        [Route("ResetPassword")]
        [HttpPost]
        public async Task<ActionResult> ResetPassword(ChangePassword modelToken)
        {
            if (modelToken == null)
            {
                return BadRequest("Datos Invalidos");
            }

            var user = _context.Usuarios.Where(x => x.idUsuario == modelToken.idUsuario).FirstOrDefault();
            user.Active = true;
            user.Password = modelToken.Password;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var ToEmail = await _context.Clientes.Where(x => x.idUsuario == user.idUsuario).Select(x => x.Email).FirstOrDefaultAsync();
            await _emailService.SendEmailAsync("Cambio de Password Exitoso", $"Se realizo el Cambio de Password Con Exito el {DateTime.Now.ToString()}", ToEmail);
            return Ok();
        }

    }
}
