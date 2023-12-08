using ChatAI_ChatWindow.Properties;
using ChatAI_ChatWindow.UserWindow;
using ChatAI_Connect.Connect;
using ChatAI_Connect.Connect.TcpConn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatAI_ChatWindow
{
    public partial class ChatAI_MainWindow : Form
    {
        private TcpClientConn clientConn;
        private JsonMessage jsonMessage = new JsonMessage();
        private SidebarWindow sidebar = new SidebarWindow();
        private ChatWindow chatWindow = new ChatWindow();
        private ChatMessageWindow chatMessage = new ChatMessageWindow();

        public ChatAI_MainWindow()
        {
            InitializeComponent();
        }

        private void InitSidebarButtons()
        {
            sidebar.Dock = DockStyle.Fill;
            panel1.Controls.Add(sidebar);

            Button btn = new Button()
            {
                BackgroundImage = Resources.信息_message,
                BackgroundImageLayout = ImageLayout.Zoom,
                FlatStyle = FlatStyle.Flat
            };
            btn.Click += (s, h) =>
            {
                MessageBox.Show("Hello");
            };
            
            sidebar.AddNewButton(btn);
        }

        private void ChatAI_Chat_Load(object sender, EventArgs e)
        {
            InitSidebarButtons();
            chatWindow.Dock = DockStyle.Fill;
            splitContainer2.Panel2.Controls.Add(chatWindow);

            chatMessage.Dock = DockStyle.Fill;
            panel3.Controls.Add(chatMessage);
        }
    }
}
