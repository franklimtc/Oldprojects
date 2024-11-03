namespace MonitorEtqSuprimentos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ControleTrocaTonner")]
    public partial class ControleTrocaTonner
    {
        [Key]
        public int idControleTrocaTonner { get; set; }

        public int? idEquipamento { get; set; }

        public string serie { get; set; }

        public string serial { get; set; }

        public int? Toner { get; set; }

        public int? TonerAnterior { get; set; }

        public int? contador { get; set; }

        public DateTime? data { get; set; }
    }
}
