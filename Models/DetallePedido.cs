using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_MaestroDetalle.Models
{
    public class DetallePedido
    {
        [Key]
        public int DetallePedidoId { get; set; }
        public string Producto { get; set; }=null!;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        [ForeignKey("Pedido")]
        public int PedidoId { get; set; }
    }
}