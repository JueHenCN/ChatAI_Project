using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatAI_ChatWindow.UserWindow
{
    public partial class ChatMessageWindow : UserControl
    {
        public ChatMessageWindow()
        {
            InitializeComponent();
        }

        private Label LastMessage;
        private Point Point = new Point(1, 3);

        public void AddUserMessage(string message)
        {
            var msg = CreateMessageLabel(message);
            AddMessageToWindow(true, msg);
        }

        public void AddAIMessage(string message)
        {
            var msg = CreateMessageLabel(message);
            AddMessageToWindow(false, msg);
        }

        private void AddMessageToWindow(bool isUser, Label msg)
        {
            if (isUser)
            {
                msg.Location = Point;
            }
            else
            {
                if (msg.Size.Width > panel2.Width / 2)
                {
                    msg.Location = new Point(panel2.Width / 2, Point.Y);
                }
                else
                {
                    msg.Location = new Point(panel2.Width - msg.Size.Width, Point.Y);
                }
            }
            panel2.Controls.Add(msg);
        }

        private Label CreateMessageLabel(string message)
        {
            Label label = new Label()
            {
                Text = message,
                AutoSize = true,
                Font = new Font("微软雅黑", 12),
                MaximumSize = new Size(panel2.Width / 2, 0)
            };
            
            if (null != LastMessage)
                Point = new Point(1, LastMessage.Location.Y + LastMessage.Height + 3);
            LastMessage = label;
            return label;
        }

        private void Btn_SendMessage_Click(object sender, EventArgs e)
        {
            AddUserMessage(textBox1.Text);
            AddAIMessage(textBox1.Text);
        }

        private void ChatMessageWindow_SizeChanged(object sender, EventArgs e)
        {
            panel2.AutoScrollMinSize = new Size(panel2.Width, 0);
        }
    }
}
