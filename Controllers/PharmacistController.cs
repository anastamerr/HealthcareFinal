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
    public class PharmacistController : Controller
    {
        // GET: Pharmacist
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();

        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Pharmacist userModel)

        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Final;Integrated Security=True;MultipleActiveResultSets=True"; // Replace with your actual connection string
            if (ModelState.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Pharmacists WHERE UserEmail = @Email AND Password = @Password";
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

        public ActionResult PharmacistRegister(FormCollection form)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connstring"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand("PharmacistRegister", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (cmd)
                {
                    //TO BE CONT

                    cmd.Parameters.AddWithValue("@UserEmail", form["UserEmail"]);
                    cmd.Parameters.AddWithValue("@Password", form["Password"]);
                    cmd.Parameters.AddWithValue("@IdentificationCode", form["identificationCode"]);
                    cmd.Parameters.AddWithValue("@Name", form["name"]);
                    cmd.Parameters.AddWithValue("@Pharmacy_name", form["pharmacy_name"]);
                    cmd.Parameters.AddWithValue("@First_working_day", form["first_working_day"]);
                    conn.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("Register");
                    }
                    conn.Close();
                }
                return RedirectToAction("Index");

            }


        }
        


        // i will come up back to this
        public ActionResult ViewMyInfo()
        {
            List<Pharmacist> data = new List<Pharmacist>();

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Final;Integrated Security=True;MultipleActiveResultSets=True"; // Replace with your actual connection string
                                                                                                                                                          //  string query = "SELECT * FROM Nurse WHERE Nurse.UserEmail = " + temp; // Replace with your query
            string query = "SELECT * FROM Pharmacists";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Pharmacist item = new Pharmacist
                            {
                                UserEmail = reader.GetString(reader.GetOrdinal("UserEmail")),
                                Password = reader.GetString(reader.GetOrdinal("Password")),
                                identificationCode = reader.GetInt32(reader.GetOrdinal("identificationCode")),
                                name = reader.GetString(reader.GetOrdinal("name")),
                                pharmacy_name = reader.GetString(reader.GetOrdinal("pharmacy_name")),
                                first_working_day = reader.GetDateTime(reader.GetOrdinal("first_working_day")),


                            };
                            data.Add(item);
                        }
                    }
                }
            }

            return View(data); // Pass the data to the view
        }

        


        public ActionResult CheckPrescriptions()
        {
            return View();
        }

        public ActionResult CheckPres(FormCollection form)
        {
            {
                List<CheckPres> data = new List<CheckPres>();

                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Final;Integrated Security=True;MultipleActiveResultSets=True"; // Replace with your actual connection string
                string query = "SELECT * FROM Prescriptions WHERE Doctor_id = @DoctorID AND SSN = @PatientID AND DateOfPres = @Date "; // Replace with your query
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DoctorID", form["Doctor_id"]);
                        command.Parameters.AddWithValue("@PatientID", form["SSN"]);
                        command.Parameters.AddWithValue("@Date", form["dateofpres"]);
                       
                        
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CheckPres item = new CheckPres
                                {
                                    dateofpres = reader.GetDateTime(reader.GetOrdinal("dateofpres")),

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
                SqlCommand cmd = new SqlCommand("EditPharrProfile", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@identificationCode", form["identificationCode"]);

                using (cmd)
                {
                    // Add other required parameters similarly
                    cmd.Parameters.AddWithValue("@name", form["name"]);
                    cmd.Parameters.AddWithValue("@pharmacy_name", form["pharmacy_name"]);
                    cmd.Parameters.AddWithValue("@first_working_day", form["first_working_day"]);
                    

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return View();
        }
    }
}


