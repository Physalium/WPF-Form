using System.Windows.Controls;
using System.Windows.Media;

namespace Lab1
{
    /// <summary>
    /// Interaction logic for TextBoxWithErrorProvider.xaml
    /// </summary>
    public partial class TextBoxWithErrorProvider : UserControl
    {
        // latwiej utrzymac porzadek
        #region Prop 
        public string Text { get { return textBoxContent.Text; } set { textBoxContent.Text = value; } }
        public static Brush BrushForAll { get; set; } = Brushes.Red;
        public Brush TextBorderBrush
        {
            get;
            set;
        }
        #endregion
        public TextBoxWithErrorProvider()
        {
            InitializeComponent();
            SetError("");

        }

        public void SetError(string errorText)
        {
            if (errorText == "")
            {
                border.BorderThickness = new System.Windows.Thickness(0);
                tooltipW.Visibility = System.Windows.Visibility.Hidden;
                textBlockErrorText.Text = "";
                return;
            }
            textBlockErrorText.Text = errorText;
            border.BorderThickness = new System.Windows.Thickness(1);
            tooltipW.Visibility = System.Windows.Visibility.Visible;

        }
        public void SetFocus()
        {
            textBlockErrorText.Focus();
        }
    }
}
