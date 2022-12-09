using MySql.Data.MySqlClient;
using System.Data;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {

        string conString = @"Server=localhost;Database=playerdb;Uid=root;Pwd=1234;";
        int playerId=0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, EventArgs e)
        {

              using (MySqlConnection mysqlcon = new MySqlConnection(conString))
            {
                mysqlcon.Open();
                MySqlCommand mySqlCmd = new MySqlCommand("PlayerAddOrEdit", mysqlcon);
                mySqlCmd.CommandType = CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_Playerid", playerId);
                mySqlCmd.Parameters.AddWithValue("_PlayerName", txtName.Text.Trim());
                mySqlCmd.Parameters.AddWithValue("_Club", txtClub.Text.Trim());
                mySqlCmd.Parameters.AddWithValue("_Desciption", txtDescription.Text.Trim());
                mySqlCmd.ExecuteNonQuery();
                MessageBox.Show("Succesfully saved!");
                GridFill();
            }  


        }

       void GridFill()
        {

            using (MySqlConnection mysqlcon = new MySqlConnection(conString))
            {
                mysqlcon.Open();
                MySqlDataAdapter sqlData = new MySqlDataAdapter("PlayerViewAll", mysqlcon);
                sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dtplayer = new DataTable();
                sqlData.Fill(dtplayer);
                playerview.DataSource = dtplayer;

               
            }

        }
    }
}