namespace OpenIDConnect.Inspector.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class WebFormEditor : UserControl
    {
        public WebFormEditor()
        {
            this.InitializeComponent();
            this.gridQueryString.BackgroundColor = Fiddler.CONFIG.colorDisabledEdit;
            this.gridQueryString.ColumnCount = 2;
            this.gridQueryString.Columns[0].Name = "Name";
            this.gridQueryString.Columns[0].MinimumWidth = 40;
            this.gridQueryString.Columns[1].Name = "Value";
            this.gridBody.BackgroundColor = Fiddler.CONFIG.colorDisabledEdit;
            this.gridBody.ColumnCount = 2;
            this.gridBody.Columns[0].Name = "Name";
            this.gridBody.Columns[0].MinimumWidth = 40;
            this.gridBody.Columns[1].Name = "Value";
            this.gridBody.RowHeadersWidth = 0x19;
            this.gridQueryString.RowHeadersWidth = 0x19;
            this.gridBody.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.gridQueryString.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            if (this.gridBody.Font.Size != Fiddler.CONFIG.flFontSize)
            {
                this.gridBody.Font = new Font(this.gridBody.Font.FontFamily, Fiddler.CONFIG.flFontSize);
            }
            if (this.gridQueryString.Font.Size != Fiddler.CONFIG.flFontSize)
            {
                this.gridQueryString.Font = new Font(this.gridQueryString.Font.FontFamily, Fiddler.CONFIG.flFontSize);
            }
        }

        private string getSelectedCellsText()
        {
            DataGridView sourceControl = this.mnuContext.SourceControl as DataGridView;
            if (sourceControl == null)
            {
                return null;
            }
            DataObject clipboardContent = sourceControl.GetClipboardContent();
            if (clipboardContent == null)
            {
                return null;
            }
            return clipboardContent.GetText();
        }

        private void mnuContext_Opening(object sender, CancelEventArgs e)
        {
            bool flag = !string.IsNullOrEmpty(this.getSelectedCellsText());
            this.tsmiCopyCell.Enabled = this.tsmiSendCellToTextWizard.Enabled = flag;
        }

        private void tsmiCopyCell_Click(object sender, EventArgs e)
        {
            string str = this.getSelectedCellsText();
            if (!string.IsNullOrEmpty(str))
            {
                Fiddler.Utilities.CopyToClipboard(str);
            }
        }

        private void tsmiSendCellToTextWizard_Click(object sender, EventArgs e)
        {
            string str = this.getSelectedCellsText();
            if (!string.IsNullOrEmpty(str))
            {
                Fiddler.FiddlerApplication.UI.actShowTextWizard(str);
            }
        }

        public bool BodyIsReadOnly
        {
            get
            {
                return this.gridBody.ReadOnly;
            }
            set
            {
                this.gridBody.ReadOnly = value;
                this.gridBody.RowHeadersVisible = this.gridBody.AllowUserToAddRows = this.gridBody.AllowUserToDeleteRows = !value;
            }
        }

        public bool QueryStringIsReadOnly
        {
            get
            {
                return this.gridQueryString.ReadOnly;
            }
            set
            {
                this.gridQueryString.ReadOnly = value;
                this.gridQueryString.RowHeadersVisible = this.gridQueryString.AllowUserToAddRows = this.gridQueryString.AllowUserToDeleteRows = !value;
            }
        }
    }
}
