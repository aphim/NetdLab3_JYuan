//Project Name: Net3202_Lab3
//Author: Jacky Yuan
//Date: Nov 2, 2020
//Description: Makes a database software program for purchasing shares.
//Change log: N/A

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
using System.Data;
using System.Data.SqlClient;

namespace NetdLab3_JYuan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FillDataGrid();
            FillSummary();
        }
        /// <summary>
        /// Function used for filling the data grid for view entries
        /// </summary>
        private void FillDataGrid()
        {
            try
            { //Connect to data source
                string connectString = Properties.Settings.Default.connect_string;
                SqlConnection cn = new SqlConnection(connectString);
                cn.Open();
                //This is the SQL query we want to run.
                string selectionQuery = "SELECT * FROM entries";
                SqlCommand command = new SqlCommand(selectionQuery, cn);
                SqlDataAdapter sda = new SqlDataAdapter(command);
                //import the table
                DataTable dt = new DataTable("PurchaseEntries");
                sda.Fill(dt);
                //Bind datatable to the sql table
                dtgOutputDisplay.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                //Shows error in message box.
                MessageBox.Show(ex.ToString());
            }

        }

        /// <summary>
        /// Function used to fill the summary page
        /// </summary>
        private void FillSummary()
        {
            try
            {
                //Connect to data source
                string connectString = Properties.Settings.Default.connect_string;
                SqlConnection cn = new SqlConnection(connectString);
                cn.Open();
                //This is the SQL query we want to run.
                string selectionQuery = "SELECT * FROM shares";
                SqlCommand command = new SqlCommand(selectionQuery, cn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    txtCommonAvailable.Text = reader.GetInt32(0).ToString();
                    txtPreferredAvailable.Text = reader.GetInt32(1).ToString();
                }
                selectionQuery = "SELECT SUM(shares) FROM entries WHERE shareType= \'Common\'";
                command = new SqlCommand(selectionQuery, cn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    txtCommonSold.Text = reader.GetInt32(0).ToString();
                }
                selectionQuery = "SELECT SUM(shares) FROM entries WHERE shareType= \'Preferred\'";
                command = new SqlCommand(selectionQuery, cn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    txtPreferredSold.Text = reader.GetInt32(0).ToString();
                }

            }
            catch (Exception ex)
            {
                //Shows error in message box.
                MessageBox.Show(ex.ToString());
            }

        }

            /// <summary>
            /// Event handler for button press of create entry
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void btnCreateEntry_Click(object sender, RoutedEventArgs e)
        {
            int numberofCommons;
            int numberofPreferred;
            //connect to the database
            string connectString = Properties.Settings.Default.connect_string;
            SqlConnection cn = new SqlConnection(connectString);
            cn.Open();
            //create new query statement
            string insertQuery = "SELECT numCommonShares, numPreferredShares FROM shares";
            SqlCommand command = new SqlCommand(insertQuery, cn);
            //read query data and insert into variable
            using (SqlDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                numberofCommons = reader.GetInt32(0);
                numberofPreferred = reader.GetInt32(1);
            }

            int tempint;
            string TypeofShare;
            //checks if the name field is empty
            if ((txtBuyerName.Text != string.Empty))
            {
                //checks if the employee ID field is empty
                if ((txtShareNo.Text != string.Empty))
                {
                    //checks if the employee ID is an integer
                    if (int.TryParse(txtShareNo.Text, out tempint))
                    {
                        if (tempint > 0)
                        {
                            //Checks if the common radio button is checked
                            if (radCommon.IsChecked == true)
                            {
                                //Checks if the number of common shares are greater than 0 and more than the amount to be bought
                                if (numberofCommons != 0 && tempint <= numberofCommons)
                                {
                                    //Create entry for number of shares purchased
                                    TypeofShare = "Common";
                                    insertQuery = "INSERT INTO entries(buyerName, shares, datePurchased, shareType) VALUES('" + txtBuyerName.Text + "', '" + txtShareNo.Text + "', '" + dtpDatePicker.Text + "', '" + TypeofShare + "')";
                                    command = new SqlCommand(insertQuery, cn);
                                    command.ExecuteNonQuery();
                                    //Calculate new amount of common shares
                                    int updatedShares = numberofCommons - tempint;
                                    //updates the shares table for number of shares remaining
                                    insertQuery = "UPDATE shares SET numCommonShares = " + updatedShares;
                                    command = new SqlCommand(insertQuery, cn);
                                    command.ExecuteNonQuery();
                                    cn.Close();
                                    //Messagebox for a successful input
                                    MessageBox.Show("The entry has been submitted");
                                    //resets the form
                                    txtBuyerName.Text = " ";
                                    txtShareNo.Text = " ";
                                    dtpDatePicker.SelectedDate = null;


                                }
                                //not enough for the desired amount of shares
                                else
                                {
                                    txtShareNo.Focus();
                                    txtShareNo.SelectAll();
                                    MessageBox.Show("Attempted purchase amount exceeded the number of shares available.");
                                    cn.Close();
                                }
                            }
                            //Checks if the preferred radio button is checked
                            else if (radPreferred.IsChecked == true)
                            {
                                //Checks if the number of preferred shares are greater than 0 and more than the amount to be bought
                                if (numberofPreferred != 0 && tempint <= numberofPreferred)
                                {
                                    //Create entry for number of shares purchased
                                    TypeofShare = "Preferred";
                                    insertQuery = "INSERT INTO entries(buyerName, shares, datePurchased, shareType) VALUES('" + txtBuyerName.Text + "', '" + txtShareNo.Text + "', '" + dtpDatePicker.Text + "', '" + TypeofShare + "')";
                                    command = new SqlCommand(insertQuery, cn);
                                    command.ExecuteNonQuery();
                                    //Calculate new amount of preferred shares
                                    int updatedShares = numberofPreferred - tempint;
                                    //updates the shares table for number of shares remaining
                                    insertQuery = "UPDATE shares SET numPreferredShares = " + updatedShares;
                                    command = new SqlCommand(insertQuery, cn);
                                    command.ExecuteNonQuery();
                                    cn.Close();
                                    //Messagebox for a successful input
                                    MessageBox.Show("The entry has been submitted");
                                    //resets the form
                                    txtBuyerName.Text = " ";
                                    txtShareNo.Text = " ";
                                    dtpDatePicker.SelectedDate = null;
                                    radCommon.IsChecked = true;
                                    radPreferred.IsChecked = false;
                                }
                                //not enough for the desired amount of shares
                                else
                                {
                                    txtShareNo.Focus();
                                    txtShareNo.SelectAll();
                                    MessageBox.Show("Attempted purchase amount exceeded the number of shares available.");
                                    cn.Close();
                                }
                            }
                        }
                        //number of share to be purchase less than 0
                        else
                        {
                            txtShareNo.Focus();
                            txtShareNo.SelectAll();
                            MessageBox.Show("The number you wish to purchase must be greater than 0.");
                            cn.Close();
                        }
                    }
                    //non-numeric entry for number of shares wished to be purchased
                    else
                    {
                        txtShareNo.Focus();
                        txtShareNo.SelectAll();
                        MessageBox.Show("The number you wish to purchase must be entered numerically.");
                        cn.Close();
                    }
                }
                //number of shares to be purchased is empty
                else
                {
                    txtShareNo.Focus();
                    txtShareNo.SelectAll();
                    MessageBox.Show("Please enter the number of shares you wish to purchase");
                    cn.Close();
                }
            }
            //name is empty
            else
            {
                txtBuyerName.Focus();
                txtBuyerName.SelectAll();
                MessageBox.Show("You must enter your Name.");
                cn.Close();
            }
        }
    }
}
