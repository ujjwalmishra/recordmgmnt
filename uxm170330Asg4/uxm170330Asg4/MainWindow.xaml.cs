using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using Microsoft.Win32;

namespace uxm170330Asg4
{

    public class Result //Class for every search result record
    {
  
        public Result(int lineNum, string line)
        {
            this.LineNumber = lineNum; //Line number
            this.Line = line; //Line containing search string
        }

        public int LineNumber { get; set; }
        public string Line { get; set; }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Result> Results;

        CancellationTokenSource cancelToken;

        Progress<double> progressOperation;

        string fileName;
        public MainWindow()
        {
            InitializeComponent();
            //Fetch search reuslt asynchronously in other thread
            Results = new ObservableCollection<Result>();

            //Disable cancel button
            btnSearchCancel.IsEnabled = false;

            //Assign list
            SearchedText.ItemsSource = Results;

            //Hide error text block
            Error.Visibility = Visibility.Hidden;
            Error.Foreground = Brushes.Red;
        }

        //Select the file to search
        //Click handler for browsing file
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            Error.Visibility = Visibility.Hidden;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                fileName = openFileDialog.FileName;
                FilePath.Text = openFileDialog.FileName;
             }
        }

        //Click handler for search button
        private async void btnSearchFile_Click(object sender, RoutedEventArgs e)
        {
            Error.Visibility = Visibility.Hidden;

            //For cancelling search
            cancelToken = new CancellationTokenSource();
            btnSearchFile.IsEnabled = false;
            btnSearchCancel.IsEnabled = true;

            //Search progress
            progressOperation = new Progress<double>(value => searchProgress.Value = value);
            string searchStr = SearchText.Text;

            try
            {

                Results = await SearchTextAsync(cancelToken.Token, progressOperation, searchStr);

            }
            catch (OperationCanceledException ex)
            {
                Error.Text = "Search Cancelled";
                Error.Visibility = Visibility.Visible;

            }
            catch (Exception ex)
            {
                Error.Text = ex.Message;
                Error.Visibility = Visibility.Visible;
            }
            finally
            {
                cancelToken.Dispose();
                btnSearchFile.IsEnabled = true;
                btnSearchCancel.IsEnabled = false;
            }


        }

        async Task<ObservableCollection<Result>> SearchTextAsync(CancellationToken ct, IProgress<double> progress, string searchStr)
        {
            Results.Clear();
            var task = Task.Run(() => {
                long fileRead = 0;
                string line;
                int lineCount = 0;
                if(!File.Exists(fileName))
                {
                    throw new Exception("File doesn't exists");
                }
                else
                {
                    long fileSize = new System.IO.FileInfo(fileName).Length;
                    System.IO.StreamReader file = new System.IO.StreamReader(fileName);
                    while ((line = file.ReadLine()) != null)
                    {
                        lineCount++;
                        int lineSize = System.Text.ASCIIEncoding.Unicode.GetByteCount(line);
                        //int lineSize = line.Length * sizeof(Char);
                        fileRead = fileRead + lineSize;
                        double percent = fileRead / fileSize ;
                        if (line.ToLower().Contains(searchStr.ToLower()))
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                Results.Add(new Result(lineCount, line));
                            });
                            
                        }
           
                        progress.Report(fileRead *100 /fileSize);
                        Thread.Sleep(1);//Sleep 1 ms

                        //Throw cancel exception when cancelled
                        ct.ThrowIfCancellationRequested();
                    }

                    file.Close();
                }
    

                return Results;

            });

            return await task;
        }

        private void btnSearchCancel_Click(object sender, RoutedEventArgs e)
        {
            cancelToken.Cancel();
        }
    }
}
