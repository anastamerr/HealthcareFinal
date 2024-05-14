using HealthcareFinal.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Configuration;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;

namespace HealthcareFinal.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();

        }
        string temp = "";
        public ActionResult RegisterPatients(FormCollection form) {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connstring"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("PatientRegister",conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using(cmd)
                {
                   //TO BE CONT
                   
                    cmd.Parameters.AddWithValue("@email", form["UserEmail"]);
                    if (!(form["UserEmail"].Equals (null)))
                    {
                        temp = form["UserEmail"].ToString();
                    }
                    cmd.Parameters.AddWithValue("@Pass", form["Password"]);
                    cmd.Parameters.AddWithValue("@SSN", form["SSN"]);
                    cmd.Parameters.AddWithValue("@ElectronicHealthRecords", form["ElectronicHealthRecords"]);
                    cmd.Parameters.AddWithValue("@EmergencyContactInformation", form["EmergencyContactInformation"]);
                    cmd.Parameters.AddWithValue("@Allergies", form["Allergies"]);
                    cmd.Parameters.AddWithValue("@ChronicDiseases", form["ChronicDiseases"]);
                    cmd.Parameters.AddWithValue("@Vaccines", form["Vaccines"]);
                    cmd.Parameters.AddWithValue("@PrescribedDrugs", form["PrescribedDrugs"]);
                    cmd.Parameters.AddWithValue("@Results", form["Results"]);
                    cmd.Parameters.AddWithValue("@Hospital_name", form["HospitalName.Hospital_name"]);
                    cmd.Parameters.AddWithValue("@Companyid", form["CompanyId.CompanyId"]);
                    cmd.Parameters.AddWithValue("@NurseID", form["NurseId.NurseId"]);
                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }catch (Exception ex)
                    {
                        return RedirectToAction("Register");
                    }
                    conn.Close();
                }
                return RedirectToAction("Index");

            }
        
        
        }
        public ActionResult MakeAppointments()
        {
            return View();
        }


        public ActionResult ApptsMaking(FormCollection form)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connstring"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("MakeAppointmentWithDoctor", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (cmd)
                {
                    //TO BE CONT


                    cmd.Parameters.AddWithValue("@Doctor_id", form["Doctor_id"]);
                  
                    
                    cmd.Parameters.AddWithValue("@PatientSSN", form["SSN"]);
                    cmd.Parameters.AddWithValue("@AppointmentDate", form["Appointment_date"]);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return RedirectToAction("Index");

            }


        }
        public ActionResult EditMyProfile()
        {
            return View();
        }
        public ActionResult EditInfo(FormCollection form)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connstring"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("EditMyProfile", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SSN", form["SSN"]);



                using (cmd)
                {
                    
                    // Add other required parameters similarly
                    cmd.Parameters.AddWithValue("@ElectronicHealthRecords", form["ElectronicHealthRecords"]);
                    cmd.Parameters.AddWithValue("@EmergencyContactInformation", form["EmergencyContactInformation"]);
                    cmd.Parameters.AddWithValue("@Allergies", form["Allergies"]);
                    cmd.Parameters.AddWithValue("@ChronicDiseases", form["ChronicDiseases"]);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return RedirectToAction("Index");
        }






        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginPatient(Patient userModel)

        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Final;Integrated Security=True;MultipleActiveResultSets=True"; // Replace with your actual connection string
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Patient WHERE UserEmail = @Email AND Password = @Password";
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
    

        
    





        public ActionResult ChecksHospitalDocs()
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

        public ActionResult ChecksHospital()
        {
            return View();
        }
        public ActionResult ChecksHospitalfunc(FormCollection form)
        {
            List<ChecksHospitalDocs> data = new List<ChecksHospitalDocs>();

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Final;Integrated Security=True;MultipleActiveResultSets=True"; // Replace with your actual connection string
            string query = "SELECT * FROM ChecksInHospital WHERE Doctor_id = @DoctorId "; // Replace with your query
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DoctorId", form["DoctorId"] );

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







        [HttpPost]
        public ActionResult LoginAsPatient(string username, string password)
        {
            string connectionString = "YourConnectionString"; // Replace with your actual connection string
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";

            int result = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    result = (int)command.ExecuteScalar(); // Check if there is a matching user with the given username and password
                }
            }

            if (result > 0)
            {
                // Authentication successful, redirect to a different page
                return RedirectToAction("ViewMyInfo");
            }
            else
            {
                // Authentication failed, display error message or redirect back to login
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }
        }
    }





}

    
