namespace Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_ErrosEquipamentos
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idEquipamento { get; set; }

        [StringLength(2)]
        public string UF { get; set; }

        [StringLength(35)]
        public string Cidade { get; set; }

        public string Unidade { get; set; }

        public string Cod { get; set; }

        public string Ambiente { get; set; }

        [StringLength(30)]
        public string Fila { get; set; }

        [StringLength(15)]
        public string serie { get; set; }

        [StringLength(15)]
        public string ip { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PortaAberta { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BandejaAberta { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BandejaVazia { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PapelAtolado { get; set; }

        public int? Erros { get; set; }
    }
}
