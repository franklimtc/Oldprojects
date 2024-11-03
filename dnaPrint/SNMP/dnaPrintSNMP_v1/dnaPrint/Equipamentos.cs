using System;
using System.Text;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Net;


namespace dnaPrint
{
    class Equipamentos
    {
        static string msg = "";

        // Tratar Campos NULL par InsertSql
        #region Dados da tabela dbo.equipamentos

        private string _idEquipamento;
        public string IdEquipamento
        {
            get
            {
                if (this._idEquipamento != null)
                {
                    return _idEquipamento;
                }
                else
                {
                    return null;
                }
            }
            set { _idEquipamento = value; }
        }

        private string _fabricante;
        public string Fabricante
        {
            get
            {
                if (this._fabricante != null)
                {
                    return _fabricante;
                }
                else
                {
                    return null;
                }
            }
            set { _fabricante = value.Replace(" ", ""); }
        }

        private string _ip;
        public string Ip
        {
            get
            {
                if (this._ip != null)
                {
                    return _ip;
                }
                else
                {
                    return null;
                }
            }
            set { _ip = value.Replace(" ", ""); }
        }

        private string _cor;

        public string Cor
        {
            get { return _cor; }
            set { _cor = value; }
        }


        private string _idPerfil;

        public string IdPerfil
        {
            get
            {
                if (this._idPerfil != null)
                {
                    return _idPerfil;
                }
                else
                {
                    _idPerfil = "null";
                    return _idPerfil;
                }
            }
            set { _idPerfil = value; }
        }

        #endregion

        #region Dados de Verificação

        private string _firmware;
        public string Firmware
        {
            get
            {
                if (this._firmware != null && this._firmware != "")
                {
                    return _firmware;
                }
                else
                {
                    return null;
                }
            }
            set { _firmware = value; }
        }

        private string _grupo;
        public string Grupo
        {
            get
            {
                if (this._grupo != null && this._grupo != "")
                {
                    return _grupo;
                }
                else
                {
                    return null;
                }
            }
            set { _grupo = value; }
        }

        private string data;

        public string Data
        {
            get { return data; }
            set { data = value; }
        }

        #endregion

        #region Dados da tabela dbo.DadosDisparos

        private string _localizacao;

        public string Localizacao
        {
            get { return _localizacao; }
            set { _localizacao = value; }
        }

        private string _modelo;

        public string Modelo
        {
            get
            {
                if (this._modelo != null && this._modelo != "")
                {
                    return _modelo;
                }
                else
                {
                    return null;
                }
            }
            set { _modelo = value; }
        }
        private string _nome;

        public string Nome
        {
            get
            {
                if (this._nome != null && this._nome != "")
                {
                    return _nome;
                }
                else
                {
                    return null;
                }
            }

            set { _nome = value; }
        }
        private string _serie;

        public string Serie
        {
            get
            {
                if (this._serie != null && this._serie != "")
                {
                    return _serie;
                }
                else
                {
                    return null;
                }
            }
            set { _serie = value; }
        }
        private string _impr_pf_color;

        public string Impr_pf_color
        {
            get
            {
                if (_impr_pf_color != null && _impr_pf_color != "")
                    return _impr_pf_color;
                else
                    return "0";
            }
            set { _impr_pf_color = value; }
        }
        private string _impr_pf_mono;

        public string Impr_pf_mono
        {
            get
            {
                if (_impr_pf_mono != null && _impr_pf_mono != "")
                    return _impr_pf_mono;
                else
                    return "0";
            }
            set { _impr_pf_mono = value; }
        }
        private string _impr_gf_color;

        public string Impr_gf_color
        {
            get
            {
                if (_impr_gf_color != null && _impr_gf_color != "")
                    return _impr_gf_color;
                else
                    return "0";
            }
            set { _impr_gf_color = value; }
        }
        private string _impr_gf_mono;

        public string Impr_gf_mono
        {
            get
            {
                if (_impr_gf_mono != null && _impr_gf_mono != "")
                    return _impr_gf_mono;
                else
                    return "0";
            }
            set { _impr_gf_mono = value; }
        }
        private string _copia_pf_color;

        public string Copia_pf_color
        {
            get
            {
                if (_copia_pf_color != null && _copia_pf_color != "")
                    return _copia_pf_color;
                else
                    return "0";
            }
            set { _copia_pf_color = value; }
        }
        private string _copia_pf_mono;

        public string Copia_pf_mono
        {
            get
            {
                if (_copia_pf_mono != null && _copia_pf_mono != "")
                    return _copia_pf_mono;
                else
                    return "0";
            }
            set { _copia_pf_mono = value; }
        }
        private string _copia_gf_color;

