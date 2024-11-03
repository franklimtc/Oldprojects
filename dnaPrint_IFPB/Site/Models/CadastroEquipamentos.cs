namespace Site.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CadastroEquipamentos
    {
        [Key]
        public int idEquipamento { get; set; }

        public int? idEstado { get; set; }

        public int? idCidade { get; set; }

        public int? idLocalidade { get; set; }

        public int? idSetor { get; set; }

        public int? idModeloEquipamento { get; set; }

        [StringLength(15)]
        public string ip { get; set; }

        [StringLength(50)]
        public string fabricante { get; set; }

        [StringLength(15)]
        public string serie { get; set; }

        [StringLength(30)]
        public string nome { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtAtivacao { get; set; }

        public DateTime? dtDesativacao { get; set; }

        public bool? leituraDireta { get; set; }

        public DateTime? dtModificacao { get; set; }

        [DefaultValueAttribute(false)]
        public bool? cor { get; set; }

        [DefaultValueAttribute(true)]
        public bool? status { get; set; }

        public virtual CadastroCidade CadastroCidade { get; set; }

        public virtual CadastroEquipamentoModelo CadastroEquipamentoModelo { get; set; }

        public virtual CadastroEstado CadastroEstado { get; set; }

        public virtual CadastroUnidade CadastroUnidade { get; set; }

        public virtual CadastroSetor CadastroSetor { get; set; }
    }
}
