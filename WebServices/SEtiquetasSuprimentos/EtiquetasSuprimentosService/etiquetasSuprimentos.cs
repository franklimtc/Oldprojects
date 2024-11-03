namespace EtiquetasSuprimentosService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class etiquetasSuprimentos
    {
        public int id { get; set; }

        [Required]
        public string suprimento { get; set; }

        [Required]
        public string serialSuprimento { get; set; }

        [Required]
        public string etiqueta { get; set; }

        [Required]
        public string operador { get; set; }

        public DateTime? data { get; set; }

        public DateTime? dtTroca { get; set; }

        public int? producao { get; set; }
        public int? valorAtual { get; set; }

        public string status { get; set; }

        public DateTime? dtTermino { get; set; }
        public DateTime? dtAtualizacao { get; set; }

    }
}