        public string Copia_gf_color
        {
            get
            {
                if (_copia_gf_color != null && _copia_gf_color != "")
                    return _copia_gf_color;
                else
                    return "0";
            }
            set { _copia_gf_color = value; }
        }
        private string _copia_gf_mono;

        public string Copia_gf_mono
        {
            get
            {
                if (_copia_gf_mono != null && _copia_gf_mono != "")
                    return _copia_gf_mono;
                else
                    return "0";
            }
            set { _copia_gf_mono = value; }
        }
        private string _fax_pf_color;

        public string Fax_pf_color
        {
            get
            {
                if (_fax_pf_color != null && _fax_pf_color != "")
                    return _fax_pf_color;
                else
                    return "0";
            }
            set { _fax_pf_color = value; }
        }
        private string _fax_pf_mono;

        public string Fax_pf_mono
        {
            get
            {
                if (_fax_pf_mono != null && _fax_pf_mono != "")
                    return _fax_pf_mono;
                else
                    return "0";
            }
            set { _fax_pf_mono = value; }
        }
        private string _fax_gf_color;

        public string Fax_gf_color
        {
            get
            {
                if (_fax_gf_color != null && _fax_gf_color != "")
                    return _fax_gf_color;
                else
                    return "0";
            }
            set { _fax_gf_color = value; }
        }
        private string _fax_gf_mono;

        public string Fax_gf_mono
        {
            get
            {
                if (_fax_gf_mono != null && _fax_gf_mono != "")
                    return _fax_gf_mono;
                else
                    return "0";
            }
            set { _fax_gf_mono = value; }
        }
        private string _total_pf_color;

        public string Total_pf_color
        {
            get
            {
                if (_total_pf_color != null && _total_pf_color != "")
                    return _total_pf_color;
                else
                    return "0";
            }
            set { _total_pf_color = value; }
        }
        private string _total_pf_mono;

        public string Total_pf_mono
        {
            get
            {
                if (_total_pf_mono != null && _total_pf_mono != "")
                    return _total_pf_mono;
                else
                    return "0";
            }
            set { _total_pf_mono = value; }
        }
        private string _total_gf_color;

        public string Total_gf_color
        {
            get
            {
                if (_total_gf_color != null && _total_gf_color != "")
                    return _total_gf_color;
                else
                    return "0";
            }
            set { _total_gf_color = value; }
        }
        private string _total_gf_mono;

        public string Total_gf_mono
        {
            get
            {
                if (_total_gf_mono != null && _total_gf_mono != "")
                    return _total_gf_mono;
                else
                    return "0";
            }
            set { _total_gf_mono = value; }
        }


        private string[] _qtdTonerPr = new string[2];

        public void QtdTonerPr(int i, string valor)
        {
            this._qtdTonerPr[i] = valor;
        }
        public string QtdTonerPr()
        {
            string c = "null";
            if (this._qtdTonerPr[0] != null && this._qtdTonerPr[1] != null)
            {
                string a = this._qtdTonerPr[0];
                string b = this._qtdTonerPr[1];
                if (a != "" && b != "")
                {
                    c = ((int)((float.Parse(b) / float.Parse(a)) * 100)).ToString();
                }
            }
            return c;
        }

        private string[] _qtdTonerCi = new string[2];

        public void QtdTonerCi(int i, string valor)
        {
            this._qtdTonerCi[i] = valor;
        }
        public string QtdTonerCi()
        {
            string c = "null";
            if (this._qtdTonerCi[0] != null && this._qtdTonerCi[1] != null && _cor == "S")
            {
                string a = this._qtdTonerCi[0];
                string b = this._qtdTonerCi[1];
                if (a != "" && b != "")
                {
                    c = ((int)((float.Parse(b) / float.Parse(a)) * 100)).ToString();
                }
            }
            return c;
        }

        private string[] _qtdTonerAm = new string[2];

        public void QtdTonerAm(int i, string valor)
        {
            this._qtdTonerAm[i] = valor;
        }
        public string QtdTonerAm()
        {
            string c = "null";
            if (this._qtdTonerAm[0] != null && this._qtdTonerAm[1] != null && _cor == "S")
            {
                string a = this._qtdTonerAm[0];
                string b = this._qtdTonerAm[1];
                if (a != "" && b != "")
                {
                    c = ((int)((float.Parse(b) / float.Parse(a)) * 100)).ToString();
                }
            }
            return c;
        }


