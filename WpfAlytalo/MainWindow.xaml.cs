﻿using System;
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
using System.Windows.Threading;

namespace WpfAlytalo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Lights olohuone = new Lights();
        Lights keittio = new Lights();
        Thermostat talo = new Thermostat();
        Sauna sauna = new Sauna();

        public DispatcherTimer SaunanLammitin = new DispatcherTimer();
        public DispatcherTimer SaunanKylmetin = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            tbOlohuoneInfo.Text = "Olohuoneen valot päällä.";
            olohuone.Switched = true;
            slOlohuone.Value = 100;

            talo.SetTemperature(22);
            tbLampotilaNyt.Text = talo.Temperature.ToString();

            sauna.Switched = false;
            sauna.SaunaTempe = talo.Temperature;

            SaunanLammitin.Tick += SaunanLammitin_Tick;
            SaunanLammitin.Interval = new TimeSpan(0, 0, 0, 2); // 2 sekuntia -> 1 asteen nousu

            SaunanKylmetin.Tick += SaunanKylmetin_Tick;
            SaunanKylmetin.Interval = new TimeSpan(0, 0, 0, 1); // 1 sekuntia -> 1 asteen lasku
        }

        private void SaunanLammitin_Tick(object sender, EventArgs e)
        {
            if(sauna.SaunaTempe < 26)
            {
                sauna.SaunaTempe += 1;
                lblLampotilaSauna.Content = sauna.SaunaTempe;
            }
            else
            {
                SaunanLammitin.Stop();
            }

        }
        private void SaunanKylmetin_Tick(object sender, EventArgs e)
        {
            if (sauna.SaunaTempe > talo.Temperature)
            {
                sauna.SaunaTempe -= 1;
                lblLampotilaSauna.Content = sauna.SaunaTempe;
            }
            else
            {
                SaunanKylmetin.Stop();
            }

        }
        private void btnOlohuone_Click(object sender, RoutedEventArgs e)
        {
            if (olohuone.Switched)
            {
                btnOlohuone.Content = "Sytytä valot";
                olohuone.Switched = false;             
                tbOlohuoneInfo.Text = "Olohuoneen valot sammutettu.";
                slOlohuone.Value = 0;
            }
            else
            {
                btnOlohuone.Content = "Sammuta valot";
                olohuone.Switched = true;              
                tbOlohuoneInfo.Text = "Olohuoneen valot päällä.";
                slOlohuone.Value = 100;
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

        private void tbLampotilaTavoite_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void btnLampotila_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                talo.SetTemperature(int.Parse(tbLampotilaTavoite.Text));

                if(talo.Temperature >= 5 && talo.Temperature <= 35)
                {
                    tbLampotilaNyt.Text = talo.Temperature.ToString();
                    sauna.SaunaTempe = talo.Temperature;
                    lblLampotilaSauna.Content = talo.Temperature.ToString();
                    tbLampotilaTavoite.Text = "";
                    lblTavoiteInfo.Content = "";
                }
                else
                {
                    lblTavoiteInfo.Content = "Lämpötilan pitää olla luku\nvälillä 5-35.";
                    tbLampotilaTavoite.Text = "";
                    tbLampotilaTavoite.Focus();
                }
                
            }
            catch (Exception)
            {
                lblTavoiteInfo.Content = "Lämpötilan pitää olla luku\nvälillä 5-35.";
                tbLampotilaTavoite.Text = "";
                tbLampotilaTavoite.Focus();
            }
            
        }
        private void btnSauna_Click(object sender, RoutedEventArgs e)
        {
            if (sauna.Switched)
            {
                sauna.SaunaOff();
                lblKiuasPaalla.Content = "Kiuas ei toiminnassa.";
                btnSauna.Content = "Sauna päälle";
                SaunanKylmetin.Start();
                
            }
            else
            {
                sauna.SaunaOn();
                lblKiuasPaalla.Content = "Kiuas toiminnassa.";
                btnSauna.Content = "Sauna pois päältä";
                SaunanLammitin.Start();
                lblLampotilaSauna.Content = sauna.SaunaTempe;
            }
        }
    }
}
