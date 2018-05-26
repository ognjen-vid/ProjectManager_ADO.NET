using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectManager.Models;
using ProjectManager.Repositories.Interfaces;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ProjectManager.Repositories
{
    public class FirmRepo : IFirmRepo
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ProjectManagerContext"].ConnectionString;
        private SqlConnection conn;

        public IEnumerable<Firm> GetAll()
        {
            string select = "SELECT * FROM Firm";
            conn = new SqlConnection(connectionString);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand(select, conn);
            adapter.SelectCommand = cmd;
            adapter.Fill(ds, "Firm");
            dt = ds.Tables["Firm"];
            conn.Close();

            List<Firm> firms = new List<Firm>();

            foreach (DataRow row in dt.Rows)
            {
                int id = int.Parse(row["id"].ToString());
                string name = row["name"].ToString();

                firms.Add(new Firm() { Id = id, Name = name });
            }

            return firms;
        }

        public Firm GetById(int id)
        {
            string selectById = "SELECT * FROM Firm WHERE id = @id";
            conn = new SqlConnection(connectionString);
            conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand(selectById, conn);
            cmd.Parameters.AddWithValue("@id", id);

            adapter.SelectCommand = cmd;
            adapter.Fill(ds, "Firm");
            dt = ds.Tables["Firm"];
            conn.Close();

            Firm firm = null;

            foreach (DataRow row in dt.Rows)
            {
                int firmId = int.Parse(row["id"].ToString());
                string name = row["name"].ToString();
                firm = new Firm() {Id = firmId, Name = name};
            }

            return firm;
        }

        public bool Create(Firm firm)
        {
            conn = new SqlConnection(connectionString);
            conn.Open();

            string insert = "INSERT INTO Firm (name) VALUES (@name)";
            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@name", firm.Name);

            var retVal = cmd.ExecuteNonQuery();
            conn.Close();

            if (retVal == 1)
                return true;

            return false;

        }

        public bool Update(int id, Firm firm)
        {
            conn = new SqlConnection(connectionString);
            conn.Open();

            string update = "UPDATE Firm SET name = @name WHERE id = @id";
            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@name", firm.Name);
            cmd.Parameters.AddWithValue("@id", id);

            var retVal = cmd.ExecuteNonQuery();
            conn.Close();

            if (retVal == 1)
                return true;
            return false;
        }

        public void Delete(int id)
        {
            conn = new SqlConnection(connectionString);
            conn.Open();

            string delete = "DELETE FROM Firm WHERE id = @id";
            SqlCommand cmd = new SqlCommand(delete, conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}