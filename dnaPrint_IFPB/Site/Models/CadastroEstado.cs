namespace Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CadastroEstado")]
    public partial class CadastroEstado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CadastroEstado()
        {
            CadastroCidade = new HashSet<CadastroCidade>();
            CadastroEquipamentos = new HashSet<CadastroEquipamentos>();
        }

        [Key]
        public int idEstado { get; set; }

        [Required]
        [StringLength(2)]
        public string UF { get; set; }

        [Required]
        [StringLength(20)]
        public string estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CadastroCidade> CadastroCidade { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CadastroEquipamentos> CadastroEquipamentos { get; set; }
    }
}
