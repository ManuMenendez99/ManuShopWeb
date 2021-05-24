using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Models
{
    [Table("Mesas")]
    public class Mesa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        [Column("numero")]
        public int numero { get; set; }
        [Column("tipoMesa")]
        public int tipoMesa { get; set; }
        [Column("capacidad")]
        public int capacidad { get; set; }
        [Column("estado")]
        public int estado { get; set; }
        [Column("tienda")]
        public int tienda { get; set; }
    }
}
