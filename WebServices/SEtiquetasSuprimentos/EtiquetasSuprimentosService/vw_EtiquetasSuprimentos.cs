namespace EtiquetasSuprimentosService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_EtiquetasSuprimentos
    {
        [Key]
        public int id { get; set; }

        public string suprimento { get; set; }

        public string serialSuprimento { get; set; }

        public string etiqueta { get; set; }

        public string operador { get; set; }

        public DateTime? data { get; set; }

        public DateTime? dtTroca { get; set; }

        public int? producao { get; set; }

        public string status { get; set; }

        public DateTime? dtTermino { get; set; }

        public int? ValorAtual { get; set; }
        public long? Grupo { get; set; }
    }
}
