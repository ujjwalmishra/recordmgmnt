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

namespace Asg2_uxm170330
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RecordsAPI api = RecordsAPI.Instance;
        Boolean modify = true;
        Record currentRecord = null;
        int selectedIndex = 0;
        List<Record> records;
        List<Record> cloneRecords;
        DateTime firstField;
        public MainWindow()
        {
            
            InitializeComponent();
            //this.Title = "App";
            records = api.getRecords();
            cloneRecords = records.ConvertAll(record => (Record)record.Clone()).ToList();
            lbTodoList.ItemsSource = records;
            if (cloneRecords.Count > 0)
            {
                this.DataContext = cloneRecords[0];
                this.currentRecord = cloneRecords[0];
                Add_Button.Focus();
                lbTodoList.SelectedItem = records[0];
            }
            else
            {
                this.modify = false;
                this.DataContext = null;
            }

        }

        private void Reload_Records()
        {
            //this.Title = "App";
            records = api.getRecords();
            cloneRecords = records.ConvertAll(record => (Record)record.Clone()).ToList();
            lbTodoList.ItemsSource = records;
            if (cloneRecords.Count > 0)
            {
                this.DataContext = cloneRecords[0];
                this.currentRecord = cloneRecords[0];
                lbTodoList.SelectedItem = records[0];
            }
            else
            {
                this.modify = false;
                this.DataContext = null;
            }

        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "Enter items";
            this.DataContext = null;
            this.modify = false;
            fName.Focus();
        }

        private void Select_Record(Object sender, RoutedEventArgs e)
        {
            Grid grid = sender as Grid;
            int index = lbTodoList.Items.IndexOf(grid.DataContext);
            this.DataContext = cloneRecords[index];
            Console.WriteLine(index);
            Save_Button.IsEnabled = false;
            selectedIndex = index;
            this.modify = true;
            //this.Title = "App";
        }
        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "Deleting";
            if(this.DataContext == null)
            {
                this.Title = "No Record Selected";
                return;
            }
            else
            {
                Record record = (Record)this.DataContext;
                Result result = api.deleteRecord(record);
                this.Title = result.message;
            }

            Reload_Records();
        }

        private void textChangedEventHandler(object sender, TextChangedEventArgs e)
        {

            Save_Button.IsEnabled = true;
            //this.Title = "Record Modified";

        }

        private void firstNameEnteredHandler(object sender, KeyEventArgs e)
        {
            firstField = DateTime.Now;
        }

        private void dateChangedEventHandler(object sender, SelectionChangedEventArgs e)
        {

            Save_Button.IsEnabled = true; //this.Title = "Record Modified";

        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {

            this.Title = "Saving";
            Result result = new Result();
            if (this.modify)
            {
                Record mRecord = (Record)this.DataContext;
                string key = String.Concat(mRecord.fName, mRecord.lName, mRecord.phone);
                Record oldRec = records[selectedIndex];
                string oldKey = String.Concat(oldRec.fName, oldRec.lName, oldRec.phone);
                if(String.Equals(oldKey, key))
                {
                    result = api.modifyRecord(mRecord, oldKey);
                    Save_Button.IsEnabled = false;
                }
                else
                {
                    if (!api.keyExists(key))
                    {
                        result = api.modifyRecord(mRecord, oldKey);
                        Save_Button.IsEnabled = false;
                    }
                    else
                    {
                        result.message = "Duplicate key";
                    }
                }


            }
            else
            {
                string fiName = fName.Text;
                string miName = mName.Text;
                string laName = lName.Text;
                string aaddress1 = address1.Text;
                string aaddress2 = address2.Text;
                string ccity = city.Text;
                string sstate = state.Text;
                string zzip = zip.Text;
                string ggender = gender.Text;
                string pphone = phone.Text;
                string eemail = email.Text;
                string pproof = proof.Text;
                string ttime = time.Text;
                DateTime ftime = firstField;
                DateTime sTime = DateTime.Now;
                int count = 0;
                Record record = new Record(fiName, miName, laName, aaddress1, aaddress2, ccity, sstate, Int32.Parse(zzip), ggender, pphone, eemail,
                   Boolean.Parse(pproof), DateTime.Parse(ttime), ftime, sTime, count);
                result = api.addRecord(record);
                this.modify = true;
                Save_Button.IsEnabled = false;
            }

            this.Title = result.message;
            Reload_Records();
            Save_Button.IsEnabled = false;
        }
    }
}
