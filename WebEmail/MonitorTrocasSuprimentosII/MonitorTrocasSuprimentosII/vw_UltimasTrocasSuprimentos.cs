namespace MonitorTrocasSuprimentosII
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_UltimasTrocasSuprimentos
    {
        public string serie { get; set; }

        [Key]
        public string serial { get; set; }

        public int? suprimentoAtual { get; set; }

        public int? suprimentoAnterior { get; set; }

        public int? contador { get; set; }

        public DateTime? data { get; set; }
        
        public string Suprimento { get; set; }
    }
}
