using System.ComponentModel.DataAnnotations;

namespace API_MaestroDetalle.Models
{
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }
        public DateTime FechaPedido { get; set; }
        public List<DetallePedido>? Detalles { get; set; }
    }
}