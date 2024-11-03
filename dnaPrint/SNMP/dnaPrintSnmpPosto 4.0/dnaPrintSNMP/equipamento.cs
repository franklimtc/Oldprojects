using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Xml;
using Parametros.WebService;

namespace dnaPrintSNMP
{
    public class equipamento
    {
        public enum tpAgente { local, externo };
        #region Campos
        private string _fabricante;
        public string Fabricante
        {
            get { return _fabricante; }
            set { _fabricante = value; }
        }
        private string _idEquipamento;
        public string IdEquipamento
        {
            get { return _idEquipamento; }
            set { _idEquipamento = value; }
        }
        private string _modelo;
        public string Modelo
        {
            get { return _modelo; }
            set { _modelo = value; }
        }
        private string _ip;
        public string Ip
        {
            get { return _ip; }
            set { _ip = value; }
        }
        private List<string[]> listaOids = new List<string[]>();
        private string _firmware;
        public string Firmware
        {
            get { return _firmware; }
            set { _firmware = value; }
        }

        #endregion
        public equipamento(string ip, string idEquipamento, bool DisparoInicial)
        {
            //Configurações iniciais

            this.IdEquipamento = idEquipamento;
            this.Ip = ip;

            //Fim

            string oidPrinter = ".1.3.6.1.4.1.236.11.5.1.1.1.26.0";
            string eqptoPrinter = snmp.getValue(oidPrinter, this.Ip);
            this.Firmware = snmp.getValue(".1.3.6.1.4.1.236.11.5.1.1.1.2.0", this.Ip);
            if (eqptoPrinter.Contains("CLS:PRINTER"))
            {
                string[] dados = eqptoPrinter.Split(';');
                string fabricante = "";
                string modelo = "";

                for (int i = 0; i < dados.Length; i++)
                {
                    if (dados[i].Contains("MFG"))
                        fabricante = dados[i].Replace("MFG:", "");
                    if (dados[i].Contains("MDL"))
                        modelo = dados[i].Replace("MDL:", "");
                }
                this.Fabricante = fabricante;
                this.Modelo = modelo;
            }
        }

        public equipamento(string ip, string idEquipamento)
        {
            this.IdEquipamento = idEquipamento;
            this.Ip = ip;
        }
        public equipamento(string ip)
        {
            this.Ip = ip;
        }

        public bool retornaOids(tpAgente tipo)
        {
            bool result = false;

            string connString = null;
            DataTable dtOids = new DataTable();

            //string sysDescri = "Samsung SCX-6x55X Series; V2.00.03.09 03-18-2014;Engine 0.41.69;NIC V5.01.87(SCX-6x55X) 03-18-2014;S/N ZESKBQBFC000C9L";
            string sysDescri = snmp.getValue(".1.3.6.1.2.1.1.1.0", this.Ip);

            if (!sysDescri.Contains("Falha na consulta SNMP") && sysDescri != null)
            {

                switch (tipo)
                {
                    case tpAgente.local:
                        connString = Util.Descriptografar(parametros.retornaParametro("connectionString"));
                        break;
                    case tpAgente.externo:
                        //listaOidsExterna = Parametros.Metodos.RetornaOids();
                        break;
                }

                retornaFabricante(sysDescri);
                retornaModelo(sysDescri, connString, tipo);
                retornaFirmware(sysDescri, connString, tipo);

                if (this.Fabricante != null && this.Modelo != null && this.Firmware != null)
                {
                    

                    switch (tipo)
                    {
                        case tpAgente.local:
                            string tsqllistaOids = string.Format("select propriedade, oid from listaOids where modelo = '{0}' and firmware = '{1}'", this.Modelo, this.Firmware);
                            dtOids = DAO.RetornaDt(connString, tsqllistaOids);
                            if (dtOids.Rows.Count > 0)
                            {
                                result = true;
                                log.escrever("Oids", string.Format("{2} - {0} oids encontradas para o modelo {1}.", dtOids.Rows.Count.ToString(), this.Modelo, this.Ip));
                                foreach (DataRow r in dtOids.Rows)
                                {
                                    string[] oid = new string[3];
                                    oid[0] = r["propriedade"].ToString();
                                    oid[1] = r["oid"].ToString();
                                    if (!listaOids.Contains(oid))
                                        this.listaOids.Add(oid);
                                }
                            }
                            else
                            {
                                log.escrever("Oids", string.Format("{1} - Nenhuma oid foi encontrada para o modelo {0}.", this.Modelo, this.Ip));
                            }
                            break;
                        case tpAgente.externo:
                            Parametros.WebService.Oids[] listaOidsExterna = Parametros.Metodos.RetornaOidsParcial(this.Fabricante, this.Modelo, this.Firmware);
                            if(listaOidsExterna.Length > 0)
                            {
                                result = true;
                                foreach (Oids oid in listaOidsExterna)
                                {
                                    string[] oidTemp = new string[3];
                                    oidTemp[0] = oid.Propriedade;
                                    oidTemp[1] = oid.Oid;
                                    if (!listaOids.Contains(oidTemp))
                                        this.listaOids.Add(oidTemp);
                                }
                            }
                            else
                            {
                                log.escrever("Oids", string.Format("{1} - Nenhuma oid foi encontrada para o modelo {0}.", this.Modelo, this.Ip)); 
                            }
                            break;
                    }
                    
                }
            }
            else
            {
                log.escrever("Leitura SNMP", string.Format("{0} - Falha na leitura do equipamento.", this.Ip));
            }

            return result;
        }

