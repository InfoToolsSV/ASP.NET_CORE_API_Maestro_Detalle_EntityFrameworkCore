using API_MaestroDetalle.Data;
using API_MaestroDetalle.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_MaestroDetalle.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetallesPedidoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DetallesPedidoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<DetallePedido>> PostDetallePedido(DetallePedido detallePedido)
        {
            var pedidoExistente = await _context.Pedidos.FindAsync(detallePedido.PedidoId);

            if (pedidoExistente == null)
            {
                return BadRequest("El pedido al que se intenta asociar el detalle no existe.");
            }

            _context.DetallesPedidos.Add(detallePedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDetallePedido), new { id = detallePedido.DetallePedidoId }, detallePedido);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetallePedido>> GetDetallePedido(int id)
        {
            var detallePedido = await _context.DetallesPedidos.FindAsync(id);
            if (detallePedido == null)
            {
                return NotFound();
            }
            return Ok(detallePedido);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutDetallePedido(int id, DetallePedido detallePedido)
        {
            if (id != detallePedido.DetallePedidoId)
            {
                return BadRequest();
            }

            _context.Entry(detallePedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetallePedidoExiste(id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        private bool DetallePedidoExiste(int id)
        {
            return _context.DetallesPedidos.Any(d => d.DetallePedidoId == id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DetallePedido>> DeleteDetallePedido(int id)
        {
            var detallePedido = await _context.DetallesPedidos.FindAsync(id);
            if (detallePedido == null)
            {
                return NotFound();
            }
            _context.DetallesPedidos.Remove(detallePedido);
            await _context.SaveChangesAsync();

            return Ok(detallePedido);
        }

    }
}