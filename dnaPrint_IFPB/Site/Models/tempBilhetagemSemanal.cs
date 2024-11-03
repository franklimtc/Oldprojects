namespace Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tempBilhetagemSemanal")]
    public partial class tempBilhetagemSemanal
    {
        public int? dom { get; set; }

        public int? seg { get; set; }

        public int? ter { get; set; }

        public int? qua { get; set; }

        public int? qui { get; set; }

        public int? sex { get; set; }

        public int? sab { get; set; }

        [Key]
        public DateTime AtualizadoEm { get; set; }
    }
}
