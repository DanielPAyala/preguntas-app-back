using BackEnd.Domain.IRepositories;
using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public class RespuestaCuestionarioService: IRespuestaCuestionarioService
    {
        private readonly IRespuestaCuestionarioRepository _respuestaCuestionarioRepository;
        public RespuestaCuestionarioService(IRespuestaCuestionarioRepository respuestaCuestionarioRepository)
        {
            _respuestaCuestionarioRepository = respuestaCuestionarioRepository;
        }

        public async Task<RespuestaCuestionario> BuscarRespuestaCuestionario(int idRtaCuestionario, int idUsuario)
        {
            return await _respuestaCuestionarioRepository.BuscarRespuestaCuestionario(idRtaCuestionario, idUsuario);
        }

        public async Task DeleteRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario)
        {
            await _respuestaCuestionarioRepository.DeleteRespuestaCuestionario(respuestaCuestionario);
        }

        public async Task<int> GetIdCuestionarioByIdRtaCuestionario(int idRtaCuestionario)
        {
            return await _respuestaCuestionarioRepository.GetIdCuestionarioByIdRtaCuestionario(idRtaCuestionario);
        }

        public async Task<List<RespuestaCuestionarioDetalle>> GetListRespuestas(int idRespuestaCuestionario)
        {
            return await _respuestaCuestionarioRepository.GetListRespuestas(idRespuestaCuestionario);
        }

        public async Task<List<RespuestaCuestionario>> ListRespuestaCuestionario(int idCuestionario, int idUsuario)
        {
            return await _respuestaCuestionarioRepository.ListRespuestaCuestionario(idCuestionario, idUsuario);
        }

        public async Task SaveRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario)
        {
            await _respuestaCuestionarioRepository.SaveRespuestaCuestionario(respuestaCuestionario);
        }
    }
}
