namespace OpenIDConnect.Inspector.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    partial class WebFormEditor
    {
        internal DataGridView gridQueryString;
        internal Label lblQueryString;
        private ContextMenuStrip mnuContext;
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
            this.components = new System.ComponentModel.Container();
            this.splitHeadersBody = new System.Windows.Forms.Splitter();
            this.pnlHeaders = new System.Windows.Forms.Panel();
            this.gridQueryString = new System.Windows.Forms.DataGridView();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopyCell = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSendCellToTextWizard = new System.Windows.Forms.ToolStripMenuItem();
            this.lblQueryString = new System.Windows.Forms.Label();
            this.pnlHeaders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridQueryString)).BeginInit();
            this.mnuContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitHeadersBody
            // 
            this.splitHeadersBody.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitHeadersBody.Location = new System.Drawing.Point(0, 277);
            this.splitHeadersBody.Name = "splitHeadersBody";
            this.splitHeadersBody.Size = new System.Drawing.Size(561, 3);
            this.splitHeadersBody.TabIndex = 3;
            this.splitHeadersBody.TabStop = false;
            // 
            // pnlHeaders
            // 
            this.pnlHeaders.Controls.Add(this.gridQueryString);
            this.pnlHeaders.Controls.Add(this.lblQueryString);
            this.pnlHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeaders.Location = new System.Drawing.Point(0, 0);
            this.pnlHeaders.Name = "pnlHeaders";
            this.pnlHeaders.Size = new System.Drawing.Size(561, 277);
            this.pnlHeaders.TabIndex = 6;
            // 
            // gridQueryString
            // 
            this.gridQueryString.AllowUserToAddRows = false;
            this.gridQueryString.AllowUserToDeleteRows = false;
            this.gridQueryString.AllowUserToResizeRows = false;
            this.gridQueryString.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridQueryString.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridQueryString.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gridQueryString.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridQueryString.ContextMenuStrip = this.mnuContext;
            this.gridQueryString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridQueryString.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gridQueryString.EnableHeadersVisualStyles = false;
            this.gridQueryString.Location = new System.Drawing.Point(0, 13);
            this.gridQueryString.Name = "gridQueryString";
            this.gridQueryString.ReadOnly = true;
            this.gridQueryString.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gridQueryString.Size = new System.Drawing.Size(561, 264);
            this.gridQueryString.TabIndex = 5;
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyCell,
            this.tsmiSendCellToTextWizard});
            this.mnuContext.Name = "contextMenuStrip1";
            this.mnuContext.ShowImageMargin = false;
            this.mnuContext.Size = new System.Drawing.Size(160, 48);
            this.mnuContext.Opening += new System.ComponentModel.CancelEventHandler(this.mnuContext_Opening);
            // 
            // tsmiCopyCell
            // 
            this.tsmiCopyCell.Name = "tsmiCopyCell";
            this.tsmiCopyCell.ShortcutKeyDisplayString = "Ctrl+C";
            this.tsmiCopyCell.Size = new System.Drawing.Size(159, 22);
            this.tsmiCopyCell.Text = "&Copy";
            this.tsmiCopyCell.Click += new System.EventHandler(this.tsmiCopyCell_Click);
            // 
            // tsmiSendCellToTextWizard
            // 
            this.tsmiSendCellToTextWizard.Name = "tsmiSendCellToTextWizard";
            this.tsmiSendCellToTextWizard.Size = new System.Drawing.Size(159, 22);
            this.tsmiSendCellToTextWizard.Text = "S&end to TextWizard...";
            this.tsmiSendCellToTextWizard.Click += new System.EventHandler(this.tsmiSendCellToTextWizard_Click);
            // 
            // lblQueryString
            // 
            this.lblQueryString.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblQueryString.Location = new System.Drawing.Point(0, 0);
            this.lblQueryString.Name = "lblQueryString";
            this.lblQueryString.Size = new System.Drawing.Size(561, 13);
            this.lblQueryString.TabIndex = 8;
            this.lblQueryString.Text = "Context";
            // 
            // WebFormEditor
            // 
            this.Controls.Add(this.pnlHeaders);
            this.Controls.Add(this.splitHeadersBody);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "WebFormEditor";
            this.Size = new System.Drawing.Size(561, 280);
            this.pnlHeaders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridQueryString)).EndInit();
            this.mnuContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
