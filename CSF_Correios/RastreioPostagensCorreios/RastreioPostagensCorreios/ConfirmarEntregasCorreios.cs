using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace RastreioPostagensCorreios
{
    public class ConfirmarEntregasCorreios
    {
        #region Campos
        public enum Status { Pendente, Em_analise, Concluido}
        private static string conexao = ConfigurationManager.ConnectionStrings["dbPecas"].ToString();

        private int _id;
        private string _postagem;
        private string _serie;
        private DateTime _dtEnvio;
        private DateTime _dtEntrega;
        private DateTime _dtConfirmacao;
        private string _operador;
        private string _servico;
        private string _status;
        private string _observacoes;
        public string Email { get; set; }

        public DateTime dtCobrancaEmail { get; set; }

        public int Id { get => _id; set => _id = value; }
        public string Postagem { get => _postagem; set => _postagem = value; }
        public string Serie { get => _serie; set => _serie = value; }
        public DateTime DtEnvio { get => _dtEnvio; set => _dtEnvio = value; }
        public DateTime DtEntrega { get => _dtEntrega; set => _dtEntrega = value; }

        public DateTime DtConfirmacao { get => _dtConfirmacao; set => _dtConfirmacao = value; }
        public string Operador { get => _operador; set => _operador = value; }
        public string Servico { get => _servico; set => _servico = value; }
        public string Status1 { get => _status; set => _status = value; }
        public string Observacoes { get => _observacoes; set => _observacoes = value; }
        #endregion

        public ConfirmarEntregasCorreios()
        {

        }

        public ConfirmarEntregasCorreios(string _post, string _serie, DateTime _dataEnvio, DateTime _dataEntrega, string servico)
        {
            this.Postagem = _post;
            this.Serie = _serie;
            this.DtEnvio = _dataEnvio;
            this.DtEntrega = _dataEntrega;
            this.Servico = servico;
        }

        public void Registrar()
        {
            this.Status1 = Status.Pendente.ToString();

            string tsqlInsert = @"insert into confirmarEntregasCorreios(postagem, serie, dtEnvio, servico, status, dtEntrega)
            values(@postagem, @serie, @dtEnvio, @servico, @status, @dtEntrega)";

            List<object[]> parametros = new List<object[]>();
            parametros.Add(new object[] { "@postagem", this.Postagem });
            parametros.Add(new object[] { "@serie", this.Serie});
            parametros.Add(new object[] { "@dtEnvio", this.DtEnvio});
            parametros.Add(new object[] { "@servico", this.Servico });
            parametros.Add(new object[] { "@status", this.Status1});
            parametros.Add(new object[] { "@dtEntrega", this.DtEntrega});

            new dnaPrint.DAO.SQLServer().ExecuteNonQuery(conexao, tsqlInsert, parametros);
        }

        public static string GerarEmail(string postagem, string data, string serie)
        {
            return @"<html xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:word' xmlns:m='http://schemas.microsoft.com/office/2004/12/omml' xmlns='http://www.w3.org/TR/REC-html40'><head><meta http-equiv=Content-Type content='text/html; charset=utf-8'><meta name=Generator content='Microsoft Word 15 (filtered medium)'><style><!--
/* Font Definitions */
@font-face
	{font-family:'Cambria Math';
	panose-1:2 4 5 3 5 4 6 3 2 4;}
@font-face
	{font-family:Calibri;
	panose-1:2 15 5 2 2 2 4 3 2 4;}
/* Style Definitions */
p.MsoNormal, li.MsoNormal, div.MsoNormal
	{margin-top:0cm;
	margin-right:0cm;
	margin-bottom:8.0pt;
	margin-left:0cm;
	line-height:105%;
	font-size:11.0pt;
	font-family:'Calibri',sans-serif;
	mso-fareast-language:EN-US;}
a:link, span.MsoHyperlink
	{mso-style-priority:99;
	color:#0563C1;
	text-decoration:underline;}
a:visited, span.MsoHyperlinkFollowed
	{mso-style-priority:99;
	color:#954F72;
	text-decoration:underline;}
p.MsoListParagraph, li.MsoListParagraph, div.MsoListParagraph
	{mso-style-priority:34;
	margin-top:0cm;
	margin-right:0cm;
	margin-bottom:8.0pt;
	margin-left:36.0pt;
	line-height:105%;
	font-size:11.0pt;
	font-family:'Calibri',sans-serif;
	mso-fareast-language:EN-US;}
span.EstiloDeEmail17
	{mso-style-type:personal;
	font-family:'Calibri',sans-serif;
	color:windowtext;}
span.EstiloDeEmail18
	{mso-style-type:personal;
	font-family:'Calibri',sans-serif;
	color:#1F497D;}
span.EstiloDeEmail19
	{mso-style-type:personal;
	font-family:'Calibri',sans-serif;
	color:#1F497D;}
span.EstiloDeEmail20
	{mso-style-type:personal-reply;
	font-family:'Calibri',sans-serif;
	color:#1F497D;}
.MsoChpDefault
	{mso-style-type:export-only;
	font-size:10.0pt;}
@page WordSection1
	{size:612.0pt 792.0pt;
	margin:70.85pt 3.0cm 70.85pt 3.0cm;}
div.WordSection1
	{page:WordSection1;}
--></style><!--[if gte mso 9]><xml>
<o:shapedefaults v:ext='edit' spidmax='1026' />
</xml><![endif]--><!--[if gte mso 9]><xml>
<o:shapelayout v:ext='edit'>
<o:idmap v:ext='edit' data='1' />
</o:shapelayout></xml><![endif]--></head><body lang=PT-BR link='#0563C1' vlink='#954F72'><div class=WordSection1><p class=MsoNormal><o:p>&nbsp;</o:p></p><table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 style='border-collapse:collapse'><tr><td width=566 valign=top style='width:424.7pt;background:maroon;padding:0cm 5.4pt 0cm 5.4pt'><p class=MsoNormal align=center style='mso-margin-top-alt:8.0pt;margin-right:0cm;margin-bottom:0cm;margin-left:0cm;margin-bottom:.0001pt;text-align:center;line-height:normal'><b><span style='font-size:20.0pt;color:white'>Comunicado Importante<o:p></o:p></span></b></p><p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'><o:p>&nbsp;</o:p></p></td></tr><tr><td width=566 valign=top style='width:424.7pt;padding:0cm 5.4pt 0cm 5.4pt'><p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'><o:p>&nbsp;</o:p></p><p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:justify;text-indent:22.7pt;line-height:normal'><span style='font-size:14.0pt'>Prezado cliente,<o:p></o:p></span></p><p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:justify;line-height:normal'><span style='font-size:14.0pt'><o:p>&nbsp;</o:p></span></p><p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:justify;text-indent:22.7pt;line-height:normal'><span style='font-size:14.0pt'>Recebemos a confirmação dos correios de que um suprimento (<i>toner ou cilindro</i>) enviado com postagem <b>"
+ postagem + @"</b> foi entregue em sua unidade na data <b>" 
+ data + @"</b> destinado ao equipamento de série <b>" 
+ serie + @"</b>.<o:p></o:p></span></p><p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;text-align:justify;text-indent:22.7pt;line-height:normal'><span style='font-size:14.0pt'>Favor responder esta mensagem confirmando o recebimento do objeto.<o:p></o:p></span></p></td></tr><tr><td width=566 valign=top style='width:424.7pt;padding:0cm 5.4pt 0cm 5.4pt'><p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'><span style='mso-fareast-language:PT-BR'><o:p>&nbsp;</o:p></span></p><div align=center><table class=MsoNormalTable border=0 cellspacing=0 cellpadding=0 style='border-collapse:collapse'><tr style='height:54.15pt'><td width=238 style='width:178.65pt;border:none;border-right:ridge #C00000 2.25pt;padding:0cm 5.4pt 0cm 5.4pt;height:54.15pt'><p class=MsoNormal align=center style='text-align:center'><span style='font-size:14.0pt;line-height:105%'>Atenciosamente,<o:p></o:p></span></p></td><td width=225 style='width:168.45pt;padding:0cm 5.4pt 0cm 5.4pt;height:54.15pt'><p class=MsoNormal><b>Gestão de Suprimentos<br></b><span style='font-size:10.0pt;line-height:105%;color:#404040;mso-fareast-language:PT-BR'>CSF Serviços Digitais Ltda.<br>E-mail:&nbsp;</span><a href='mailto:sac@csfdigital.com.br'>sac<span lang=EN-US>@csfdigital.com.br</span></a><span lang=EN-US><o:p></o:p></span></p></td></tr></table></div><p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'><b><span style='font-size:10.0pt;color:#1A1A1A;mso-fareast-language:PT-BR'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style='letter-spacing:-1.0pt'>&nbsp;&nbsp;&nbsp; </span></span></b><span style='letter-spacing:-1.0pt;mso-fareast-language:PT-BR'><o:p></o:p></span></p><p class=MsoNormal style='margin-bottom:0cm;margin-bottom:.0001pt;line-height:normal'><o:p>&nbsp;</o:p></p></td></tr><tr><td width=566 valign=top style='width:424.7pt;background:maroon;padding:0cm 5.4pt 0cm 5.4pt'><p class=MsoNormal style='margin-top:8.0pt;text-align:justify;line-height:normal'><i><span style='color:white'>* Caso não queira receber estas mensagens, favor indicar o funcionário responsável pelo equipamento informado.<o:p></o:p></span></i></p></td></tr></table><p class=MsoNormal>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <o:p></o:p></p><p class=MsoNormal><o:p>&nbsp;</o:p></p></div></body></html>";
        }

        public static List<ConfirmarEntregasCorreios> ListarEmailsPendentes()
        {
            List<ConfirmarEntregasCorreios> Lista = new List<ConfirmarEntregasCorreios>();
            string query = @"select * from vw_confirmarEntregasCorreios 
                            where status not in ('Concluido') 
                            and email is not null 
                            and DATEDIFF(day, dtCobrancaEmail, getdate()) > 0
                            and email like '%@%.%'";
            DataTable dt = new dnaPrint.DAO.SQLServer().ReturnDt(ConfigurationManager.ConnectionStrings["dbPecas"].ToString(), query);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow post in dt.Rows)
                {
                    ConfirmarEntregasCorreios confirm = new ConfirmarEntregasCorreios();
                    confirm.Id = int.Parse(post["id"].ToString());
                    confirm.Email = post["email"].ToString();
                    confirm.Postagem = post["postagem"].ToString();
                    confirm.Serie = post["serie"].ToString();
                    confirm.DtEntrega = DateTime.Parse(post["dtEntrega"].ToString());
                    Lista.Add(confirm);
                }
            }
            return Lista;
        }

        public int AtualizarDataEmail()
        {
            string query = $@"update confirmarEntregasCorreios set dtcobrancaEmail = Getdate() where id = {this.Id};";
            return new dnaPrint.DAO.SQLServer().ExecuteNonQuery(ConfigurationManager.ConnectionStrings["dbPecas"].ToString(), query);
        }

    }
}
