using Apis_de_prueba.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Apis_de_prueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly CrudPruebaContext _dbContext;
        private readonly ILogger<TareasController> _logger;

        public TareasController(CrudPruebaContext dbContext, ILogger<TareasController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var lista = await _dbContext.Tareas.ToListAsync();
            return Ok(lista);
        }

        [HttpGet("mitarea/{valor}")]
        public async Task<ActionResult> GetByMitarea(string valor)
        {
            try
            {
                var palabrasClave = valor.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var totaltareas = await _dbContext.Tareas.ToListAsync();
                var consulta = totaltareas
                    .Where(t => palabrasClave.All(p => t.MiTarea.ToLower().Contains(p)))
                    .ToList();
                if (!consulta.Any())
                {
                    return NotFound($"No se encontraron resultados con la palabra consultada");
                }

                return Ok(consulta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al ejecutar la solicitud para MiTarea y valor {valor}");
                return BadRequest(new { mensaje = "Error al ejecutar la solicitud." });
            }
        }

        [HttpGet("estado/{valor}")]
        public async Task<ActionResult> GetByEstado(string valor)
        {
            try
            {
                var consulta = await _dbContext.Tareas
                    .Where(t => t.Estado.Equals(valor))
                    .ToListAsync();

                if (!consulta.Any())
                {
                    return NotFound($"No se encontraron tareas con Estado igual a {valor}.");
                }

                return Ok(consulta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al ejecutar la solicitud para Estado y valor {valor}");
                return BadRequest(new { mensaje = "Error al ejecutar la solicitud." });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TareasPostModel tarea)
        {
            Tarea tarea1 = new Tarea();
            tarea1.CodigoTarea = tarea.Codigotarea;
            tarea1.MiTarea = tarea.Mitarea;
            tarea1.FechaInicio = tarea.Fechainicio;
            tarea1.Estado = tarea.Estado;

            try
            {
                _dbContext.Tareas.Add(tarea1);
                await _dbContext.SaveChangesAsync();
                return Ok(new { mensaje = "Tarea ingresada exitosamente." });
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 2627)
                {
                    return BadRequest(new { mensaje = "El código de la tarea ingresada ya existe." });
                }
                else
                {
                    _logger.LogError(ex, "Error al ejecutar la solicitud");
                    return BadRequest(new { mensaje = "Error al ejecutar solicitud." });
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TareasPutModel tarea)
        {
            Tarea? tarea1 = await _dbContext.Tareas.FindAsync(id);
            if (tarea1 == null)
            {
                return NotFound(new { mensaje = "Codigo de tarea no encontrado en el sistema." });
            }
            tarea1.CodigoTarea = tarea.Codigotarea;
            tarea1.MiTarea = tarea.Mitarea;
            tarea1.FechaInicio = tarea.Fechainicio;
            tarea1.Estado = tarea.Estado;
            try
            {
                _dbContext.Entry(tarea1).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return Ok(new { mensaje = "La tarea ha sido actualizada exitosamente." });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Error de concurrencia al actualizar la tarea.");
                return BadRequest(new { mensaje = "Error de concurrencia al actualizar la tarea." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Tarea? tarea1 = await _dbContext.Tareas.FindAsync(id);
            if (tarea1 == null)
            {
                return NotFound(new { mensaje = "Codigo de tarea no encontrado en el sistema." });
            }
            try
            {
                _dbContext.Tareas.Remove(tarea1);
                await _dbContext.SaveChangesAsync();
                return Ok(new { mensaje = "Tarea eliminada exitosamente." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la tarea");
                return BadRequest(new { mensaje = "Error al eliminar la tarea." });
            }
        }
    }
}
