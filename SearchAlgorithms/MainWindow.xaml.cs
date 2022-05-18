using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        string text = "";
        static string searchText = "";
        bool isLoaded = false;

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
                using (StreamReader sr = new StreamReader(pathToFile))
                {
                    text = sr.ReadToEnd();
                    isLoaded = true;
                    isLoadedText.Content = "Loaded";
                }
            }
        }

        private void Brute_Click(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            searchText = searchTextBox.Text;
            bool finished = false;
            // Not Finished
            if (finished) 
            {
                timeLength.Content = "Finished";
            } else {
                timeLength.Content = "Not Finished";
            } 
        }

        private void Preview_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(text, "Text Preview");
        }

        private void KMP_Click(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            var stopwatch = new Stopwatch();
            searchText = searchTextBox.Text;
            timeLength.Content = "0 ms";
            stopwatch.Start();
            for (int j = 0; j < int.Parse(repeatAmount.Text); j++)
            {
                int M = searchText.Length;
                int N = text.Length;
                int[] KMPNext = new int[M + 1];
                int i, b, pp;

                KMPNext[0] = b = -1;
                for (i = 1; i <= M; i++)
                {
                    while ((b > -1) && (searchText[b] != searchText[i - 1]))
                    {
                        b = KMPNext[b];
                    }
                    ++b;
                    if ((i == M) || (searchText[i] != searchText[b]))
                    {
                        KMPNext[i] = b;
                    }
                    else
                    {
                        KMPNext[i] = KMPNext[b];
                    }
                }

                pp = b = 0;
                for (i = 0; i < N; i++)
                {
                    while ((b > -1) && (searchText[b] != text[i]))
                    {
                        b = KMPNext[b];
                    }
                    if (++b == M)
                    {
                        b = KMPNext[b];
                    }
                }
            }
            stopwatch.Stop();
            timeLength.Content = stopwatch.ElapsedMilliseconds.ToString()+" ms";
        }
    }
}
