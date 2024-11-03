using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IsEmail" in both code and config file together.
[ServiceContract]
public interface IsEmail
{
    [OperationContract]
    bool Enviar(string emailPara, string emailCopia, string emailCopiaOculta, string titulo, string mensagem, bool html);

    [OperationContract]
    IList<sEmail> Listar();

    [OperationContract]
    string EmailUsuario(string SerieEquipamento);
}
