using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Models
{
    [Table("Franquicias")]
    public class Franquicia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        [Column("nombre")]
        public String nombre { get; set; }
        [Column("jefe")]
        public int jefe { get; set; }
    }
}
