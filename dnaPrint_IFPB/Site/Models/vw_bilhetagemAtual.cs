namespace Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_bilhetagemAtual
    {
        [StringLength(2)]
        public string UF { get; set; }

        [StringLength(35)]
        public string cidade { get; set; }

        public string local { get; set; }

        public string CC { get; set; }

        public string setor { get; set; }

        [StringLength(15)]
        public string serie { get; set; }

        [StringLength(30)]
        public string nome { get; set; }

        [StringLength(15)]
        public string ip { get; set; }

        public int? Iniimpressoes { get; set; }

        public int? Inicopias { get; set; }

        public int? Inifax { get; set; }

        public int? Initotal { get; set; }

        public int? Finimpressoes { get; set; }

        public int? Fincopias { get; set; }

        public int? Finfax { get; set; }

        public int? Fintotal { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idEquipamento { get; set; }

        public string Modelo { get; set; }

        public int? franquia { get; set; }

        public float? valor { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Tipo { get; set; }

        public int? Volume { get; set; }

        public int? VolumeTotal { get; set; }
    }
}
