using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Models
{
    [Table("LineasTickets")]
    public class LineasTicket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        [Column("ticket")]
        public int ticket { get; set; }
        [Column("version")]
        public int versionProducto { get; set; }
        [Column("descuento")]
        public int descuento { get; set; }
        [Column("detalles")]
        public String detalles { get; set; }
        [Column("cantidad")]
        public int cantidad { get; set; }
        [Column("cancelada")]
        public bool cancelada { get; set; }

    }
}
