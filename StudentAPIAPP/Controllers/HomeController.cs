using StudentAPIAPP.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentAPIAPP.Controllers
{
    public class HomeController : ApiController
    {
        private int studentID;
        private DateTime CreatedTime;
        private String studentName;
        private string rollNo;
        private int malayalam;
        private int english;
        private int hindi;

        public int Total { get; private set; }

        private object average;
        List<String> NameList = new List<string>();
       
        [HttpGet]
        public String StudentInsert(int rollNO,String Name, int malayalam, int english , int hindi)
        {
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("StudentInsert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("RollNO",rollNO);
            cmd.Parameters.AddWithValue("Name", Name);
            cmd.Parameters.AddWithValue("Malayalam", malayalam);
            cmd.Parameters.AddWithValue("English", english);
            cmd.Parameters.AddWithValue("Hindi", hindi);
            cmd.ExecuteNonQuery();
            con.Close();

            return "Insert Sucessfully";
        }
        [HttpGet]
        public String StudentSave()
        {
            
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("StudentSave", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
              studentID = Convert.ToInt32(reader["ID"]);
              CreatedTime = Convert.ToDateTime( reader["CreatedTime"]);             
            }
            con.Close();

            return "ID   :   " + studentID +"    ,   "+"Time   :"+ CreatedTime;
        }
        [HttpGet]
        public List<String> StudentList(int Order)
        {
           
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("StudentList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("order",Order);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
               NameList.Add(reader["Name"].ToString());
                
            }
            con.Close();


            return  NameList ;

        }

       

        [HttpGet]
        public String StudentDetails(int Id)
        {
            Student student = new Student();
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("StudentDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ID",Id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                studentName = reader["Name"].ToString();
                rollNo = reader["RollNO"].ToString();
                malayalam = Convert.ToInt32(reader["Malayalam"]);
                english = Convert.ToInt32(reader["English"]);
                hindi= Convert.ToInt32(reader["Hindi"]);
                Total = malayalam + english + hindi;
                average = Total / 3;
            }
            con.Close();

            return "Name  :   " + studentName + "," +
                   "RollNo   :   " + rollNo + "," +
                   "Malayalam  :" + malayalam + "," +
                   "English  :" + english + "," +
                   "Hindi  :" + hindi + "," +
                   "Total  :" + Total + "," +
                   "Average :" + average;
        }

        [HttpGet]
        public String StudentUpdate(int Id,int malayalam)
        {

            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("StudentUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ID", Id);
            cmd.Parameters.AddWithValue("Malayalam",malayalam);

            cmd.ExecuteNonQuery();
            con.Close();


            return "ID   : "+Id+" ,"+
                   "FieldName  : "+"Malayalam"+"";

        }

        [HttpGet]
        public string StudentCount(int Sort)
        {
            int count= 0;
            string strcon = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("StudentCount", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Sort", Sort);

            //cmd.Parameters.Add("@Count", SqlDbType.Int).Direction = ParameterDirection.Output;
            //tempInt = Convert.ToInt32(cmd.Parameters["@Count"].Value);

            SqlParameter RuturnValue = new SqlParameter("@Count", SqlDbType.Int);
            RuturnValue.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(RuturnValue);
            cmd.ExecuteNonQuery();
            count = (int)cmd.Parameters["@Count"].Value;
        


            return "Count  : " + count.ToString();

        }

    }
}
