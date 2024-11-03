namespace Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_suprimentos2
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

        public int? MediaDia { get; set; }

        public int? ContadorAtual { get; set; }

        public double? Toner { get; set; }

        public int? TonerEstimativaDias { get; set; }

        public DateTime? DataEnvioToner { get; set; }

        public DateTime? DataTrocaTonner { get; set; }

        public string PostagemToner { get; set; }

        public double? Cilindro { get; set; }

        public int? CilindroEstimativaDias { get; set; }

        public DateTime? DataEnvioCilindro { get; set; }

        public DateTime? DataTrocaCilindro { get; set; }

        public string PostagemCilindro { get; set; }

        public DateTime? DataUltimaLeitura { get; set; }

        public string serialToner { get; set; }

        public string serialFoto { get; set; }
    }
}
