using MVC6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
namespace ADOMVC.Controllers
{
    public class EmployeeController : Controller
    {
        SqlConnection connection = null;
        SqlCommand command = null;

        static string GetConnectionString()
        {
            return @"data source=(localdb)\MSSQLLocalDB;initial catalog=EmpDb;integrated security=true";
        }
        static SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }
        public EmployeeController()
        {
        }
        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            using (connection = GetConnection())
            {
                {
                    using (command = new SqlCommand())
                    {
                        command.CommandText = "Select * from [Table]";
                        command.Connection = connection;
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Employee employee = new Employee()
                                {
                                    Id = (int)reader["id"],
                                    Name = reader["name"].ToString(),
                                    Dob = (DateTime)reader["dob"],
                                    Salary = (int)reader["salary"]

                                };
                                employees.Add(employee);

                            }
                        }
                        else
                        {
                            ViewBag.msg = "There are no records";
                            return View();
                        }
                    }
                }
            }
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            using (connection = GetConnection())
            {
                {
                    using (command = new SqlCommand())
                    {
                        command.CommandText = $"insert into [Table](id,name, dob,salary) values({employee.Id},'{employee.Name}','{employee.Dob.ToString("yyy-MM-dd hh:mm:ss")}', {employee.Salary})";
                        command.Connection = connection;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        return RedirectToAction("Index");
                    }
                }
            }
        }
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                ViewBag.msg = "Please provide an ID";
                return View();
            }
            else
            {
                Employee emp = null;
                using (connection = GetConnection())
                {
                    using (command = new SqlCommand())
                    {
                        command.CommandText = $"Select * from [Table] where Id='{id}'";
                        command.Connection = connection;
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Employee employee = new Employee()
                                {
                                    Id = (int)reader["id"],
                                    Name = reader["name"].ToString(),
                                    Dob = (DateTime)reader["dob"],
                                    Salary = (int)reader["salary"]
                                };
                                emp = employee;
                            }
                            return View(emp);
                        }
                        else
                        {
                            ViewBag.msg = "There is no such record.";
                            return View();
                        }
                    }
                }
            }
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                using (connection = GetConnection())
                {
                    using (command = new SqlCommand())
                    {
                        command.CommandText = $"UPDATE [Table] SET Name='{employee.Name}', Dob='{employee.Dob.ToString("yyyy-MM-dd")}', Salary={employee.Salary} WHERE Id={employee.Id}";
                        command.Connection = connection;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(employee);
        }
        public IActionResult Display(int? id)
        {
            if (!id.HasValue)
            {
                ViewBag.msg = "Please provide a ID"; return View();
            }
            else
            {
                Employee emp = null;
                using (connection = GetConnection())
                {
                    {
                        using (command = new SqlCommand())
                        {
                            command.CommandText = $"Select * from [Table] where Id='{id}'";
                            command.Connection = connection;
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Employee employee = new Employee()
                                    {
                                        Id = (int)reader["id"],
                                        Name = reader["name"].ToString(),
                                        Dob = (DateTime)reader["dob"],
                                        Salary = (int)reader["salary"]
                                    };
                                    emp = employee;
                                }
                                return View(emp);
                            }
                            else
                            {
                                ViewBag.msg = "There is no such record";
                                return View();
                            }
                        }
                    }
                }
            }
        }
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                ViewBag.msg = "Please provide a ID"; return View();
            }
            else
            {
                Employee emp = null;
                using (connection = GetConnection())
                {
                    {
                        using (command = new SqlCommand())
                        {
                            command.CommandText = $"Select * from [Table] where Id='{id}'";
                            command.Connection = connection;
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Employee employee = new Employee()
                                    {
                                        Id = (int)reader["id"],
                                        Name = reader["name"].ToString(),
                                        Dob = (DateTime)reader["dob"],
                                        Salary = (int)reader["salary"]
                                    };
                                    emp = employee;
                                }
                            }
                            else
                            {
                                ViewBag.msg = "There is no such record";
                                return View();
                            }
                        }
                    }
                }
                if (emp == null)
                {
                    ViewBag.msg = "There is no record woth this ID";
                    return View();
                }
                else
                    return View(emp);
            }
        }

        [HttpPost]
        public IActionResult Delete(Employee employee, int id)
        {
            using (connection = GetConnection())
            {
                {
                    using (command = new SqlCommand())
                    {
                        command.CommandText = $"DELETE FROM [Table] WHERE Id='{id}';";
                        command.Connection = connection;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        return RedirectToAction("Index");
                    }
                }
            }
        }
    }
}
