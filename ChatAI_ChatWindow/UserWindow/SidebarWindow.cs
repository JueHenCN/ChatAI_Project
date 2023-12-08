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
    public partial class SidebarWindow : UserControl
    {
        public SidebarWindow()
        {
            InitializeComponent();
        }

        private List<Button> buttons = new List<Button>();

        public void AddNewButton(Button button)
        {
            button.Width = Width - 10;
            button.Height = button.Width;

            buttons.Add(button);

            UpdateButtons();
        }

        private void UpdateButtons()
        {
            int x = 4;
            int y = 5;
            Point point = new Point(x, y);
            foreach (var btn in buttons)
            {
                btn.FlatAppearance.BorderSize = 0;
                btn.Location = point;
                y += btn.Height + 5;
                point = new Point(x, y);
                Controls.Add(btn);
            }
        }
    }
}
