﻿namespace OpenIDConnect.Inspector.Controls
{
    using System;
    using System.Collections.Specialized;
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
            this.gridQueryString.RowHeadersWidth = 0x19;
            this.gridQueryString.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

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

        public int GetAllGridRowsCount()
        {
            return this.gridQueryString.Rows.Count;
        }

        public NameValueCollection GetAllGridRows()
        {
            var map = new NameValueCollection();
            foreach (DataGridViewRow gridRow in this.gridQueryString.Rows)
            {
                map.Add(gridRow.Cells[0].Value.ToString(), gridRow.Cells[1].Value.ToString());
            }

            return map;
        }

        public void Clear()
        {
            this.gridQueryString.Rows.Clear();
        }

        public void Append(NameValueCollection dataSource)
        {
            foreach (var key in dataSource.AllKeys)
            {
                this.gridQueryString.Rows.Add(key, dataSource[key]);
            }
        }
    }
}
