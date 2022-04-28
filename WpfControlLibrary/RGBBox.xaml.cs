using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfControlLibrary
{

    public partial class RGBBox : UserControl
    {
        public RGBBox()
        {
            InitializeComponent();
        }

        public string Text
        { 
            get 
            { 
                return text.Text; 
            } 
            set 
            { 
                text.Text = value; 
            } 
        }

        public bool Hex = false;
        public delegate void TextChangeHandler(object sender, EventArgs e);
        public event TextChangeHandler TextChange;

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(text.Text, " "))
                Text = text.Text.Replace(" ", "");
            else
                Text = text.Text;
            if (Text == " " || Text == "")
                Text = "0";
            if (Hex)
            {
                if (int.Parse(Text, System.Globalization.NumberStyles.HexNumber) > 255)
                    Text = "FF";
            }
            else
            {
                if (int.Parse(Text) > 255)
                    Text = "255";
            }
            TextChange?.Invoke(sender, e);
        }

        public void ToHex()
        {
            Hex = true;
            Text = Convert.ToString(int.Parse(Text), 16);
        }

        public void ToDec()
        {
            Hex = false;
            Text = Convert.ToString(int.Parse(Text, System.Globalization.NumberStyles.HexNumber), 10);
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            string tmp = Clipboard.GetText();
            if (Regex.IsMatch(tmp, "[A-Z]", RegexOptions.IgnoreCase))
            {
                if (int.TryParse(tmp, System.Globalization.NumberStyles.HexNumber, null, out int res))
                {
                    if (Hex)
                        text.Text = tmp;
                    else
                        text.Text = res.ToString();
                }
                else
                    MessageBox.Show("Содержит недопустимые символы!");
            }
            else
            { 
                if (int.TryParse(tmp, out int res) && !Regex.IsMatch(tmp, "-"))
                {
                    if (Hex)
                        text.Text = Convert.ToString(int.Parse(tmp), 16);
                    else
                        text.Text = res.ToString();
                }
                else
                    MessageBox.Show("Содержит недопустимые символы!");
            }
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Text);
        }

        private void text_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.V)
            {
                Paste_Click(sender, e);
                e.Handled = true;
            }
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.C)
            {
                Copy_Click(sender, e);
                e.Handled = true;
            }
            if (Hex)
            {
                if (!(e.Key == Key.A || e.Key == Key.B || e.Key == Key.C || e.Key == Key.D || e.Key == Key.E || e.Key == Key.F || (e.Key >= Key.D0 && e.Key <= Key.D9)))
                    e.Handled = true;
            }
            else
            {
                if (!(e.Key >= Key.D0 && e.Key <= Key.D9))
                    e.Handled = true;
            }
        }
    }
}
