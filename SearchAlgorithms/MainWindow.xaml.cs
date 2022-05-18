using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace SearchAlgorithms
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            string pathToFile = "";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open Text File";
            dialog.Filter = "TXT files|*.txt";
            dialog.InitialDirectory = @"C:\";
            if (dialog.ShowDialog() == true)
            {
                MessageBox.Show(dialog.FileName.ToString());
                pathToFile = dialog.FileName;
            }

            if (File.Exists(pathToFile))
            {
                //method1
                string firstLine = File.ReadAllLines(pathToFile).Skip(0).Take(1).First();
                string secondLine = File.ReadAllLines(pathToFile).Skip(1).Take(1).First();

                //method2
                string text = "";
                using (StreamReader sr = new StreamReader(pathToFile))
                {
                    text = sr.ReadToEnd();

                    MessageBox.Show(text);
                    isLoadedText.Content = "Loaded";
                }
            }
        }
    }
}
