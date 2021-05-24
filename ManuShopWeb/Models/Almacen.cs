using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Models
{
    [Table("Almacen")]
    public class Almacen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        [Column("tienda")]
        public int tienda { get; set; }
        [Column("versionProducto")]
        public int versionProducto { get; set; }
        [Column("cantidad")]
        public int cantidad { get; set; }

    }
}
