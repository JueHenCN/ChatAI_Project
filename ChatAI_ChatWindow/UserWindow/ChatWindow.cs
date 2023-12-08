using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace ChatAI_ChatWindow.UserWindow
{
    public partial class ChatWindow : UserControl
    {
        public ChatWindow()
        {
            InitializeComponent();
        }

        private SessionWindow session = new SessionWindow();



        private void ChatWindow_Load(object sender, EventArgs e)
        {
            session.Dock = DockStyle.Fill;
            panel2.Controls.Add(session);
        }

        private void Btn_CreateNewSession_Click(object sender, EventArgs e)
        {
            //OutputMessage outMsg = new OutputMessage()
            //{
            //    type = "creat_session",
            //    session_id = "魏幕青"
            //};
            //jsonMessage.SendTcpMessage(outMsg, clientConn);

            session.AddNewButton("魏幕青");
        }

    }
}
