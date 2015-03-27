namespace OpenIDConnect.Inspector.Controls
{
    using System.Text;
    using System.Windows.Forms;

    public partial class SimpleTextView : UserControl
    {
        public override string Text
        {
            get { return this.textBox.Text; }
            set { this.textBox.Text = value; }
        }

        public SimpleTextView()
        {
            InitializeComponent();
        }

        public void AppendLine(string value)
        {
            var sb = new StringBuilder(this.Text);
            sb.AppendLine(value);
            this.Text = sb.ToString();
        }
    }
}