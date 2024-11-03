using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSFDigital.Controls
{
    public enum TipoStatusChamado
    {
        Null,
        Outros,
        NenhumaAlteracaoStatus,
        Solucionado,

        Aberto,
        Aceito,
        Acknowledged,
        AguardandoAutorizacao,
        CausaRaizID,
        CloseRequested,
        ClosedUnresolved,
        Devolvido,
        DevolvidoST,
        EmAtendimento,
        EmClassificacao,
        EmDiagnostico,
        EmEncerramento,
        EmProgresso,
        EmSolucao,
        Encaminhado,
        EncaminhadoST,
        EncaminhadoCE,
        ErroConhecido,
        Fechado,
        FixInProgress,
        Hold,
        Pendente,
        PendenteGSOL,
        PendenteRDM,
        PendenteUsuario,
        ProblemClosed,
        ProblemFixed,
        ProblemOpen,
        Reaberto,
        Rejeitada,
        Researching,
        Resolvido,
        WorkInProgress
    }

    public class Status
    {
        private string _code;
        private string _descricao;
        private TipoStatusChamado _statusOcorrencia = TipoStatusChamado.Null;

        public Status(string code, string descricao, TipoStatusChamado statusOcorrencia)
        {
            _code = code;
            _descricao = descricao;
            _statusOcorrencia = statusOcorrencia;
        }
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
        public TipoStatusChamado StatusOcorrencia
        {
            get { return _statusOcorrencia; }
            set { _statusOcorrencia = value; }
        }       
        public override string ToString()
        {
            return _descricao;
        }
        public static List<Status> ListaStatus()
        {
            List<Status> lista = new List<Status>();

            lista.Add(new Status("OP", "Aberto", TipoStatusChamado.Aberto));
            lista.Add(new Status("ACTO", "Aceito", TipoStatusChamado.Aceito));
            lista.Add(new Status("ACK", "Acknowledged", TipoStatusChamado.Acknowledged));
            lista.Add(new Status("AGA", "Aguardando Autorização", TipoStatusChamado.AguardandoAutorizacao));
            lista.Add(new Status("03", "Causa Raiz ID", TipoStatusChamado.CausaRaizID));
            lista.Add(new Status("CLREQ", "Close Requested", TipoStatusChamado.CloseRequested));
            lista.Add(new Status("CLUNRSLV", "Closed-Unresolved", TipoStatusChamado.ClosedUnresolved));
            lista.Add(new Status("DEVOL", "Devolvido", TipoStatusChamado.Devolvido));
            lista.Add(new Status("DEVST", "Devolvido p/ ST", TipoStatusChamado.DevolvidoST));
            lista.Add(new Status("EMAT", "Em Atendimento", TipoStatusChamado.EmAtendimento));
            lista.Add(new Status("EMCLA", "Em Classificação", TipoStatusChamado.EmClassificacao));
            lista.Add(new Status("EMDIAG", "Em Diagnóstico", TipoStatusChamado.EmDiagnostico));
            lista.Add(new Status("EMENC", "Em Encerramento", TipoStatusChamado.EmEncerramento));
            lista.Add(new Status("EMPROG", "Em Progresso", TipoStatusChamado.EmProgresso));
            lista.Add(new Status("EMSOL", "Em Solução", TipoStatusChamado.EmSolucao));
            lista.Add(new Status("ENC", "Encaminhado", TipoStatusChamado.Encaminhado));
            lista.Add(new Status("ENCST", "Encaminhado p/ ST", TipoStatusChamado.EncaminhadoST));
            lista.Add(new Status("02", "Encaminhado p/CE", TipoStatusChamado.EncaminhadoCE));
            lista.Add(new Status("KE", "Erro Conhecido", TipoStatusChamado.ErroConhecido));
            lista.Add(new Status("CL", "Fechado", TipoStatusChamado.Fechado));
            lista.Add(new Status("FIP", "Fix in Progress", TipoStatusChamado.FixInProgress));
            lista.Add(new Status("HOLD", "Hold", TipoStatusChamado.Hold));
            lista.Add(new Status("PEND", "Pendente", TipoStatusChamado.Pendente));
            lista.Add(new Status("PENDGSOL", "Pendente c/ GSOL", TipoStatusChamado.PendenteGSOL));
            lista.Add(new Status("RDMAb", "Pendente c/ RDM", TipoStatusChamado.PendenteRDM));
            lista.Add(new Status("PENDUSER", "Pendente c/ Usuário", TipoStatusChamado.PendenteUsuario));
            lista.Add(new Status("PC", "Problem-Closed", TipoStatusChamado.ProblemClosed));
            lista.Add(new Status("PF", "Problem-Fixed", TipoStatusChamado.ProblemFixed));
            lista.Add(new Status("PO", "Problem-Open", TipoStatusChamado.ProblemOpen));
            lista.Add(new Status("REAB", "Reaberto", TipoStatusChamado.Reaberto));
            lista.Add(new Status("REJ", "Rejeitada", TipoStatusChamado.Rejeitada));
            lista.Add(new Status("RSCH", "Researching", TipoStatusChamado.Researching));
            lista.Add(new Status("RE", "Resolvido", TipoStatusChamado.Resolvido));
            lista.Add(new Status("WIP", "Work In Progress", TipoStatusChamado.WorkInProgress));
            lista.Add(new Status("SOLN", "Solucionado", TipoStatusChamado.Solucionado));

            return lista;
        }

        public static Status ConvertCodeToStatus(string code, string descricao)
        {
            Status status = ListaStatus().Find(s => s.Code.ToUpper() == code.ToUpper());

            if (status != null)
                return status;
            else
                return new Status(String.Empty, String.Format("{0} - Desconhecido", descricao), TipoStatusChamado.Outros);
        }

        public static Status ConvertDescriptionToStatus(string descricao)
        {
            Status status = ListaStatus().Find(s => s.Descricao.ToUpper() == descricao.Trim().ToUpper());

            if (status != null)
                return status;
            else
                return new Status(String.Empty, String.Format("{0} - Desconhecido", descricao), TipoStatusChamado.Outros);
        }
    }
}