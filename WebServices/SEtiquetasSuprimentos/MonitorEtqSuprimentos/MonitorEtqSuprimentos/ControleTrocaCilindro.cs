namespace MonitorEtqSuprimentos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ControleTrocaCilindro")]
    public partial class ControleTrocaCilindro
    {
        [Key]
        public int idControleTrocaCilindro { get; set; }

        public int? idEquipamento { get; set; }

        public string serie { get; set; }

        public string serial { get; set; }

        public int? Cilindro { get; set; }

        public int? CilindroAnterior { get; set; }

        public int? contador { get; set; }

        public DateTime? data { get; set; }

        public bool? retorno { get; set; }
    }
}
