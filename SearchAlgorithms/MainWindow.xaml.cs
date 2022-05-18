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

        static string text = "";
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
                    text = sr.ReadToEnd().ToUpper();
                    isLoaded = true;
                    isLoadedText.Content = "Loaded";
                }
            }
        }

        private void Brute_Click(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            searchText = searchTextBox.Text.ToUpper();
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
            searchText = searchTextBox.Text.ToUpper();
            timeLength.Content = "0 ms";
            stopwatch.Start();
            for (long repeat = 0; repeat < long.Parse(repeatAmount.Text); repeat++)
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
            stopwatch.Reset();
        }

        private void Boyer_Click(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            var stopwatch = new Stopwatch();
            searchText = searchTextBox.Text.ToUpper();
            timeLength.Content = "0 ms";
            stopwatch.Start();
            for (long repeat = 0; repeat < long.Parse(repeatAmount.Text); repeat++)
            {
                int M = searchText.Length;
                int N = text.Length;
                int zp = 0;
                int zk = 90;

                int[] Last = new int[zk - zp + 1];
                int i, j, pp;
                for (i = 0; i <= zk - zp; i++)
                {
                    Last[i] = -1;
                }
                for (i = 0; i < M; i++)
                {
                    Last[searchText[i] - zp] = i;
                }

                pp = i = 0;
                while (i <= N - M)
                {
                    j = M - 1;
                    while ((j > -1) && (searchText[j] == text[i + j]))
                    {
                        j--;
                    }
                    if (j == -1)
                    {
                        pp++;
                        i++;
                    }
                    else
                    {
                        i += Math.Max(1, j - Last[text[i + j] - zp]);
                    }
                }
            }
            stopwatch.Stop();
            timeLength.Content = stopwatch.ElapsedMilliseconds.ToString() + " ms";
            stopwatch.Reset();
        }

        private int h(string x)
        {
            int i, hx;

            hx = 0;
            for (i = 0; i < searchText.Length; i++)
            {
                hx = 3 * hx + (x[i] - 65);
            }
            return hx;
        }


        private void Rabin_Click(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            var stopwatch = new Stopwatch();
            searchText = searchTextBox.Text.ToUpper();
            timeLength.Content = "0 ms";
            stopwatch.Start();
            for (long repeat = 0; repeat < long.Parse(repeatAmount.Text); repeat++)
            {
                int M = searchText.Length;
                int N = text.Length;
                int pp, i, Hp, Hs;

                Hp = h(searchText);

                Hs = h(text);

                pp = i = 0;

                while (true)
                {
                    if ((Hp == Hs) && (searchText == text.Substring(i, M)))
                    {
                        pp++;
                    }
                    i++;
                    if (i == N - M)
                    {
                        break;
                    }
                    Hs = (Hs - (text[i - 1] - 65) * 27) * 3 + text[i + M - 1] - 65;
                }

            }
            stopwatch.Stop();
            timeLength.Content = stopwatch.ElapsedMilliseconds.ToString() + " ms";
            stopwatch.Reset();
        }
    }
}
