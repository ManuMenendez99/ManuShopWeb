using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Models
{
    [Table("VersionesProductos")]
    public class VersionesProducto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        [Column("precio")]
        public double precio { get; set; }
        [Column("producto")]
        public int producto { get; set; }
        [Column("nombre")]
        public String nombre { get; set; }
        [Column("detalles")]
        public String detalles { get; set; }
    }
}