        private string[] _qtdTonerMa = new string[2];

        public void QtdTonerMa(int i, string valor)
        {
            this._qtdTonerMa[i] = valor;
        }
        public string QtdTonerMa()
        {
            string c = "null";
            if (this._qtdTonerMa[0] != null && this._qtdTonerMa[1] != null && _cor == "S")
            {
                string a = this._qtdTonerMa[0];
                string b = this._qtdTonerMa[1];
                if (a != "" && b != "")
                {
                    c = ((int)((float.Parse(b) / float.Parse(a)) * 100)).ToString();
                }
            }
            return c;
        }

        private string[] _qtdCilindro = new string[2];

        public void QtdCilindro(int i, string valor)
        {
            this._qtdCilindro[i] = valor;
        }
        public string QtdCilindro()
        {
            string c = "null";
            if (this._qtdCilindro[0] != null && this._qtdCilindro[1] != null)
            {
                string a = this._qtdCilindro[0];
                string b = this._qtdCilindro[1];
                if (a != "" && b != "")
                {
                    c = ((int)((float.Parse(b) / float.Parse(a)) * 100)).ToString();
                }
            }
            return c;
        }


        private string _mac;

        public string Mac
        {
            get
            {
                if (_mac != null)
                {
                    return _mac;
                }
                else
                {
                    _mac = "null";
                    return _mac;
                }
            }
            set { _mac = value; }
        }

        #endregion

        public bool StatusDados()
        {
            bool status = false;

            if (
                    this.IdEquipamento != null
                    && this.Serie != null
                    && this.Ip != null
                    && this.Impr_pf_mono != null
                    && this.Copia_pf_mono != null
                    && this.QtdTonerPr() != null
                )
            {
                status = true;
            }
            return status;
        }

        public void LerDados(List<Oids> ListaOids)
        {
            this.Data = DateTime.Now.ToString();

            int contOids = 0;

            foreach (Oids oid in ListaOids)
            {
                if (this.Firmware != null && this.Firmware != "" && this.Firmware != " ")
                {
                    if (this.Firmware.Contains(oid.Firmware))
                    {
                        contOids++;
                        this.Firmware = oid.Firmware;
                        this.IdPerfil = oid.IdPerfil;
                        switch (oid.Propriedade)
                        {
                            #region Case

                            case "modelo":
                                this.Modelo = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            case "nome":
                                this.Nome = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            case "serie":
                                this.Serie = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            case "localizacao":
                                this.Localizacao = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            case "impr_pf_color":
                                this.Impr_pf_color = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            case "impr_pf_mono":
                                this.Impr_pf_mono = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            case "impr_gf_color":
                                this.Impr_gf_color = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            case "impr_gf_mono":
                                this.Impr_gf_mono = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            case "copia_pf_color":
                                this.Copia_pf_color = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            case "copia_pf_mono":
                                this.Copia_pf_mono = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            case "copia_gf_color":
                                this.Copia_gf_color = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            case "copia_gf_mono":
                                this.Copia_gf_mono = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            case "fax_pf_color":
                                this.Fax_pf_color = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            case "fax_pf_mono":
                                this.Fax_pf_mono = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            case "fax_gf_color":
                                this.Fax_gf_color = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            case "fax_gf_mono":
                                this.Fax_gf_mono = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            case "total_pf_color":
                                this.Total_pf_color = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            case "total_pf_mono":
                                this.Total_pf_mono = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            case "total_gf_color":
                                this.Total_gf_color = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            case "total_gf_mono":
                                this.Total_gf_mono = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            case "toner_total_pr":
                                this.QtdTonerPr(0, DAO.ConsultaSNMP(oid.Oid, this.Ip));
                                break;
                            case "toner_atual_pr":
                                this.QtdTonerPr(1, DAO.ConsultaSNMP(oid.Oid, this.Ip));
                                break;
                            case "toner_total_ci":
                                this.QtdTonerCi(0, DAO.ConsultaSNMP(oid.Oid, this.Ip));
                                break;
                            case "toner_atual_ci":
                                this.QtdTonerCi(1, DAO.ConsultaSNMP(oid.Oid, this.Ip));
                                break;
                            case "toner_total_am":
                                this.QtdTonerAm(0, DAO.ConsultaSNMP(oid.Oid, this.Ip));
                                break;
                            case "toner_atual_am":
                                this.QtdTonerAm(1, DAO.ConsultaSNMP(oid.Oid, this.Ip));
                                break;
                            case "toner_total_ma":
                                this.QtdTonerMa(0, DAO.ConsultaSNMP(oid.Oid, this.Ip));
                                break;
                            case "toner_atual_ma":
                                this.QtdTonerMa(1, DAO.ConsultaSNMP(oid.Oid, this.Ip));
                                break;
                            case "cilindro_total":
                                this.QtdCilindro(0, DAO.ConsultaSNMP(oid.Oid, this.Ip));
                                break;
                            case "cilindro_atual":
                                this.QtdCilindro(1, DAO.ConsultaSNMP(oid.Oid, this.Ip));
                                break;
                            case "mac":
                                this.Mac = DAO.ConsultaSNMP(oid.Oid, this.Ip);
                                break;
                            #endregion

                        }
                    }
                }
            }

            if (contOids == 0 && (this.Firmware != null && this.Firmware.Trim() != ""))
            {
                msg = "Modelo " + this.Firmware + " não cadastrado. ";
                Logs.GerarLogs(Logs.TipoLogs.geral, msg);
            }
        }

