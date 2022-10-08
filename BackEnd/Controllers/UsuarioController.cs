using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.DTO;
using BackEnd.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            try
            {
                var validateExistence = await _usuarioService.ValidateExistence(usuario);
                if (validateExistence)
                {
                    return BadRequest(new { message = "El usuario \"" + usuario.NombreUsuario + "\" ya existe!" });
                }
                usuario.Password = Encriptar.EncriptarPassword(usuario.Password);
                await _usuarioService.SaveUser(usuario);
                return Ok(new { message = "Usuario registrado con éxito!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("CambiarPassword")]
        [HttpPut]
        public async Task<IActionResult> CambiarPassword(CambiarPasswordDTO cambiarPassword)
        {
            try
            {
                int id = 10;
                string passwordEncriptado = Encriptar.EncriptarPassword(cambiarPassword.passwordAnterior);
                var usuario = await _usuarioService.ValidatePassword(id, passwordEncriptado);
                if (usuario == null)
                {
                    return BadRequest(new { message = "Password incorrecta"});
                }
                else
                {
                    usuario.Password = Encriptar.EncriptarPassword(passwordEncriptado);
                    await _usuarioService.UpdatePassword(usuario);
                    return Ok(new { message = "Password actualizada con éxito" });
                }
                return Ok(new { message = "Password actualizada con éxito"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
