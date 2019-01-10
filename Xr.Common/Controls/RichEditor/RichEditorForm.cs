using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Xr.Common.Controls
{
    public partial class RichEditorForm : Form
    {
        public RichEditorForm()
        {
            InitializeComponent();
        }

        public String text { get; set; }

        private void RichEditorForm_Load(object sender, EventArgs e)
        {
            richEditor1.LoadText(text);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            text = richEditor1.InnerHtml;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