        public void GravarDados(string connString, Disparo.TipoConexao tipo)
        {
            StringBuilder sqlInsert = new StringBuilder();

            sqlInsert.AppendLine("INSERT INTO [dadosDisparos]");
            sqlInsert.AppendLine(" (");
            sqlInsert.AppendLine("	[idEquipamento],[idPerfil],[modelo],[nome],[serie],[impr_pf_color]");
            sqlInsert.AppendLine("	,[impr_pf_mono],[impr_gf_color],[impr_gf_mono],[copia_pf_color],[copia_pf_mono]");
            sqlInsert.AppendLine("	,[copia_gf_color],[copia_gf_mono],[fax_pf_color],[fax_pf_mono],[fax_gf_color]");
            sqlInsert.AppendLine("	,[fax_gf_mono],[total_pf_color],[total_pf_mono],[total_gf_color],[total_gf_mono],[qtdTonerPr]");
            sqlInsert.AppendLine("	,[qtdTonerCi],[qtdTonerAm],[qtdTonerMa],[qtdCilindro],[mac],[data],[localizacao]");
            sqlInsert.AppendLine(" )");
            sqlInsert.AppendLine(" VALUES");
            sqlInsert.AppendLine(" (");
            sqlInsert.AppendLine(this.IdEquipamento + "," + this.IdPerfil + ",'" + this.Modelo + "','" + this.Nome + "','" + this.Serie + "'," + this.Impr_pf_color + ",");
            sqlInsert.AppendLine(this.Impr_pf_mono + "," + this.Impr_gf_color + "," + this.Impr_gf_mono + "," + this.Copia_pf_color + "," + Copia_pf_mono + ",");
            sqlInsert.AppendLine(this.Copia_gf_color + "," + this.Copia_gf_mono + "," + this.Fax_pf_color + "," + this.Fax_pf_mono + "," + this.Fax_gf_color + ",");
            sqlInsert.AppendLine(this.Fax_gf_mono + "," + this.Total_pf_color + "," + this.Total_pf_mono + "," + this.Total_gf_color + "," + this.Total_gf_mono + "," + this.QtdTonerPr() + ",");
            sqlInsert.AppendLine(this.QtdTonerCi().ToString() + "," + this.QtdTonerAm().ToString() + "," + this.QtdTonerMa().ToString() + "," + this.QtdCilindro().ToString() + ",'" + this.Mac + "', getdate() ,'" + this.Localizacao + "'");
            sqlInsert.AppendLine("");
            sqlInsert.AppendLine(" )");

            if (tipo == Disparo.TipoConexao.sqlserver)
            {
                DAO.ExecutaSQL(connString, sqlInsert.ToString());
            }
            else
            {
                DAO.ExecutaSqlCompact(connString, sqlInsert.ToString());
            }
        }

