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
    public class ProjectRepo : IProjectRepo
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ProjectManagerContext"].ConnectionString;
        private SqlConnection conn;
        private FirmRepo firmRepo = new FirmRepo();

        public IEnumerable<Project> GetAll()
        {
            string select = "SELECT * FROM Project";
            conn = new SqlConnection(connectionString);
            conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(select, conn);

            adapter.SelectCommand = cmd;
            adapter.Fill(ds, "Project");
            dt = ds.Tables["Project"];
            conn.Close();

            List<Project> projects = new List<Project>();

            foreach (DataRow row in dt.Rows)
            {
                int id = int.Parse(row["id"].ToString());
                string name = row["name"].ToString();
                Firm firm = firmRepo.GetById(int.Parse(row["firm_id"].ToString()));
                projects.Add(new Project() { Id = id, Name = name, Firm = firm });
            }
            return projects;
        }

        public Project GetById(int id)
        {
            conn = new SqlConnection(connectionString);
            conn.Open();

            string selectById = "SELECT * FROM Project WHERE id = @id";
            SqlCommand cmd = new SqlCommand(selectById, conn);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            adapter.Fill(ds, "Project");
            dt = ds.Tables["Project"];
            conn.Close();

            Project retVal = null;

            foreach (DataRow row in dt.Rows)
            {
                int project_id = int.Parse(row["id"].ToString());
                string name = row["name"].ToString();
                Firm firm = firmRepo.GetById(int.Parse(row["firm_id"].ToString()));

                retVal = new Project() { Id = project_id, Name = name, Firm = firm };
            }

            return retVal;
        }

        public bool Create(Project project)
        {
            string insert = "INSERT INTO Project (name, firm_id) VALUES (@name, @firm_id)";
            conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@name", project.Name);
            cmd.Parameters.AddWithValue("@firm_id", project.Firm.Id);

            var retVal = cmd.ExecuteNonQuery();
            conn.Close();

            if (retVal == 1)
                return true;

            return false;
        }

        public bool Update(int id, Project project)
        {
            conn = new SqlConnection(connectionString);
            conn.Open();

            string update = "UPDATE Project SET name = @name, firm_id = @firm_id WHERE id = @id";
            SqlCommand cmd = new SqlCommand(update, conn);
            cmd.Parameters.AddWithValue("@name", project.Name);
            cmd.Parameters.AddWithValue("@firm_id", project.Firm.Id);
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

            string delete = "DELETE FROM Project WHERE id = @id";
            SqlCommand cmd = new SqlCommand(delete, conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public IEnumerable<Project> GetByFirmId(int firmId)
        {
            string select = "SELECT * FROM Project WHERE firm_id = @firmId";
            conn = new SqlConnection(connectionString);
            conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(select, conn);
            cmd.Parameters.AddWithValue("@firmId", firmId);

            adapter.SelectCommand = cmd;
            adapter.Fill(ds, "Project");
            dt = ds.Tables["Project"];
            conn.Close();

            List<Project> projects = new List<Project>();

            foreach (DataRow row in dt.Rows)
            {
                int id = int.Parse(row["id"].ToString());
                string name = row["name"].ToString();
                Firm firm = firmRepo.GetById(int.Parse(row["firm_id"].ToString()));
                projects.Add(new Project() { Id = id, Name = name, Firm = firm });
            }
            return projects;
        }

        public IEnumerable<Project> GetByEmployeeId(int employeeId)
        {
            string select = "SELECT * FROM Active WHERE employee_id = @employeeId";
            conn = new SqlConnection(connectionString);
            conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(select, conn);
            cmd.Parameters.AddWithValue("@employeeId", employeeId);

            adapter.SelectCommand = cmd;
            adapter.Fill(ds, "Project");
            dt = ds.Tables["Project"];
            conn.Close();

            List<Project> projects = new List<Project>();

            foreach (DataRow row in dt.Rows)
            {
                Project project = GetById(int.Parse(row["project_id"].ToString()));
                projects.Add(project);
            }
            return projects;
        }

        public bool AddToProject(int SelectedEmployeeId, int projectId)
        {
            string insert = "INSERT INTO Active (employee_id, project_id) VALUES (@SelectedEmployeeId, @projectId)";
            conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@SelectedEmployeeId", SelectedEmployeeId);
            cmd.Parameters.AddWithValue("@projectId", projectId);

            var retVal = cmd.ExecuteNonQuery();
            conn.Close();

            if (retVal == 1)
                return true;

            return false;
        }

        public bool RemoveFromProject(int employeeId, int projectId)
        {
            string delete = "DELETE FROM Active WHERE employee_id = @employeeId AND project_id = @projectId";
            conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(delete, conn);
            cmd.Parameters.AddWithValue("@employeeId", employeeId);
            cmd.Parameters.AddWithValue("@projectId", projectId);

            var retVal = cmd.ExecuteNonQuery();
            conn.Close();

            if (retVal == 1)
                return true;

            return false;
        }
    }
}