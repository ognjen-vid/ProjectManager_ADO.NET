using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectManager.Models;
using ProjectManager.Repositories.Interfaces;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using ProjectManager.Repositories;
using ProjectManager.ViewModel;

namespace ProjectManager.Repositories
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ProjectManagerContext"].ConnectionString;
        private SqlConnection conn;
        private FirmRepo firmRepo = new FirmRepo();
        private ProjectRepo projectRepo = new ProjectRepo();

        public IEnumerable<Employee> GetAll()
        {
            string select = "SELECT * FROM Employee";
            conn = new SqlConnection(connectionString);
            conn.Open();
                      
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(select, conn);

            adapter.SelectCommand = cmd;
            adapter.Fill(ds, "Employee");
            dt = ds.Tables["Employee"];
            conn.Close();

            List<Employee> employees = new List<Employee>();

            foreach (DataRow row in dt.Rows)
            {
                int id = int.Parse(row["id"].ToString());
                string name = row["name"].ToString();
                string lastname = row["lastname"].ToString();
                Firm firm = firmRepo.GetById(int.Parse(row["firm_id"].ToString()));
                employees.Add(new Employee() { Id = id, Name = name, LastName = lastname, Firm = firm });
            }
            return employees;
        }

        public Employee GetById(int id)
        {
            conn = new SqlConnection(connectionString);
            conn.Open();

            string selectById = "SELECT * FROM Employee WHERE id = @id";
            SqlCommand cmd = new SqlCommand(selectById, conn);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            adapter.Fill(ds, "Employee");
            dt = ds.Tables["Employee"];
            conn.Close();

            Employee retVal = null;

            foreach (DataRow row in dt.Rows)
            {
                int emp_id = int.Parse(row["id"].ToString());
                string name = row["name"].ToString();
                string lastname = row["lastname"].ToString();
                Firm firm = firmRepo.GetById(int.Parse(row["firm_id"].ToString()));

                retVal = new Employee() { Id = emp_id, Name = name, LastName = lastname, Firm = firm };
            }
            
            return retVal;
        }

        public bool Create(Employee employee)
        {
            string insert = "INSERT INTO Employee (name, lastname, firm_id) VALUES (@name, @lastname, @firm_id)";
            conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@name", employee.Name);
            cmd.Parameters.AddWithValue("@lastname", employee.LastName);
            cmd.Parameters.AddWithValue("@firm_id", employee.Firm.Id);

            var retVal = cmd.ExecuteNonQuery();
            conn.Close();

            if (retVal == 1)
                return true;

            return false;
        }

        public bool Update(int id, Employee employee)
        {
            conn = new SqlConnection(connectionString);
            conn.Open();

            string update = "UPDATE Employee SET name = @name, lastname = @lastname, firm_id = @firm_id WHERE id = @id";
            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@name",employee.Name);
            cmd.Parameters.AddWithValue("@lastname", employee.LastName);
            cmd.Parameters.AddWithValue("@firm_id", employee.Firm.Id);
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

            string delete = "DELETE FROM Employee WHERE id = @id";
            SqlCommand cmd = new SqlCommand(delete, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public IEnumerable<Employee> GetByFirmId(int firmId)
        {
            string selectByFirmId = "SELECT * FROM Employee WHERE firm_id = @firmId";
            conn = new SqlConnection(connectionString);
            conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand(selectByFirmId, conn);
            cmd.Parameters.AddWithValue("@firmId", firmId);

            adapter.SelectCommand = cmd;
            adapter.Fill(ds, "Employee");
            dt = ds.Tables["Employee"];
            conn.Close();

            List<Employee> employees = new List<Employee>();

            foreach (DataRow row in dt.Rows)
            {
                int id = int.Parse(row["id"].ToString());
                string name = row["name"].ToString();
                string lastname = row["lastname"].ToString();
                Firm firm = firmRepo.GetById(int.Parse(row["firm_id"].ToString()));
                employees.Add(new Employee() { Id = id, Name = name, LastName = lastname, Firm = firm });
            }
            return employees;
        }

        public IEnumerable<Employee> GetByProjectId(int projectId)
        {
            string selectByProjectId = "SELECT * FROM Active WHERE project_id = @projectId";
            conn = new SqlConnection(connectionString);
            conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand(selectByProjectId, conn);
            cmd.Parameters.AddWithValue("@projectId", projectId);

            adapter.SelectCommand = cmd;
            adapter.Fill(ds, "Employee");
            dt = ds.Tables["Employee"];
            conn.Close();

            List<Employee> employees = new List<Employee>();

            foreach (DataRow row in dt.Rows)
            {
                Employee employee = GetById(int.Parse(row["employee_id"].ToString()));
                employees.Add(employee);
            }
            return employees;
        }

    }
}