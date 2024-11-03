namespace MonitorEtqSuprimentos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_relatorio_suprimentos
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

        public int? Cod { get; set; }

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

        public DateTime? DataEntregaToner { get; set; }

        public string tpEnvioToner { get; set; }

        public DateTime? DataTrocaTonner { get; set; }

        public string PostagemToner { get; set; }

        public double? Cilindro { get; set; }

        public int? CilindroEstimativaDias { get; set; }

        public DateTime? DataEnvioCilindro { get; set; }

        public DateTime? DataEntregaCilindro { get; set; }

        public string tpEnvioCilindro { get; set; }

        public DateTime? DataTrocaCilindro { get; set; }

        public string PostagemCilindro { get; set; }

        public DateTime? DataUltimaLeitura { get; set; }

        public string serialToner { get; set; }

        public string serialFoto { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string SugestaoToner { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string SugestaoCilindro { get; set; }
    }
}
