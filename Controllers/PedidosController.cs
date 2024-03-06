using API_MaestroDetalle.Data;
using API_MaestroDetalle.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_MaestroDetalle.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pedido.FechaPedido = DateTime.Now;

            if (pedido.Detalles != null && pedido.Detalles.Any())
            {
                foreach (var item in pedido.Detalles)
                {
                    _context.Entry(item).State = EntityState.Added;
                }
            }

            _context.Pedidos.Add(pedido);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedido), new { id = pedido.PedidoId }, pedido);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos.Include(p => p.Detalles)
            .FirstOrDefaultAsync(p => p.PedidoId == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            var pedidos = await _context.Pedidos.Include(p => p.Detalles)
            .ToListAsync();

            return Ok(pedidos);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.PedidoId)
            {
                return BadRequest();
            }

            var pedidoExistente = await _context.Pedidos.Include(p => p.Detalles).FirstOrDefaultAsync(p => p.PedidoId == id);

            if (pedidoExistente == null)
            {
                return NotFound();
            }

            pedidoExistente.FechaPedido = pedido.FechaPedido;


            if (pedido.Detalles != null)
            {
                foreach (var detalle in pedido.Detalles)
                {
                    if (pedidoExistente.Detalles != null)
                    {
                        var detalleExistente = pedidoExistente.Detalles.FirstOrDefault(d => d.DetallePedidoId == detalle.DetallePedidoId);

                        if (detalleExistente != null)
                        {
                            detalleExistente.Producto = detalle.Producto;
                            detalleExistente.Cantidad = detalle.Cantidad;
                            detalleExistente.PrecioUnitario = detalle.PrecioUnitario;
                        }
                    }

                }
            }



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExiste(id))
                {
                    return NotFound();
                }
                else
                    throw;
            }

            return NoContent();
        }

        private bool PedidoExiste(int id)
        {
            return _context.Pedidos.Any(e => e.PedidoId == id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Pedido>> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido== null)
            {
                return NotFound();
            }
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return Ok(pedido);
        }
    }
}