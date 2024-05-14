using HealthcareFinal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthcareFinal.Controllers
{
    public class NurseController : Controller
    {
        // GET: Nurse
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();

        }
        string temp = "";
        public ActionResult NurseRegister(FormCollection form)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connstring"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("NurseRegister", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (cmd)
                {
                    //TO BE CONT

                    cmd.Parameters.AddWithValue("@UserEmail", form["UserEmail"]);
                    if (!(form["UserEmail"].Equals(null)))
                    {
                        temp = form["UserEmail"].ToString();
                    }
                    cmd.Parameters.AddWithValue("@Password", form["Password"]);
                    cmd.Parameters.AddWithValue("@First_name", form["FirstName"]);
                    cmd.Parameters.AddWithValue("@Middle_name", form["MiddleName"]);
                    cmd.Parameters.AddWithValue("@Last_name", form["LastName"]);
                    cmd.Parameters.AddWithValue("@Nurse_ID", form["NurseId"]);
                    cmd.Parameters.AddWithValue("@Shifts", form["Shifts"]);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return RedirectToAction("Index");

            }


        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Nurse userModel)

        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Final;Integrated Security=True;MultipleActiveResultSets=True"; // Replace with your actual connection string
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Nurse WHERE UserEmail = @Email AND Password = @Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", userModel.UserEmail);
                        command.Parameters.AddWithValue("@Password", userModel.Password);

                        int count = (int)command.ExecuteScalar();
                        if (count > 0)
                        {
                            // Successful login
                            return RedirectToAction("Index"); // Change to your desired action and controller
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid email or password");
                        }
                    }
                }
            }
            return View(userModel);
        }

        public ActionResult ViewMyInfo()
        {
            List<Patient> data = new List<Patient>();

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Final;Integrated Security=True;MultipleActiveResultSets=True"; // Replace with your actual connection string
            string query = "SELECT * FROM Patient"; // Replace with your query

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //command.parameter
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Patient item = new Patient
                            {
                                UserEmail = reader.GetString(reader.GetOrdinal("UserEmail")),
                                Password = reader.GetString(reader.GetOrdinal("Password")),
                                SSN = reader.GetInt32(reader.GetOrdinal("SSN")),
                                ElectronicHealthRecords = reader.GetString(reader.GetOrdinal("ElectronicHealthRecords")),
                                EmergencyContactInformation = reader.GetString(reader.GetOrdinal("EmergencyContactInformation")),
                                Allergies = reader.GetString(reader.GetOrdinal("Allergies")),
                                ChronicDiseases = reader.GetString(reader.GetOrdinal("chronicDiseases")),
                                Vaccines = reader.GetString(reader.GetOrdinal("vaccines")),
                                PrescribedDrugs = reader.GetString(reader.GetOrdinal("prescribedDrugs")),
                                Results = reader.GetString(reader.GetOrdinal("Results")),
                            };
                            data.Add(item);
                        }
                    }
                }
            }

            return View(data); // Pass the data to the view
        }



        public ActionResult CheckHospitalDocsAndPatients()
        {
            List<ChecksHospitalDocs> data = new List<ChecksHospitalDocs>();

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Final;Integrated Security=True;MultipleActiveResultSets=True"; // Replace with your actual connection string
            string query = "SELECT * FROM ChecksInHospital "; // Replace with your query
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ChecksHospitalDocs item = new ChecksHospitalDocs
                            {
                                hospital_name = reader.GetString(reader.GetOrdinal("Hospital_name")),

                                SSN = reader.GetInt32(reader.GetOrdinal("SSN")),
                                Doctor_id = reader.GetInt32(reader.GetOrdinal("Doctor_id")),

                            };
                            data.Add(item);
                        }
                    }
                }
            }

            return View(data);

        }



        public ActionResult ManageCallCenter()
        {
            {
                List<call_center> data = new List<call_center>();

                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Final;Integrated Security=True;MultipleActiveResultSets=True"; // Replace with your actual connection string
                string query = "SELECT * FROM call_center "; // Replace with your query
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                call_center item = new call_center
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("id")),

                                    EMS_id = reader.GetInt32(reader.GetOrdinal("EMS_id")),
                                   

                                };
                                data.Add(item);
                            }
                        }
                    }
                }

                return View(data);

            }
        }


    }
}