        public void GravarDados(string Diretorio, string Arquivo)
        {
            if (!File.Exists(Diretorio + Arquivo))
            {
                string Cabecalho = @"idEquipamento|idPerfil|modelo|nome|serie|impr_pf_color|impr_pf_mono|impr_gf_color|impr_gf_mono"
                    + "|copia_pf_color|copia_pf_mono|copia_gf_color|copia_gf_mono|fax_pf_color|fax_pf_mono|fax_gf_color|fax_gf_mono|"
                    + "total_pf_color|total_pf_mono|total_gf_color|total_gf_mono|qtdTonerPr|qtdTonerCi|qtdTonerAm|qtdTonerMa|qtdCilindro|mac|data|localizacao";
                DAO.GerarTXT(Diretorio + Arquivo, Cabecalho);
            }
            string dadosDisparo = this.IdEquipamento.ToString() + "|"
                + this.IdPerfil.ToString() + "|"
                + this.Modelo.ToString() + "|"
                + this.Nome.ToString() + "|"
                + this.Serie.ToString() + "|"
                + this.Impr_pf_color.ToString() + "|"
                + this.Impr_pf_mono.ToString() + "|"
                + this.Impr_gf_color.ToString() + "|"
                + this.Impr_gf_mono.ToString() + "|"
                + this.Copia_pf_color.ToString() + "|"
                + this.Copia_pf_mono.ToString() + "|"
                + this.Copia_gf_color.ToString() + "|"
                + this.Copia_gf_mono.ToString() + "|"
                + this.Fax_pf_color.ToString() + "|"
                + this.Fax_pf_mono.ToString() + "|"
                + this.Fax_gf_color.ToString() + "|"
                + this.Fax_gf_mono.ToString() + "|"
                + this.Total_pf_color.ToString() + "|"
                + this.Total_pf_mono.ToString() + "|"
                + this.Total_gf_color.ToString() + "|"
                + this.Total_gf_mono.ToString() + "|"
                + this.QtdTonerPr().ToString() + "|"
                + this.QtdTonerCi().ToString() + "|"
                + this.QtdTonerAm().ToString() + "|"
                + this.QtdTonerMa().ToString() + "|"
                + this.QtdCilindro().ToString() + "|"
                + this.Mac.ToString() + "|"
                + DateTime.Now.ToString() + "|"
                + this.Localizacao.ToString();

            DAO.GerarTXT(Diretorio + Arquivo, dadosDisparo);
        }

        public void TestarConexao(List<oidsPadrao> ListaOidsPadrao)
        {
            string strOidPadrao = null;
            string msg = "";

            foreach (oidsPadrao oidPadrao in ListaOidsPadrao)
            {
                if (oidPadrao.Fabricante.Contains(this.Fabricante))
                {
                    strOidPadrao = oidPadrao.Oid;
                    break;
                }
            }

            if (strOidPadrao != null)
            {
                try
                {
                    this.Firmware = DAO.ConsultaSNMP(strOidPadrao, this.Ip);
                }
                catch
                {
                    msg = "Falha no disparo SNMP para o equipamento de ip: " + this.Ip;
                    Logs.GerarLogs(Logs.TipoLogs.snmp, msg);
                }
            }
            else
            {
                msg = "Firmware não cadastrado para o ip: " + this.Ip;
                Logs.GerarLogs(Logs.TipoLogs.snmp, msg);
            }
        }

        public static List<Equipamentos> RetornaEqpXml(string ArquivoXml)
        {
            List<Equipamentos> lista = new List<Equipamentos>();
            if (File.Exists(ArquivoXml))
            {

                XmlTextReader reader = new XmlTextReader(ArquivoXml);
                while (reader.Read())
                {
                    if (reader.AttributeCount > 0)
                    {
                        Equipamentos eqp = new Equipamentos();
                        reader.MoveToAttribute("idEquipamento");
                        eqp.IdEquipamento = reader.Value;
                        reader.MoveToAttribute("ip");
                        eqp.Ip = reader.Value;
                        reader.MoveToAttribute("fabricante");
                        eqp.Fabricante = reader.Value;
                        reader.MoveToAttribute("cor");
                        eqp.Cor = reader.Value;
                        reader.MoveToAttribute("grupo");
                        eqp.Grupo = reader.Value;
                        lista.Add(eqp);
                    }
                }
            }
            else
            {
                List<System.Net.IPAddress> list = Portas.RetornaIps();
                if (list.Count > 0)
                {
                    foreach (IPAddress ip in list)
                    {
                        Equipamentos eqp = new Equipamentos();
                        eqp.IdEquipamento = (list.IndexOf(ip) + 1).ToString();
                        eqp.Ip = ip.ToString();
                        if (DAO.ConsultaSNMP("1.3.6.1.2.1.1.1.0", ip.ToString()).Contains("Xerox"))
                        {
                            eqp.Fabricante = "Xerox";
                        }
                        else
                        {
                            eqp.Fabricante = "Kyocera";
                        }
                        
                        eqp.Cor = "n";
                        lista.Add(eqp);
                    }
                }
                else
                {
                    string computador = System.Environment.MachineName;
                    string msg = "Não foi possível localizar nenhum ip válido no computador " + computador;
                    string Assunto = "Falha ao tentar localizar ips locais.";
                    SMTP.Enviar(Assunto, msg);
                }

            }
            return lista;
        }
    }
}