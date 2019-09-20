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
        List<Record> records;
        public MainWindow()
        {
            
            InitializeComponent();
            this.Title = "App";
            records = api.getRecords();
            lbTodoList.ItemsSource = records;
            if(records.Count > 0)
            {
                this.DataContext = records[0];
                this.currentRecord = records[0];
            }

        }

        private void Reload_Records()
        {
            this.Title = "App";
            records = api.getRecords();
            lbTodoList.ItemsSource = records;
            if (records.Count > 0)
            {
                this.DataContext = records[0];
                this.currentRecord = records[0];
            }
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "Enter items";
            this.DataContext = null;
            this.modify = false;
        }

        private void Select_Record(Object sender, RoutedEventArgs e)
        {
            Grid grid = sender as Grid;
            int index = lbTodoList.Items.IndexOf(grid.DataContext);
            this.DataContext = records[index];
            Console.WriteLine(index);
            Save_Button.IsEnabled = false;
            this.Title = "App";
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
            this.Title = "Record Modified";

        }

        private void dateChangedEventHandler(object sender, SelectionChangedEventArgs e)
        {

            Save_Button.IsEnabled = true; this.Title = "Record Modified";

        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {

            this.Title = "Saving";
            Result result = new Result();
            if (this.modify)
            {
                Record mRecord = (Record)this.DataContext;
                result = api.modifyRecord(mRecord);
                Save_Button.IsEnabled = false;
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
                Record record = new Record(fiName, miName, laName, aaddress1, aaddress2, ccity, sstate, Int32.Parse(zzip), ggender, pphone, eemail,
                   Boolean.Parse(pproof), DateTime.Parse(ttime));
                result = api.addRecord(record);
                this.modify = false;
                Save_Button.IsEnabled = false;
            }

            this.Title = result.message;
            Reload_Records();
        }
    }
}
