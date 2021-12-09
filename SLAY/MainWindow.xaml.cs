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

namespace SLAY
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text == "")
            {
                return;
            }
            ConvertStringToEquation convertStringToEquation = new ConvertStringToEquation();
            var s = convertStringToEquation.ConvertationEquation(textBox.Text);
            var ss = s.CalculateAlphaAndBeta();
          /*  foreach(var sss in ss)
            {
                textBox1.Text += String.Format("alpha {0:F2} beta {1:F2} \n", sss.Item1, sss.Item2);
            }*/
            var sssss = s.CalculateValues();
            foreach (var sssssss in sssss)
            {
                textBox1.Text += String.Format("alpha {0:F2}  \n", sssssss);
            }
        }
    }
}
