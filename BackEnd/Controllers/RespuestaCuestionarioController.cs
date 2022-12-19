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
    public class RespuestaCuestionarioController : ControllerBase
    {
        private readonly IRespuestaCuestionarioService _respuestaCuestionarioService;
        private readonly ICuestionarioService _cuestionarioService;
        public RespuestaCuestionarioController(IRespuestaCuestionarioService respuestaCuestionarioService, ICuestionarioService cuestionarioService)
        {
            _respuestaCuestionarioService = respuestaCuestionarioService;
            _cuestionarioService = cuestionarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RespuestaCuestionario respuestaCuestionario)
        {
            try
            {
                await _respuestaCuestionarioService.SaveRespuestaCuestionario(respuestaCuestionario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{idCuestionario}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get(int idCuestionario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfigurator.GetIdUsuario(identity);

                var listRespuestaCuestionario = await _respuestaCuestionarioService.ListRespuestaCuestionario(idCuestionario, idUsuario);

                if (listRespuestaCuestionario == null)
                {
                    return BadRequest(new { message = "Error al buscar las respuestas" });
                }
                return Ok(listRespuestaCuestionario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfigurator.GetIdUsuario(identity);

                var rtaCuestionario = await _respuestaCuestionarioService.BuscarRespuestaCuestionario(id, idUsuario);
                if (rtaCuestionario == null)
                {
                    return BadRequest();
                }
                await _respuestaCuestionarioService.DeleteRespuestaCuestionario(rtaCuestionario);

                return Ok(new { message = "La respuesta al cuestionario fue eliminada con exito"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetCuestionarioByIdRespuesta/{idRtaCuestionario}")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCuestionarioByIdRespuesta(int idRtaCuestionario)
        {
            try
            {
                // Obtener el idCuestionario dado un idRtaCuestionario
                var idCuestionario = await _respuestaCuestionarioService.GetIdCuestionarioByIdRtaCuestionario(idRtaCuestionario);
                // Buscamos el cuestionario
                var cuestionario = await _cuestionarioService.GetCuestionario(idCuestionario);
                // Buscamos las respuestas seleccionadas 
                var listRespuestas = await _respuestaCuestionarioService.GetListRespuestas(idRtaCuestionario);
                return Ok(new { cuestionario, respuestas = listRespuestas });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
