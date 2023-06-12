using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Inventory
{
    internal class Placeholder
    {
        public void TextBox_Enter(TextBox TE, string text)
        {
            if (TE.Text == text)
            {
                TE.Text = null;
            }
            TE.Font = new Font(TE.Font, FontStyle.Regular);
            TE.ForeColor = Color.Black;
        }
        public void TexBox_Leave(TextBox TE, string text)
        {
            if (TE.Text.Trim() == "")
            {
                TE.Text = text;
                TE.ForeColor = Color.DarkGray;
                TE.Font = new Font(TE.Font, FontStyle.Italic);
            }
            else
            {
                TE.Font = new Font(TE.Font, FontStyle.Regular);
                TE.ForeColor = Color.Black;
            }
        }
    }
}
