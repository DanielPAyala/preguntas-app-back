using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuestionarioController : ControllerBase
    {
        private readonly ICuestionarioService _cuestionarioService;

        public CuestionarioController(ICuestionarioService cuestionarioService)
        {
            _cuestionarioService = cuestionarioService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody]Cuestionario cuestionario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int id = JwtConfigurator.GetIdUsuario(identity);

                cuestionario.UsuarioId = id;
                cuestionario.Activo = 1;
                cuestionario.FechaCreacion = DateTime.Now;

                await _cuestionarioService.CreateCuestionario(cuestionario);
                return Ok(new { message = "Se agregó el cuestionario exitosamente!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetListCuestionarioByUser")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetListCuestionarioByUser()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int id = JwtConfigurator.GetIdUsuario(identity);
                var listCuestionario = await _cuestionarioService.GetListCuestionarioByUser(id);
                return Ok(listCuestionario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{idCuestionario}")]
        public async Task<IActionResult> Get(int idCuestionario)
        {
            try
            {
                var cuestionario = await _cuestionarioService.GetCuestionario(idCuestionario);
                return Ok(cuestionario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{idCuestionario}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int idCuestionario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfigurator.GetIdUsuario(identity);
                var cuestionario = await _cuestionarioService.BuscarCuestionario(idCuestionario, idUsuario);
                if (cuestionario == null)
                {
                    return BadRequest(new { message = "No se encontro ningun cuestionario"});
                }
                await _cuestionarioService.DeleteCuestionario(cuestionario);
                
                return Ok(new { message = "El cuestionario fue eliminado con éxito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetListCuestionarios")]
        [HttpGet]
        public async Task<IActionResult> GetListCuestionarios()
        {
            try
            {
                var listCuestionarios = await _cuestionarioService.GetListCuestionarios();
                return Ok(listCuestionarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
