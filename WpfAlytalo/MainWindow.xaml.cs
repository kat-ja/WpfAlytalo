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

namespace WpfAlytalo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Lights olohuone = new Lights();
        Lights keittio = new Lights();

        public MainWindow()
        {
            InitializeComponent();     
        }

        private void btnOlohuone_Click(object sender, RoutedEventArgs e)
        {
            if (olohuone.Switched)
            {
                btnOlohuone.Content = "Sytytä valot";
                olohuone.Switched = false;
                //olohuone.Dimmer = "0";
                tbOlohuoneInfo.Text = "Olohuoneen valot sammutettu.";
                //slOlohuone.Value = int.Parse(olohuone.Dimmer);
            }
            else
            {
                btnOlohuone.Content = "Sammuta valot";
                olohuone.Switched = true;
                //olohuone.Dimmer = "100";
                tbOlohuoneInfo.Text = "Olohuoneen valot päällä.";
                
            }
            
            
        }

        private void slOlohuone_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;

            olohuone.Dimmer = Math.Round(slider.Value).ToString();
            lblSlider.Content = olohuone.Dimmer;
            
            if(slider.Value == 0)
            {
                btnOlohuone.Content = "Sytytä valot";
                olohuone.Switched = false;
                tbOlohuoneInfo.Text = "Olohuoneen valot sammutettu. Himmentimen asento: " + olohuone.Dimmer +".";
            }else
            {
                btnOlohuone.Content = "Sammuta valot";
                olohuone.Switched = true;
                tbOlohuoneInfo.Text = "Olohuoneen valot päällä. Himmentimen asento: " + olohuone.Dimmer + "."; ;
            }
        }
        
        
    }
}
