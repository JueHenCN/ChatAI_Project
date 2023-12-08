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
    public partial class SessionWindow : UserControl
    {

        public Action<string> ButtonClick;

        public SessionWindow()
        {
            InitializeComponent();
        }

        private List<Button> buttons = new List<Button>();
        private Button selBtn;

        public void AddNewButton(string text, string sessionId = "")
        {
            Guid guid = Guid.NewGuid();
            if (!string.IsNullOrEmpty(sessionId))
                guid = Guid.Parse(sessionId);
            buttons.Add(CreateNewSessionButton(text, guid));
            UpdateButtons();
        }

        /// <summary>
        /// 创建会话按钮
        /// </summary>
        private Button CreateNewSessionButton(string sessionName, Guid guid)
        {
            Button btn = new Button()
            {
                BackgroundImageLayout = ImageLayout.Zoom,
                FlatStyle = FlatStyle.Flat,
                Text = sessionName,
                Width = Width - 2,
                Height = 50
            };
            btn.Click += BtnClick;
            
            btn.FlatAppearance.BorderSize = 0;

            return btn;
        }

        private void BtnClick(object sender, EventArgs e)
        {
            if (selBtn != null)
                selBtn.BackColor = Color.Transparent;
            selBtn = sender as Button;
            selBtn.BackColor = SystemColors.AppWorkspace;
            ButtonClick?.Invoke(selBtn.Tag.ToString());
        }

        private void UpdateButtons()
        {
            Controls.Clear();
            int x = 1;
            int y = 1;
            Point point = new Point(x, y);
            foreach (var btn in buttons)
            {
                btn.Location = point;
                y += btn.Height + 1;
                point = new Point(x, y);
                Controls.Add(btn);
            }
        }
    }
}
