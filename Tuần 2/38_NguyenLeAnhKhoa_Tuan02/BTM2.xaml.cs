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
using System.Windows.Shapes;

namespace _38_NguyenLeAnhKhoa_Tuan02
{
    /// <summary>
    /// Interaction logic for BTM2.xaml
    /// </summary>
    public partial class BTM2 : Window
    {
        public BTM2()
        {
            InitializeComponent();
        }
        private void ChangeTheme(string themeName)
        {
            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri("Themes/" + themeName + ".xaml", UriKind.Relative);

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }

        private void cboTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboTheme.SelectedIndex == 0)
                ChangeTheme("ThemeLight");
            else
                ChangeTheme("ThemeDark");
        }
    }
}
