using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Models
{
    [Table("Personas")]
    public class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        [Column("nombre")]
        public String nombre { get; set; }
        [Column("apellidos")]
        public String apellidos { get; set; }
        [Column("email")]
        public String email { get; set; }
        [Column("fechaNacimiento")]
        public String fechaNacimiento { get; set; }
        [Column("telefono")]
        public int telefono { get; set; }
        [Column("tiposPersona")]
        public int tiposPersona { get; set; }
        [Column("contrasena")]
        public String contrasena { get; set; }
    }
}
