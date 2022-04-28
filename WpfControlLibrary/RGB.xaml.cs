using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    public partial class RGB : UserControl
    {
        public RGB()
        {
            InitializeComponent();
            Dec.IsChecked = true;
            R.TextChange += RGB_TextChange;
            G.TextChange += RGB_TextChange;
            B.TextChange += RGB_TextChange;

        }

        public Color ColorImg
        {
            get 
            { 
                return ((System.Windows.Media.SolidColorBrush)(Img.Fill)).Color;
            }
            set
            {
                Img.Fill = new SolidColorBrush(value);
                R.Text = ((System.Windows.Media.SolidColorBrush)(Img.Fill)).Color.R.ToString();
                G.Text = ((System.Windows.Media.SolidColorBrush)(Img.Fill)).Color.G.ToString();
                B.Text = ((System.Windows.Media.SolidColorBrush)(Img.Fill)).Color.B.ToString();
            }
        }

        public delegate void ColorChangeHandler(object sender, EventArgs e);
        public event ColorChangeHandler ColorChange;

        private void Dec_Checked(object sender, RoutedEventArgs e)
        {
            if (Dec.IsChecked == true)
            {
                Hex.IsChecked = false;
                R.ToDec();
                G.ToDec();
                B.ToDec();
            }
        }

        private void Hex_Checked(object sender, RoutedEventArgs e)
        {
            if (Hex.IsChecked == true)
            {
                Dec.IsChecked = false;
                R.ToHex();
                G.ToHex();
                B.ToHex();
            }
        }

        private void RGB_TextChange(object sender, EventArgs e)
        {
            try
            {
                if (Dec.IsChecked == true)
                    Img.Fill = new SolidColorBrush(Color.FromRgb(Convert.ToByte(R.Text), Convert.ToByte(G.Text), Convert.ToByte(B.Text)));
                else
                    Img.Fill = new SolidColorBrush(Color.FromRgb(Convert.ToByte(Convert.ToString(int.Parse(R.Text, System.Globalization.NumberStyles.HexNumber), 10)), Convert.ToByte(Convert.ToString(int.Parse(G.Text, System.Globalization.NumberStyles.HexNumber), 10)), Convert.ToByte(Convert.ToString(int.Parse(B.Text, System.Globalization.NumberStyles.HexNumber), 10))));
                ColorChange?.Invoke(sender, e);
            }
            catch
            {

            }
        }
    }
}
