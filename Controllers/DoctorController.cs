using HealthcareFinal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Input;

namespace HealthcareFinal.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();

        }



        public ActionResult RegisterDoctors(FormCollection form)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connstring"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("DoctorRegister", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                
                using (cmd)
                {
                    //TO BE CONT
                    // need to be created
                    cmd.Parameters.AddWithValue("@UserEmail", form["UserEmail"]);
                    cmd.Parameters.AddWithValue("@Password", form["Password"]);
                    cmd.Parameters.AddWithValue("@emergencyContactInformation", form["emergencyContactInformation"]);
                    cmd.Parameters.AddWithValue("@Doctor_id", form["Doctor_id"]);
                    cmd.Parameters.AddWithValue("@name", form["name"]);
                    cmd.Parameters.AddWithValue("@specialization", form["specialization"]);
                    cmd.Parameters.AddWithValue("@workingDaysAndHours", form["workingDaysAndHours"]);
                    cmd.Parameters.AddWithValue("@phoneNumber", form["phoneNumber"]);
                    cmd.Parameters.AddWithValue("@Hospital_name", form["Hospital_name"]);

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
        public ActionResult Login(Doctor userModel)

        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Final;Integrated Security=True;MultipleActiveResultSets=True"; // Replace with your actual connection string
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Doctor WHERE UserEmail = @Email AND Password = @Password";
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




        public ActionResult ViewMedicalHistory()
        {
            Patient patient = new Patient();

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Final;Integrated Security=True;MultipleActiveResultSets=True"; // Replace with your actual connection string
                                                                                                                                                          //  string query = "SELECT * FROM Nurse WHERE Nurse.UserEmail = " + temp; // Replace with your query
            string query = "SELECT allergies,chronicDiseases FROM Patient WHERE SSN=@SSN";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SSN", patient.SSN);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {


                            patient.Allergies = reader.GetString(reader.GetOrdinal("allergies"));
                            patient.ChronicDiseases = reader.GetString(reader.GetOrdinal("chronicDiseases"));




                        }
                    }
                }
            }

            return View(patient); // Pass the data to the view
        }



        public ActionResult DocRequests()
        {
            return View();
        }
        public ActionResult RequestLabtests(FormCollection form)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connstring"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("DoctorReqLab", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DoctorID", form["DoctorId"]);
                cmd.Parameters.AddWithValue("@PatientID", form["Patient.SSN"]);
                cmd.Parameters.AddWithValue("@LabTestID", form["Labtest.LabtestID"]);

                using (cmd)
                {
                    //TO BE CONT
                    // need to be created



                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return RedirectToAction("Index");
            }
        }
        public ActionResult DoctorWrite()
        {
            return View();
        }
        public ActionResult DoctorWritesPres(FormCollection form)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connstring"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("DocWritePres", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DoctorID", form["DoctorId"]);
                cmd.Parameters.AddWithValue("@PatientID", form["Patient.SSN"]);
                cmd.Parameters.AddWithValue("@Date", form["WritesPres.dateofpres"]);

                using (cmd)
                {
                    //TO BE CONT
                    // need to be created



                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return RedirectToAction("Index");
            }

        }
        public ActionResult DoctorPhone()
        {
            return View();
        }
        public ActionResult AddPhone(FormCollection form)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connstring"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("addMobile", conn);


                using (cmd)
                {
                    //TO BE CONT
                    // need to be created

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Doctor_id", form["DoctorId"]);
                    cmd.Parameters.AddWithValue("@mobile_number", form["PhoneNumber"]);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return RedirectToAction("Index");
            }
        }


        public ActionResult ViewMyInfo()
        {
            return View();
        }

        public ActionResult DocInfo(FormCollection form)
        {
           List<Doctor> data = new List<Doctor>();

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Final;Integrated Security=True;MultipleActiveResultSets=True";
            string query = "SELECT * FROM Doctor WHERE DoctorId = 1"; // Query to retrieve a specific doctor by ID
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DoctorId", form["DoctorId"]); // Parameterized query to avoid SQL injection

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Doctor doctor1 = new Doctor
                            {
                                UserEmail = reader.GetString(reader.GetOrdinal("UserEmail")),
                                Password = reader.GetString(reader.GetOrdinal("Password")),
                             //   ElectronicHealthRecords = reader.GetString(reader.GetOrdinal("ElectronicHealthRecords")),
                                EmergencyContactInformation = reader.GetString(reader.GetOrdinal("EmergencyContactInformation")),
                                DoctorId = reader.GetInt32(reader.GetOrdinal("DoctorId")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Specialization = reader.GetString(reader.GetOrdinal("Specialization")),
                                WorkingDaysAndHours = reader.GetString(reader.GetOrdinal("WorkingDaysAndHours")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                               HospitalName = reader.GetString(reader.GetOrdinal("HospitalName"))
                                // Add other properties as needed
                            };
                            data.Add(doctor1);
                        }
                    }
                }
            }

            return View(data); // Pass the doctor details to the view
        }

    }
}