        public bool disparoSnmp()
        {
            bool result = false;
            List<string[]> resposta = snmp.getList(this.listaOids, this.Ip);
            if (resposta.Count > 2)
            {
                result = true;
                log.escrever("Disparo Snmp", string.Format("{0} - Disparo snmp efetuado para o equipamento.", this.Ip));
                foreach (string[] rFinal in this.listaOids)
                {
                    foreach (string[] rParcial in resposta)
                    {
                        if (rFinal[1] == null)
                        {
                            break;
                        }
                        else
                        {
                            if (rFinal[1].Contains(rParcial[0]))
                            {
                                rFinal[2] = rParcial[1];
                                resposta.Remove(rParcial);
                                break;
                            }
                        }
                    }
                }
            }
        
            return result;
        }

        public bool gravar()
        {
            bool result = false;
            if (this.listaOids.Count == 0)
            {
                //Perfil não foi encontrado.
                //Criar log de erro.
            }
            else
            {
                if (validaDisparo())
                {
                    string inicio = null;
                    string fim = null;
                    foreach (string[] s in listaOids)
                    {
                        if (inicio != null)
                            inicio += "," + s[0];
                        else
                            inicio = "insert into dadosDisparos(idEquipamento," + s[0];

                        if (fim != null)
                            fim += "," + testarInt(s[2]);
                        else
                            fim = " values(" + this.IdEquipamento + "," + testarInt(s[2]);
                    }
                    inicio += ") " + fim + ")";
                    //Console.WriteLine(inicio);
                    if (DAO.ExecutaSQL(Util.Descriptografar(parametros.retornaParametro("connectionString")), inicio))
                    {
                        result = true;
                        log.escrever("Gravar", string.Format("{0} - Disparo armazenado.", this.Ip));
                    }
                }
            }
            return result;
        }

