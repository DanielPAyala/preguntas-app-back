using BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Domain.IServices
{
    public interface IRespuestaCuestionarioService
    {
        Task SaveRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario);
        Task<List<RespuestaCuestionario>> ListRespuestaCuestionario(int idCuestionario, int idUsuario);
        Task<RespuestaCuestionario> BuscarRespuestaCuestionario(int idRtaCuestionario, int idUsuario);
        Task DeleteRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario);
        Task<int> GetIdCuestionarioByIdRtaCuestionario(int idRtaCuestionario);
        Task<List<RespuestaCuestionarioDetalle>> GetListRespuestas(int idRespuestaCuestionario);
    }
}
