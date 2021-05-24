using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Models
{
    [Table("Tickets")]
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        [Column("tienda")]
        public int tienda { get; set; }
        [Column("personas")]
        public int personas { get; set; }
        [Column("fechaApertura")]
        public String fechaApertura { get; set; }
        [Column("fechaCierre")]
        public String fechaCierre { get; set; }
        [Column("detalles")]
        public String detalles { get; set; }
        [Column("mesa")]
        public int mesa { get; set; }
        [Column("descuentoGeneral")]
        public int descuentoGeneral { get; set; }
    }
}
