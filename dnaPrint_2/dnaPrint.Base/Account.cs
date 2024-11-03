using System;
using System.Collections.Generic;
using dnaPrint.DAO;
using System.Data;
using System.Linq;

namespace dnaPrint.Base
{
    public class Account 
    {
        private static string chave = "csfdigital2017ce";
        private static string vetor = "csfdigital2017ma";

        public enum PerfilUsuario {Administradores, Operadores, Usuarios }

        #region Campos
        public int idusuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int idGrupo { get; set; }
        public string Senha { get; set; }
        public string Grupo { get; set; }
        public bool Status { get; set; }
        public PerfilUsuario Perfil { get; set; }
        #endregion

        public bool Adicionar(string connString, Operacoes.tipo Tipo)
        {
            bool result = false;

            string tsql = "insert into Usuarios(nome, email, senha, idGrupoUsuario) values(@nome, @email, @senha, @idGrupoUsuario);";

            List<object[]> parametros = new List<object[]>();

            parametros.Add(new object[] { "@nome", this.Nome });
            parametros.Add(new object[] { "@email", this.Email });
            parametros.Add(new object[] { "@senha", Cripto.Criptografar(chave, vetor, this.Senha) });
            parametros.Add(new object[] { "@idGrupoUsuario", this.idGrupo });

            int qtdLinhas = new DAO.Operacoes(connString, Tipo).ExecuteNonQuery(tsql, parametros);
            if (qtdLinhas > 0)
                result = true;

            return result;
        }

        public bool Atualizar(string connString, Operacoes.tipo Tipo)
        {
            bool result = false;

            string tsql = "update Usuarios set senha = @senha, idGrupoUsuario = @idGrupoUsuario where idUsuario = @idUsuario;";

            List<object[]> parametros = new List<object[]>();

            parametros.Add(new object[] { "@senha", Cripto.Criptografar(chave, vetor, this.Senha) });
            parametros.Add(new object[] { "@idGrupoUsuario", this.idGrupo });
            parametros.Add(new object[] { "@idUsuario", this.idusuario });

            int qtdLinhas = new DAO.Operacoes(connString, Tipo).ExecuteNonQuery(tsql, parametros);
            if (qtdLinhas > 0)
                result = true;

            return result;
        }

        public bool Excluir(string connString, Operacoes.tipo Tipo)
        {
            bool result = false;

            string tsql = "update Usuarios set status = '0' where idUsuario = @idUsuario;";

            List<object[]> parametros = new List<object[]>();

            parametros.Add(new object[] { "@idUsuario", this, idusuario });

            int qtdLinhas = new DAO.Operacoes(connString, Tipo).ExecuteNonQuery(tsql, parametros);
            if (qtdLinhas > 0)
                result = true;

            return result;
        }

        public List<Account> Listar(string connString, Operacoes.tipo Tipo)
        {
            List<Account> Lista = new List<Account>();

            string tsql = $"select idUsuario, nome, email, grupo from vw_usuarios;";

            DataTable dt = new DAO.Operacoes(connString, Tipo).ReturnDt(tsql);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow orow in dt.Rows)
                {
                    Account ac = new Account();
                    ac.idusuario = int.Parse(orow["idUsuario"].ToString());
                    ac.Nome = orow["nome"].ToString();
                    ac.Email = orow["email"].ToString();
                    ac.Grupo = orow["grupo"].ToString();
                    Lista.Add(ac);
                }
            }

            return Lista;
        }

        public static bool ValidarUsuar(string connString, Operacoes.tipo Tipo, string email, string senha)
        {
            bool result = false;

            string tsql = "select count(*) from Usuarios where email = @email and senha = @senha;";

            List<object[]> parametros = new List<object[]>();

            string senhaCripto = Cripto.Criptografar(chave, vetor, senha);

            parametros.Add(new object[] { "@email", email });
            parametros.Add(new object[] { "@senha", senhaCripto });

            int qtdLinhas = int.Parse(new DAO.Operacoes(connString, Tipo).ExecuteScalar(tsql, parametros).ToString());
            if (qtdLinhas == 1)
                result = true;

            return result;
        }

        public static bool UsuarioExiste(string connString, Operacoes.tipo Tipo, string nome, string email)
        {
            bool result = false;

            string tsql = "select count(*) from Usuarios where email = @email or nome = @nome";

            List<object[]> parametros = new List<object[]>();

            parametros.Add(new object[] { "@email", email });
            parametros.Add(new object[] { "@nome", nome });

            int qtdLinhas = int.Parse(new DAO.Operacoes(connString, Tipo).ExecuteScalar(tsql, parametros).ToString());
            if (qtdLinhas > 0)
                result = true;

            return result;
        }

        public static PerfilUsuario RetornaPerfil(string connString, Operacoes.tipo Tipo, string Usuario)
        {
            PerfilUsuario perfil = PerfilUsuario.Usuarios;



            return perfil;
        }

        public static Account FindByEmail(string connString, Operacoes.tipo Tipo, string email)
        {
            List<Account> Lista = new List<Account>();

            string tsql = $"select idgrupousuario, idUsuario, nome, email, grupo from vw_usuarios where email = '{email}';";

            DataTable dt = new DAO.Operacoes(connString, Tipo).ReturnDt(tsql);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow orow in dt.Rows)
                {
                    Account ac = new Account();
                    ac.idusuario = int.Parse(orow["idUsuario"].ToString());
                    ac.Nome = orow["nome"].ToString();
                    ac.Email = orow["email"].ToString();
                    ac.Grupo = orow["grupo"].ToString();
                    ac.idGrupo = int.Parse(orow["idgrupousuario"].ToString());
                    Lista.Add(ac);
                }
            }

            return Lista.First();
        }

    }
}
