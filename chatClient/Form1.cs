using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace chatClient
{
    public partial class Form1 : Form
    {
        static public UdpClient client = new UdpClient();
        static public IPEndPoint ep = new IPEndPoint(IPAddress.Parse("10.4.37.132"), 11000);
        public Form1()
        {
            InitializeComponent();
            client.Connect(ep);
            client.BeginReceive(new AsyncCallback(onUdpData), client);
        }
        public void onUdpData(IAsyncResult result)
        {

            byte[] data = client.EndReceive(result, ref ep);
            chatLabel.Invoke((MethodInvoker)delegate { chatLabel.Text = Encoding.ASCII.GetString(data); });
            client.BeginReceive(new AsyncCallback(onUdpData), client);
        }
        private void sendButton_Click(object sender, EventArgs e)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(chatTextBox.Text);
            client.Send(bytes, bytes.Length);
            chatTextBox.Text = "";
        }
    }
}