namespace Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_disponibilidade3
    {
        [Key]
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

        public DateTime? data { get; set; }

        public DateTime? dtAtivacao { get; set; }

        public int? qtdDias { get; set; }
    }
}
