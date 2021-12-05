using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Butthesda_Wpf
{
    /// <summary>
    /// Interaktionslogik für ConfiguraDevices.xaml
    /// </summary>
    public partial class ConfigureDevices : UserControl
    {
        public ConfigureDevices()
        {
            InitializeComponent();

            foreach (var ev in Enum.GetNames(typeof(DeviceConfiguration.EventType)))
            {
                this.CheckBoxGrid.Children.Add(new TextBlock { Text = ev });
            }

            foreach (var bodypart in Enum.GetNames(typeof(DeviceConfiguration.BodyPart)))
            {
                this.CheckBoxGrid.Children.Add(new TextBlock { Text = bodypart });
                foreach (var ev in Enum.GetNames(typeof(DeviceConfiguration.EventType)))
                {
                    var checkbox = new CheckBox { };
                    checkbox.SetBinding(CheckBox.IsCheckedProperty, $"{bodypart}.{ev}");
                    
                    this.CheckBoxGrid.Children.Add(checkbox);
                }
            }
        }
    }
}
