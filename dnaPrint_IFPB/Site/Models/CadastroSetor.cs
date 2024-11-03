namespace Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CadastroSetor")]
    public partial class CadastroSetor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CadastroSetor()
        {
            CadastroEquipamentos = new HashSet<CadastroEquipamentos>();
        }

        [Key]
        public int idSetor { get; set; }

        public int? idLocalidade { get; set; }

        [Required]
        public string descricao { get; set; }

        [Required]
        public string centroCusto { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        public int? cotaMensal { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CadastroEquipamentos> CadastroEquipamentos { get; set; }

        public virtual CadastroUnidade CadastroUnidade { get; set; }
    }
}
