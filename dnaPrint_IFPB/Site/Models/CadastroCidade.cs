namespace Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CadastroCidade")]
    public partial class CadastroCidade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CadastroCidade()
        {
            CadastroEquipamentos = new HashSet<CadastroEquipamentos>();
            CadastroUnidade = new HashSet<CadastroUnidade>();
        }

        [Key]
        public int idCidade { get; set; }

        public int? idEstado { get; set; }

        [Required]
        [StringLength(35)]
        public string cidade { get; set; }

        public virtual CadastroEstado CadastroEstado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CadastroEquipamentos> CadastroEquipamentos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CadastroUnidade> CadastroUnidade { get; set; }
    }
}
