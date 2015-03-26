namespace OpenIDConnect.Inspector.Controls
{
    using System.Windows.Forms;

    public partial class SimpleTextView : UserControl
    {
        public SimpleTextView()
        {
            InitializeComponent();
        }

        public void SetText(string valueStr)
        {
            this.textBox.Text = valueStr;
        }
    }
}