        public bool gravar(bool Externo)
        {
            bool result = true;
            if (this.listaOids.Count == 0)
            {
                //Perfil não foi encontrado.
                //Criar log de erro.
            }
            else
            {
                if (validaDisparo())
                {
                    foreach (string[] s in listaOids)
                    {
                        if (s[0].Contains("serie"))
                        {
                            this.IdEquipamento = Parametros.Metodos.retornaID(s[2]).ToString();
                            break;
                        }
                    }

                    string inicio = null;
                    string fim = null;

                    foreach (string[] s in listaOids)
                    {
                        if (inicio != null)
                            inicio += "," + s[0];
                        else
                        {
                            if (int.Parse(this.IdEquipamento) > 0)
                            {
                                inicio = "insert into dadosDisparos(idEquipamento," + s[0];
                            }
                            else
                            {
                                inicio = "insert into dadosDisparosErros(idEquipamento," + s[0];
                            }
                        }



                        if (fim != null)
                            fim += "," + testarInt(s[2]);
                        else
                            fim = " values(" + this.IdEquipamento + "," + testarInt(s[2]);
                    }
                    inicio += ") " + fim + ")";

                    Parametros.Metodos.CadastrarDisparo(inicio);
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }

        public string testarInt(string value)
        {
            string result = null;
            if (value == "")
            {
                result = "null";
            }
            else
            {
                int rInt = 0;
                if (int.TryParse(value, out rInt))
                {
                    result = rInt.ToString();
                }
                else
                {
                    result = "'" + value + "'";
                }
            }
            return result;
        }

        public static List<equipamento> retornaEquipamentosxml(string ArquivoXml)
        {
            List<equipamento> lista = new List<equipamento>();
            XmlTextReader reader = new XmlTextReader(ArquivoXml);
            if (File.Exists(ArquivoXml))
            {
                while (reader.Read())
                {
                    if (reader.AttributeCount > 0)
                    {
                        string ip = null;
                        reader.MoveToAttribute("ip");
                        ip = reader.Value;
                        equipamento eqp = new equipamento(ip);
                        lista.Add(eqp);
                    }
                }
            }
           
            if (lista.Count == 0)
            {
                log.escrever("Equipamentos", "Nenhum equipamento cadastrado!");
            }
            return lista;
        }

        public bool validaDisparo()
        {
            bool result = false;
            int count = 0;
            foreach (string[] s in listaOids)
            {
                if (s[2] == null)
                    count++;
            }
            if (count < 3 && this.listaOids.Count > 1)
                result = true;
            return result;
        }

        internal static List<equipamento> retornaEquipamentos()
        {
            string tsql = "select idEquipamento, ip from CadastroEquipamentos";
            DataTable dtEquipamentos = DAO.RetornaDt(parametros.retornaParametro("connectionString"), tsql);
            List<equipamento> lEquipamentos = new List<equipamento>();
            foreach (DataRow eqp in dtEquipamentos.Rows)
            {
                equipamento e = new equipamento(eqp["ip"].ToString(), eqp["idEquipamento"].ToString());
                //if (!e.Firmware.Contains("ERRO"))
                lEquipamentos.Add(e);
            }
            return lEquipamentos;
        }

        internal void gravarxml()
        {
            string content = null;
            foreach (string[] s in this.listaOids)
            {
                if (content == null)
                {
                    content = "<disparo " + s[0].Trim() + "=" + '"' + s[2].Trim() + '"' + " ";
                }
                else
                {
                    content += s[0].Trim() + "=" + '"' + s[2].Trim() + '"' + " ";
                }
            }
            content += @"/>";
            content = @"<dadosdisparos>" + content + @"</dadosdisparos>";
            DAO.GerarTXT(string.Format(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/dados//" + GerarNome("xml")), content);
            log.escrever("Gravar", string.Format("{0} - Arquivo xml gravado.", this.Ip));
        }

        public static string GerarNome(string extensao)
        {
            return DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + "." + extensao;
        }

        void retornaFabricante(string SysDescri)
        {
            if (SysDescri != null)
            {
                if (SysDescri.ToLower().Contains("xerox"))
                {
                    this.Fabricante = "Xerox";
                }
                else
                {
                    if (SysDescri.ToLower().Contains("samsung"))
                    {
                        this.Fabricante = "Samsung";
                    }
                    else
                    {
                        if (SysDescri.ToLower().Contains("kyocera"))
                        {
                            this.Fabricante = "Kyocera";
                        }
                        else
                        {
                            log.escrever("Fabricante", string.Format("{0} - Nenhum fabricante encontrado.", this.Ip));

                        }
                    }
                }
            }
        }
        void retornaModelo(string SysDescri, string connString, tpAgente tipo)
        {
            DataTable dtModelos = null;
            switch (tipo)
            {
                case tpAgente.local:
                    dtModelos = DAO.RetornaDtCompact(connString, string.Format("select distinct modelo from listaOids where fabricante = '{0}';", this.Fabricante));
                    foreach (DataRow mod in dtModelos.Rows)
                    {
                        if (SysDescri.Contains(mod[0].ToString().Trim()))
                        {
                            this.Modelo = mod[0].ToString().Trim();
                        }
                    }
                    break;
                case tpAgente.externo:
                    Parametros.WebService.Modelos[] listaModelosExterna = Parametros.Metodos.RetornaModelos();
                    foreach (Modelos mod in listaModelosExterna)
                    {
                        if (SysDescri.Contains(mod.Modelo.ToString().Trim()))
                        {
                            this.Modelo = mod.Modelo.ToString().Trim();
                        }  
                    }
                    break;
            }

            
        }
        void retornaFirmware(string SysDescri, string connString, tpAgente tipo)
        {
            DataTable dtFirmwares = null;

            switch (tipo)
            {
                case tpAgente.local:
                    dtFirmwares = DAO.RetornaDt(connString, string.Format("select distinct firmware, idPerfil from listaOids where fabricante = '{0}';", this.Fabricante));
                    foreach (DataRow firm in dtFirmwares.Rows)
                    {
                        if (SysDescri.Contains(firm[0].ToString().Trim()))
                        {
                            this.Firmware = firm[0].ToString().Trim();
                            string[] perfil = new string[3];
                            perfil[0] = "idPerfil";
                            perfil[2] = firm[1].ToString().Trim();
                            this.listaOids.Add(perfil);

                            string[] data = new string[3];
                            data[0] = "data";
                            data[2] = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                            this.listaOids.Add(data);
                        }
                    }
                    break;
                case tpAgente.externo:
                    Parametros.WebService.Firmwares[] listaFirmwaresExterna = Parametros.Metodos.RetornaFirmwares(this.Fabricante, this.Modelo);
                    foreach (Firmwares firm in listaFirmwaresExterna)
                    {
                        if (SysDescri.Contains(firm.Firmwares1))
                        {
                            this.Firmware = firm.Firmwares1;

                            string[] perfil = new string[3];
                            perfil[0] = "idPerfil";
                            perfil[2] = firm.IdFirmware;
                            this.listaOids.Add(perfil);

                            string[] data = new string[3];
                            data[0] = "data";
                            data[2] = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                            this.listaOids.Add(data);
                        }
                    }
                    break;
            }


          
        }

        public static List<equipamento> retornaEquipamentosLocais()
        {
            List<equipamento> lista = new List<equipamento>();
            DataTable dtEquipamentos = DAO.RetornaDtCompact(connStringCompat(), "select * from equipamentos");
            if (dtEquipamentos.Rows.Count > 0)
            {
                foreach (DataRow eqp in dtEquipamentos.Rows)
                {
                    equipamento eqptemp = new equipamento(eqp["ip"].ToString().Trim(), eqp["id"].ToString().Trim());
                    lista.Add(eqptemp);
                }
            }
            return lista;
        }

        internal static string connStringCompat()
        {
            return string.Format("Data Source={0};Password=Senh@123", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\config\oids.sdf");
        }
    }
}