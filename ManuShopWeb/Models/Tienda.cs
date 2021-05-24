using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Models
{
    [Table("Tiendas")]
    public class Tienda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        [Column("nombre")]
        public String nombre { get; set; }
        [Column("direccion")]
        public String direccion { get; set; }
        [Column("franquicia")]
        public int franquicia { get; set; }
    }
}
