using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DW.Microservicio.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string NombreImagen { get; set; }
        public string UriImagen { get; set; }
        public int TipoProductoId { get; set; }
        public TipoProducto TipoProducto { get; set; }
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }
        public int Stock { get; set; }
        public int PuntoReorden { get; set; }
        public int StockMaximo { get; set; }

        public bool OnReorder { get; set; }

    }
}
