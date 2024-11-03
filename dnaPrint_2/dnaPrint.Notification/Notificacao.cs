using System.Drawing;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace dnaPrint.Notification
{
    public class Notificacao
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

        public void Exibir()
        {
            PopupNotifier popup = new PopupNotifier();
            popup.Image = Properties.Resources.logo02_small;
            popup.ImageSize = new Size(107, 41);
            popup.ImagePadding = new Padding(20);
            popup.ContentText = this.Mensagem;
            popup.TitleText = this.Titulo;
            popup.TitlePadding = new Padding(10);
            popup.Popup();
        }
    }
}
