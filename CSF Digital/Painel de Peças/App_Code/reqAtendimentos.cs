using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for reqAtendimentos
/// </summary>
public class reqAtendimentos
{
	public reqAtendimentos()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static bool Abrir(string idCliente, string req, string endereco, string serie, string cliente, string telefone, string falha, string solicitante, string tipo, string tecnico, string emailTecnico)
    {
        string tsql = string.Format(@"insert into reqAtendimentos(idCliente, req, endereco, serie, cliente, telefone, falha, solicitante, tipo, tecnico, emailTecnico)
            values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}', {8},'{9}','{10}');", idCliente, req, endereco, serie, cliente, telefone, falha, solicitante, tipo, tecnico, emailTecnico);
        return DAO.ExecuteNonQuery(DAO.connString(), tsql);
    }
    public static bool Abrir(string uf, string cidade, string modelo, string idCliente, string req, string endereco, string serie, string cliente, string telefone, string falha, string solicitante, string tipo, string tecnico, string emailTecnico, string emailCopia, bool enviarEmail)
    {
        string tsql = null;
        if (enviarEmail)
        {
            tsql = string.Format(@"insert into reqAtendimentos(idCliente, req, endereco, serie, cliente, telefone, falha, solicitante, tipo, tecnico, emailTecnico)
            values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}', {8},'{9}','{10}');", idCliente, req, endereco, serie, cliente, telefone, falha, solicitante, tipo, tecnico, emailTecnico);
            string assunto = string.Format("RAT {0} - {1}  - {2} - {3} - {4}", req, serie, uf.ToUpper(), cidade.ToUpper(), tecnico.ToUpper());
            InformarOperador(assunto, GerarMensagem(solicitante, req, cliente, serie, modelo, endereco, telefone, falha), emailTecnico, emailCopia);
        }
        else
        { 
        tsql = string.Format(@"insert into reqAtendimentos(idCliente, req, endereco, serie, cliente, telefone, falha, solicitante, tipo, tecnico, emailTecnico,emailEnviado)
            values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}', {8},'{9}','{10}', 1);", idCliente, req, endereco, serie, cliente, telefone, falha, solicitante, tipo, tecnico, emailTecnico);
        }
        
        return DAO.ExecuteNonQuery(DAO.connString(), tsql);
    }

    public static void InformarOperador(string assunto, string mensagem, string para, string copia)
    {
        EmailWeb.EmailSoapClient msg = new EmailWeb.EmailSoapClient();
        if (copia != "")
        {
            msg.EnviarHtmlMessageCopia(para, copia, assunto, mensagem, "Atendimento Técnico", true);
        }
        else
        {
            msg.EnviarHtmlMessage(para, assunto, mensagem, "Atendimento Técnico", true);
        }

    }

    public static string GerarMensagem(string operador, string Requisicao, string Contato, string Serie, string Modelo, string Endereco, string Telefone, string DescricaoFalha)
    {
        return string.Format(@"<p>Gentileza providenciar o atendimento abaixo e informar o prazo por <span>email</span> para <b>{0}</b> (sac@csfdigital.com.br).<o:p></o:p></p>

<p>Favor solicitar ao técnico para ligar da unidade para o SAC após a conclusão do atendimento.<o:p></o:p></p>

<p>Tel.: (85) 3299-5516 / 5517 ou VOIP *5516. Falar com <b>{0}</b>.<o:p></o:p></p>

<p><o:p>&nbsp;</o:p></p>

<p><b>Requisição: </b>{1}<o:p></o:p></p>

<p><b>Contato: </b>{2}</p>
<p><b>Série: </b>{3}</p>    
<p><b>Modelo: </b>{4}</p>

<p><b>Endereço: </b>{5}<o:p></o:p></p>

<p><b>Telefone: </b>{6}<o:p></o:p></p>

<p><b>Descrição da falha: </b>{7}<o:p></o:p></p>

<p>Aguardo retorno,<o:p></o:p></p>

<p><b>{0}<o:p></o:p></b></p>

<p>CSF Digital</p>", operador, Requisicao, Contato, Serie, Modelo, Endereco, Telefone, DescricaoFalha);
    }

    public static bool Agendar(string idreqAtendimento, string data, string operador)
    {
        string tsql = string.Format(@"exec AgendarAtedimentoTecnico {0}, '{1}', '{2}';", idreqAtendimento, data, operador);
        return DAO.ExecuteNonQuery(DAO.connString(), tsql);
    }

    public static bool Concluir(string idreqAtendimento, string data, string operador, string Pendencias)
    {
        string tsql = string.Format(@"exec ConcluirAtendimentoTecnico {0}, '{1}', {2}, '{3}';", idreqAtendimento, operador, Pendencias, data);
        return DAO.ExecuteNonQuery(DAO.connString(), tsql);
    }

    public static bool SolicitarRetorno(string idreqAtendimento, string operador)
    {
        string tsql = string.Format(@"exec SolicitarRetorno {0},  '{1}';", idreqAtendimento, operador);
        return DAO.ExecuteNonQuery(DAO.connString(), tsql);
    }

    public static bool Cancelar(string idreqAtendimento, string operador, string obs)
    {
        string tsql = string.Format(@"exec CancelarAtendimentoTecnico {0}, '{1}', '{2}'", idreqAtendimento, operador, obs);
        return DAO.ExecuteNonQuery(DAO.connString(), tsql);
    }
}