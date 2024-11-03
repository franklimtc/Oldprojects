using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Parametros
{
    class Program
    {
        public static void Main()
        {
            ExibirMensagem();
            string idAcao = Console.ReadLine();
            switch (idAcao)
            {
                case "1":
                    Console.WriteLine("Digite o endereço desejado:");
                    string ip = Console.ReadLine();
                    DAO.ExecuteCompact(connStringCompat(), string.Format("insert into equipamentos(ip) values('{0}')", ip));
                    break;
                case "2":
                    DataTable dtEquipamentos = DAO.RetornaDtCompact(connStringCompat(), "select * from equipamentos");
                    Console.WriteLine("");
                    Console.WriteLine("Lista de Equipamentos");
                    foreach (DataRow eqpto in dtEquipamentos.Rows)
                    {
                        Console.WriteLine(string.Format("ID: {0} - IP: {1}", eqpto["id"].ToString(), eqpto["ip"].ToString()));
                    }
                    Console.WriteLine("Informe o id do equipamento que deseja excluir.");
                    string id = Console.ReadLine();
                    DAO.ExecuteCompact(connStringCompat(), string.Format("delete equipamentos where id = {0}", id));
                    break;
                case "3":
                    Console.WriteLine("");
                    Console.WriteLine("Digite o tempo entre cada disparo (em minutos):");
                    Console.Write("Tempo: ");
                    string tempo = Console.ReadLine();
                    DAO.ExecuteCompact(connStringCompat(), string.Format("update parametros set value = {0} where id = 3", tempo));
                    break;
                case "4":
                    Console.WriteLine("");
                    Console.WriteLine("Logs dos disparos:");
                    DataTable dtLogs = DAO.RetornaDT(connStringCompat(), "select * from logs where data > getdate()-1");
                    foreach (DataRow log in dtLogs.Rows)
                    {
                        Console.WriteLine(string.Format("ID: {0} - Componente: {1} - Data: {2} ", log["id"].ToString(), log["Componente"].ToString(), log["data"].ToString()));
                        Console.WriteLine(string.Format("Mensagem: {0}", log["mensagem"].ToString()));
                    }
                    Console.WriteLine("Aperte Enter para sair!");
                    Console.Read();
                    break;
                default:
                    Console.WriteLine("Número inválido");
                    break;
            }

        }
        public static void ExibirMensagem()
        {
            Console.WriteLine("Digite o número da ação desejada:");
            Console.WriteLine();
            Console.WriteLine("1 - Adicionar ip de equipamento");
            Console.WriteLine("2 - Excluir ip de equipamento");
            Console.WriteLine("3 - Modificar tempo do disparo");
            Console.WriteLine("4 - Listar logs");
        }

        internal static string connStringCompat()
        {
            return string.Format("Data Source={0};Password=Senh@123", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\config\oids.sdf");
        }
    }
}
