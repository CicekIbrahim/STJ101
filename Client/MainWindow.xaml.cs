using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TcpClient clientSocket = new TcpClient();
        public MainWindow()
        {
            InitializeComponent();
            if (!clientSocket.Connected)
            {
                clientSocket.Connect("localhost", 5000);
                var w = this;
                Task.Run(() =>
                {
                    while (true)
                    {
                        byte[] bt = new byte[1024];
                        int bytesRead = clientSocket.Client.Receive(bt);
                        string result = Encoding.ASCII.GetString(bt, 0, bytesRead);
                        w.Dispatcher.Invoke(() =>
                        {
                            w.tbl.Text += "\n" + DateTime.UtcNow.ToString() + ": " + result;
                        });
                    }
                });
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var stream = clientSocket.GetStream();
            var msg = Encoding.ASCII.GetBytes(tb.Text);
            stream.Write(msg, 0, msg.Length);
        }
    }
}
