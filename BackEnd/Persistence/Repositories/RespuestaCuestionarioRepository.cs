using BackEnd.Domain.IRepositories;
using BackEnd.Domain.Models;
using BackEnd.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Persistence.Repositories
{
    public class RespuestaCuestionarioRepository: IRespuestaCuestionarioRepository
    {
        private readonly AplicationDbContext _context;

        public RespuestaCuestionarioRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RespuestaCuestionario> BuscarRespuestaCuestionario(int idRtaCuestionario, int idUsuario)
        {
            return await _context.RespuestaCuestionario
                .Where(x => x.Id == idRtaCuestionario && x.Cuestionario.UsuarioId == idUsuario && x.Activo == 1).FirstOrDefaultAsync();
        }

        public async Task DeleteRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario)
        {
            respuestaCuestionario.Activo = 0;
            _context.Entry(respuestaCuestionario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetIdCuestionarioByIdRtaCuestionario(int idRtaCuestionario)
        {
            return await _context.RespuestaCuestionario
                .Where(x => x.Id == idRtaCuestionario && x.Activo == 1)
                .Select(x => x.CuestionarioId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<RespuestaCuestionarioDetalle>> GetListRespuestas(int idRespuestaCuestionario)
        {
            return await _context.RespuestaCuestionarioDetalle
                .Where(x => x.RespuestaCuestionarioId == idRespuestaCuestionario)
                .Select(x => new RespuestaCuestionarioDetalle
                {
                    RespuestaId = x.RespuestaId
                })
                .ToListAsync();
        }

        public async Task<List<RespuestaCuestionario>> ListRespuestaCuestionario(int idCuestionario, int idUsuario)
        {
            return await _context.RespuestaCuestionario
                .Where(x => x.CuestionarioId == idCuestionario && x.Activo == 1 && x.Cuestionario.UsuarioId == idUsuario)
                .ToListAsync();
        }

        public async Task SaveRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario)
        {
            respuestaCuestionario.Activo = 1;
            respuestaCuestionario.Fecha = DateTime.Now;
            _context.Add(respuestaCuestionario);
            await _context.SaveChangesAsync();
        }
    }
}
