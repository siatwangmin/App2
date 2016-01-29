using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Devices.Gpio;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace App2
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private GpioPin pin5;
        private GpioPin pin6;
        public MainPage()
        {
            this.InitializeComponent();
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                pin5 = null;
                //GpioStatus.Text = "There is no GPIO controller on this device.";
                return;
            }

            pin5 = gpio.OpenPin(5);
            pin6 = gpio.OpenPin(6);

            // Show an error if the pin5 wasn't initialized properly
            if (pin5 == null)
            {
                //GpioStatus.Text = "There were problems initializing the GPIO pin5.";
                return;
            }
           

            pin6.SetDriveMode(GpioPinDriveMode.Output);
            pin5.SetDriveMode(GpioPinDriveMode.InputPullUp);
            pin5.ValueChanged += Pin5OnValueChanged;
        }

        private void Pin5OnValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            pin6.Write(pin5.Read());
        }
    }
}
