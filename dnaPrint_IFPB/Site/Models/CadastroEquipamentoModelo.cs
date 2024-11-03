namespace Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CadastroEquipamentoModelo")]
    public partial class CadastroEquipamentoModelo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CadastroEquipamentoModelo()
        {
            CadastroEquipamentos = new HashSet<CadastroEquipamentos>();
        }

        [Key]
        public int idModeloEquipamento { get; set; }

        public string Fabricante { get; set; }

        public string Modelo { get; set; }

        public int? franquia { get; set; }

        public float? valor { get; set; }

        public bool? status { get; set; }

        [Column(TypeName = "money")]
        public decimal? valorExcedente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CadastroEquipamentos> CadastroEquipamentos { get; set; }
    }
}
