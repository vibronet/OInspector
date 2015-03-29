namespace OpenIDConnect.Inspector.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    partial class WebFormEditor
    {
        internal DataGridView gridBody;
        internal DataGridView gridQueryString;
        internal Label lblBody;
        internal Label lblQueryString;
        private ContextMenuStrip mnuContext;
        internal Panel pnlBody;
        internal Panel pnlHeaders;
        private Splitter splitHeadersBody;
        private ToolStripMenuItem tsmiCopyCell;
        private ToolStripMenuItem tsmiSendCellToTextWizard;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new Container();
            this.splitHeadersBody = new Splitter();
            this.pnlHeaders = new Panel();
            this.gridQueryString = new DataGridView();
            this.mnuContext = new ContextMenuStrip(this.components);
            this.tsmiCopyCell = new ToolStripMenuItem();
            this.tsmiSendCellToTextWizard = new ToolStripMenuItem();
            this.lblQueryString = new Label();
            this.pnlBody = new Panel();
            this.gridBody = new DataGridView();
            this.lblBody = new Label();
            this.pnlHeaders.SuspendLayout();
            ((ISupportInitialize)this.gridQueryString).BeginInit();
            this.mnuContext.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((ISupportInitialize)this.gridBody).BeginInit();
            base.SuspendLayout();
            this.splitHeadersBody.Dock = DockStyle.Bottom;
            this.splitHeadersBody.Location = new Point(0, 0x85);
            this.splitHeadersBody.Name = "splitHeadersBody";
            this.splitHeadersBody.Size = new Size(0x231, 3);
            this.splitHeadersBody.TabIndex = 3;
            this.splitHeadersBody.TabStop = false;
            this.pnlHeaders.Controls.Add(this.gridQueryString);
            this.pnlHeaders.Controls.Add(this.lblQueryString);
            this.pnlHeaders.Dock = DockStyle.Fill;
            this.pnlHeaders.Location = new Point(0, 0);
            this.pnlHeaders.Name = "pnlHeaders";
            this.pnlHeaders.Size = new Size(0x231, 0x85);
            this.pnlHeaders.TabIndex = 6;
            this.gridQueryString.AllowUserToAddRows = false;
            this.gridQueryString.AllowUserToDeleteRows = false;
            this.gridQueryString.AllowUserToResizeRows = false;
            this.gridQueryString.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridQueryString.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.gridQueryString.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.gridQueryString.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridQueryString.ContextMenuStrip = this.mnuContext;
            this.gridQueryString.Dock = DockStyle.Fill;
            this.gridQueryString.EditMode = DataGridViewEditMode.EditOnEnter;
            this.gridQueryString.EnableHeadersVisualStyles = false;
            this.gridQueryString.Location = new Point(0, 13);
            this.gridQueryString.Name = "gridQueryString";
            this.gridQueryString.ReadOnly = true;
            this.gridQueryString.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.gridQueryString.Size = new Size(0x231, 120);
            this.gridQueryString.TabIndex = 5;
            this.mnuContext.Items.AddRange(new ToolStripItem[] { this.tsmiCopyCell, this.tsmiSendCellToTextWizard });
            this.mnuContext.Name = "contextMenuStrip1";
            this.mnuContext.ShowImageMargin = false;
            this.mnuContext.Size = new Size(160, 0x30);
            this.mnuContext.Opening += new CancelEventHandler(this.mnuContext_Opening);
            this.tsmiCopyCell.Name = "tsmiCopyCell";
            this.tsmiCopyCell.ShortcutKeyDisplayString = "Ctrl+C";
            this.tsmiCopyCell.Size = new Size(0xb8, 0x16);
            this.tsmiCopyCell.Text = "&Copy";
            this.tsmiCopyCell.Click += new EventHandler(this.tsmiCopyCell_Click);
            this.tsmiSendCellToTextWizard.Name = "tsmiSendCellToTextWizard";
            this.tsmiSendCellToTextWizard.Size = new Size(0xb8, 0x16);
            this.tsmiSendCellToTextWizard.Text = "S&end to TextWizard...";
            this.tsmiSendCellToTextWizard.Click += new EventHandler(this.tsmiSendCellToTextWizard_Click);
            this.lblQueryString.Dock = DockStyle.Top;
            this.lblQueryString.Location = new Point(0, 0);
            this.lblQueryString.Name = "lblQueryString";
            this.lblQueryString.Size = new Size(0x231, 13);
            this.lblQueryString.TabIndex = 8;
            this.lblQueryString.Text = "QueryString";
            this.pnlBody.Controls.Add(this.gridBody);
            this.pnlBody.Controls.Add(this.lblBody);
            this.pnlBody.Dock = DockStyle.Bottom;
            this.pnlBody.Location = new Point(0, 0x88);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new Size(0x231, 0x90);
            this.pnlBody.TabIndex = 7;
            this.gridBody.AllowUserToAddRows = false;
            this.gridBody.AllowUserToDeleteRows = false;
            this.gridBody.AllowUserToResizeRows = false;
            this.gridBody.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridBody.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.gridBody.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.gridBody.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridBody.ContextMenuStrip = this.mnuContext;
            this.gridBody.Dock = DockStyle.Fill;
            this.gridBody.EditMode = DataGridViewEditMode.EditOnEnter;
            this.gridBody.EnableHeadersVisualStyles = false;
            this.gridBody.Location = new Point(0, 13);
            this.gridBody.Name = "gridBody";
            this.gridBody.ReadOnly = true;
            this.gridBody.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.gridBody.Size = new Size(0x231, 0x83);
            this.gridBody.TabIndex = 6;
            this.lblBody.Dock = DockStyle.Top;
            this.lblBody.Location = new Point(0, 0);
            this.lblBody.Name = "lblBody";
            this.lblBody.Size = new Size(0x231, 13);
            this.lblBody.TabIndex = 9;
            this.lblBody.Text = "Body";
            base.Controls.Add(this.pnlHeaders);
            base.Controls.Add(this.splitHeadersBody);
            base.Controls.Add(this.pnlBody);
            this.Font = new Font("Tahoma", 8.25f);
            base.Name = "WebFormEditor";
            base.Size = new Size(0x231, 280);
            this.pnlHeaders.ResumeLayout(false);
            ((ISupportInitialize)this.gridQueryString).EndInit();
            this.mnuContext.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            ((ISupportInitialize)this.gridBody).EndInit();
            base.ResumeLayout(false);
        }

        #endregion
    }
}
