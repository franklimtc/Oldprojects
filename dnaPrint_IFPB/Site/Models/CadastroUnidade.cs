namespace Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CadastroUnidade")]
    public partial class CadastroUnidade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CadastroUnidade()
        {
            CadastroEquipamentos = new HashSet<CadastroEquipamentos>();
            CadastroSetor = new HashSet<CadastroSetor>();
        }

        [Key]
        public int idLocalidade { get; set; }

        public int? idCidade { get; set; }

        [Required]
        public string descricao { get; set; }

        public string endereco { get; set; }

        public string telefone { get; set; }

        [StringLength(20)]
        public string contato { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        public string razaoSocial { get; set; }

        public int? numero { get; set; }

        public string bairro { get; set; }

        public string cep { get; set; }

        public string cnpj { get; set; }

        public string ie { get; set; }

        public string fax { get; set; }

        public string email { get; set; }

        public int? idRevenda { get; set; }

        public virtual CadastroCidade CadastroCidade { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CadastroEquipamentos> CadastroEquipamentos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CadastroSetor> CadastroSetor { get; set; }
    }
}
