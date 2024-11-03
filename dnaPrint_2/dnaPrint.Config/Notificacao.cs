using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace dnaPrint.Config
{
    class Notificacao
    {
        public string Titulo { get; set; }
        public string Mensagem { get; set; }

        public Notificacao()
        {

        }

        public Notificacao(string _titulo, string _mensagem)
        {
            this.Titulo = _titulo;
            this.Mensagem = _mensagem;
        }

        public static void Exibir(string titulo, string mensagem)
        {
            PopupNotifier popup = new PopupNotifier();
            popup.Image = Properties.Resources.logo02_small;
            popup.ImageSize = new Size(107, 41);
            popup.ImagePadding = new Padding(20);
            popup.ContentText = mensagem;
            popup.TitleText = titulo;
            popup.TitlePadding = new Padding(10);
            popup.Delay = 1500;
            popup.Popup();

        }

    }
}
