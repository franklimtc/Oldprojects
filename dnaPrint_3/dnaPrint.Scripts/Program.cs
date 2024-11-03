using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceProcess;
using dnaPrint.Log;
using Microsoft.Win32;

namespace dnaPrint.Scripts
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Compartilhar Pasta

            string dir = Directory.GetCurrentDirectory();

            string sPath = Path.GetDirectoryName(dir);
            string sTmpPath = sPath;

            // ********* REMOVER *************
            //MessageBox.Show("Path: " + sTmpPath, "Tz0 info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //if (!Directory.Exists(sTmpPath))
            //    Directory.CreateDirectory(sTmpPath);

            // Pega a seguranÃ§a atual da pasta
            DirectorySecurity oDirSec = Directory.GetAccessControl(sTmpPath);

            // Define o usuÃ¡rio Everyone (Todos)
            SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
            //SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);
            NTAccount oAccount = sid.Translate(typeof(NTAccount)) as NTAccount;

            oDirSec.PurgeAccessRules(oAccount);

            FileSystemAccessRule fsAR = new FileSystemAccessRule(oAccount,
                                                                 FileSystemRights.Modify,
                                                                 InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                                                                 PropagationFlags.None,
                                                                 AccessControlType.Allow);

            // Atribui a regra de acesso alterada
            oDirSec.SetAccessRule(fsAR);
            try
            {
                Directory.SetAccessControl(sTmpPath, oDirSec);
            }
            catch (Exception ex)
            {
                string connstring = @"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\dnaPrintDB.mdf;integrated security=True;MultipleActiveResultSets=True";
                Log.Log.Adicionar(connstring, Log.Log.Tipo.Erro, ex.Message);
            }

            #endregion

            #region Iniciar Serviço

            //string serviceName = "dnaPrint";
            //double timeoutMilliseconds = 10000;
            //ServiceController service = new ServiceController(serviceName);
            //try
            //{
            //    TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
            //    service.Refresh();
            //    if (service.Status == ServiceControllerStatus.Stopped)
            //    {
            //        service.Start();
            //        service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            //    }
            //    else
            //    {
            //        //throw new Exception(string.Format("{0} --> já esta iniciado.", service.DisplayName));
            //    }
            //}
            //catch
            //{
            //    //throw;
            //}

            #endregion

            #region Inicializar com o Windows

            try
            {
                //Nome a ser exibido no registro ou quando Der MSCONFIG - Pode Alterar
                string appName = "dnaPrint";

                //Diretorio da chave do Registro NAO ALTERAR
                //string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

                //Abre o registro
                RegistryKey startupKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                //Valida se vai incluir o iniciar com o Windows ou remover
                if (true)//Iniciar
                {
                    if (startupKey.GetValue(appName) == null)
                    {
                        // Add startup reg key
                        startupKey.SetValue(appName, @"""" + Directory.GetCurrentDirectory() + "\\dnaPrint.Config.exe" + @"""");
                        startupKey.Close();
                    }
                }
                //else//Nao iniciar mais
                //{
                //    // remove startup
                //    startupKey = Registry.LocalMachine.OpenSubKey(runKey, true);
                //    startupKey.DeleteValue(appName, false);
                //    startupKey.Close();
                //}
            }
            catch 
            {
                //MessageBox.Show(ex.Message);
            }

            #endregion

        }
    }
}
