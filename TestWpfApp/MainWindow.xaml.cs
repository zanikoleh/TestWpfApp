using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NaturalTextResolver TextModel = new NaturalTextResolver();

        public MainWindow()
        {
            InitializeComponent();
            InputRichTextBox.Text = "xabcdecdeabcbx";
            InitProperties();
        }

        private void InputRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            InitProperties();
        }

        private void InitProperties()
        {
            lock (TextModel)
            {
                string text = InputRichTextBox.Text.Replace("\r", " ").Replace("\n", " ");
                TextModel = new NaturalTextResolver(text);
                SubstringsGrid.ItemsSource = TextModel.Substrings;
                ResultLabel.Content = TextModel.ToString();
            }
        }

        private void InputBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                InputRichTextBox.Text = File.ReadAllText(openFileDialog.FileName);
                InitProperties();
            }
        }
    }
}
