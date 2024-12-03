using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using JobPortalWebApplication.Models.Response;
using JobPortalWebApplication.Models.Request;
using System.Web.UI.WebControls;
using System.Drawing.Drawing2D;
using System.Drawing;

using System.IO;
using System.Xml.Linq;
using Document = iTextSharp.text.Document;
using iTextSharp.text.pdf.parser.clipper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using static iTextSharp.text.pdf.PRTokeniser;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Data.SqlTypes;
using JobPortalWebApplication.Candidate;
using System.Globalization;
using System.Web.Configuration;
using JobPortalWebApplication.Employer;
using Org.BouncyCastle.Asn1.X509;
using ViewJobs = JobPortalWebApplication.Models.Response.ViewJobs;
using System.Collections;
using System.Diagnostics;




namespace JobPortalWebApplication.DataBase
{

    public class DataAccess
    {
        private string _connectionString;
        private string ResumeUploadFilePath = "C:\\Nimisha\\uploads";

        public DataAccess(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        public List<PlanType> GetPlanTypes()
        {
            var planTypes = new List<PlanType>();

            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                List<PlanType> plansList = new List<PlanType>();

                string PlanName = "";
                string query = "Select * from [Plan]";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;

                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    {
                        while (reader.Read())
                        {
                            planTypes.Add(new PlanType
                            {
                                PlanId = reader.GetInt32(reader.GetOrdinal("PlanId")),
                                PlanName = reader.GetString(reader.GetOrdinal("PlanName"))
                            });
                        }
                    }
                }
            }

            return planTypes;
        }
        public List<JobTitles> GetJobTitles()
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    List<JobTitles> jobTitlesList = new List<JobTitles>();

                    Sqlcon.Open();

                    string query = "Select JobTitle from JobPostings ";
                    using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.Text;
                        SqlDataReader reader = sqlcmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                JobTitles jobtitle = new JobTitles();
                                jobtitle.JobTitle = reader["JobTitle"].ToString();

                                jobTitlesList.Add(jobtitle);
                            }
                        }

                    }
                    return jobTitlesList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<JobLocation> GetJobLocation()
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    List<JobLocation> jobLocationsList = new List<JobLocation>();

                    Sqlcon.Open();
                    string Location = "";
                    string query = "Select * from JobLocation ";
                    using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.Text;
                        SqlDataReader reader = sqlcmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                JobLocation jobLocation = new JobLocation();
                                jobLocation.LocationName = reader["Location"].ToString();
                                jobLocation.JobLocationID = (int)reader["JobLocationID"];
                                jobLocationsList.Add(jobLocation);
                            }

                        }

                    }
                    return jobLocationsList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SkillList> GetCoreSkill(int CandidateId)
        {
            var coreSkills = new List<SkillList>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select * from CandidateSkills where CandidateId=@CandidateId", connection))
                {
                    command.CommandType = CommandType.Text;

                    connection.Open();
                    command.Parameters.AddWithValue("@CandidateId", CandidateId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int Id = reader["CoreSkill"] != DBNull.Value ? Convert.ToInt32(reader["CoreSkill"]) : 0;
                                string CoreSkill = GetSkillById(Id, "CoreSkills");
                                if (CoreSkill != "")
                                {
                                    float percentage = reader["CoreSkillPercentage"] != DBNull.Value ? Convert.ToSingle(reader["CoreSkillPercentage"]) : 0.0f;

                                    coreSkills.Add(new SkillList
                                    {
                                        CandidateSkillId = reader["CandidateSkillId"] != null ? Convert.ToInt32(reader["CandidateSkillId"]) : 0,

                                        Id = Id,
                                        CoreSkills = CoreSkill,
                                        Percentage = percentage
                                    });
                                }

                            }
                        }
                    }
                }
            }
            return coreSkills;
        }

        public List<SkillList> GetSoftSkill(int CandidateId)
        {
            var softSkills = new List<SkillList>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select * from CandidateSkills where CandidateId=@CandidateId", connection))
                {
                    command.CommandType = CommandType.Text;

                    connection.Open();
                    command.Parameters.AddWithValue("@CandidateId", CandidateId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int Id = 0;
                            if (!reader.IsDBNull(reader.GetOrdinal("SoftSkill")))
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("SoftSkill"));
                            }

                            // Only add to the list if Id is valid (non-zero)
                            if (Id > 0)
                            {
                                string SoftSkill = GetSkillById(Id, "SoftSkills");
                                softSkills.Add(new SkillList
                                {
                                    CandidateSkillId = reader["CandidateSkillId"] != null ? Convert.ToInt32(reader["CandidateSkillId"]) : 0,
                                    Id = Id,
                                    SoftSkills = SoftSkill,
                                    Percentage = (float)(reader["SoftSkillPercentage"] != null ? Convert.ToDecimal(reader["SoftSkillPercentage"]) : 0)

                                });
                            }
                        }
                    }
                }
            }
            return softSkills;
        }
        public List<JobLocation> GetAllJobLocation()
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    List<JobLocation> jobLocationsList = new List<JobLocation>();

                    Sqlcon.Open();
                    string Location = "";
                    string query = "Select * from JobLocation ";
                    using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.Text;
                        SqlDataReader reader = sqlcmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                JobLocation jobLocation = new JobLocation();
                                jobLocation.LocationName = reader["Location"].ToString();
                                jobLocation.JobLocationID = (int)reader["JobLocationID"];
                                jobLocationsList.Add(jobLocation);
                            }

                        }

                    }
                    return jobLocationsList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SkillList> GetCoreSkills()
        {
            var coreSkills = new List<SkillList>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select * from Skills", connection))
                {
                    command.CommandType = CommandType.Text;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            coreSkills.Add(new SkillList
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("SkillId")),
                                CoreSkills = reader.GetString(reader.GetOrdinal("CoreSkills"))
                            });
                        }
                    }
                }
            }
            return coreSkills;
        }

        public List<SkillList> GetSoftSkills()
        {
            var softSkills = new List<SkillList>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("Select * from Skills", connection))
                {
                    command.CommandType = CommandType.Text;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            softSkills.Add(new SkillList
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("SkillId")),
                                SoftSkills = reader.GetString(reader.GetOrdinal("SoftSkills"))
                            });
                        }
                    }
                }
            }

            return softSkills;
        }

        public List<Availability> GetAllAvailability()
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    Sqlcon.Open();
                    List<Availability> availabilityList = new List<Availability>();

                    string query = "Select * from Availability";
                    using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.Text;
                        // sqlcmd.Parameters.AddWithValue("@AvailabilityID", availabilityid);
                        //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                        SqlDataReader reader = sqlcmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                availabilityList.Add(new Availability
                                {
                                    AvailabilityName = reader["AvailabilityName"].ToString(),
                                    AvailabilityID = (int)reader["AvailabilityID"]
                                });
                                //return JobType;
                            }
                        }

                    }
                    return availabilityList;
                }
            }
            catch (Exception ex) { throw ex; }
            throw new NotImplementedException();
        }
        public List<JobTypes> GetAllJobTypes()
        {
            try
            {
                List<JobTypes> jobTypesList = new List<JobTypes>();

                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    Sqlcon.Open();
                    string JobType = "";
                    string query = "Select * from JobTypes ";
                    using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.Text;
                        SqlDataReader reader = sqlcmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                JobTypes jobtype = new JobTypes();
                                jobtype.JobName = reader["JobTypeName"].ToString();
                                jobtype.JobTypeID = (int)reader["JobTypeID"];
                                jobtype.Description = reader["Description"].ToString();
                                jobTypesList.Add(jobtype);
                                //return JobType;
                            }
                        }
                    }
                    return jobTypesList;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Country> GetAllCountry()
        {
            try
            {
                List<Country> countryList = new List<Country>();

                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    Sqlcon.Open();
                    string CountryName = "";
                    string query = "Select * from Country ";
                    using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.Text;
                        // sqlcmd.Parameters.AddWithValue("@CountryID", countryid);
                        //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                        SqlDataReader reader = sqlcmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Country country = new Country();
                                country.CountryName = reader["CountryName"].ToString();
                                country.CountryID = (int)reader["CountryID"];
                                countryList.Add(country);
                            }

                        }
                    }
                    return countryList;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<Degrees> GetAllDegrees()
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    Sqlcon.Open();
                    List<Degrees> degreesList = new List<Degrees>();

                    string DegreeName = "";
                    string query = "Select * from Degree";
                    using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.Text;

                        SqlDataReader reader = sqlcmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Degrees degrees = new Degrees();
                                degrees.DegreeName = reader["DegreeName"] != null ? reader["DegreeName"].ToString() : null;
                                degrees.DegreeId = (int)reader["DegreeId"];
                                degreesList.Add(degrees);

                            }
                        }

                    }
                    return degreesList;
                }
            }
            catch (Exception ex) { throw ex; }

        }

        public string UserOTP(string otp, string email, string user)
        {
            try
            {
                // string connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
                string message = "";
                var startTime = DateTime.Now;
                DateTime endTime = startTime.AddMinutes(10);
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {

                    using (SqlCommand sqlcmd = new SqlCommand())
                    {
                        sqlcmd.Connection = connection;
                        connection.Open();
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.CommandText = "SP_InsertOrUpdateOTP";

                        sqlcmd.Parameters.AddWithValue("@OTP", otp);
                        sqlcmd.Parameters.AddWithValue("@Email", email);
                        sqlcmd.Parameters.AddWithValue("@StartTime", startTime);
                        sqlcmd.Parameters.AddWithValue("@EndTime", endTime);
                        sqlcmd.Parameters.AddWithValue("@UserType", user);
                        sqlcmd.Parameters.AddWithValue("@Used", 0);
                        sqlcmd.Parameters.AddWithValue("@Action", "Insert");
                        int rows = (int)sqlcmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            message = "Success";
                            return message;
                        }
                        else return message;
                        connection.Close();
                    }
                }

                //return flag;
            }
            catch (Exception e)
            {
                throw e;

            }
        }
        public EmployerRegResponse TrainerLogin(LoginRequest request)
        {
            try
            {
                EmployerRegResponse response = new EmployerRegResponse();
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand())
                    {
                        sqlcmd.Connection = Sqlcon;
                        Sqlcon.Open();
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.CommandText = "SP_TrainerLogin";
                        sqlcmd.Parameters.AddWithValue("@Email", request.Email);

                        using (SqlDataReader reader = sqlcmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    response.EmployeeId = (int)reader["TrainerID"];
                                    response.CompanyEmail = reader["CompanyEmailAddress"] != DBNull.Value ? reader["CompanyEmailAddress"].ToString() : null;
                                    response.Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : null;
                                    response.EmailStatus = reader["EmailStatus"] != DBNull.Value ? reader["EmailStatus"].ToString() : null;
                                }

                                return response;
                            }
                            else
                            {

                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string CandidateRegister(CandidateRegister request, string pwdencrypt)
        {
            try
            {
                string message = "";

                using (SqlConnection sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_RegisterCandidate", sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;

                        sqlcmd.Parameters.AddWithValue("@OTP", request.OTP);
                        sqlcmd.Parameters.AddWithValue("@FirstName", request.FirstName);
                        sqlcmd.Parameters.AddWithValue("@LastName", request.LastName);
                        sqlcmd.Parameters.AddWithValue("@PhoneNumber", request.PhoneNumber);
                        sqlcmd.Parameters.AddWithValue("@Email", request.Email);
                        sqlcmd.Parameters.AddWithValue("@Password", pwdencrypt);
                        sqlcmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        sqlcmd.Parameters.AddWithValue("@Action", "INSERT");
                        SqlParameter resultParam = new SqlParameter("@NewCandidateID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        sqlcmd.Parameters.Add(resultParam);
                        try
                        {
                            sqlcon.Open();
                            sqlcmd.ExecuteNonQuery();
                            int result = (int)resultParam.Value;
                            // int rows = sqlcmd.ExecuteNonQuery();
                            sqlcon.Close();
                            if (result > 0)
                            {
                                message = "Registered successfully with CandidateId" + result;
                                return message;
                            }
                            else
                                return message;

                        }
                        catch (SqlException ex)
                        {
                            return "An error occurred: " + ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
        public bool CheckEmailExists(string email)
        {

            using (SqlConnection sqlcon = new SqlConnection(_connectionString))
            {
                using (SqlCommand sqlcmd = new SqlCommand(" SELECT 1 FROM Candidate WHERE EmailAddress = @Email", sqlcon))
                {
                    sqlcmd.Parameters.AddWithValue("@Email", email);

                    sqlcon.Open();


                    object result = sqlcmd.ExecuteScalar();


                    if (result != null)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }
        public CandidateRegResponse CandidateLogin(LoginRequest request)
        {
            try
            {
                string message = "";
                CandidateRegResponse response = new CandidateRegResponse();
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand())
                    {
                        sqlcmd.Connection = Sqlcon;
                        Sqlcon.Open();
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.CommandText = "SP_CandidateLogin";

                        // Parameters for the stored procedure
                        sqlcmd.Parameters.AddWithValue("@Email", request.Email);
                        // You should ideally hash the password before sending it to the database
                        // string hashedPassword = HashPassword(request.Password);
                        sqlcmd.Parameters.AddWithValue("@Password", request.Password);
                        //sqlcmd.Parameters.AddWithValue("@Action", "Login");

                        using (SqlDataReader reader = sqlcmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    response.CandidateId = (int)reader["CandidateID"];
                                    response.EmailAddress = reader["EmailAddress"] != DBNull.Value ? reader["EmailAddress"].ToString() : null;
                                    response.Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : null;
                                    response.EmailStatus = reader["EmailStatus"] != DBNull.Value ? reader["EmailStatus"].ToString() : null;
                                }

                                return response;
                            }
                            else
                            {
                                //response = new CandidateResponse();
                                //response.Message = "InvalidLogin";
                                // Log error or provide a response indicating login failure
                                return response;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CandidateRegResponse response = new CandidateRegResponse();
                // response.Message = "InvalidLogin";
                return response;
                // Handle exceptions (e.g., logging)
                // _logger.LogError($"Error during login: {ex.Message}");
                throw;
            }
        }
        public bool CheckIfEmailExists(string email)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(1) FROM OTPLog WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", email);

                con.Open();
                int count = (int)cmd.ExecuteScalar();
                con.Close();

                return count > 0; // If count is greater than 0, the email exists
            }
        }
        public string EmployeeRegister(EmployeeRegister request, string pwdencrypt)
        {
            try
            {
                string message = "";

                using (SqlConnection sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_EmployeeRegister", sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;

                        sqlcmd.Parameters.AddWithValue("@OTP", request.OTP);
                        sqlcmd.Parameters.AddWithValue("@CompanyName", request.CompanyName);
                        sqlcmd.Parameters.AddWithValue("@PhoneNumber", request.PhoneNumber);
                        sqlcmd.Parameters.AddWithValue("@Email", request.Email);
                        sqlcmd.Parameters.AddWithValue("@Password", pwdencrypt);
                        sqlcmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        sqlcmd.Parameters.AddWithValue("@AgreementToTerms", request.AgreeTotermsAndConditions);
                        sqlcmd.Parameters.AddWithValue("@Action", "Register");
                        SqlParameter resultParam = new SqlParameter("@EmployeeID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        sqlcmd.Parameters.Add(resultParam);
                        try
                        {
                            sqlcon.Open();
                            sqlcmd.ExecuteNonQuery();
                            int result = (int)resultParam.Value;
                            // int rows = sqlcmd.ExecuteNonQuery();
                            sqlcon.Close();
                            if (result > 0)
                            {
                                message = "Registered successfully with EmployeeID " + result;
                                return message;
                            }
                            else
                                return message;
                        }
                        catch (SqlException ex)
                        {
                            return "An error occurred: " + ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
        public string TrainerRegister(TrainerRegisterRequest request, string pwdencrypt)
        {
            try
            {
                string message = "";
                using (SqlConnection sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_TrainerRegister", sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;

                        sqlcmd.Parameters.AddWithValue("@OTP", request.OTP);
                        sqlcmd.Parameters.AddWithValue("@CompanyName", request.CompanyName);
                        sqlcmd.Parameters.AddWithValue("@PhoneNumber", request.PhoneNumber);
                        sqlcmd.Parameters.AddWithValue("@Email", request.Email);
                        sqlcmd.Parameters.AddWithValue("@Password", pwdencrypt);
                        sqlcmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        sqlcmd.Parameters.AddWithValue("@AgreementToTerms", request.AgreeTotermsAndConditions);
                        sqlcmd.Parameters.AddWithValue("@Action", "Register");
                        SqlParameter resultParam = new SqlParameter("@TrainerID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        sqlcmd.Parameters.Add(resultParam);
                        try
                        {
                            sqlcon.Open();
                            sqlcmd.ExecuteNonQuery();
                            int result = (int)resultParam.Value;
                            // int rows = sqlcmd.ExecuteNonQuery();
                            sqlcon.Close();
                            if (result > 0)
                            {
                                message = "Registered successfully with TrainerID " + result;
                                return message;
                            }
                            else
                                return message;

                        }
                        catch (SqlException ex)
                        {
                            return "An error occurred: " + ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
        public EmployerRegResponse EmployeeLogin(LoginRequest request)
        {
            EmployerRegResponse response = new EmployerRegResponse();
            try
            {
                string message = "";
                // EmployerRegResponse response = new EmployerRegResponse();
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand())
                    {
                        sqlcmd.Connection = Sqlcon;
                        Sqlcon.Open();
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.CommandText = "SP_EmployeeLogin";

                        // Parameters for the stored procedure
                        sqlcmd.Parameters.AddWithValue("@Email", request.Email);
                        // You should ideally hash the password before sending it to the database
                        // string hashedPassword = HashPassword(request.Password);
                        // sqlcmd.Parameters.AddWithValue("@Password", hashedPassword);
                        //sqlcmd.Parameters.AddWithValue("@Action", "Login");

                        using (SqlDataReader reader = sqlcmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    response.EmployeeId = (int)reader["CompanyID"];
                                    response.CompanyEmail = reader["CompanyEmailAddress"] != DBNull.Value ? reader["CompanyEmailAddress"].ToString() : null;
                                    response.Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : null;
                                    response.EmailStatus = reader["EmailStatus"] != DBNull.Value ? reader["EmailStatus"].ToString() : null;
                                }

                                return response;
                            }
                            else
                            {
                                response.message = "Invalid Email";
                                // Log error or provide a response indicating login failure
                                return response;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {


                response.message = ex.Message;
                return response;
            }
        }
        private void ConvertToPdf(HttpPostedFile file, Stream outputStream)
        {
            // Ensure the outputStream is valid
            if (outputStream == null)
            {
                throw new ArgumentNullException(nameof(outputStream));
            }

            // Ensure the file is valid
            if (file == null || file.ContentLength == 0)
            {
                throw new ArgumentException("Invalid file", nameof(file));
            }

            try
            {
                using (var reader = new StreamReader(file.InputStream))
                {
                    using (var document = new Document())
                    {
                        PdfWriter.GetInstance(document, outputStream);

                        document.Open();
                        document.Add(new Paragraph(reader.ReadToEnd()));
                        document.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                // For example: _logger.LogError($"Error converting file to PDF: {ex.Message}");
                throw; // Re-throw the exception after logging
            }
        }

        public string CandidateUpdate(CandidateRequest request, int CandidateId)
        {

            try
            {
                string fileName = "";
                if (request.ResumeFile != null)
                {
                    fileName = $"{Path.GetFileNameWithoutExtension(request.ResumeFile.FileName)}.pdf";

                    // var filePath = Path.Combine("C:\\Nimisha\\", "uploads", fileName);
                    var filePath = Path.Combine("C:\\Users\\ThinkPad\\source\\repos\\JobPortalWebApplication\\uploads", fileName);
                    // Ensure the directory exists
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    // Save the file to the server as a byte stream
                    string fileExtension = Path.GetExtension(request.ResumeFile.FileName);


                    // Create directory if it doesn't exist
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ConvertToPdf(request.ResumeFile, stream);
                    }
                }

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string concatenatedCoreSkills = "";
                    string concatenatedSoftSkills = ""; var coreskillid = "";
                    if (request.SkillLists!=null && request.SkillLists.Count > 0)
                    {
                        foreach (var skilllist in request.SkillLists)
                        {
                            if (!string.IsNullOrEmpty(skilllist.CoreSkills))
                            {
                                concatenatedCoreSkills = concatenatedCoreSkills + skilllist.CoreSkills;
                            }

                            // Check if SoftSkill exists and concatenate it
                            if (!string.IsNullOrEmpty(skilllist.SoftSkills))
                            {
                                concatenatedSoftSkills = concatenatedSoftSkills + skilllist.SoftSkills;
                            }
                        }
                    }
                    //if (request.Skills != null)
                    //{
                    //    var coreSkills = request.Skills.CoreSkill == true ? request.Skills.CoreSkillIds.ToList() : null;
                    //    var softSkills = request.Skills.SoftSkill == true ? request.Skills.SoftSkillIds.ToList() : null;

                    //    if (coreSkills.Any())
                    //    {
                    //        concatenatedCoreSkills = GetSkillsByIds(connection, coreSkills, "CoreSkills");
                    //    }

                    //    // Retrieve Soft Skills
                    //    if (softSkills.Any())
                    //    {
                    //        concatenatedSoftSkills = GetSkillsByIds(connection, softSkills, "SoftSkills");
                    //    }


                    using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand sqlcmd = new SqlCommand("SP_CandidateUpdate", Sqlcon))
                        {
                            sqlcmd.CommandType = CommandType.StoredProcedure;

                            sqlcmd.Parameters.AddWithValue("@PlanId", request.PlanId);
                            //sqlcmd.Parameters.AddWithValue("@FirstName", request.FirstName);
                            //sqlcmd.Parameters.AddWithValue("@LastName", request.LastName);
                            //sqlcmd.Parameters.AddWithValue("@EmailAddress", request.EmailAddress);                          
                            //sqlcmd.Parameters.AddWithValue("@MobileNumber", request.MobileNumber);
                            sqlcmd.Parameters.AddWithValue("@Address", request.Address);
                            sqlcmd.Parameters.AddWithValue("@City", request.City);
                            sqlcmd.Parameters.AddWithValue("@PostalZipCode", request.PostalOrZipCode);
                            sqlcmd.Parameters.AddWithValue("@StateProvince", request.StateOrProvince);
                            sqlcmd.Parameters.AddWithValue("@CountryID", request.CountryID != 0 ? request.CountryID : 0);
                            sqlcmd.Parameters.AddWithValue("@HighestEducationLevel", request.HighestEducationLevel);
                            sqlcmd.Parameters.AddWithValue("@CoreSkills", concatenatedCoreSkills);
                            sqlcmd.Parameters.AddWithValue("@SoftSkills", concatenatedSoftSkills);
                            sqlcmd.Parameters.AddWithValue("@JobTypeID", request.JobTypesID);
                            sqlcmd.Parameters.AddWithValue("@JobLocationID", request.JobLocationID);
                            sqlcmd.Parameters.AddWithValue("@AvailabilityID", request.AvailabilityID);
                            sqlcmd.Parameters.AddWithValue("@FreeTraining", request.FreeTraining);
                            sqlcmd.Parameters.AddWithValue("@PaidTraining", request.PaidTraining);
                            sqlcmd.Parameters.AddWithValue("@CareerConsultantContact", request.CareerConsultantContact);
                            sqlcmd.Parameters.AddWithValue("@ResumeFile", fileName != null ? fileName : null);
                            sqlcmd.Parameters.AddWithValue("@CoverLetter", request.CoverLetter);
                            sqlcmd.Parameters.AddWithValue("@LinkedInProfile", request.LinkedInProfile);
                            sqlcmd.Parameters.AddWithValue("@Portfolio", request.Portfolio);
                            sqlcmd.Parameters.AddWithValue("@CreatedDate", null);
                            sqlcmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                            sqlcmd.Parameters.AddWithValue("@NewCandidateID", CandidateId);
                            sqlcmd.Parameters.AddWithValue("@Action", "UPDATE");
                            // Add the output parameter


                            Sqlcon.Open();
                            //sqlcmd.ExecuteNonQuery();
                            //int result = (int)resultParam.Value;

                            // Add the output parameter

                            //Sqlcon.Open();
                            int result = sqlcmd.ExecuteNonQuery();



                            //if (request.Educations != null && request.Educations.Count > 0)
                            //{
                            //    foreach (var education in request.Educations)
                            //    {
                            //        using (var sqlcmd1 = new SqlCommand("INSERT INTO Education (CandidateID, DegreeId, CollegeUniversityName, PlaceAddress, GraduatedOrPursuing, KeySkills, AcademicProject, CreatedDate) VALUES (@NewCandidateID, @DegreeId, @CollegeUniversityName, @PlaceAddress, @GraduatedOrPursuing, @KeySkillsEdu, @AcademicProject, @CreatedDate)", Sqlcon))
                            //        {
                            //            // Add parameters for the current education entry
                            //            sqlcmd1.Parameters.AddWithValue("@DegreeId", education.DegreeId != null ? (object)education.DegreeId : DBNull.Value);
                            //            sqlcmd1.Parameters.AddWithValue("@CollegeUniversityName", education.CollegeUniversityName != null ? (object)education.CollegeUniversityName : DBNull.Value);
                            //            sqlcmd1.Parameters.AddWithValue("@PlaceAddress", education.PlaceAddress != null ? (object)education.PlaceAddress : DBNull.Value);
                            //            sqlcmd1.Parameters.AddWithValue("@GraduatedOrPursuing", education.GraduatedOrPursuing != null ? (object)education.GraduatedOrPursuing : DBNull.Value);
                            //            sqlcmd1.Parameters.AddWithValue("@KeySkillsEdu", education.KeySkills != null ? (object)education.KeySkills : DBNull.Value);
                            //            sqlcmd1.Parameters.AddWithValue("@AcademicProject", education.AcademicProject != null ? (object)education.AcademicProject : DBNull.Value);
                            //            sqlcmd1.Parameters.AddWithValue("@NewCandidateID", CandidateId);
                            //            sqlcmd1.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                            //            // Execute the query
                            //            int rows = sqlcmd1.ExecuteNonQuery();
                            //        }
                            //    }
                            //}
                            //if (request.WorkExperience != null && request.WorkExperience.Count > 0)
                            //{
                            //    foreach (var workExperience in request.WorkExperience)
                            //    {
                            //        using (var sqlcmd2 = new SqlCommand("INSERT INTO WorkExperience (CandidateID, CompanyName, CompanyAddress, Designation, KeySkills, CreatedDate) VALUES (@NewCandidateID, @CompanyName, @CompanyAddress, @Designation, @KeySkills, @CreatedDate)", Sqlcon))
                            //        {
                            //            // Add parameters for the current work experience entry
                            //            sqlcmd2.Parameters.AddWithValue("@NewCandidateID", CandidateId);
                            //            sqlcmd2.Parameters.AddWithValue("@CompanyName", workExperience.CompanyName != null ? (object)workExperience.CompanyName : DBNull.Value);
                            //            sqlcmd2.Parameters.AddWithValue("@CompanyAddress", workExperience.CompanyAddress != null ? (object)workExperience.CompanyAddress : DBNull.Value);
                            //            sqlcmd2.Parameters.AddWithValue("@Designation", workExperience.Designation != null ? (object)workExperience.Designation : DBNull.Value);
                            //            sqlcmd2.Parameters.AddWithValue("@KeySkills", workExperience.KeySkillsPracticed != null ? (object)workExperience.KeySkillsPracticed : DBNull.Value);
                            //            sqlcmd2.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                            //            // Execute the query
                            //            sqlcmd2.ExecuteNonQuery();
                            //        }
                            //    }
                            //}

                            //SaveCandidateSkills(connection, CandidateId, request.Skills.CoreSkillIds, request.Skills.CoreSkillPercentages, request.Skills.SoftSkillIds, request.Skills.SoftSkillPercentages);
                            //
                            if (result <= 0)
                                return "updation failed";
                            else if (result >= 1)
                                return "Successfully updated";
                            else return " data error";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log them)
                return "Error: " + ex.Message;
            }
        }

        public void AddEducationDetail(string degreeId, string collegeName, string address, string graduated, string keySkills, string academicProject, int candidateId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Education (CandidateID, DegreeId, CollegeUniversityName, PlaceAddress, GraduatedOrPursuing, KeySkills, AcademicProject, CreatedDate) VALUES (@CandidateID, @DegreeId, @CollegeUniversityName, @PlaceAddress, @GraduatedOrPursuing, @KeySkills, @AcademicProject, @CreatedDate)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CandidateID", candidateId);
                cmd.Parameters.AddWithValue("@DegreeId", degreeId);
                cmd.Parameters.AddWithValue("@CollegeUniversityName", collegeName);
                cmd.Parameters.AddWithValue("@PlaceAddress", address);
                cmd.Parameters.AddWithValue("@GraduatedOrPursuing", graduated);
                cmd.Parameters.AddWithValue("@KeySkills", keySkills);
                cmd.Parameters.AddWithValue("@AcademicProject", academicProject);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public List<SkillList> GetSkillsFromDB(int CandidateId)
        {
            List<SkillList> skillLists = new List<SkillList>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT  * FROM CandidateSkills WHERE CandidateId=@CandidateId ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {

                    cmd.Parameters.AddWithValue("@CandidateId", CandidateId);

                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        SkillList skill = new SkillList();
                        skill.SoftSkills = sqlDataReader["SoftSkills"] != DBNull.Value ? sqlDataReader["SoftSkills"].ToString() : null;
                        skill.CoreSkills = sqlDataReader["CoreSkills"] != DBNull.Value ? sqlDataReader["CoreSkills"].ToString() : null;

                        skillLists.Add(skill);
                    }
                }
            }
            return skillLists;
        }
        public void AddCoreSkillToDatabase(SkillList coreSkill, int CandidateId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO CandidateSkills (CoreSkill, CoreSkillPercentage, CandidateId,CreatedDate) VALUES (@CoreSkill, @CoreSkillPercentage, @CandidateId,@CreatedDate)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CoreSkill", coreSkill.Id);
                    cmd.Parameters.AddWithValue("@CoreSkillPercentage", coreSkill.Percentage);
                    cmd.Parameters.AddWithValue("@CandidateId", CandidateId);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void AddSoftSkillToDatabase(SkillList softSkill, int CandidateId)
        {

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO CandidateSkills (SoftSkill,SoftSkillPercentage, CandidateId,CreatedDate) VALUES (@CoreSkill, @CoreSkillPercentage, @CandidateId,@CreatedDate)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CoreSkill", softSkill.Id);
                    cmd.Parameters.AddWithValue("@CoreSkillPercentage", softSkill.Percentage);
                    cmd.Parameters.AddWithValue("@CandidateId", CandidateId);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddExperienceDetail(string companyName, string address, string designation, string keySkills, int candidateId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO WorkExperience (CandidateID, CompanyName, CompanyAddress, Designation, KeySkills, CreatedDate) VALUES (@CandidateID, @CompanyName, @CompanyAddress, @Designation, @KeySkills, @CreatedDate)", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CandidateID", candidateId);
                // cmd.Parameters.AddWithValue("@DegreeId", degreeId);
                cmd.Parameters.AddWithValue("@CompanyName", companyName);
                cmd.Parameters.AddWithValue("@CompanyAddress", address);
                cmd.Parameters.AddWithValue("@Designation", designation);
                cmd.Parameters.AddWithValue("@KeySkills", keySkills);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public bool DeleteEducationDetail(int candidateId, int educationId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("Delete From Education where CandidateID=@CandidateID and EducationId=@EducationID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CandidateID", candidateId);
                cmd.Parameters.AddWithValue("@EducationID", educationId);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                con.Close();
                if (rows > 0)
                    return true;
                else return false;
            }
        }
        public bool DeleteExperienceDetail(int candidateId, int experienceId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("Delete From Workexperience where CandidateID=@CandidateID and WorkExperienceId=@WorkExperienceId", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CandidateID", candidateId);
                cmd.Parameters.AddWithValue("@WorkExperienceId", experienceId);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                con.Close();
                if (rows > 0)
                    return true;
                else return false;
            }
        }
        public bool DeleteCoreSkill(int candidateId, int coreId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("Delete From CandidateSkills where CandidateId=@CandidateID and CandidateSkillId=@CoreSkillId", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CandidateID", candidateId);
                cmd.Parameters.AddWithValue("@CoreSkillId", coreId);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                con.Close();
                if (rows > 0)
                    return true;
                else return false;
            }
        }
        public bool DeleteSoftSkill(int candidateId, int softId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("Delete From CandidateSkills where CandidateId=@CandidateID and CandidateSkillId=@SoftSkillId", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CandidateID", candidateId);
                cmd.Parameters.AddWithValue("@SoftSkillId", softId);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                con.Close();
                if (rows > 0)
                    return true;
                else return false;
            }
        }
        public List<EducationList> GetEducations(int candidateId)
        {
            var educationDetails = new List<EducationList>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("Select * from Education where CandidateID=@CandidateID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CandidateID", candidateId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int DegreeId = (int)reader["DegreeId"];
                    string DegreeName = GetDegree(DegreeId);
                    educationDetails.Add(new EducationList
                    {
                        EducationId = (int)reader["EducationId"],
                        DegreeId = reader["DegreeId"].ToString(),
                        DegreeName = DegreeName,
                        CollegeUniversityName = reader["CollegeUniversityName"].ToString(),
                        PlaceAddress = reader["PlaceAddress"].ToString(),
                        GraduatedOrPursuing = reader["GraduatedOrPursuing"].ToString(),
                        KeySkills = reader["KeySkills"].ToString(),
                        AcademicProject = reader["AcademicProject"].ToString()
                    });
                }
            }

            return educationDetails;
        }
        public List<WorkExperienceList> GetWorkExperience(int candidateId)
        {
            var workDetails = new List<WorkExperienceList>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("Select * from WorkExperience where CandidateID=@CandidateID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CandidateID", candidateId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //int DegreeId = (int)reader["DegreeId"];
                    //string DegreeName = GetDegree(DegreeId);
                    workDetails.Add(new WorkExperienceList
                    {
                        WorkExperienceId = reader["WorkExperienceId"] != null ? Convert.ToInt32(reader["WorkExperienceId"]) : 0,
                        CompanyName = !reader.IsDBNull(reader.GetOrdinal("CompanyName")) ? reader.GetString(reader.GetOrdinal("CompanyName")) : null,
                        CompanyAddress = !reader.IsDBNull(reader.GetOrdinal("CompanyAddress")) ? reader.GetString(reader.GetOrdinal("CompanyAddress")) : null,
                        Designation = !reader.IsDBNull(reader.GetOrdinal("Designation")) ? reader.GetString(reader.GetOrdinal("Designation")) : null,
                        KeySkills = !reader.IsDBNull(reader.GetOrdinal("KeySkills")) ? reader.GetString(reader.GetOrdinal("KeySkills")) : null
                    });

                }
            }

            return workDetails;
        }
        public void SaveCandidateSkills(SqlConnection connection, int? candidateID, List<int> coreSkillIds, List<float> coreSkillPercentages, List<int> softSkillIds, List<float> softSkillPercentages)
        {
            //  connection.Open();

            for (int i = 0; i < coreSkillIds.Count && coreSkillIds[i] > 0; i++)
            {
                string query = @"
            INSERT INTO CandidateSkills (CandidateId, CoreSkill, CoreSkillPercentage)
            VALUES (@CandidateId, @CoreSkill,  @CoreSkillPercentage)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CandidateId", candidateID);
                    command.Parameters.AddWithValue("@CoreSkill", coreSkillIds[i] != 0 ? coreSkillIds[i] : 0);
                    // command.Parameters.AddWithValue("@SkillName", skillNames != null ? skillNames[i] : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CoreSkillPercentage", coreSkillPercentages[i]);
                    command.ExecuteNonQuery();
                }
            }
            for (int i = 0; i < softSkillIds.Count && softSkillIds[i] > 0; i++)
            {
                string query = @"
            INSERT INTO CandidateSkills (CandidateId, SoftSkill, SoftSkillPercentage)
            VALUES (@CandidateId, @SoftSkill,  @SoftSkillPercentage)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CandidateId", candidateID);
                    command.Parameters.AddWithValue("@SoftSkill", softSkillIds[i] != 0 ? softSkillIds[i] : 0);
                    // command.Parameters.AddWithValue("@SkillName", skillNames != null ? skillNames[i] : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SoftSkillPercentage", softSkillPercentages[i]);
                    command.ExecuteNonQuery();
                }
            }
        }
        private string GetSkillsByIds(SqlConnection connection, List<int> skillIds, string skillColumn)
        {
            string concatenatedSkills = "";
            string query = $"SELECT {skillColumn} FROM Skills WHERE SkillId IN ({string.Join(",", skillIds.Select((id, index) => $"@SkillId{index}"))})";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                for (int i = 0; i < skillIds.Count; i++)
                {
                    command.Parameters.AddWithValue($"@SkillId{i}", skillIds[i]);
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        concatenatedSkills += reader[skillColumn].ToString() + ", ";
                    }
                }
                concatenatedSkills = concatenatedSkills.TrimEnd(',', ' ');
            }

            return concatenatedSkills;
        }
        public string GetResumeStatus(int candidateId)
        {
            string resumeStatus = string.Empty;

            // Replace with your actual connection string

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT TOP(1) ResumeStatus FROM Resumes WHERE CandidateID = @CandidateID order by ResumeID DESC ";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CandidateID", candidateId);

                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    resumeStatus = result.ToString();
                }
                con.Close();
            }

            return resumeStatus;
        }

        public CandidateResponse CandidateProfileView(int CandidateId)
        {
            try
            {
                CandidateResponse candidateProfile = new CandidateResponse();
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_ManageJobSeeker", Sqlcon))
                    {
                        Sqlcon.Open();
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.Parameters.AddWithValue("@NewCandidateID", CandidateId);
                        sqlcmd.Parameters.AddWithValue("@Action", "SELECT");

                        using (SqlDataReader reader = sqlcmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {

                                    int planid = reader["PlanId"] != DBNull.Value ? (int)reader["PlanId"] : 0;
                                    string plantype = GetPlanType(planid);
                                    int jobtypeid = reader["JobTypeID"] != DBNull.Value ? (int)reader["JobTypeID"] : 0;
                                    string Jobtype = GetJobType(jobtypeid);
                                    int countryid = reader["CountryID"] != DBNull.Value ? (int)reader["CountryID"] : 0;
                                    string Country = GetCountry(countryid);
                                    int locationid = reader["JobLocationID"] != DBNull.Value ? (int)reader["JobLocationID"] : 0;
                                    string JobLoaction = GetJoblocation(locationid);
                                    int availabilityid = reader["AvailabilityID"] != DBNull.Value ? (int)reader["AvailabilityID"] : 0;
                                    string availability = GetAvailability(availabilityid);
                                    var education = GetEducationDetails(CandidateId);
                                    var workexp = GetWorkExperience(CandidateId);
                                    var skill = GetSkill(CandidateId);
                                    string resumepath = reader["ResumeFilePath"] != DBNull.Value ? reader["ResumeFilePath"].ToString() : null;



                                    //  candidateProfile.ResumeFileUrl = resumeFileUrl;
                                    //  IFormFile Resume=DownloadResumeFile(resumepath);
                                    candidateProfile = new CandidateResponse()
                                    {
                                        CandidateId = CandidateId,
                                        PlanType = plantype,
                                        FirstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : null,
                                        LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : null,
                                        EmailAddress = reader["EmailAddress"] != DBNull.Value ? reader["EmailAddress"].ToString() : null,
                                        MobileNumber = reader["MobileNumber"] != DBNull.Value ? reader["MobileNumber"].ToString() : null,
                                        Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : null,
                                        City = reader["City"] != DBNull.Value ? reader["City"].ToString() : null,
                                        StateOrProvince = reader["State/Province"] != DBNull.Value ? reader["State/Province"].ToString() : null,
                                        PostalOrZipCode = reader["Postal/ZipCode"] != DBNull.Value ? reader["Postal/ZipCode"].ToString() : null,
                                        HighestEducationLevel = reader["HighestEducationLevel"] != DBNull.Value ? reader["HighestEducationLevel"].ToString() : null,
                                        WillingToTakeFreeTraining = reader["FreeTraining"] != DBNull.Value ? (bool)reader["FreeTraining"] == true ? true : false : false,
                                        WillingToTakePaidTraining = reader["PaidTraining"] != DBNull.Value ? (bool)reader["PaidTraining"] == true ? true : false : false,
                                        WillingToBeContactedByCareerConsultant = reader["CareerConsultantContact"] != DBNull.Value ? (bool)reader["CareerConsultantContact"] == true ? true : false : false,
                                        CoverLetter = reader["CoverLetter"] != DBNull.Value ? reader["CoverLetter"].ToString() : null,
                                        LinkedInProfile = reader["LinkedInProfile"] != DBNull.Value ? reader["LinkedInProfile"].ToString() : null,
                                        Portfolio = reader["Portfolio"] != DBNull.Value ? reader["Portfolio"].ToString() : null,
                                        Skill = skill,
                                        JobType = Jobtype,
                                        Country = Country,
                                        JobLocation = JobLoaction,
                                        Availability = availability,
                                        Educations = education,
                                        Experiences = workexp,
                                        CreatedDate = reader["createdDate"] != DBNull.Value ? (DateTime?)reader["createdDate"] : null,
                                        Updateddate = reader["updatedDate"] != DBNull.Value ? (DateTime?)reader["updatedDate"] : null,
                                        //ResumeFilePath = resumepath,
                                    };

                                    candidateProfile.ResumeFilePath = resumepath;

                                    // candidateProfile.ResumeFilePath = _linkGenerator.GetPathByAction(_httpContextAccessor.HttpContext, "DownloadResumeFile", "Candidate", new { filepath = resumepath });




                                }
                                return candidateProfile;
                            }

                            else
                                return new CandidateResponse()
                                {
                                    Message = "Candidate with Id not available"
                                };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetResumePath(int CandidateId)
        {
            string resumePath = "";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT ResumeFilePath FROM Candidate WHERE CandidateID = @CandidateID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CandidateID", CandidateId);

                    con.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        resumePath = result.ToString();
                    }
                }
            }

            return resumePath;
        }
        public bool UpdateResumeStatus(int resumeId, string status)
        {
            int resumeid = 0;
            bool response = false;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "Update Resumes  set ResumeStatus=@Status, ModifiedDate=@ModifiedDate WHERE ResumeId = @ResumeId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ResumeId", resumeId);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@ModifiedDate",DateTime.Now);
                    con.Open();
                    object result = cmd.ExecuteNonQuery();

                    if (result != null)
                    {
                        resumeid = (int)result;
                        if (resumeid > 0)
                            response = true;
                        else
                            response = false;
                    }
                }
            }

            return response;
        }
        public void DeleteResume(int resumeId)
        {
            int resumeid = 0;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "Delete from Resumes  WHERE ResumeId = @ResumeId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ResumeId", resumeId);
                    // cmd.Parameters.AddWithValue("@Status", status);

                    con.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        resumeid = (int)result;
                    }
                }
            }

            //return resumeid;
        }
        public int GetResumeIdFromDatabase(int CandidateId)
        {
            int resumeid = 0;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT Top(1) ResumeId FROM Resumes WHERE CandidateID = @CandidateID order by ResumeId desc";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CandidateID", CandidateId);

                    con.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        resumeid = (int)result;
                    }
                }
            }

            return resumeid;
        }
        public bool PostJobsDB(JobPostingRequest request, int EmployeeId)
        {

            try
            {
                bool isSuccess;
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_InsertJobPost", Sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;

                        sqlcmd.Parameters.AddWithValue("@EmployeeID", EmployeeId != 0 ? EmployeeId : 0);
                        sqlcmd.Parameters.AddWithValue("@JobTitle", request.JobTitle != null ? request.JobTitle : null);
                        sqlcmd.Parameters.AddWithValue("@Vacancy", request.Vacancy != 0 ? request.Vacancy : 0);
                        sqlcmd.Parameters.AddWithValue("@JobDescription", request.JobDescription != null ? request.JobDescription : null);
                        sqlcmd.Parameters.AddWithValue("@Qualifications", request.Qualifications != null ? request.Qualifications : null);
                        sqlcmd.Parameters.AddWithValue("@Experience", request.Experience != null ? request.Experience : null);
                        sqlcmd.Parameters.AddWithValue("@RequiredSkills", request.RequiredSkills != null ? request.RequiredSkills : null);
                        sqlcmd.Parameters.AddWithValue("@JobLocation", request.JobLocationId != 0 ? request.JobLocationId : 0);
                        sqlcmd.Parameters.AddWithValue("@Salary", request.Salary.HasValue ? (object)request.Salary.Value : DBNull.Value);
                        // sqlcmd.Parameters.AddWithValue("@CompanyName", request.CompanyName != null ? request.CompanyName : null);
                        sqlcmd.Parameters.AddWithValue("@JobType", request.JobTypeId != 0 ? request.JobTypeId : 0);
                        // sqlcmd.Parameters.AddWithValue("@Address", request.Address != null ? request.Address : null);
                        sqlcmd.Parameters.AddWithValue("@ApplicationDeadline", request.ApplicationDeadline.HasValue ? (object)request.ApplicationDeadline.Value : DBNull.Value);
                        //sqlcmd.Parameters.AddWithValue("@ContactEmail", request.ContactEmail != null ? request.ContactEmail : null);
                        //sqlcmd.Parameters.AddWithValue("@Website", request.Website != null ? request.Website : null);
                        sqlcmd.Parameters.AddWithValue("@JobCreatedDate", DateTime.Now);
                        sqlcmd.Parameters.AddWithValue("@ApplicationStartDate", request.ApplicationStartDate.HasValue ? (object)request.ApplicationStartDate.Value : DBNull.Value);
                        // sqlcmd.Parameters.AddWithValue("@Industry", request.Industry != null ? request.Industry : null);

                        Sqlcon.Open();
                        SqlParameter isSuccessParam = new SqlParameter("@IsSuccess", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        sqlcmd.Parameters.Add(isSuccessParam);


                        sqlcmd.ExecuteNonQuery();

                        isSuccess = (bool)isSuccessParam.Value;
                        if (isSuccess)
                        {
                            return true;
                        }
                        else return false;

                    }
                }
                return isSuccess;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CandidateRequest CandidateView(int CandidateId)
        {
            try
            {
                CandidateRequest candidateProfile = new CandidateRequest();

                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_ManageJobSeeker", Sqlcon))
                    {
                        Sqlcon.Open();
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.Parameters.AddWithValue("@NewCandidateID", CandidateId);
                        sqlcmd.Parameters.AddWithValue("@Action", "SELECT");

                        using (SqlDataReader reader = sqlcmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    int planid = reader["PlanId"] != DBNull.Value ? (int)reader["PlanId"] : 0;
                                    string plantype = GetPlanType(planid);
                                    int jobtypeid = reader["JobTypeID"] != DBNull.Value ? (int)reader["JobTypeID"] : 0;
                                    string Jobtype = GetJobType(jobtypeid);
                                    int countryid = reader["CountryID"] != DBNull.Value ? (int)reader["CountryID"] : 0;
                                    string Country = GetCountry(countryid);
                                    int locationid = reader["JobLocationID"] != DBNull.Value ? (int)reader["JobLocationID"] : 0;
                                    string JobLoaction = GetJoblocation(locationid);
                                    int availabilityid = reader["AvailabilityID"] != DBNull.Value ? (int)reader["AvailabilityID"] : 0;
                                    string availability = GetAvailability(availabilityid);

                                    var workexp = GetWork(CandidateId);
                                    var educationdetail = GetEducation(CandidateId);
                                    string resumepath = reader["ResumeFilePath"] != DBNull.Value ? reader["ResumeFilePath"].ToString() : null;



                                    //  candidateProfile.ResumeFileUrl = resumeFileUrl;
                                    //  IFormFile Resume=DownloadResumeFile(resumepath);
                                    candidateProfile = new CandidateRequest()
                                    {
                                        PlanId = reader["PlanId"] != DBNull.Value ? (int)reader["PlanId"] : 0,
                                        FirstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : null,
                                        LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : null,
                                        EmailAddress = reader["EmailAddress"] != DBNull.Value ? reader["EmailAddress"].ToString() : null,
                                        MobileNumber = reader["MobileNumber"] != DBNull.Value ? reader["MobileNumber"].ToString() : null,
                                        Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : null,
                                        City = reader["City"] != DBNull.Value ? reader["City"].ToString() : null,
                                        StateOrProvince = reader["State/Province"] != DBNull.Value ? reader["State/Province"].ToString() : null,
                                        PostalOrZipCode = reader["Postal/ZipCode"] != DBNull.Value ? reader["Postal/ZipCode"].ToString() : null,
                                        HighestEducationLevel = reader["HighestEducationLevel"] != DBNull.Value ? reader["HighestEducationLevel"].ToString() : null,
                                        FreeTraining = reader["FreeTraining"] != DBNull.Value ? reader["FreeTraining"] == "YES" ? true : false : false,
                                        PaidTraining = reader["PaidTraining"] != DBNull.Value ? reader["PaidTraining"] == "YES" ? true : false : false,
                                        CareerConsultantContact = reader["CareerConsultantContact"] != DBNull.Value ? reader["CareerConsultantContact"] == "YES" ? true : false : false,
                                        CoverLetter = reader["CoverLetter"] != DBNull.Value ? reader["CoverLetter"].ToString() : null,
                                        LinkedInProfile = reader["LinkedInProfile"] != DBNull.Value ? reader["LinkedInProfile"].ToString() : null,
                                        Portfolio = reader["Portfolio"] != DBNull.Value ? reader["Portfolio"].ToString() : null,
                                        // CoreSkills = reader["CoreSkills"] != DBNull.Value ? reader["CoreSkills"].ToString() : null,
                                        //SoftSkills = reader["SoftSkills"] != DBNull.Value ? reader["SoftSkills"].ToString() : null,
                                        JobTypes = Jobtype,
                                        Country = Country,
                                        JobLocation = JobLoaction,
                                        Availability = availability,
                                        Educations = educationdetail,
                                        WorkExperience = workexp,
                                        CreatedDate = reader["createdDate"] != DBNull.Value ? (DateTime?)reader["createdDate"] : null,
                                        Updateddate = reader["updatedDate"] != DBNull.Value ? (DateTime?)reader["updatedDate"] : null,
                                        Resumepath = resumepath,
                                    };

                                    //  candidateProfile.ResumeFilePath = resumepath;

                                    // candidateProfile.ResumeFilePath = _linkGenerator.GetPathByAction(_httpContextAccessor.HttpContext, "DownloadResumeFile", "Candidate", new { filepath = resumepath });




                                }
                                return candidateProfile;
                            }

                            //else
                            //    return new CandidateRequest()
                            //    {
                            //        Message = "Candidate with Id not available"
                            //    };
                        }
                    }
                    return candidateProfile;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<Skill> GetSkill(int candidateId)
        {
            List<Skill> skilllist = new List<Skill>();
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();

                string query = "Select * from CandidateSkills where CandidateID=@CandidateID";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@CandidateID", candidateId);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int coreskillid = reader["CoreSkill"] != DBNull.Value ? (int)reader["CoreSkill"] : 0;
                            string coreskill = GetSkillById(coreskillid, "CoreSkills");
                            int softskillid = reader["SoftSkill"] != DBNull.Value ? (int)reader["SoftSkill"] : 0;
                            string SoftSkills = GetSkillById(softskillid, "SoftSkills");
                            Skill skills = new Skill
                            {

                                CoreSkills = coreskill,
                                SoftSkills = SoftSkills,

                                CoreSkillPercentage = reader["CoreSkillPercentage"] != DBNull.Value ? reader["CoreSkillPercentage"].ToString() : null,
                                SoftSkillpercentage = reader["SoftSkillpercentage"] != DBNull.Value ? reader["SoftSkillpercentage"].ToString() : null,
                            };
                            skilllist.Add(skills);
                        }
                    }
                    else
                        return skilllist;
                }
                return skilllist;
            }
        }
        private List<WorkExperience> GetWorkExperiences(int candidateId)
        {
            List<WorkExperience> works = new List<WorkExperience>();
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                string AvailabilityName = "";
                string query = "Select * from WorkExperience where CandidateID=@CandidateID";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@CandidateID", candidateId);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            WorkExperience experiences = new WorkExperience
                            {
                                CompanyName = !reader.IsDBNull(reader.GetOrdinal("CompanyName")) ? reader.GetString(reader.GetOrdinal("CompanyName")) : null,
                                CompanyAddress = !reader.IsDBNull(reader.GetOrdinal("CompanyAddress")) ? reader.GetString(reader.GetOrdinal("CompanyAddress")) : null,
                                Designation = !reader.IsDBNull(reader.GetOrdinal("Designation")) ? reader.GetString(reader.GetOrdinal("Designation")) : null,
                                KeySkillsPracticed = !reader.IsDBNull(reader.GetOrdinal("KeySkills")) ? reader.GetString(reader.GetOrdinal("KeySkills")) : null
                            };
                            works.Add(experiences);
                        }
                    }
                    else
                        return works;
                }
                return works;
            }
        }

        private List<EducationDetails> GetEducation(int candidateId)
        {
            List<EducationDetails> educations = new List<EducationDetails>();
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                string AvailabilityName = "";
                string query = "Select * from Education where CandidateID=@CandidateID";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@CandidateID", candidateId);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int DegreeId = reader.GetInt32(reader.GetOrdinal("DegreeId"));
                            var Degree = GetDegree(DegreeId);
                            EducationDetails education = new EducationDetails
                            {
                                //EducationId = reader.GetInt32(reader.GetOrdinal("EducationID")),
                                DegreeName = Degree,
                                CollegeUniversity = !reader.IsDBNull(reader.GetOrdinal("CollegeUniversityName")) ? reader.GetString(reader.GetOrdinal("CollegeUniversityName")) : null,
                                Address = !reader.IsDBNull(reader.GetOrdinal("PlaceAddress")) ? reader.GetString(reader.GetOrdinal("PlaceAddress")) : null,
                                GraduatedOrPursuingValue = !reader.IsDBNull(reader.GetOrdinal("GraduatedOrPursuing")) ? reader.GetString(reader.GetOrdinal("GraduatedOrPursuing")) : null,
                                KeySkillsValue = !reader.IsDBNull(reader.GetOrdinal("KeySkills")) ? reader.GetString(reader.GetOrdinal("KeySkills")) : null,
                                AcademicProjectValue = !reader.IsDBNull(reader.GetOrdinal("AcademicProject")) ? reader.GetString(reader.GetOrdinal("AcademicProject")) : null
                            };
                            educations.Add(education);
                        }
                    }
                    else
                        return educations;
                }
                return educations;
            }
        }
        private List<WorkExperiences> GetWork(int candidateId)
        {
            List<WorkExperiences> works = new List<WorkExperiences>();
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                string AvailabilityName = "";
                string query = "Select * from WorkExperience where CandidateID=@CandidateID";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@CandidateID", candidateId);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            WorkExperiences experiences = new WorkExperiences
                            {
                                CompanyNameValue = !reader.IsDBNull(reader.GetOrdinal("CompanyName")) ? reader.GetString(reader.GetOrdinal("CompanyName")) : null,
                                CompanyAddressValue = !reader.IsDBNull(reader.GetOrdinal("CompanyAddress")) ? reader.GetString(reader.GetOrdinal("CompanyAddress")) : null,
                                DesignationValue = !reader.IsDBNull(reader.GetOrdinal("Designation")) ? reader.GetString(reader.GetOrdinal("Designation")) : null,
                                KeySkillsPracticedValue = !reader.IsDBNull(reader.GetOrdinal("KeySkills")) ? reader.GetString(reader.GetOrdinal("KeySkills")) : null
                            };
                            works.Add(experiences);
                        }
                    }
                    else
                        return works;
                }
                return works;
            }
        }

        private List<EducationDetail> GetEducationDetails(int candidateId)
        {
            List<EducationDetail> educations = new List<EducationDetail>();
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                string AvailabilityName = "";
                string query = "Select * from Education where CandidateID=@CandidateID";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@CandidateID", candidateId);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int DegreeId = reader.GetInt32(reader.GetOrdinal("DegreeId"));
                            var Degree = GetDegree(DegreeId);
                            EducationDetail education = new EducationDetail
                            {
                                //EducationId = reader.GetInt32(reader.GetOrdinal("EducationID")),
                                Degree = Degree,
                                CollegeUniversityName = !reader.IsDBNull(reader.GetOrdinal("CollegeUniversityName")) ? reader.GetString(reader.GetOrdinal("CollegeUniversityName")) : null,
                                PlaceAddress = !reader.IsDBNull(reader.GetOrdinal("PlaceAddress")) ? reader.GetString(reader.GetOrdinal("PlaceAddress")) : null,
                                GraduatedOrPursuing = !reader.IsDBNull(reader.GetOrdinal("GraduatedOrPursuing")) ? reader.GetString(reader.GetOrdinal("GraduatedOrPursuing")) : null,
                                KeySkills = !reader.IsDBNull(reader.GetOrdinal("KeySkills")) ? reader.GetString(reader.GetOrdinal("KeySkills")) : null,
                                AcademicProject = !reader.IsDBNull(reader.GetOrdinal("AcademicProject")) ? reader.GetString(reader.GetOrdinal("AcademicProject")) : null
                            };
                            educations.Add(education);
                        }
                    }
                    else
                        return educations;
                }
                return educations;
            }
        }
        private string GetPlanType(int planid)
        {
            if (planid <= 0) return null;
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                string PlanName = "";
                string query = "Select * from [Plan] where PlanId=@PlanId";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@PlanId", planid);
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            PlanName = reader["PlanName"] != null ? reader["PlanName"].ToString() : null;
                            //return JobType;
                        }
                    }
                    else return " No PlanName found";
                }
                return PlanName;
            }
        }
        private string GetDegree(int degreeId)
        {
            if (degreeId <= 0) return null;
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                string DegreeName = "";
                string query = "Select DegreeName from Degree where DegreeId=@DegreeId";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@DegreeId", degreeId);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DegreeName = reader["DegreeName"] != null ? reader["DegreeName"].ToString() : null;
                            //return JobType;
                        }
                    }
                    else return " No DegreeName found";
                }
                return DegreeName;
            }
        }
        private string GetAvailability(int availabilityid)
        {
            if (availabilityid <= 0) return null;
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                string AvailabilityName = "";
                string query = "Select AvailabilityName from Availability where AvailabilityID=@AvailabilityID";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@AvailabilityID", availabilityid);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            AvailabilityName = reader["AvailabilityName"].ToString();
                            //return JobType;
                        }
                    }
                    else return " No CountryId found";
                }
                return AvailabilityName;
            }
        }

        private string GetJoblocation(int locationid)
        {
            if (locationid <= 0) return null;
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                string Location = "";
                string query = "Select Location from JobLocation where JobLocationID=@JobLocationID";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@JobLocationID", locationid);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Location = reader["Location"].ToString();
                            //return JobType;
                        }
                    }
                    else return " No CountryId found";
                }
                return Location;
            }
        }
        public int GetCountryId(int candidateId)
        {
            if (candidateId <= 0) return 0;
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                int CountryName = 0;
                string query = "Select CountryId from Candidate where CandidateID=@CandidateID";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@CandidateID", candidateId);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CountryName = reader["CountryID"] != DBNull.Value ? Convert.ToInt32(reader["CountryID"]) : 0;
                            //return JobType;
                        }
                    }
                    else return 0;
                }
                return CountryName;
            }

        }
        public int GetPlanId(int trainerId)
        {
            if (trainerId <= 0) return 0;
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                int PlanName = 0;
                string query = "Select PlanId from Trainer where TrainerID=@TrainerID";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@TrainerID", trainerId);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            PlanName = reader["PlanId"] != DBNull.Value ? Convert.ToInt32(reader["PlanId"]) : 0;
                            //return JobType;
                        }
                    }
                    else return 0;
                }
                return PlanName;
            }

        }
        public int GetJobTypes(int employeeID)
        {
            if (employeeID <= 0) return 0;
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                int JobTypeName = 0;
                string query = "Select NewJobType from JobEditRequests where RequestID=@JobID";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@JobID", employeeID);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            JobTypeName = reader["NewJobType"] != DBNull.Value ? Convert.ToInt32(reader["NewJobType"]) : 0;
                            //return JobType;
                        }
                    }
                    else return 0;
                }
                return JobTypeName;
            }

        }
        public int GetJobLocation(int employeeID)
        {
            if (employeeID <= 0) return 0;
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                int JobTypeName = 0;
                string query = "Select NewJobLocation from JobEditRequests where RequestID=@JobID";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@JobID", employeeID);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            JobTypeName = reader["NewJobLocation"] != DBNull.Value ? Convert.ToInt32(reader["NewJobLocation"]) : 0;
                            //return JobType;
                        }
                    }
                    else return 0;
                }
                return JobTypeName;
            }

        }
        public int GetPlanIdforEmployee(int employeeId)
        {
            if (employeeId <= 0) return 0;
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                int PlanName = 0;
                string query = "Select PlanId from Employer where CompanyID=@CompanyID";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@CompanyID", employeeId);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            PlanName = reader["PlanId"] != DBNull.Value ? Convert.ToInt32(reader["PlanId"]) : 0;
                            //return JobType;
                        }
                    }
                    else return 0;
                }
                return PlanName;
            }

        }
        public int GetJobTypeId(int candidateId)
        {
            if (candidateId <= 0) return 0;
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                int JobTypeName = 0;
                string query = "Select JobTypeID from Candidate where CandidateID=@CandidateID";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@CandidateID", candidateId);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            JobTypeName = reader["JobTypeID"] != DBNull.Value ? Convert.ToInt32(reader["JobTypeID"]) : 0;
                            //return JobType;
                        }
                    }
                    else return 0;
                }
                return JobTypeName;
            }

        }

        public int GetJobLocationId(int candidateId)
        {
            if (candidateId <= 0) return 0;
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                int JobLocationName = 0;
                string query = "Select JobLocationID from Candidate where CandidateID=@CandidateID";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@CandidateID", candidateId);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            JobLocationName = reader["JobLocationID"] != DBNull.Value ? Convert.ToInt32(reader["JobLocationID"]) : 0;
                            //return JobType;
                        }
                    }
                    else return 0;
                }
                return JobLocationName;
            }

        }

        public int GetJobTypeIdforEmployee(int employeeID)
        {
            if (employeeID <= 0) return 0;
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                int JobTypeName = 0;
                string query = "Select JobType from JobPostings where JobID=@JobID";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@JobID", employeeID);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            JobTypeName = reader["JobType"] != DBNull.Value ? Convert.ToInt32(reader["JobType"]) : 0;
                            //return JobType;
                        }
                    }
                    else return 0;
                }
                return JobTypeName;
            }

        }

        public int GetJobLocationIdforEmployee(int employeeID)
        {
            if (employeeID <= 0) return 0;
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                int JobLocationName = 0;
                string query = "Select JobLocation from JobPostings where JobID=@JobID";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@JobID", employeeID);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            JobLocationName = reader["JobLocation"] != DBNull.Value ? Convert.ToInt32(reader["JobLocation"]) : 0;
                            //return JobType;
                        }
                    }
                    else return 0;
                }
                return JobLocationName;
            }

        }
        public int GetAvailabilityId(int candidateId)
        {
            if (candidateId <= 0) return 0;
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                int CountryName = 0;
                string query = "Select AvailabilityID from Candidate where CandidateID=@CandidateID";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@CandidateID", candidateId);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CountryName = reader["AvailabilityID"] != DBNull.Value ? Convert.ToInt32(reader["AvailabilityID"]) : 0;
                            //return JobType;
                        }
                    }
                    else return 0;
                }
                return CountryName;
            }

        }



        private string GetCountry(int countryid)
        {
            if (countryid <= 0) return null;
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                string CountryName = "";
                string query = "Select CountryName from Country where CountryID=@CountryID";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@CountryID", countryid);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CountryName = reader["CountryName"].ToString();
                            //return JobType;
                        }
                    }
                    else return " No CountryId found";
                }
                return CountryName;
            }

        }

        private string GetJobType(int jobtypeid)
        {
            if (jobtypeid <= 0) return null;
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();
                string JobType = "";
                string query = "Select JobTypeName from JobTypes where JobTypeID=@JobTypeId";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@JobTypeId", jobtypeid);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            JobType = reader["JobTypeName"].ToString();
                            //return JobType;
                        }
                    }
                    else return " No JobId found";
                }
                return JobType;
            }

        }
        public TrainerResponse TrainerView(int TrainerID)
        {
            try
            {
                TrainerResponse employerResponse = new TrainerResponse();
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Select * from Trainer where TrainerID=@TrainerID ", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCon.Open();
                        sqlCmd.Parameters.AddWithValue("@TrainerID", TrainerID);
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int planid = reader["PlanId"] != DBNull.Value ? Convert.ToInt32(reader["PlanId"]) : 0;
                            string PlanType = GetPlanType(planid);
                            employerResponse = new TrainerResponse
                            {
                                TrainerId = Convert.ToInt32(reader["TrainerID"]),
                                EmailStatus = reader["EmailStatus"].ToString(),
                                CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                                CompanyRegistrationNumber = reader["CompanyRegistrationNumber"] != DBNull.Value ? reader["CompanyRegistrationNumber"].ToString() : null,
                                CompanyEmail = reader["CompanyEmailAddress"] != DBNull.Value ? reader["CompanyEmailAddress"].ToString() : null,
                                CompanyPhoneNumber = reader["CompanyPhoneNumber"] != DBNull.Value ? reader["CompanyPhoneNumber"].ToString() : null,
                                CompanyWebsiteUrl = reader["WebsiteUrl"] != DBNull.Value ? reader["WebsiteUrl"].ToString() : null,
                                PhysicalAddress = reader["PhysicalAddress"] != DBNull.Value ? reader["PhysicalAddress"].ToString() : null,
                                CompanyDescription = reader["CompanyDescription"] != DBNull.Value ? reader["CompanyDescription"].ToString() : null,
                                IndustryType = reader["IndustryType"] != DBNull.Value ? reader["IndustryType"].ToString() : null,
                                CompanySize = reader["CompanySize"] != DBNull.Value ? reader["CompanySize"].ToString() : null,
                                ContactPersonName = reader["ContactPersonName"] != DBNull.Value ? reader["ContactPersonName"].ToString() : null,
                                ContactPersonEmail = reader["ContactPersonEmail"] != DBNull.Value ? reader["ContactPersonEmail"].ToString() : null,
                                ContactPersonPhoneNumber = reader["ContactPersonPhoneNumber"] != DBNull.Value ? reader["ContactPersonPhoneNumber"].ToString() : null,
                                CompanyLogoUrl = reader["CompanyLogo"] != DBNull.Value ? reader["CompanyLogo"].ToString() : null,
                                //Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : null,

                                AgreementToTerms = reader["AgreementToTerms"] != DBNull.Value ? reader["AgreementToTerms"].ToString() == "1" ? "YES" : "NO" : null,
                                AreaOfSpecialization = reader["AreaofSpecialization"] != DBNull.Value ? reader["AreaofSpecialization"].ToString() : null,
                                PlanType = PlanType

                            };
                        }
                        return employerResponse;
                        sqlCon.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., logging)
                throw;
            }
        }
        public List<CandidateDetails> GetCandidates()
        {
            List<CandidateDetails> candidateList = new List<CandidateDetails>();
            CandidateDetails candidateProfile = new CandidateDetails();
            using (SqlConnection SqlCon = new SqlConnection(_connectionString))
            {

                using (SqlCommand sqlcmd = new SqlCommand("SELECT  ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS SLNO ,* FROM [BB_JobPortal].[dbo].[Candidate]", SqlCon))
                {
                    SqlCon.Open();
                    sqlcmd.CommandType = CommandType.Text;

                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            candidateProfile = new CandidateDetails()
                            {
                                SLNO = reader["SLNO"] != DBNull.Value ? Convert.ToInt32(reader["SLNO"]) : 0,
                                CandidateId = reader["CandidateID"] != DBNull.Value ? Convert.ToInt32(reader["CandidateID"]) : 0,
                                ///PlanId = reader["PlanId"] != DBNull.Value ? (int)reader["PlanId"] : 0,
                                FirstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : null,
                                LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : null,
                                EmailAddress = reader["EmailAddress"] != DBNull.Value ? reader["EmailAddress"].ToString() : null,
                                MobileNumber = reader["MobileNumber"] != DBNull.Value ? reader["MobileNumber"].ToString() : null,
                                Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : null,
                                City = reader["City"] != DBNull.Value ? reader["City"].ToString() : null,
                                StateOrProvince = reader["State/Province"] != DBNull.Value ? reader["State/Province"].ToString() : null,
                                PostalOrZipCode = reader["Postal/ZipCode"] != DBNull.Value ? reader["Postal/ZipCode"].ToString() : null,
                                HighestEducationLevel = reader["HighestEducationLevel"] != DBNull.Value ? reader["HighestEducationLevel"].ToString() : null,
                                //FreeTraining = reader["FreeTraining"] != DBNull.Value ? reader["FreeTraining"] == "YES" ? true : false : false,
                                //PaidTraining = reader["PaidTraining"] != DBNull.Value ? reader["PaidTraining"] == "YES" ? true : false : false,
                                //CareerConsultantContact = reader["CareerConsultantContact"] != DBNull.Value ? reader["CareerConsultantContact"] == "YES" ? true : false : false,
                                //CoverLetter = reader["CoverLetter"] != DBNull.Value ? reader["CoverLetter"].ToString() : null,
                                //LinkedInProfile = reader["LinkedInProfile"] != DBNull.Value ? reader["LinkedInProfile"].ToString() : null,
                                //Portfolio = reader["Portfolio"] != DBNull.Value ? reader["Portfolio"].ToString() : null,
                            };
                            candidateList.Add(candidateProfile);
                        }
                    }
                }
            }
            return candidateList;
        }
        public List<EmployerResponse> GetTrainers()
        {
            try
            {
                List<EmployerResponse> emplList = new List<EmployerResponse>();
                EmployerResponse employerResponse = new EmployerResponse();
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Select ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS SLNO ,* from Trainer", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCon.Open();
                        //sqlCmd.Parameters.AddWithValue("@CompanyID", EmployeeID);
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int planid = reader["PlanId"] != DBNull.Value ? Convert.ToInt32(reader["PlanId"]) : 0;
                            string PlanType = GetPlanType(planid);
                            employerResponse = new EmployerResponse
                            {
                                SLNO = Convert.ToInt32(reader["SLNO"]),
                                TrainerID = Convert.ToInt32(reader["TrainerID"]),
                                //EmailStatus = reader["EmailStatus"].ToString(),
                                CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                                CompanyRegistrationNumber = reader["CompanyRegistrationNumber"] != DBNull.Value ? reader["CompanyRegistrationNumber"].ToString() : null,
                                CompanyEmail = reader["CompanyEmailAddress"] != DBNull.Value ? reader["CompanyEmailAddress"].ToString() : null,
                                CompanyPhoneNumber = reader["CompanyPhoneNumber"] != DBNull.Value ? reader["CompanyPhoneNumber"].ToString() : null,
                                CompanyWebsiteUrl = reader["WebsiteUrl"] != DBNull.Value ? reader["WebsiteUrl"].ToString() : null,
                                PhysicalAddress = reader["PhysicalAddress"] != DBNull.Value ? reader["PhysicalAddress"].ToString() : null,
                                //CompanyDescription = reader["CompanyDescription"] != DBNull.Value ? reader["CompanyDescription"].ToString() : null,
                                //IndustryType = reader["IndustryType"] != DBNull.Value ? reader["IndustryType"].ToString() : null,
                                //CompanySize = reader["CompanySize"] != DBNull.Value ? reader["CompanySize"].ToString() : null,
                                //ContactPersonName = reader["ContactPersonName"] != DBNull.Value ? reader["ContactPersonName"].ToString() : null,
                                //ContactPersonEmail = reader["ContactPersonEmail"] != DBNull.Value ? reader["ContactPersonEmail"].ToString() : null,
                                //ContactPersonPhoneNumber = reader["ContactPersonPhoneNumber"] != DBNull.Value ? reader["ContactPersonPhoneNumber"].ToString() : null,
                                //CompanyLogoUrl = reader["CompanyLogo"] != DBNull.Value ? reader["CompanyLogo"].ToString() : null,
                                ////Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : null,

                                //AgreementToTerms = reader["AgreementToTerms"] != DBNull.Value ? reader["AgreementToTerms"].ToString() == "1" ? "YES" : "NO" : null,
                                //TrainingAndPlacementProgram = reader["TrainingAndPlacementProgram"] != DBNull.Value ? reader["TrainingAndPlacementProgram"] == "true" ? "YES" : "NO" : "NO",
                                //PlanId = PlanType

                            };
                            emplList.Add(employerResponse);
                        }
                        sqlCon.Close();
                        return emplList;

                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., logging)
                throw;
            }
        }
        public List<EmployerResponse> GetEmployees()
        {
            try
            {
                List<EmployerResponse> emplList = new List<EmployerResponse>();
                EmployerResponse employerResponse = new EmployerResponse();
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Select ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS SLNO ,* from Employer", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCon.Open();
                        //sqlCmd.Parameters.AddWithValue("@CompanyID", EmployeeID);
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int planid = reader["PlanId"] != DBNull.Value ? Convert.ToInt32(reader["PlanId"]) : 0;
                            string PlanType = GetPlanType(planid);
                            employerResponse = new EmployerResponse
                            {
                                SLNO = Convert.ToInt32(reader["SLNO"]),
                                EmployeeId = Convert.ToInt32(reader["CompanyID"]),
                                //EmailStatus = reader["EmailStatus"].ToString(),
                                CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                                CompanyRegistrationNumber = reader["CompanyRegistrationNumber"] != DBNull.Value ? reader["CompanyRegistrationNumber"].ToString() : null,
                                CompanyEmail = reader["CompanyEmailAddress"] != DBNull.Value ? reader["CompanyEmailAddress"].ToString() : null,
                                CompanyPhoneNumber = reader["CompanyPhoneNumber"] != DBNull.Value ? reader["CompanyPhoneNumber"].ToString() : null,
                                CompanyWebsiteUrl = reader["WebsiteUrl"] != DBNull.Value ? reader["WebsiteUrl"].ToString() : null,
                                PhysicalAddress = reader["PhysicalAddress"] != DBNull.Value ? reader["PhysicalAddress"].ToString() : null,
                                CompanyDescription = reader["CompanyDescription"] != DBNull.Value ? reader["CompanyDescription"].ToString() : null,
                                //IndustryType = reader["IndustryType"] != DBNull.Value ? reader["IndustryType"].ToString() : null,
                                CompanySize = reader["CompanySize"] != DBNull.Value ? reader["CompanySize"].ToString() : null,
                                ContactPersonName = reader["ContactPersonName"] != DBNull.Value ? reader["ContactPersonName"].ToString() : null,
                                ContactPersonEmail = reader["ContactPersonEmail"] != DBNull.Value ? reader["ContactPersonEmail"].ToString() : null,
                                ContactPersonPhoneNumber = reader["ContactPersonPhoneNumber"] != DBNull.Value ? reader["ContactPersonPhoneNumber"].ToString() : null,
                                //CompanyLogoUrl = reader["CompanyLogo"] != DBNull.Value ? reader["CompanyLogo"].ToString() : null,
                                ////Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : null,

                                //AgreementToTerms = reader["AgreementToTerms"] != DBNull.Value ? reader["AgreementToTerms"].ToString() == "1" ? "YES" : "NO" : null,
                                //TrainingAndPlacementProgram = reader["TrainingAndPlacementProgram"] != DBNull.Value ? reader["TrainingAndPlacementProgram"] == "true" ? "YES" : "NO" : "NO",
                                //PlanId = PlanType

                            };
                            emplList.Add(employerResponse);
                        }
                        sqlCon.Close();
                        return emplList;

                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., logging)
                throw;
            }
        }
        public EmployerResponse EmployeeView(int EmployeeID)
        {
            try
            {
                EmployerResponse employerResponse = new EmployerResponse();
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Select * from Employer where CompanyID=@CompanyID ", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCon.Open();
                        sqlCmd.Parameters.AddWithValue("@CompanyID", EmployeeID);
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int planid = reader["PlanId"] != DBNull.Value ? Convert.ToInt32(reader["PlanId"]) : 0;
                            string PlanType = GetPlanType(planid);
                            employerResponse = new EmployerResponse
                            {
                                EmployeeId = Convert.ToInt32(reader["CompanyID"]),
                                EmailStatus = reader["EmailStatus"].ToString(),
                                CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                                CompanyRegistrationNumber = reader["CompanyRegistrationNumber"] != DBNull.Value ? reader["CompanyRegistrationNumber"].ToString() : null,
                                CompanyEmail = reader["CompanyEmailAddress"] != DBNull.Value ? reader["CompanyEmailAddress"].ToString() : null,
                                CompanyPhoneNumber = reader["CompanyPhoneNumber"] != DBNull.Value ? reader["CompanyPhoneNumber"].ToString() : null,
                                CompanyWebsiteUrl = reader["WebsiteUrl"] != DBNull.Value ? reader["WebsiteUrl"].ToString() : null,
                                PhysicalAddress = reader["PhysicalAddress"] != DBNull.Value ? reader["PhysicalAddress"].ToString() : null,
                                CompanyDescription = reader["CompanyDescription"] != DBNull.Value ? reader["CompanyDescription"].ToString() : null,
                                IndustryType = reader["IndustryType"] != DBNull.Value ? reader["IndustryType"].ToString() : null,
                                CompanySize = reader["CompanySize"] != DBNull.Value ? reader["CompanySize"].ToString() : null,
                                ContactPersonName = reader["ContactPersonName"] != DBNull.Value ? reader["ContactPersonName"].ToString() : null,
                                ContactPersonEmail = reader["ContactPersonEmail"] != DBNull.Value ? reader["ContactPersonEmail"].ToString() : null,
                                ContactPersonPhoneNumber = reader["ContactPersonPhoneNumber"] != DBNull.Value ? reader["ContactPersonPhoneNumber"].ToString() : null,
                                CompanyLogoUrl = reader["CompanyLogo"] != DBNull.Value ? reader["CompanyLogo"].ToString() : null,
                                //Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : null,

                                AgreementToTerms = reader["AgreementToTerms"] != DBNull.Value ? reader["AgreementToTerms"].ToString() == "1" ? "YES" : "NO" : null,
                                TrainingAndPlacementProgram = reader["TrainingAndPlacementProgram"] != DBNull.Value ? reader["TrainingAndPlacementProgram"] == "true" ? "YES" : "NO" : "NO",
                                PlanId = PlanType

                            };
                        }
                        return employerResponse;
                        sqlCon.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., logging)
                throw;
            }
        }
        public string TrainerUpdate(TrainerProfileRequest request)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("SP_TrainerUpdate", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@CompanyRegistrationNumber", request.CompanyRegistrationNumber ?? (object)DBNull.Value);
                        sqlCmd.Parameters.AddWithValue("@WebsiteUrl", request.WebsiteUrl ?? (object)DBNull.Value);
                        sqlCmd.Parameters.AddWithValue("@PhysicalAddress", request.PhysicalAddress ?? (object)DBNull.Value);
                        sqlCmd.Parameters.AddWithValue("@CompanyDescription", request.CompanyDescription ?? (object)DBNull.Value);
                        sqlCmd.Parameters.AddWithValue("@IndustryType", request.IndustryType ?? (object)DBNull.Value);
                        sqlCmd.Parameters.AddWithValue("@CompanySize", request.CompanySize ?? (object)DBNull.Value);
                        sqlCmd.Parameters.AddWithValue("@ContactPersonName", request.ContactPersonName ?? (object)DBNull.Value);
                        sqlCmd.Parameters.AddWithValue("@ContactPersonEmail", request.ContactPersonEmail ?? (object)DBNull.Value);
                        sqlCmd.Parameters.AddWithValue("@ContactPersonPhoneNumber", request.ContactPersonPhoneNumber ?? (object)DBNull.Value);
                        sqlCmd.Parameters.AddWithValue("@CompanyLogo", request.CompanyLogo ?? (object)DBNull.Value);
                        sqlCmd.Parameters.AddWithValue("@AreasOfSpecialization", request.AreasOfSpecialization);
                        sqlCmd.Parameters.AddWithValue("@PlanId", request.PlanId != 0 ? request.PlanId : 0);
                        sqlCmd.Parameters.AddWithValue("@TrainerID", request.TrainerID);
                        sqlCmd.Parameters.AddWithValue("@Action", "Update");
                        SqlParameter updateSuccessParam = new SqlParameter("@UpdateSuccess", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        sqlCmd.Parameters.Add(updateSuccessParam);
                        sqlCon.Open();
                        sqlCmd.ExecuteNonQuery();
                        bool result = (bool)updateSuccessParam.Value;
                        if (result)
                            return "Successfully Updated";
                        else
                            return "Updation Failed";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., logging)
                throw;
            }
        }
        public AdminRegistrationRequest LoadAdmin(int adminId)
        {
            AdminRegistrationRequest request = new AdminRegistrationRequest();
            using (var sqlcon = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Admins WHERE AdminID = @AdminID";
                using (var sqlcmd = new SqlCommand(query, sqlcon))
                {
                    sqlcmd.Parameters.AddWithValue("@AdminID", adminId);
                    sqlcon.Open();
                    var reader = sqlcmd.ExecuteReader();
                    if (reader.Read())
                    {
                        request.Email = reader["Email"].ToString();
                        request.Username = reader["Username"].ToString();
                        request.Name = reader["Name"].ToString();
                    }
                    sqlcon.Close();
                }
                return request;
            }
        }
        public string UpdateAdmin(string email, string username, string name, int adminId)
        {
            AdminRegistrationRequest request = new AdminRegistrationRequest();


            using (var sqlcon = new SqlConnection(_connectionString))
            {
                var query = "UPDATE Admins SET Email = @Email, Username = @Username , Name=@Name WHERE AdminID = @AdminID";
                using (var sqlcmd = new SqlCommand(query, sqlcon))
                {
                    sqlcmd.Parameters.AddWithValue("@Email", email);
                    sqlcmd.Parameters.AddWithValue("@Name", name);
                    sqlcmd.Parameters.AddWithValue("@Username", username);
                    sqlcmd.Parameters.AddWithValue("@AdminID", adminId);

                    sqlcon.Open();
                    int rows = sqlcmd.ExecuteNonQuery();
                    sqlcon.Close();
                    if (rows > 0)
                    {
                        return "success";
                    }
                    else return "failed";
                }
            }
        }
        public string EmployeeUpdate(EmployeeProfileRequest request, int EmployeeID)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("SP_EmployeeUpdate", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@CompanyRegistrationNumber", request.CompanyRegistrationNumber);
                        sqlCmd.Parameters.AddWithValue("@WebsiteUrl", request.WebsiteUrl);
                        sqlCmd.Parameters.AddWithValue("@PhysicalAddress", request.PhysicalAddress);
                        sqlCmd.Parameters.AddWithValue("@CompanyDescription", request.CompanyDescription);
                        sqlCmd.Parameters.AddWithValue("@IndustryType", request.IndustryType);
                        sqlCmd.Parameters.AddWithValue("@CompanySize", request.CompanySize);
                        sqlCmd.Parameters.AddWithValue("@ContactPersonName", request.ContactPersonName);
                        sqlCmd.Parameters.AddWithValue("@ContactPersonEmail", request.ContactPersonEmail);
                        sqlCmd.Parameters.AddWithValue("@ContactPersonPhoneNumber", request.ContactPersonPhoneNumber);
                        sqlCmd.Parameters.AddWithValue("@CompanyLogo", request.CompanyLogo);
                        sqlCmd.Parameters.AddWithValue("@TrainingAndPlacementProgram", request.TrainingAndPlacementProgram);
                        sqlCmd.Parameters.AddWithValue("@PlanId", request.PlanId);
                        sqlCmd.Parameters.AddWithValue("@CompanyID", EmployeeID);
                        sqlCmd.Parameters.AddWithValue("@Action", "Update");
                        SqlParameter updateSuccessParam = new SqlParameter("@UpdateSuccess", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        sqlCmd.Parameters.Add(updateSuccessParam);
                        sqlCon.Open();
                        sqlCmd.ExecuteNonQuery();
                        bool result = (bool)updateSuccessParam.Value;
                        if (result)
                            return "Successfully Updated";
                        else
                            return "Updation Failed";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., logging)
                throw;
            }
        }
        public string AdminRegister(AdminRegistrationRequest request, string encryptedPassword)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_RegisterAdmin", Sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;

                        // Hash the password before sending it to the database
                        // string passwordHash = HashPassword(request.Password);
                        sqlcmd.Parameters.AddWithValue("@Name", request.Name);
                        sqlcmd.Parameters.AddWithValue("@Username", request.Username);
                        sqlcmd.Parameters.AddWithValue("@Email", request.Email);
                        sqlcmd.Parameters.AddWithValue("@PasswordHash", encryptedPassword);
                        sqlcmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        sqlcmd.Parameters.AddWithValue("@AdminRole", request.AdminRole);
                        SqlParameter AdminId = new SqlParameter("@AdminId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        sqlcmd.Parameters.Add(AdminId);

                        Sqlcon.Open();
                        sqlcmd.ExecuteNonQuery();

                        int result = (int)AdminId.Value;

                        if (result > 0)
                            return "Registered successfully";
                        else
                            return "Registration Failed";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<JobSearchResult> GetJobsFromDB(JobSearch criteria, int CandidateId)
        {
            try
            {

                string Keyword = !string.IsNullOrEmpty(criteria.Keyword) && criteria.Keyword != "string" ? criteria.Keyword : null;
                decimal? MinSalary = criteria.MinSalary != 0 ? criteria.MinSalary : (decimal?)null;
                decimal? MaxSalary = criteria.MaxSalary != 0 ? criteria.MaxSalary : (decimal?)null;
                string RequiredSkills = !string.IsNullOrEmpty(criteria.RequiredSkills) && criteria.RequiredSkills != "string" ? criteria.RequiredSkills : null;

                // string postalCode = !string.IsNullOrEmpty(locationRequest.PostalCode) ? locationRequest.PostalCode == "string" ? null : locationRequest.PostalCode : null;


                List<JobSearchResult> results = new List<JobSearchResult>();

                using (SqlConnection sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_CandidateSearchJobs", sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.Parameters.AddWithValue("@Keyword", Keyword ?? (object)DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@Location", criteria.Location != 0 ? (object)criteria.Location : (object)DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@MinSalary", MinSalary ?? (object)DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@MaxSalary", MaxSalary ?? (object)DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@RequiredSkills", RequiredSkills ?? (object)DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@CandidateID", CandidateId);

                        sqlcon.Open();
                        using (SqlDataReader reader = sqlcmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int JobType = reader["JobType"] != DBNull.Value ? Convert.ToInt32(reader["JobType"]) : 0;
                                int loc = reader["JobLocation"] != DBNull.Value ? Convert.ToInt32(reader["JobLocation"]) : 0;
                                string location = GetJoblocation(loc);

                                string Jobtype = GetJobType(JobType);
                                JobSearchResult result = new JobSearchResult
                                {
                                    JobID = reader["JobID"] != DBNull.Value ? Convert.ToInt32(reader["JobID"]) : 0,
                                    JobTitle = reader["JobTitle"] != DBNull.Value ? reader["JobTitle"].ToString() : string.Empty,
                                    JobDescription = reader["JobDescription"] != DBNull.Value ? reader["JobDescription"].ToString() : string.Empty,
                                    JobLocation = location,
                                    Salary = reader["Salary"] != DBNull.Value ? Convert.ToDecimal(reader["Salary"]) : 0,
                                    CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : string.Empty,
                                    JobType = Jobtype,
                                    ApplicationDeadline = reader["ApplicationDeadline"] != DBNull.Value ? Convert.ToDateTime(reader["ApplicationDeadline"]) : DateTime.MinValue,
                                    RequiredSkills = reader["RequiredSkills"] != DBNull.Value ? reader["RequiredSkills"].ToString() : string.Empty
                                };
                                results.Add(result);
                            }
                        }
                    }
                }

                return (results);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CandidateDetails> GetCandidatesbasedonSkills(CandidateSearchRequest request)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var candidates = GetCandidatesBySkillPercentage(connection, request.SkillId, request.MinSkillPercentage, request.MaxSkillPercentage, request.Core);
                return candidates;
            }
        }
        private List<CandidateDetails> GetCandidatesBySkillPercentage(SqlConnection connection, int? skillId, double? minSkillPercentage, double? maxSkillPercentage, bool core)
        {
            float? coreSkillPercentage = 0, softSkillPercentage = 0;

            var candidates = new List<CandidateDetails>();
            using (SqlCommand command = new SqlCommand("SP_FilterBySkillPercentage", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SkillId", (object)skillId ?? DBNull.Value);
                command.Parameters.AddWithValue("@MinSkillPercentage", (object)minSkillPercentage ?? DBNull.Value);
                command.Parameters.AddWithValue("@MaxSkillPercentage", (object)maxSkillPercentage ?? DBNull.Value);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        //string CoreSkill = core ? GetSkillById((int)skillId, "CoreSkills") : null;
                        //string SoftSkill = !core ? GetSkillById((int)skillId, "SoftSkills") : null;

                        string FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        string LastName = reader.GetString(reader.GetOrdinal("LastName"));

                        string skillName = core ? GetSkillById((int)skillId, "CoreSkills") : GetSkillById((int)skillId, "SoftSkills");
                        float? skillPercentage = reader["SkillPercentage"] != DBNull.Value ? Convert.ToSingle(reader["SkillPercentage"]) : (float?)null;

                        // Check if the skill percentage falls within the provided range
                        bool isValidSkill = (skillPercentage >= minSkillPercentage && skillPercentage <= maxSkillPercentage);

                        if (isValidSkill)
                        {

                            candidates.Add(new CandidateDetails
                            {
                                CandidateId = reader.GetInt32(reader.GetOrdinal("CandidateId")),
                                CandidateName = FirstName + " " + LastName,
                                EmailAddress = reader["EmailAddress"].ToString(),
                                MobileNumber = reader["MobileNumber"].ToString(),
                                Address = reader["Address"].ToString(),
                                City = reader["City"].ToString(),
                                StateOrProvince = reader["State/Province"].ToString(),
                                PostalOrZipCode = reader["Postal/ZipCode"].ToString(),
                                HighestEducationLevel = reader["HighestEducationLevel"].ToString(),
                                CoreSkill = core ? skillName : null,
                                CoreSkillPercentage = core ? skillPercentage : null,
                                SoftSkill = !core ? skillName : null,
                                SoftSkillPercentage = !core ? skillPercentage : null,

                            });
                        }

                    }
                }
            }
            return candidates;
        }
        private string GetSkillById(int skillId, string skillColumn)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string Skills = "";
                string query = $"SELECT {skillColumn} FROM Skills WHERE SkillId =@SkillId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue($"@SkillId", skillId);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Skills = reader[skillColumn].ToString();
                        }
                    }
                    connection.Close();
                    // concatenatedSkills = concatenatedSkills.TrimEnd(',', ' ');
                }

                return Skills;
            }
        }

        public List<ViewJobs> GetJobsFromDB(int EmployeeID)
        {
            try
            {
                string Jobtype = "", JobLoaction = "";
                List<ViewJobs> viewJobsList = new List<ViewJobs>();
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(@"SELECT
                            ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS SLNo ,*  FROM JobPostings WHERE EmployeeID = @EmployeeID", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCon.Open();
                        sqlCmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int jobtypeid = reader["JobType"] != DBNull.Value ? Convert.ToInt32(reader["JobType"]) : 0;
                                if (jobtypeid != 0)
                                {
                                    Jobtype = GetJobType(jobtypeid);
                                }
                                int locationid = reader["JobLocation"] != DBNull.Value ? Convert.ToInt32(reader["JobLocation"]) : 0;
                                if (jobtypeid != 0)
                                {
                                    JobLoaction = GetJoblocation(locationid);
                                }

                                ViewJobs viewJobs = new ViewJobs
                                {
                                    SLNo = Convert.ToInt32(reader["SLNo"]),
                                    JobID = Convert.ToInt32(reader["JobID"]),
                                    JobTitle = reader["JobTitle"].ToString(),
                                    JobDescription = reader["JobDescription"].ToString(),
                                    JobLocation = JobLoaction,
                                    JobType = Jobtype,
                                    //CompanyName = reader["CompanyName"].ToString(),
                                    //ContactEmail = reader["ContactEmail"].ToString(),
                                    Vacancy = Convert.ToInt32(reader["Vacancy"]),
                                    //Website = reader["Website"].ToString(),
                                    Qualifications = reader["Qualifications"].ToString(),
                                    Experience = reader["Experience"].ToString(),
                                    //Address = reader["Address"].ToString(),
                                    RequiredSkills = reader["RequiredSkills"] != DBNull.Value ? reader["RequiredSkills"].ToString() : null,
                                    Salary = reader["Salary"] != DBNull.Value ? Convert.ToDecimal(reader["Salary"]) : 0,
                                    ApplicationDeadline = reader["ApplicationDeadline"] != DBNull.Value ? (DateTime?)reader["ApplicationDeadline"] : null,
                                    ApplicationStartDate = reader["ApplicationStartDate"] != DBNull.Value ? (DateTime?)reader["ApplicationStartDate"] : null
                                };
                                viewJobsList.Add(viewJobs);
                            }

                        }
                        sqlCon.Close();
                        return viewJobsList;


                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., logging)
                throw;
            }
        }
        public ViewJobs GetJobRequestFromDB(int RequestID)
        {
            try
            {
                string Jobtype = "", JobLoaction = "";
                ViewJobs viewJobs = new ViewJobs();
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("SP_GetJobDetails", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;


                        sqlCon.Open();
                        sqlCmd.Parameters.AddWithValue("@Action", "EditJob");
                        sqlCmd.Parameters.AddWithValue("@RequestID", RequestID);
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int jobtypeid = reader["NewJobType"] != DBNull.Value ? Convert.ToInt32(reader["NewJobType"]) : 0;
                                if (jobtypeid != 0)
                                {
                                    Jobtype = GetJobType(jobtypeid);
                                }
                                int locationid = reader["NewJobLocation"] != DBNull.Value ? Convert.ToInt32(reader["NewJobLocation"]) : 0;
                                if (jobtypeid != 0)
                                {
                                    JobLoaction = GetJoblocation(locationid);
                                }

                                viewJobs = new ViewJobs
                                {
                                    //SLNo = Convert.ToInt32(reader["SLNo"]),
                                    JobID = Convert.ToInt32(reader["JobID"]),
                                    JobTitle = reader["NewJobTitle"].ToString(),
                                    JobDescription = reader["NewJobDescription"].ToString(),
                                    JobLocation = JobLoaction,
                                    JobType = Jobtype,
                                    CompanyName = reader["CompanyName"].ToString(),
                                    //ContactEmail = reader["ContactEmail"].ToString(),
                                    Vacancy = Convert.ToInt32(reader["NewVacancy"]),
                                    // Website = reader["WebsiteURL"].ToString(),
                                    Qualifications = reader["NewQualifications"].ToString(),
                                    Experience = reader["NewExperience"].ToString(),
                                    //Address = reader["Address"].ToString(),
                                    RequiredSkills = reader["NewRequiredSkills"] != DBNull.Value ? reader["NewRequiredSkills"].ToString() : null,
                                    Salary = reader["NewSalary"] != DBNull.Value ? Convert.ToDecimal(reader["NewSalary"]) : 0,
                                    ApplicationDeadline = reader["NewApplicationDeadline"] != DBNull.Value ? (DateTime?)reader["NewApplicationDeadline"] : null,
                                    ApplicationStartDate = reader["NewApplicationStartDate"] != DBNull.Value ? (DateTime?)reader["NewApplicationStartDate"] : null
                                };
                                // viewJobsList.Add(viewJobs);
                            }

                        }
                        sqlCon.Close();
                        return viewJobs;


                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., logging)
                throw;
            }
        }
        public ViewJobs GetJobFromDB(int JobId)
        {
            try
            {
                string Jobtype = "", JobLoaction = "";
                ViewJobs viewJobs = new ViewJobs();
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("SP_GetJobDetails", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                      
                        sqlCon.Open();
                         sqlCmd.Parameters.AddWithValue("@Action","GetJob");
                        sqlCmd.Parameters.AddWithValue("@JobId", JobId);
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int jobtypeid = reader["JobType"] != DBNull.Value ? Convert.ToInt32(reader["JobType"]) : 0;
                                if (jobtypeid != 0)
                                {
                                    Jobtype = GetJobType(jobtypeid);
                                }
                                int locationid = reader["JobLocation"] != DBNull.Value ? Convert.ToInt32(reader["JobLocation"]) : 0;
                                if (jobtypeid != 0)
                                {
                                    JobLoaction = GetJoblocation(locationid);
                                }

                                viewJobs = new ViewJobs
                                {
                                    //SLNo = Convert.ToInt32(reader["SLNo"]),
                                    JobID = Convert.ToInt32(reader["JobID"]),
                                    JobTitle = reader["JobTitle"].ToString(),
                                    JobDescription = reader["JobDescription"].ToString(),
                                    JobLocation = JobLoaction,
                                    JobType = Jobtype,
                                    CompanyName = reader["CompanyName"].ToString(),
                                    //ContactEmail = reader["ContactEmail"].ToString(),
                                    Vacancy = Convert.ToInt32(reader["Vacancy"]),
                                   // Website = reader["WebsiteURL"].ToString(),
                                    Qualifications = reader["Qualifications"].ToString(),
                                    Experience = reader["Experience"].ToString(),
                                    //Address = reader["Address"].ToString(),
                                    RequiredSkills = reader["RequiredSkills"] != DBNull.Value ? reader["RequiredSkills"].ToString() : null,
                                    Salary = reader["Salary"] != DBNull.Value ? Convert.ToDecimal(reader["Salary"]) : 0,
                                    ApplicationDeadline = reader["ApplicationDeadline"] != DBNull.Value ? (DateTime?)reader["ApplicationDeadline"] : null,
                                    ApplicationStartDate = reader["ApplicationStartDate"] != DBNull.Value ? (DateTime?)reader["ApplicationStartDate"] : null
                                };
                                // viewJobsList.Add(viewJobs);
                            }

                        }
                        sqlCon.Close();
                        return viewJobs;


                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., logging)
                throw;
            }
        }
        public bool UpdateJobByAdmin(JobPostingRequest request, int JobId)
        {
            try
            {
                bool isSuccess;
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_UpdateJobPost", Sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;

                        //sqlcmd.Parameters.AddWithValue("@EmployeeID", EmployeeId);
                        sqlcmd.Parameters.AddWithValue("@JobID", JobId);
                        sqlcmd.Parameters.AddWithValue("@JobTitle", (object)request.JobTitle ?? DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@Vacancy", request.Vacancy != 0 ? (object)request.Vacancy : DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@JobDescription", (object)request.JobDescription ?? DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@Qualifications", (object)request.Qualifications ?? DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@Experience", (object)request.Experience ?? DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@RequiredSkills", (object)request.RequiredSkills ?? DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@JobLocation", request.JobLocationId != 0 ? (object)request.JobLocationId : DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@Salary", request.Salary.HasValue ? (object)request.Salary.Value : DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@CompanyName", (object)request.CompanyName ?? DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@JobType", request.JobTypeId != 0 ? (object)request.JobTypeId : DBNull.Value);
                        //sqlcmd.Parameters.AddWithValue("@NewAddress", (object)request.Address ?? DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@ApplicationDeadline", request.ApplicationDeadline.HasValue ? (object)request.ApplicationDeadline.Value : DBNull.Value);
                        //sqlcmd.Parameters.AddWithValue("@NewContactEmail", (object)request.ContactEmail ?? DBNull.Value);
                        //sqlcmd.Parameters.AddWithValue("@NewWebsite", (object)request.Website ?? DBNull.Value);
                        //sqlcmd.Parameters.AddWithValue("@NewJobUpdatedDate", DateTime.Now);
                        sqlcmd.Parameters.AddWithValue("@ApplicationStartDate", request.ApplicationStartDate.HasValue ? (object)request.ApplicationStartDate.Value : DBNull.Value);
                        //sqlcmd.Parameters.AddWithValue("@NewIndustry", request.Industry != null ? request.Industry : null);

                        // Output parameter
                        SqlParameter isSuccessParam = new SqlParameter("@IsSuccess", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        sqlcmd.Parameters.Add(isSuccessParam);

                        try
                        {
                            Sqlcon.Open();
                            sqlcmd.ExecuteNonQuery();

                            isSuccess = (bool)isSuccessParam.Value;
                            //message = isSuccess ? "Job edit request submitted successfully" : "Failed to submit job edit request..";
                        }
                        catch (Exception ex)
                        {
                            // Log or handle the exception
                            throw ex;
                        }
                    }
                }
                return isSuccess;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DashBoardData GetDashboardAnalytics()
        {
            DashBoardData data = new DashBoardData();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetDashboardAnalytics", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        data.TotalEmployers = Convert.ToInt32(reader["TotalEmployers"]);
                        data.TotalCandidates = Convert.ToInt32(reader["TotalCandidates"]);
                        data.TotalJobPostings = Convert.ToInt32(reader["TotalJobPostings"]);
                        data.TotalApplications = Convert.ToInt32(reader["TotalApplications"]);
                    }
                    con.Close();
                }
            }
            return data;
        }
        public bool UpdateJobsDB(JobPostingRequest request, int EmployeeId, int JobId)
        {
            try
            {
                bool isSuccess;
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_RequestJobEdit", Sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;

                        sqlcmd.Parameters.AddWithValue("@EmployeeID", EmployeeId);
                        sqlcmd.Parameters.AddWithValue("@JobID", JobId);
                        sqlcmd.Parameters.AddWithValue("@NewJobTitle", (object)request.JobTitle ?? DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@NewVacancy", request.Vacancy != 0 ? (object)request.Vacancy : DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@NewJobDescription", (object)request.JobDescription ?? DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@NewQualifications", (object)request.Qualifications ?? DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@NewExperience", (object)request.Experience ?? DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@NewRequiredSkills", (object)request.RequiredSkills ?? DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@NewJobLocation", request.JobLocationId != 0 ? (object)request.JobLocationId : DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@NewSalary", request.Salary.HasValue ? (object)request.Salary.Value : DBNull.Value);
                        // sqlcmd.Parameters.AddWithValue("@NewCompanyName", (object)request.CompanyName ?? DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@NewJobType", request.JobTypeId != 0 ? (object)request.JobTypeId : DBNull.Value);
                        //sqlcmd.Parameters.AddWithValue("@NewAddress", (object)request.Address ?? DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@NewApplicationDeadline", request.ApplicationDeadline.HasValue ? (object)request.ApplicationDeadline.Value : DBNull.Value);
                        //sqlcmd.Parameters.AddWithValue("@NewContactEmail", (object)request.ContactEmail ?? DBNull.Value);
                        //sqlcmd.Parameters.AddWithValue("@NewWebsite", (object)request.Website ?? DBNull.Value);
                        //sqlcmd.Parameters.AddWithValue("@NewJobUpdatedDate", DateTime.Now);
                        sqlcmd.Parameters.AddWithValue("@NewApplicationStartDate", request.ApplicationStartDate.HasValue ? (object)request.ApplicationStartDate.Value : DBNull.Value);
                        //sqlcmd.Parameters.AddWithValue("@NewIndustry", request.Industry != null ? request.Industry : null);

                        // Output parameter
                        SqlParameter isSuccessParam = new SqlParameter("@IsSuccess", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        sqlcmd.Parameters.Add(isSuccessParam);

                        try
                        {
                            Sqlcon.Open();
                            sqlcmd.ExecuteNonQuery();

                            isSuccess = (bool)isSuccessParam.Value;
                            //message = isSuccess ? "Job edit request submitted successfully" : "Failed to submit job edit request..";
                        }
                        catch (Exception ex)
                        {
                            // Log or handle the exception
                            throw ex;
                        }
                    }
                }
                return isSuccess;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public AdminRegistrationRequest AdminLogin(string email, string password)
        {
            try
            {

                AdminRegistrationRequest response = new AdminRegistrationRequest();
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand())
                    {
                        sqlcmd.Connection = Sqlcon;

                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.CommandText = "SP_AdminLogin";

                        // Parameters for the stored procedure
                        sqlcmd.Parameters.AddWithValue("@Email", email);
                        sqlcmd.Parameters.AddWithValue("@Password", password);
                        // You should ideally hash the password before sending it to the database
                        // string hashedPassword = HashPassword(request.Password);
                        // sqlcmd.Parameters.AddWithValue("@Password", hashedPassword);
                        //sqlcmd.Parameters.AddWithValue("@Action", "Login");
                        Sqlcon.Open();

                        try
                        {
                            using (SqlDataReader reader = sqlcmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    response = new AdminRegistrationRequest();
                                    while (reader.Read())
                                    {
                                        response.AdminId = Convert.ToInt32(reader["AdminID"]);
                                        response.Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null;
                                        response.Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : null;
                                        // response.EmailStatus = reader["EmailStatus"] != DBNull.Value ? reader["EmailStatus"].ToString() : null;
                                    }

                                }
                            }

                            if (response.AdminId != 0)
                            {
                                return response;
                            }
                            return response;


                        }
                        catch (SqlException sqlEx)
                        {
                            // Check if the error is the custom 'Invalid email or password.' error
                            if (sqlEx.Message.Contains("Invalid email or password."))
                            {
                                response = new AdminRegistrationRequest();
                                response.Message = "Invalid email or password.";
                                // Return null or an appropriate response indicating invalid credentials
                                return response;  // Invalid credentials
                            }
                            else
                            {
                                // Log other SQL exceptions
                                throw new Exception("SQL Error: " + sqlEx.Message, sqlEx);
                            }
                        }
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                // Catch any general exceptions and rethrow or handle them
                throw new Exception("An error occurred during admin login: " + ex.Message, ex);
            }
        }
        public string GetEmployerName(int employerId)
        {

            string employerName = string.Empty;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT CompanyName FROM Employer WHERE CompanyID = @EmployeeID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@EmployeeID", employerId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    employerName = reader["CompanyName"].ToString();
                }

                reader.Close();
            }

            return employerName;
        }
        public Jobs GetJobPostings(int EmployeeId, int JobId)
        {
            try
            {
                string Jobtype = " ", JobLoaction = "";
                List<Jobs> viewJobsList = new List<Jobs>();
                Jobs viewJobs = new Jobs();
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("SELECT * FROM JobPostings where EmployeeID=@EmployeeID and JobID=@JobID", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCon.Open();
                        sqlCmd.Parameters.AddWithValue("@EmployeeID", EmployeeId);
                        sqlCmd.Parameters.AddWithValue("@JobID", JobId);
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int jobtypeid = reader["JobType"] != DBNull.Value ? Convert.ToInt32(reader["JobType"]) : 0;
                                if (jobtypeid != 0)
                                {
                                    Jobtype = GetJobType(jobtypeid);
                                }

                                int locationid = reader["JobLocation"] != DBNull.Value ? Convert.ToInt32(reader["JobLocation"]) : 0;
                                if (locationid != 0)
                                {
                                    JobLoaction = GetJoblocation(locationid);
                                }

                                viewJobs = new Jobs
                                {
                                    //SLNo = Convert.ToInt32(reader["SLNo"]),
                                    // = Convert.ToInt32(reader["JobID"]),
                                    EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                    JobID = Convert.ToInt32(reader["JobID"]),
                                    JobTitle = reader["JobTitle"] != DBNull.Value ? reader["JobTitle"].ToString() : null,
                                    JobDescription = reader["JobDescription"] != DBNull.Value ? reader["JobDescription"].ToString() : null,
                                    JobLocation = JobLoaction != null && JobLoaction != "" ? JobLoaction : null,
                                    JobType = Jobtype != null && JobLoaction != " " ? JobLoaction : null,
                                    //CompanyName = reader["CompanyName"].ToString(),
                                    //ContactEmail = reader["NewContactEmail"].ToString(),
                                    Vacancy = reader["Vacancy"] != DBNull.Value ? Convert.ToInt32(reader["Vacancy"]) : 0,
                                    //Website = reader["NewWebsite"].ToString(),
                                    Qualifications = reader["Qualifications"] != DBNull.Value ? reader["Qualifications"].ToString() : null,
                                    Experience = reader["Experience"] != DBNull.Value ? reader["Experience"].ToString() : null,
                                    //Address = reader["NewAddress"].ToString(),
                                    RequiredSkills = reader["RequiredSkills"] != DBNull.Value ? reader["RequiredSkills"].ToString() : null,
                                    Salary = reader["Salary"] != DBNull.Value ? (decimal?)reader["Salary"] : 0,
                                    ApplicationDeadline = reader["ApplicationDeadline"] != DBNull.Value ? (DateTime?)reader["ApplicationDeadline"] : null,
                                    ApplicationStartDate = reader["ApplicationStartDate"] != DBNull.Value ? (DateTime?)reader["ApplicationStartDate"] : null,
                                    // RequestStatus = reader["RequestStatus"].ToString(),
                                    //R//equestDate = (DateTime)reader["RequestDate"]

                                };
                                //  viewJobsList.Add(viewJobs);
                            }

                        }

                    }
                    sqlCon.Close();
                    return viewJobs;


                }

            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., logging)
                throw;
            }
        }
        public JobList GetJobEditRequests(int EmployeeId, int RequestID)
        {
            try
            {
                string Jobtype = " ", JobLoaction = "";
                List<JobList> viewJobsList = new List<JobList>();
                JobList viewJobs = new JobList();
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(@"SELECT *   FROM JobeditRequests where EmployeeId=@EmployeeID and RequestID=@RequestID", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCon.Open();
                        sqlCmd.Parameters.AddWithValue("@EmployeeID", EmployeeId);
                        sqlCmd.Parameters.AddWithValue("@RequestID", RequestID);
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                int jobtypeid = reader["NewJobType"] != DBNull.Value ? Convert.ToInt32(reader["NewJobType"]) : 0;
                                if (jobtypeid != 0)
                                {
                                    Jobtype = GetJobType(jobtypeid);
                                }


                                int locationid = reader["NewJobLocation"] != DBNull.Value ? Convert.ToInt32(reader["NewJobLocation"]) : 0;
                                if (locationid != 0)
                                {
                                    JobLoaction = GetJoblocation(locationid);
                                }

                                viewJobs = new JobList
                                {
                                    // SLNo = Convert.ToInt32(reader["SLNo"]),
                                    RequestID = Convert.ToInt32(reader["RequestID"]),
                                    EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                    JobID = Convert.ToInt32(reader["JobID"]),
                                    JobTitle = reader["NewJobTitle"].ToString(),
                                    JobDescription = reader["NewJobDescription"].ToString(),
                                    JobLocation = JobLoaction,
                                    JobType = Jobtype,
                                    CompanyName = reader["NewCompanyName"].ToString(),
                                    ContactEmail = reader["NewContactEmail"].ToString(),
                                    Vacancy = Convert.ToInt32(reader["NewVacancy"]),
                                    Website = reader["NewWebsite"].ToString(),
                                    Qualifications = reader["NewQualifications"].ToString(),
                                    Experience = reader["NewExperience"].ToString(),
                                    Address = reader["NewAddress"].ToString(),
                                    RequiredSkills = reader["NewRequiredSkills"].ToString(),
                                    Salary = (decimal?)reader["NewSalary"],
                                    ApplicationDeadline = reader["NewApplicationDeadline"] != DBNull.Value ? (DateTime?)reader["NewApplicationDeadline"] : null,
                                    ApplicationStartDate = reader["NewApplicationStartDate"] != DBNull.Value ? (DateTime?)reader["NewApplicationStartDate"] : null,
                                    RequestStatus = reader["RequestStatus"].ToString(),
                                    RequestDate = (DateTime)reader["RequestDate"]

                                };
                                //viewJobsList.Add(viewJobs);
                            }

                        }

                    }
                    sqlCon.Close();
                    return viewJobs;


                }

            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., logging)
                throw;
            }
        }
        public bool DeleteEmployee(int EmployeeID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Employer WHERE CompanyID=@EmployeeID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        return true;
                    }
                    else return false;


                }
            }
        }
        public bool DeleteCandidate(int CandidateID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Candidate WHERE CandidateID = @CandidateID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CandidateID", CandidateID);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        return true;
                    }
                    else return false;


                }
            }
        }
        public bool DeleteJobEditRequest(int RequestID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM [JobEditRequests] WHERE [RequestID] = @RequestID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RequestID", RequestID);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        return true;
                    }
                    else return false;


                }
            }
        }
        public bool DeleteJobPosting(int JobID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM JobPostings WHERE JobID = @JobID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@JobID", JobID);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        return true;
                    }
                    else return false;


                }
            }
        }
        public bool DeleteTrainer(int TrainerID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Trainer WHERE TrainerID = @TrainerID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TrainerID", TrainerID);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        return true;
                    }
                    else return false;


                }
            }
        }
        public string VerifyJobRequest(int requestId, string RequestStatus)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_ApproveRejectJobRequests", Sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.Parameters.AddWithValue("@RequestId", requestId);
                      //  sqlcmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                        sqlcmd.Parameters.AddWithValue("@Action", RequestStatus);
                        SqlParameter statusParam = new SqlParameter("@Status", SqlDbType.VarChar, 20)
                        {
                            Direction = ParameterDirection.Output
                        };
                        sqlcmd.Parameters.Add(statusParam);
                        try
                        {
                            Sqlcon.Open();
                            sqlcmd.ExecuteNonQuery();

                            string status = statusParam.Value.ToString();
                            return status;
                        }
                        catch (SqlException ex)
                        {
                            // Log the error message if necessary
                            string errorMessage = ex.Message;
                            return $"Error processing job edit request: {errorMessage}";
                        }
                        finally
                        {
                            Sqlcon.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ViewJobs> GetJobRequestFromDB()
        {
            try
            {
                string Jobtype = " ", JobLoaction = "";
                List<ViewJobs> viewJobsList = new List<ViewJobs>();
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(@"SELECT
                            ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS SLNo ,*  
                              FROM JobEditRequests" /*where RequestStatus!=@RequestStatus*/, sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCon.Open();
                        //sqlCmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                        sqlCmd.Parameters.AddWithValue("@RequestStatus", "Approved");
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                int jobtypeid = reader["NewJobType"] != DBNull.Value ? Convert.ToInt32(reader["NewJobType"]) : 0;
                                if (jobtypeid != 0)
                                {
                                    Jobtype = GetJobType(jobtypeid);
                                }


                                int locationid = reader["NewJobLocation"] != DBNull.Value ? Convert.ToInt32(reader["NewJobLocation"]) : 0;
                                if (locationid != 0)
                                {
                                    JobLoaction = GetJoblocation(locationid);
                                }

                                ViewJobs viewJobs = new ViewJobs
                                {
                                    SLNo = Convert.ToInt32(reader["SLNo"]),
                                    RequestID = Convert.ToInt32(reader["RequestID"]),
                                    EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                    JobID = Convert.ToInt32(reader["JobID"]),
                                    JobTitle = reader["NewJobTitle"].ToString(),
                                    JobDescription = reader["NewJobDescription"].ToString(),
                                    JobLocation = JobLoaction,
                                    JobType = Jobtype,
                                    CompanyName = reader["NewCompanyName"].ToString(),
                                    ContactEmail = reader["NewContactEmail"].ToString(),
                                    Vacancy = Convert.ToInt32(reader["NewVacancy"]),
                                    Website = reader["NewWebsite"].ToString(),
                                    Qualifications = reader["NewQualifications"].ToString(),
                                    Experience = reader["NewExperience"].ToString(),
                                    Address = reader["NewAddress"].ToString(),
                                    RequiredSkills = reader["NewRequiredSkills"].ToString(),
                                    Salary = (decimal?)reader["NewSalary"],
                                    ApplicationDeadline = reader["NewApplicationDeadline"] != DBNull.Value ? (DateTime?)reader["NewApplicationDeadline"] : null,
                                    ApplicationStartDate = reader["NewApplicationStartDate"] != DBNull.Value ? (DateTime?)reader["NewApplicationStartDate"] : null,
                                    RequestStatus = reader["RequestStatus"].ToString(),
                                    RequestDate = (DateTime)reader["RequestDate"]

                                };
                                viewJobsList.Add(viewJobs);
                            }

                        }

                    }
                    sqlCon.Close();
                    return viewJobsList;


                }

            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., logging)
                throw;
            }
        }
        public string VerifyRequests(int requestId, int employeeId, string RequestStatus)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_ApproveRejectJobRequests", Sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.Parameters.AddWithValue("@RequestId", requestId);
                        sqlcmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                        sqlcmd.Parameters.AddWithValue("@Action", RequestStatus);
                        SqlParameter statusParam = new SqlParameter("@Status", SqlDbType.VarChar, 20)
                        {
                            Direction = ParameterDirection.Output
                        };
                        sqlcmd.Parameters.Add(statusParam);
                        try
                        {
                            Sqlcon.Open();
                            sqlcmd.ExecuteNonQuery();

                            string status = statusParam.Value.ToString();
                            return status;
                        }
                        catch (SqlException ex)
                        {
                            // Log the error message if necessary
                            string errorMessage = ex.Message;
                            return $"Error processing job edit request: {errorMessage}";
                        }
                        finally
                        {
                            Sqlcon.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string JobApplication(JobApplication application)
        {
            try
            {
                string fileName = "";
                string Message = "";
                if (application.ResumeFile != null)
                {
                    fileName = $"{Path.GetFileNameWithoutExtension(application.ResumeFile.FileName)}.pdf";

                    // var filePath = Path.Combine("C:\\Nimisha\\", "uploads", fileName);
                    var filePath = Path.Combine(ResumeUploadFilePath, fileName);
                    // Ensure the directory exists
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    // Save the file to the server as a byte stream
                    string fileExtension = Path.GetExtension(application.ResumeFile.FileName);


                    // Create directory if it doesn't exist
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ConvertToPdf(application.ResumeFile, stream);
                    }
                }
                else
                {
                    using (SqlConnection Sqlcon1 = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand sqlcmd = new SqlCommand("Select ResumeFilePath from Candidate where CandidateID=@CandidateID", Sqlcon1))
                        {
                            sqlcmd.CommandType = CommandType.Text;
                            sqlcmd.Parameters.AddWithValue("@CandidateID", application.CandidateID);
                            Sqlcon1.Open();
                            SqlDataReader reader = sqlcmd.ExecuteReader();
                            while (reader.Read())
                            {
                                fileName = reader["ResumeFilePath"].ToString();

                            }
                            Sqlcon1.Close();

                        }
                    }
                }
                string uniqueCode = "APP-" + DateTime.Now.ToString("yyyy") + "-" + Guid.NewGuid().ToString().Substring(0, 6);

                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_ApplyForJob", Sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;

                        sqlcmd.Parameters.AddWithValue("@JobID", application.JobID);
                        sqlcmd.Parameters.AddWithValue("@CandidateID", application.CandidateID);
                        sqlcmd.Parameters.AddWithValue("@ApplicationCode", uniqueCode);
                        sqlcmd.Parameters.AddWithValue("@ApplicationDate", System.DateTime.Now);
                        //sqlcmd.Parameters.AddWithValue("@CoverLetter", application.CoverLetter ?? (object)DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@ResumePath", fileName ?? (object)DBNull.Value);

                        SqlParameter applicationIdParam = new SqlParameter("@ApplicationID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        sqlcmd.Parameters.Add(applicationIdParam);
                        try
                        {
                            Sqlcon.Open();
                            int rows = sqlcmd.ExecuteNonQuery();
                            int applicationID = (int)applicationIdParam.Value;
                            if (rows > 0)
                            {
                                Message = "Application submitted successfully. Note Your ApplicationID for Future Reference" + applicationID;
                                return Message;
                            }
                            else
                                Message = "Error in submission";
                            return Message;

                        }
                        catch (SqlException ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<JobApplicationResult> GetJobApplicationFromDB()
        {
            List<JobApplicationResult> resultList = new List<JobApplicationResult>();

            JobApplicationResult result = new JobApplicationResult();
            try
            {
                using (SqlConnection Sqlcon1 = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_GetAllAppliedJobs", Sqlcon1))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                      //  sqlcmd.Parameters.AddWithValue("@EmployeeID", EmployeeId);
                        sqlcmd.Parameters.AddWithValue("@Action", "Admin");
                        Sqlcon1.Open();
                        SqlDataReader reader = sqlcmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string firstname = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : null;
                            string lastname = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : null;
                            string fullname = firstname + lastname;
                         
                            resultList.Add(new JobApplicationResult
                            {
                                ApplicationCode = reader["ApplicationCode"] != DBNull.Value ? (reader["ApplicationCode"]).ToString() : null,
                                JobID = reader["JobID"] != DBNull.Value ? (int)reader["JobID"] : 0,
                                CandidateID = reader["CandidateID"] != DBNull.Value ? (int)reader["CandidateID"] : 0,
                                CandidateEmailAddress = reader["EmailAddress"] != DBNull.Value ? reader["EmailAddress"].ToString() : null,
                                CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                                HighestEducationLevel = reader["HighestEducationLevel"] != DBNull.Value ? reader["HighestEducationLevel"].ToString() : null,
                                JobTitle = reader["JobTitle"] != DBNull.Value ? reader["JobTitle"].ToString() : null,
                                ApplicationDate = reader["ApplicationDate"] != DBNull.Value ? (DateTime?)reader["ApplicationDate"] : null,
                                JobApplicationLastDate = reader["ApplicationDeadline"] != DBNull.Value ? (DateTime?)reader["ApplicationDeadline"] : null,
                                ApplicationStatus = reader["ApplicationStatus"] != DBNull.Value ? reader["ApplicationStatus"].ToString() : null,
                                ApplicationID = reader["ApplicationID"] != DBNull.Value ? (int)reader["ApplicationID"] : 0,
                                 CandidateName = fullname,
                                CandidatePhoneNumber = reader["MobileNumber"] != DBNull.Value ? reader["MobileNumber"].ToString() : null,
                                
                            });
                        }
                        Sqlcon1.Close();
                    }
                }
                return resultList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JobApplicationResult GetApplicationFromDB(string ApplicationID)
        {
            // List<JobApplicationResult> resultList = new List<JobApplicationResult>();

            JobApplicationResult result = new JobApplicationResult();
            try
            {
                using (SqlConnection Sqlcon1 = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_GetJobApplications", Sqlcon1))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.Parameters.AddWithValue("@ApplicationCode", ApplicationID);
                        sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                        Sqlcon1.Open();
                        SqlDataReader reader = sqlcmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string firstname = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : null;
                            string lastname = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : null;
                            result = new JobApplicationResult
                            {
                                ApplicationCode = reader["ApplicationCode"] != DBNull.Value ? (reader["ApplicationCode"]).ToString() : null,
                                JobID = reader["JobID"] != DBNull.Value ? (int)reader["JobID"] : 0,
                                CandidateID = reader["CandidateID"] != DBNull.Value ? (int)reader["CandidateID"] : 0,
                                CandidateName= firstname +" "+ lastname,
                                CandidateEmailAddress = reader["EmailAddress"] != DBNull.Value ? reader["EmailAddress"].ToString() : null,
                                CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                                HighestEducationLevel = reader["HighestEducationLevel"] != DBNull.Value ? reader["HighestEducationLevel"].ToString() : null,
                                JobTitle = reader["JobTitle"] != DBNull.Value ? reader["JobTitle"].ToString() : null,
                                Vacancy = Convert.ToInt32(reader["Vacancy"]),
                                Experience = reader["Experience"].ToString(),
                                Salary = reader["Salary"] != DBNull.Value ? Convert.ToDecimal(reader["Salary"]) : 0,
                                JobType = reader["JobType"] != DBNull.Value ? reader["JobType"].ToString() : null,
                                ApplicationDate = reader["ApplicationDate"] != DBNull.Value ? (DateTime?)reader["ApplicationDate"] : null,
                                JobApplicationLastDate = reader["ApplicationDeadline"] != DBNull.Value ? (DateTime?)reader["ApplicationDeadline"] : null
                            };
                        }
                        Sqlcon1.Close();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ViewJobs GetJobDetailsFromDBForCandidate(int JobId)
        {
            try
            {
                string Jobtype = "", JobLoaction = "";
                ViewJobs viewJobs = null;
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("SP_GetJobDetails", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCon.Open();
                        sqlCmd.Parameters.AddWithValue("@Action", "SELECT");
                        sqlCmd.Parameters.AddWithValue("@JobId", JobId);
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int jobtypeid = reader["JobType"] != DBNull.Value ? Convert.ToInt32(reader["JobType"]) : 0;
                                if (jobtypeid != 0)
                                {
                                    Jobtype = GetJobType(jobtypeid);
                                }
                                int locationid = reader["JobLocation"] != DBNull.Value ? Convert.ToInt32(reader["JobLocation"]) : 0;
                                if (locationid != 0)
                                {
                                    JobLoaction = GetJoblocation(locationid);
                                }

                                viewJobs = new ViewJobs
                                {
                                    //SLNo = Convert.ToInt32(reader["SLNo"]),
                                    //JobID = Convert.ToInt32(reader["JobID"]),
                                    //EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                    JobTitle = reader["JobTitle"].ToString(),
                                    JobDescription = reader["JobDescription"].ToString(),
                                    JobLocation = JobLoaction,
                                    JobType = Jobtype,
                                    CompanyName = reader["CompanyName"].ToString(),
                                    ContactEmail = reader["ContactPersonEmail"].ToString(),
                                    Vacancy = Convert.ToInt32(reader["Vacancy"]),
                                    Website = reader["WebsiteURL"].ToString(),
                                    Qualifications = reader["Qualifications"].ToString(),
                                    Experience = reader["Experience"].ToString(),
                                    Address = reader["PhysicalAddress"].ToString(),
                                    RequiredSkills = reader["RequiredSkills"] != DBNull.Value ? reader["RequiredSkills"].ToString() : null,
                                    Salary = reader["Salary"] != DBNull.Value ? Convert.ToDecimal(reader["Salary"]) : 0,
                                    ApplicationDeadline = reader["ApplicationDeadline"] != DBNull.Value ? (DateTime?)reader["ApplicationDeadline"] : null,
                                    //ApplicationStartDate = reader["ApplicationStartDate"] != DBNull.Value ? (DateTime?)reader["ApplicationStartDate"] : null
                                };
                                // viewJobsList.Add(viewJobs);
                            }

                        }
                        sqlCon.Close();
                        if (viewJobs != null)
                            return viewJobs;
                        else return viewJobs;


                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., logging)
                throw;
            }
        }
        public DataTable GetJobEditDetails(int requestID)
        {
            try
            {
                DataTable jobDetailsTable = new DataTable();
                string Jobtype = "", JobLoaction = "";
                ViewJobs viewJobs = null;
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("SP_GetJobDetailsWithChanges", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCon.Open();
                        // sqlCmd.Parameters.AddWithValue("@Action", "GetJob");
                        sqlCmd.Parameters.AddWithValue("@RequestID", requestID);
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                           
                                jobDetailsTable.Load(reader); // Load data into DataTable
                            }
                        //while (reader.Read())
                        //{
                        //    int jobtypeid = reader["JobType"] != DBNull.Value ? Convert.ToInt32(reader["JobType"]) : 0;
                        //    if (jobtypeid != 0)
                        //    {
                        //        Jobtype = GetJobType(jobtypeid);
                        //    }
                        //    int locationid = reader["JobLocation"] != DBNull.Value ? Convert.ToInt32(reader["JobLocation"]) : 0;
                        //    if (locationid != 0)
                        //    {
                        //        JobLoaction = GetJoblocation(locationid);
                        //    }

                        //    viewJobs = new ViewJobs
                        //    {
                        //HighlightChanges("lblJobTitle", reader["OriginalJobTitle"], reader["RequestedJobTitle"]);
                        //HighlightChanges("lblCompanyName", reader["OriginalCompanyName"], reader["RequestedCompanyName"]);
                        //HighlightChanges("lblJobLocation", reader["OriginalJobLocation"], reader["RequestedJobLocation"]);
                        //HighlightChanges("lblSalary", reader["OriginalSalary"], reader["RequestedSalary"]);
                        //HighlightChanges("lblJobType", reader["OriginalJobType"], reader["RequestedJobType"]);
                        //HighlightChanges("lblVacancy", reader["OriginalVacancy"], reader["RequestedVacancy"]);
                        //HighlightChanges("lblExperience", reader["OriginalExperience"], reader["RequestedExperience"]);
                        //HighlightChanges("lblApplicationDeadline", reader["OriginalApplicationDeadline"], reader["RequestedApplicationDeadline"]);

                        //HighlightChanges("lblApplicationStarts", reader["OriginalStatDate"], reader["RequestedStartDate"]);
                        //HighlightChanges("lblQualifications", reader["OriginalQualifications"], reader["RequestedQualifications"]);
                        //HighlightChanges("lblRequiredSkills", reader["OriginalRequiredSkills"], reader["RequestedRequiredSkills"]);
                        //HighlightChanges("lblJobDescription", reader["OriginalJobDescription"], reader["RequestedJobDescription"]);
                        //HighlightChanges("lblContactEmail", reader["OriginalContactEmail"], reader["RequestedContactEmail"]);
                        //HighlightChanges("lnkWebsite", reader["OriginalCompanyWebsite"], reader["RequestedCompanyWebsite"]);

                        //    //SLNo = Convert.ToInt32(reader["SLNo"]),
                        //    JobID = Convert.ToInt32(reader["JobID"]),
                        //    EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                        //    JobTitle = reader["JobTitle"].ToString(),
                        //    JobDescription = reader["JobDescription"].ToString(),
                        //    JobLocation = JobLoaction,
                        //    JobType = Jobtype,
                        //    CompanyName = reader["CompanyName"].ToString(),
                        //    ContactEmail = reader["ContactPersonEmail"].ToString(),
                        //    Vacancy = Convert.ToInt32(reader["Vacancy"]),
                        //    Website = reader["WebsiteURL"].ToString(),
                        //    Qualifications = reader["Qualifications"].ToString(),
                        //    Experience = reader["Experience"].ToString(),
                        //    Address = reader["PhysicalAddress"].ToString(),
                        //    RequiredSkills = reader["RequiredSkills"] != DBNull.Value ? reader["RequiredSkills"].ToString() : null,
                        //    Salary = reader["Salary"] != DBNull.Value ? Convert.ToDecimal(reader["Salary"]) : 0,
                        //    ApplicationDeadline = reader["ApplicationDeadline"] != DBNull.Value ? (DateTime?)reader["ApplicationDeadline"] : null,
                        //    ApplicationStartDate = reader["ApplicationStartDate"] != DBNull.Value ? (DateTime?)reader["ApplicationStartDate"] : null
                        //};
                        // viewJobsList.Add(viewJobs);
                        //};
                        return jobDetailsTable;
                            }
                            sqlCon.Close();
                            if (jobDetailsTable != null)
                                return jobDetailsTable;
                            else return jobDetailsTable;


                        }
                    
                
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., logging)
                throw;
            }
        }

    
    public ViewJobs GetJobDetailsFromDB(int JobId)
        {
            try
            {
                string Jobtype = "", JobLoaction = "";
                ViewJobs viewJobs = null;
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("SP_GetJobDetails", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCon.Open();
                        sqlCmd.Parameters.AddWithValue("@Action", "GetJob");
                        sqlCmd.Parameters.AddWithValue("@JobId", JobId);
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int jobtypeid = reader["JobType"] != DBNull.Value ? Convert.ToInt32(reader["JobType"]) : 0;
                                if (jobtypeid != 0)
                                {
                                    Jobtype = GetJobType(jobtypeid);
                                }
                                int locationid = reader["JobLocation"] != DBNull.Value ? Convert.ToInt32(reader["JobLocation"]) : 0;
                                if (locationid != 0)
                                {
                                    JobLoaction = GetJoblocation(locationid);
                                }

                                viewJobs = new ViewJobs
                                {
                                    //SLNo = Convert.ToInt32(reader["SLNo"]),
                                    JobID = Convert.ToInt32(reader["JobID"]),
                                    EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                    JobTitle = reader["JobTitle"].ToString(),
                                    JobDescription = reader["JobDescription"].ToString(),
                                    JobLocation = JobLoaction,
                                    JobType = Jobtype,
                                    CompanyName = reader["CompanyName"].ToString(),
                                    ContactEmail = reader["ContactPersonEmail"].ToString(),
                                    Vacancy = Convert.ToInt32(reader["Vacancy"]),
                                    Website = reader["WebsiteURL"].ToString(),
                                    Qualifications = reader["Qualifications"].ToString(),
                                    Experience = reader["Experience"].ToString(),
                                    Address = reader["PhysicalAddress"].ToString(),
                                    RequiredSkills = reader["RequiredSkills"] != DBNull.Value ? reader["RequiredSkills"].ToString() : null,
                                    Salary = reader["Salary"] != DBNull.Value ? Convert.ToDecimal(reader["Salary"]) : 0,
                                    ApplicationDeadline = reader["ApplicationDeadline"] != DBNull.Value ? (DateTime?)reader["ApplicationDeadline"] : null,
                                    ApplicationStartDate = reader["ApplicationStartDate"] != DBNull.Value ? (DateTime?)reader["ApplicationStartDate"] : null
                                };
                                // viewJobsList.Add(viewJobs);
                            }

                        }
                        sqlCon.Close();
                        if (viewJobs != null)
                            return viewJobs;
                        else return viewJobs;


                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., logging)
                throw;
            }
        }
        public List<int> GetAppliedJobs(int CandidateId)
        {
            // List<JobApplicationResult> resultList = new List<JobApplicationResult>();
            List<int> list = new List<int>(); int JobID = 0;
            JobApplicationResult result = new JobApplicationResult();
            try
            {
                using (SqlConnection Sqlcon1 = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("Select * From JobApplications where CandidateID=@CandidateID", Sqlcon1))
                    {
                        sqlcmd.CommandType = CommandType.Text;
                        sqlcmd.Parameters.AddWithValue("@CandidateID", CandidateId);
                        // sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                        Sqlcon1.Open();
                        SqlDataReader reader = sqlcmd.ExecuteReader();
                        while (reader.Read())
                        {


                            //ApplicationID = reader["ApplicationID"] != DBNull.Value ? (int)reader["ApplicationID"] : 0,
                            JobID = reader["JobID"] != DBNull.Value ? (int)reader["JobID"] : 0;
                            //CandidateID = reader["CandidateID"] != DBNull.Value ? (int)reader["CandidateID"] : 0,
                            //CandidateEmailAddress = reader["EmailAddress"] != DBNull.Value ? reader["EmailAddress"].ToString() : null,
                            //CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                            //HighestEducationLevel = reader["HighestEducationLevel"] != DBNull.Value ? reader["HighestEducationLevel"].ToString() : null,
                            //JobTitle = reader["JobTitle"] != DBNull.Value ? reader["JobTitle"].ToString() : null,
                            //ApplicationDate = reader["ApplicationDate"] != DBNull.Value ? (DateTime?)reader["ApplicationDate"] : null,
                            //JobApplicationLastDate = reader["ApplicationDeadline"] != DBNull.Value ? (DateTime?)reader["ApplicationDeadline"] : null
                            list.Add(JobID);
                        }
                        Sqlcon1.Close();
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ViewJobs> SearchJobsbyKeyword(string SearchText)
        {
            List<ViewJobs> resultList = new List<ViewJobs>();
            // List<int> list = new List<int>(); int JobID = 0;
            //  JobApplicationResult result = new JobApplicationResult();
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("SP_GetJobDetails", Sqlcon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        Sqlcon.Open();
                        sqlCmd.Parameters.AddWithValue("@Action", "GetJobByKeyword");
                        sqlCmd.Parameters.AddWithValue("@SearchText", SearchText);
                        // sqlcmd.Parameters.AddWithValue("@Action", "SELECT");

                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string JobLoaction = ""; string Jobtype = "";
                            int jobtypeid = reader["JobType"] != DBNull.Value ? Convert.ToInt32(reader["JobType"]) : 0;
                            if (jobtypeid != 0)
                            {
                                Jobtype = GetJobType(jobtypeid);
                            }
                            int locationid = reader["JobLocation"] != DBNull.Value ? Convert.ToInt32(reader["JobLocation"]) : 0;
                            if (jobtypeid != 0)
                            {
                                JobLoaction = GetJoblocation(locationid);
                            }


                            ViewJobs viewJobs = new ViewJobs
                            {
                                SLNo = Convert.ToInt32(reader["SLNo"]),
                                JobID = Convert.ToInt32(reader["JobID"]),
                                // EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                JobTitle = reader["JobTitle"].ToString(),
                                //JobDescription = reader["JobDescription"].ToString(),
                                JobLocation = JobLoaction,
                                JobType = Jobtype,
                                CompanyName = reader["CompanyName"].ToString(),

                                CompanyEmail = reader["CompanyEmailAddress"] != DBNull.Value ? reader["CompanyEmailAddress"].ToString() : null,
                                CompanyPhoneNumber = reader["CompanyPhoneNumber"] != DBNull.Value ? reader["CompanyPhoneNumber"].ToString() : null,

                                Vacancy = Convert.ToInt32(reader["Vacancy"]),
                                Website = reader["WebsiteURL"].ToString(),
                                // Qualifications = reader["Qualifications"].ToString(),
                                //Experience = reader["Experience"].ToString(),
                                //Address = reader["Address"].ToString(),
                                //RequiredSkills = reader["RequiredSkills"] != DBNull.Value ? reader["RequiredSkills"].ToString() : null,
                                Salary = reader["Salary"] != DBNull.Value ? Convert.ToDecimal(reader["Salary"]) : 0,
                                ApplicationDeadline = reader["ApplicationDeadline"] != DBNull.Value ? (DateTime?)reader["ApplicationDeadline"] : null,
                                //ApplicationStartDate = reader["ApplicationStartDate"] != DBNull.Value ? (DateTime?)reader["ApplicationStartDate"] : null
                                VerificationStatus = reader["JobStatus"].ToString() != null ? reader["JobStatus"].ToString() : null
                            };
                            resultList.Add(viewJobs);
                        }
                        Sqlcon.Close();
                    }
                }
                return resultList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ViewJobs> GetJobs(int? EmployeeId)
        {
             List<ViewJobs > resultList = new List<ViewJobs>();
           // List<int> list = new List<int>(); int JobID = 0;
          //  JobApplicationResult result = new JobApplicationResult();
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("SP_GetJobDetails",Sqlcon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        Sqlcon.Open();
                        sqlCmd.Parameters.AddWithValue("@Action", "GetJobByCompany");
                        sqlCmd.Parameters.AddWithValue("@CompanyID", EmployeeId);
                        // sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                        
                        SqlDataReader reader = sqlCmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string JobLoaction ="";string Jobtype = "";
                            int jobtypeid = reader["JobType"] != DBNull.Value ? Convert.ToInt32(reader["JobType"]) : 0;
                            if (jobtypeid != 0)
                            {
                                Jobtype = GetJobType(jobtypeid);
                            }
                            int locationid = reader["JobLocation"] != DBNull.Value ? Convert.ToInt32(reader["JobLocation"]) : 0;
                            if (jobtypeid != 0)
                            {
                                JobLoaction = GetJoblocation(locationid);
                            }


                            ViewJobs viewJobs = new ViewJobs {
                                SLNo = Convert.ToInt32(reader["SLNo"]),
                                JobID = Convert.ToInt32(reader["JobID"]),
                                // EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                JobTitle = reader["JobTitle"].ToString(),
                                //JobDescription = reader["JobDescription"].ToString(),
                                JobLocation = JobLoaction,
                                JobType = Jobtype,
                                CompanyName = reader["CompanyName"].ToString(),

                                CompanyEmail = reader["CompanyEmailAddress"] != DBNull.Value ? reader["CompanyEmailAddress"].ToString() : null,
                                CompanyPhoneNumber = reader["CompanyPhoneNumber"] != DBNull.Value ? reader["CompanyPhoneNumber"].ToString() : null,

                                Vacancy = Convert.ToInt32(reader["Vacancy"]),
                                Website = reader["WebsiteURL"].ToString(),
                                // Qualifications = reader["Qualifications"].ToString(),
                                //Experience = reader["Experience"].ToString(),
                                //Address = reader["Address"].ToString(),
                                //RequiredSkills = reader["RequiredSkills"] != DBNull.Value ? reader["RequiredSkills"].ToString() : null,
                                Salary = reader["Salary"] != DBNull.Value ? Convert.ToDecimal(reader["Salary"]) : 0,
                                ApplicationDeadline = reader["ApplicationDeadline"] != DBNull.Value ? (DateTime?)reader["ApplicationDeadline"] : null,
                                //ApplicationStartDate = reader["ApplicationStartDate"] != DBNull.Value ? (DateTime?)reader["ApplicationStartDate"] : null
                                VerificationStatus = reader["JobStatus"].ToString() != null ? reader["JobStatus"].ToString() : null
                            };
                            resultList.Add(viewJobs);
                        }
                        Sqlcon.Close();
                    }
                }
                return resultList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<JobSearchResult> GetAllAppliedJobs(int CandidateId)
        {
            List<JobSearchResult> joblist = new List<JobSearchResult>(); int JobID = 0; string JobLocation = "";
            JobSearchResult result = new JobSearchResult();
            try
            {
                using (SqlConnection Sqlcon1 = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_GetAllAppliedJobs", Sqlcon1))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.Parameters.AddWithValue("@CandidateID", CandidateId);
                        sqlcmd.Parameters.AddWithValue("@Action", "Candidate");
                        Sqlcon1.Open();
                        SqlDataReader reader = sqlcmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int locationid = reader["JobLocation"] != DBNull.Value ? Convert.ToInt32(reader["JobLocation"]) : 0;
                            if (locationid != 0)
                            {
                                JobLocation = GetJoblocation(locationid);
                            }

                            result = new JobSearchResult
                            {
                                //ApplicationID = reader["ApplicationID"] != DBNull.Value ? (int)reader["ApplicationID"] : 0,
                                JobID = reader["JobID"] != DBNull.Value ? (int)reader["JobID"] : 0,
                                ApplicationStatus = reader["ApplicationStatus"] != DBNull.Value ? reader["ApplicationStatus"].ToString() : null,
                                CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                                JobDescription = reader["JobDescription"] != DBNull.Value ? reader["JobDescription"].ToString() : null,
                                JobTitle = reader["JobTitle"] != DBNull.Value ? reader["JobTitle"].ToString() : null,
                                JobLocation = JobLocation,
                                Salary = reader["Salary"] != DBNull.Value ? (decimal?)reader["Salary"] : 0,
                                ApplicationDeadline = reader["ApplicationDeadline"] != DBNull.Value ? (DateTime?)reader["ApplicationDeadline"] : null
                            };
                            joblist.Add(result);
                        }
                        Sqlcon1.Close();
                    }
                }
                return joblist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<JobApplicationResult> GetAllAppliedJobsForEmployer(int EmployeeId)
        {
            List<JobApplicationResult> joblist = new List<JobApplicationResult>();
            int JobID = 0; string JobLocation = ""; string relativePath = "";
            JobApplicationResult result = new JobApplicationResult();
            try
            {
                using (SqlConnection Sqlcon1 = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_GetAllAppliedJobs", Sqlcon1))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.Parameters.AddWithValue("@EmployeeID", EmployeeId);
                        sqlcmd.Parameters.AddWithValue("@Action", "Employee");
                        Sqlcon1.Open();
                        SqlDataReader reader = sqlcmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string fileName = reader["ResumeFilePath"] != DBNull.Value ? reader["ResumeFilePath"].ToString() : null;
                            if (!string.IsNullOrEmpty(fileName))
                            {
                                // Assuming files are stored in a web-accessible directory like ~/uploads
                                relativePath = "~/uploads/" + fileName;
                            }
                            string firstname = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : null;
                            string lastname = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : null;
                            string fullname = firstname + lastname;
                            result = new JobApplicationResult
                            {
                                ApplicationID = reader["ApplicationID"] != DBNull.Value ? (int)reader["ApplicationID"] : 0,
                                ApplicationCode = reader["ApplicationCode"] != DBNull.Value ? reader["ApplicationCode"].ToString() : string.Empty,
                                JobTitle = reader["JobTitle"] != DBNull.Value ? reader["JobTitle"].ToString() : null,
                                JobID = reader["JobID"] != DBNull.Value ? (int)reader["JobID"] : 0,
                                CandidateID = reader["CandidateID"] != DBNull.Value ? (int)reader["CandidateID"] : 0,
                                ApplicationStatus = reader["ApplicationStatus"] != DBNull.Value ? reader["ApplicationStatus"].ToString() : null,
                                CandidateName = fullname,
                                CandidateEmailAddress = reader["EmailAddress"] != DBNull.Value ? reader["EmailAddress"].ToString() : null,
                                CandidatePhoneNumber = reader["MobileNumber"] != DBNull.Value ? reader["MobileNumber"].ToString() : null,
                                HighestEducationLevel = reader["HighestEducationLevel"] != DBNull.Value ? reader["HighestEducationLevel"].ToString() : null,
                                ApplicationDate = reader["ApplicationDate"] != DBNull.Value ? (DateTime?)reader["ApplicationDate"] : null,
                                ResumePath = relativePath,

                            };
                            joblist.Add(result);
                        }
                        Sqlcon1.Close();
                    }
                }
                return joblist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetApplicationStatus(string ApplicationID)
        {

            string query = "SELECT ApplicationStatus FROM JobApplications WHERE ApplicationCode = @ApplicationCode";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ApplicationCode", ApplicationID);
                    con.Open();
                    string status = cmd.ExecuteScalar()?.ToString();
                    con.Close();
                    return status;
                }
            }
        }
        public string GetApplicationStatusForAdmin(string ApplicationID)
        {

            string query = "SELECT AdminVerification FROM JobApplications WHERE ApplicationCode = @ApplicationCode";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ApplicationCode", ApplicationID);
                    con.Open();
                    string status = cmd.ExecuteScalar()?.ToString();
                    con.Close();
                    return status;
                }
            }
        }
        public string GetApprovalStatus(int jobId)
        {

            string query = "SELECT AdminVerify FROM JobPostings WHERE JobID = @JobID";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@JobID", jobId);
                    con.Open();
                    string status = cmd.ExecuteScalar()?.ToString();
                    con.Close();
                    return status;
                }
            }
        }
        public List<ViewJobs> GetAllJobsForAdmin()
        {
            try
            {
                string Jobtype = "", JobLoaction = "";
                List<ViewJobs> viewJobsList = new List<ViewJobs>();
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("SP_GetJobDetails", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCon.Open();
                        sqlCmd.Parameters.AddWithValue("@Action", "ADMIN");
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int jobtypeid = reader["JobType"] != DBNull.Value ? Convert.ToInt32(reader["JobType"]) : 0;
                                if (jobtypeid != 0)
                                {
                                    Jobtype = GetJobType(jobtypeid);
                                }
                                int locationid = reader["JobLocation"] != DBNull.Value ? Convert.ToInt32(reader["JobLocation"]) : 0;
                                if (jobtypeid != 0)
                                {
                                    JobLoaction = GetJoblocation(locationid);
                                }

                                ViewJobs viewJobs = new ViewJobs
                                {
                                    SLNo = Convert.ToInt32(reader["SLNo"]),
                                   JobID = Convert.ToInt32(reader["JobID"]),
                                    // EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                    JobTitle = reader["JobTitle"].ToString(),
                                    //JobDescription = reader["JobDescription"].ToString(),
                                    JobLocation = JobLoaction,
                                    JobType = Jobtype,
                                    CompanyName = reader["CompanyName"].ToString(),
                                   
                                    CompanyEmail = reader["CompanyEmailAddress"] != DBNull.Value ? reader["CompanyEmailAddress"].ToString() : null,
                                    CompanyPhoneNumber = reader["CompanyPhoneNumber"] != DBNull.Value ? reader["CompanyPhoneNumber"].ToString() : null,

                                    Vacancy = Convert.ToInt32(reader["Vacancy"]),
                                    Website = reader["WebsiteURL"].ToString(),
                                   // Qualifications = reader["Qualifications"].ToString(),
                                    //Experience = reader["Experience"].ToString(),
                                    //Address = reader["Address"].ToString(),
                                    //RequiredSkills = reader["RequiredSkills"] != DBNull.Value ? reader["RequiredSkills"].ToString() : null,
                                    Salary = reader["Salary"] != DBNull.Value ? Convert.ToDecimal(reader["Salary"]) : 0,
                                    ApplicationDeadline = reader["ApplicationDeadline"] != DBNull.Value ? (DateTime?)reader["ApplicationDeadline"] : null,
                                    //ApplicationStartDate = reader["ApplicationStartDate"] != DBNull.Value ? (DateTime?)reader["ApplicationStartDate"] : null
                                    VerificationStatus = reader["JobStatus"].ToString() != null ? reader["JobStatus"].ToString() : null
                                };
                                viewJobsList.Add(viewJobs);
                            }

                        }
                        sqlCon.Close();
                        return viewJobsList;


                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., logging)
                throw;
            }
        }
        public List<ViewJobs> GetAllJobsFromDB()
        {
            try
            {
                string Jobtype = "", JobLoaction = "";
                List<ViewJobs> viewJobsList = new List<ViewJobs>();
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("SP_GetJobDetails", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCon.Open();
                        sqlCmd.Parameters.AddWithValue("@Action", "ALL");
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int jobtypeid = reader["JobType"] != DBNull.Value ? Convert.ToInt32(reader["JobType"]) : 0;
                                if (jobtypeid != 0)
                                {
                                    Jobtype = GetJobType(jobtypeid);
                                }
                                int locationid = reader["JobLocation"] != DBNull.Value ? Convert.ToInt32(reader["JobLocation"]) : 0;
                                if (jobtypeid != 0)
                                {
                                    JobLoaction = GetJoblocation(locationid);
                                }

                                ViewJobs viewJobs = new ViewJobs
                                {
                                    //SLNo = Convert.ToInt32(reader["SLNo"]),
                                    JobID = Convert.ToInt32(reader["JobID"]),
                                    // EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                    JobTitle = reader["JobTitle"].ToString(),
                                    //JobDescription = reader["JobDescription"].ToString(),
                                    JobLocation = JobLoaction,
                                    //JobType = Jobtype,
                                    CompanyName = reader["CompanyName"].ToString(),
                                    //ContactEmail = reader["ContactEmail"].ToString(),
                                    //Vacancy = Convert.ToInt32(reader["Vacancy"]),
                                    //Website = reader["Website"].ToString(),
                                    //Qualifications = reader["Qualifications"].ToString(),
                                    //Experience = reader["Experience"].ToString(),
                                    //Address = reader["Address"].ToString(),
                                    RequiredSkills = reader["RequiredSkills"] != DBNull.Value ? reader["RequiredSkills"].ToString() : null,
                                    //Salary = reader["Salary"] != DBNull.Value ? Convert.ToDecimal(reader["Salary"]) : 0,
                                    ApplicationDeadline = reader["ApplicationDeadline"] != DBNull.Value ? (DateTime?)reader["ApplicationDeadline"] : null,
                                    //ApplicationStartDate = reader["ApplicationStartDate"] != DBNull.Value ? (DateTime?)reader["ApplicationStartDate"] : null
                                };
                                viewJobsList.Add(viewJobs);
                            }

                        }
                        sqlCon.Close();
                        return viewJobsList;


                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., logging)
                throw;
            }
        }
        public string VerifyJobApplication(string ApplicationId, int JobId, string RequestStatus, string Action)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_ApproveRejectJobApplication", Sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.Parameters.AddWithValue("@ApplicationCode", ApplicationId);
                        sqlcmd.Parameters.AddWithValue("@JobID", JobId);
                        sqlcmd.Parameters.AddWithValue("@RequestStatus", RequestStatus);
                        sqlcmd.Parameters.AddWithValue("@Action", Action);

                        try
                        {
                            Sqlcon.Open();
                            int status = sqlcmd.ExecuteNonQuery();
                            if (status == 1)
                            {
                                return "Success";
                            }
                            else
                            {
                                return "Failed to approve the job application";
                            }


                        }
                        catch (SqlException ex)
                        {
                            // Log the error message if necessary
                            string errorMessage = ex.Message;
                            return $"Error processing job edit request: {errorMessage}";
                        }
                        finally
                        {
                            Sqlcon.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<JobApplicationCount> GetApplicationCountsForAllJobs(int employerId)
        {
            List<JobApplicationCount> jobApplicationCounts = new List<JobApplicationCount>();
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                string query = @"
    SELECT jp.JobID, COUNT(*) AS ApplicationCount 
    FROM JobApplications ja
    INNER JOIN JobPostings jp ON ja.JobID = jp.JobID
    WHERE jp.EmployeeID = @EmployeeID AND ja.AdminVerification = 'APPROVED'
    GROUP BY jp.JobID";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@EmployeeID", employerId);
                sqlCon.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    jobApplicationCounts.Add(new JobApplicationCount
                    {
                        JobID = Convert.ToInt32(reader["JobID"]),
                        ApplicationCount = Convert.ToInt32(reader["ApplicationCount"])
                    });
                }
                sqlCon.Close();
            }
            return jobApplicationCounts;
        }
        public List<ViewJobs> GetJobEditRequestStatus(int EmployeeID)
        {
            try
            {
                string Jobtype = " ", JobLoaction = "";
                List<ViewJobs> viewJobsList = new List<ViewJobs>();
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(@"SELECT
                            ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS SLNo ,*  
                              FROM JobEditRequests where EmployeeID=@EmployeeID", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCon.Open();
                        sqlCmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                        // sqlCmd.Parameters.AddWithValue("@RequestStatus", "Approved");
                        using (SqlDataReader reader = sqlCmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                int jobtypeid = reader["NewJobType"] != DBNull.Value ? Convert.ToInt32(reader["NewJobType"]) : 0;
                                if (jobtypeid != 0)
                                {
                                    Jobtype = GetJobType(jobtypeid);
                                }


                                int locationid = reader["NewJobLocation"] != DBNull.Value ? Convert.ToInt32(reader["NewJobLocation"]) : 0;
                                if (locationid != 0)
                                {
                                    JobLoaction = GetJoblocation(locationid);
                                }

                                ViewJobs viewJobs = new ViewJobs
                                {
                                    SLNo = Convert.ToInt32(reader["SLNo"]),
                                    //RequestID = Convert.ToInt32(reader["RequestID"]),
                                    //EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                    JobID = Convert.ToInt32(reader["JobID"]),
                                    JobTitle = reader["NewJobTitle"].ToString(),
                                    JobDescription = reader["NewJobDescription"].ToString(),
                                    //JobLocation = JobLoaction,
                                    //JobType = Jobtype,
                                    //CompanyName = reader["NewCompanyName"].ToString(),
                                    //ContactEmail = reader["NewContactEmail"].ToString(),
                                    //Vacancy = Convert.ToInt32(reader["NewVacancy"]),
                                    //Website = reader["NewWebsite"].ToString(),
                                    //Qualifications = reader["NewQualifications"].ToString(),
                                    //Experience = reader["NewExperience"].ToString(),
                                    //Address = reader["NewAddress"].ToString(),
                                    //RequiredSkills = reader["NewRequiredSkills"].ToString(),
                                    //Salary = (decimal?)reader["NewSalary"],
                                    //ApplicationDeadline = reader["NewApplicationDeadline"] != DBNull.Value ? (DateTime?)reader["NewApplicationDeadline"] : null,
                                    //ApplicationStartDate = reader["NewApplicationStartDate"] != DBNull.Value ? (DateTime?)reader["NewApplicationStartDate"] : null,
                                    RequestStatus = reader["RequestStatus"].ToString(),
                                    //RequestDate = (DateTime)reader["RequestDate"]

                                };
                                viewJobsList.Add(viewJobs);
                            }

                        }

                    }
                    sqlCon.Close();
                    return viewJobsList;


                }

            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., logging)
                throw;
            }
        }

        public string ApproveJobPosting(int JobId)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_ApproveRejectJobsByAdmin", Sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        // sqlcmd.Parameters.AddWithValue("@ApplicationID", ApplicationId);
                        sqlcmd.Parameters.AddWithValue("@JobID", JobId);
                        sqlcmd.Parameters.AddWithValue("@Action", "APPROVED");
                        //SqlParameter statusParam = new SqlParameter("@Status", SqlDbType.VarChar, 20)
                        //{
                        //    Direction = ParameterDirection.Output
                        //};
                        //sqlcmd.Parameters.Add(statusParam);
                        try
                        {
                            Sqlcon.Open();
                            int status = sqlcmd.ExecuteNonQuery();
                            if (status == 1)
                            {
                                return " Success";
                            }
                            else
                            {
                                return " Failed";
                            }


                        }
                        catch (SqlException ex)
                        {
                            // Log the error message if necessary
                            string errorMessage = ex.Message;
                            return $"Error processing job edit request: {errorMessage}";
                        }
                        finally
                        {
                            Sqlcon.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string UpdateApplicationStatus(string AppCode, string status)
        {
            int result = 0;
            // string result = "Failed";
            //string connStr = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE JobApplications SET ApplicationStatus = @Status WHERE ApplicationCode = @ApplicationCode", conn))
                {
                    cmd.Parameters.AddWithValue("@Status", status == "ENABLED" ? "ENABLED" : "DISABLED");
                    cmd.Parameters.AddWithValue("@ApplicationCode", AppCode);

                    try
                    {
                        conn.Open();
                        result = cmd.ExecuteNonQuery();
                        if (result == 1)
                            return "Succesfully" + status + "";
                        else
                            return " Failed";

                    }
                    catch (Exception ex)
                    {
                        return "Error" + ex;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

        }
        public string UpdateJobStatus(int jobId, string status)
        {
            if (jobId <= 0)
            {
                return "Invalid JobID"; // Invalid JobID
            }
           
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE JobPostings SET JobStatus=@Status WHERE JobID=@JobID", conn))
                {
                    
                    string jobStatus = status.Equals("APPROVED", StringComparison.OrdinalIgnoreCase) ? "APPROVED" : "REJECTED";
                    cmd.Parameters.AddWithValue("@Status", jobStatus);
                    cmd.Parameters.AddWithValue("@JobID", jobId);

                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result == 1 ? "Success" : "Failed"; 
                    }
                    catch (SqlException sqlEx)
                    {
                       
                        return "SQL Error: " + sqlEx.Message; 
                    }
                    catch (Exception ex)
                    {
                      
                        return "Error: " + ex.Message; 
                    }
                }
            }
        }


        public string RejectJobPosting(int JobId)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_ApproveRejectJobsByAdmin", Sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        // sqlcmd.Parameters.AddWithValue("@ApplicationID", ApplicationId);
                        sqlcmd.Parameters.AddWithValue("@JobID", JobId);
                        sqlcmd.Parameters.AddWithValue("@Action", "REJECTED");
                        //SqlParameter statusParam = new SqlParameter("@Status", SqlDbType.VarChar, 20)
                        //{
                        //    Direction = ParameterDirection.Output
                        //};
                        //sqlcmd.Parameters.Add(statusParam);
                        try
                        {
                            Sqlcon.Open();
                            int status = sqlcmd.ExecuteNonQuery();
                            if (status == 1)
                            {
                                return " Successfully Rejected";
                            }
                            else
                            {
                                return " Failed ";
                            }


                        }
                        catch (SqlException ex)
                        {
                            // Log the error message if necessary
                            string errorMessage = ex.Message;
                            return $"Error processing rejecting: {errorMessage}";
                        }
                        finally
                        {
                            Sqlcon.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DisableJobApplication(string ApplicationId)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_ApproveRejectJobsByAdmin", Sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                         sqlcmd.Parameters.AddWithValue("@ApplicationCode", ApplicationId);
                       // sqlcmd.Parameters.AddWithValue("@JobID", JobId);
                        sqlcmd.Parameters.AddWithValue("@Action", "DISABLED");
                        //SqlParameter statusParam = new SqlParameter("@Status", SqlDbType.VarChar, 20)
                        //{
                        //    Direction = ParameterDirection.Output
                        //};
                        //sqlcmd.Parameters.Add(statusParam);
                        try
                        {
                            Sqlcon.Open();
                            int status = sqlcmd.ExecuteNonQuery();
                            if (status == 1)
                            {
                                return " Success";
                            }
                            else
                            {
                                return " Failed ";
                            }


                        }
                        catch (SqlException ex)
                        {
                            // Log the error message if necessary
                            string errorMessage = ex.Message;
                            return $"Error processing : {errorMessage}";
                        }
                        finally
                        {
                            Sqlcon.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string RequestResetPassword(string token, string EmailAddress, DateTime tokenExpiry, string usertype)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("SP_SavePasswordResetToken", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@Email", EmailAddress);
                        sqlCmd.Parameters.AddWithValue("@Token", token);
                        sqlCmd.Parameters.AddWithValue("@TokenExpiry", tokenExpiry);
                        sqlCmd.Parameters.AddWithValue("@UserType", usertype);
                        SqlParameter isValidEmailParam = new SqlParameter("@IsValidEmail", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        sqlCmd.Parameters.Add(isValidEmailParam);

                        sqlCon.Open();
                        sqlCmd.ExecuteNonQuery();

                        bool isValidEmail = (bool)isValidEmailParam.Value;
                        sqlCon.Close();
                        if (isValidEmail)
                        {
                            return "Password reset email sent.";
                        }
                        else
                        {
                            return "Invalid email address.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ResetPassword(string newPassword, string token, string usertype)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("SP_ResetPassword", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        // sqlCmd.Parameters.AddWithValue("@UserType", usertype);
                        sqlCmd.Parameters.AddWithValue("@Token", token);
                        sqlCmd.Parameters.AddWithValue("@NewPassword", newPassword);

                        SqlParameter isPasswordResetParam = new SqlParameter("@IsPasswordReset", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        sqlCmd.Parameters.Add(isPasswordResetParam);

                        sqlCon.Open();
                        sqlCmd.ExecuteNonQuery();

                        bool isPasswordReset = (bool)isPasswordResetParam.Value;

                        if (isPasswordReset)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool GetValidToken(string token)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    string query = "SELECT COUNT(*) FROM PasswordResetTokens WHERE Token=@Token AND TokenExpiry > GETDATE()";
                    using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                    {
                        Sqlcon.Open();
                        sqlcmd.CommandType = CommandType.Text;
                        sqlcmd.Parameters.AddWithValue("@Token", token);
                        int rows = (int)sqlcmd.ExecuteScalar();
                        Sqlcon.Close();
                        if (rows > 0)

                            return true;
                        else
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool SaveJobAlert(int candidateId, string jobTitle, int location, string requiredskills, decimal? minsalary, decimal? maxsalary, string frequency)
        {
            try
            {
                // Assuming you have a database connection set up
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_SaveJobAlert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CandidateID", candidateId);
                    cmd.Parameters.AddWithValue("@Keyword", jobTitle);
                    cmd.Parameters.AddWithValue("@Location", location);
                    cmd.Parameters.AddWithValue("@MinSalary", minsalary.HasValue ? (object)minsalary.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@MaxSalary", maxsalary.HasValue ? (object)maxsalary.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@RequiredSkills", requiredskills);
                    cmd.Parameters.AddWithValue("@AlertFrequency", frequency);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it
                return false;
            }
        }

        public List<ViewJobs> GetJobNotifications(int candidateID)
        {
            try
            {
                string Jobtype = "", JobLoaction = "";
                List<ViewJobs> viewJobsList = new List<ViewJobs>();
                using (SqlConnection con = new SqlConnection(_connectionString))
                {

                    using (SqlCommand cmd = new SqlCommand("SP_GetNotifications", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CandidateID", candidateID);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            //int jobtypeid = reader["JobType"] != DBNull.Value ? Convert.ToInt32(reader["JobType"]) : 0;
                            //if (jobtypeid != 0)
                            //{
                            //    Jobtype = GetJobType(jobtypeid);
                            //}
                            //int locationid = reader["Location"] != DBNull.Value ? Convert.ToInt32(reader["JobLocation"]) : 0;
                            //if (jobtypeid != 0)
                            //{
                            //    JobLoaction = GetJoblocation(locationid);
                            //}

                            ViewJobs viewJobs = new ViewJobs
                            {
                                //SLNo = Convert.ToInt32(reader["SLNo"]),
                                JobID = Convert.ToInt32(reader["JobID"]),
                                // EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                JobTitle = reader["JobTitle"].ToString(),
                                //JobDescription = reader["JobDescription"].ToString(),
                                JobLocation = reader["Location"] != DBNull.Value ? reader["Location"].ToString() : null,
                                //JobType = Jobtype,
                                CompanyName = reader["CompanyName"].ToString(),
                                ////ContactEmail = reader["ContactEmail"].ToString(),
                                ////Vacancy = Convert.ToInt32(reader["Vacancy"]),
                                ////Website = reader["Website"].ToString(),
                                ////Qualifications = reader["Qualifications"].ToString(),
                                ////Experience = reader["Experience"].ToString(),
                                ////Address = reader["Address"].ToString(),
                                //RequiredSkills = reader["RequiredSkills"] != DBNull.Value ? reader["RequiredSkills"].ToString() : null,
                                Salary = reader["Salary"] != DBNull.Value ? Convert.ToDecimal(reader["Salary"]) : 0,
                                //ApplicationDeadline = reader["ApplicationDeadline"] != DBNull.Value ? (DateTime?)reader["ApplicationDeadline"] : null,
                                //ApplicationStartDate = reader["ApplicationStartDate"] != DBNull.Value ? (DateTime?)reader["ApplicationStartDate"] : null
                            };
                            viewJobsList.Add(viewJobs);
                        }
                        con.Close();
                        return viewJobsList;

                    }
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public List<CandidateDetails> GetFilteredCandidates(string jobTitle, string location, string skills, string experience, string education)
        {
            List<CandidateDetails> candidates = new List<CandidateDetails>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetFilteredCandidates", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@JobTitle", !string.IsNullOrEmpty(jobTitle) ? jobTitle : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Location", !string.IsNullOrEmpty(location) ? location : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Skills", !string.IsNullOrEmpty(skills) ? skills : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Experience", !string.IsNullOrEmpty(experience) ? experience : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Education", !string.IsNullOrEmpty(education) ? education : (object)DBNull.Value);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CandidateDetails candidate = new CandidateDetails
                            {
                                CandidateId = Convert.ToInt32(reader["CandidateID"]),
                                CandidateName = reader["FullName"].ToString(),
                                //JobTitle = reader["JobTitle"].ToString(),
                                City = reader["Location"].ToString(),
                                //Skill = reader["Skills"].ToString(),
                                //Experience = Convert.ToInt32(reader["Experience"]),
                                //EducationLevel = reader["EducationLevel"].ToString(),
                                //LastUpdated = Convert.ToDateTime(reader["LastUpdated"])
                            };
                            candidates.Add(candidate);
                        }
                    }
                }
            }

            return candidates;
        }
        public List<JobSearchResult> GetFilteredJobs(string jobTitle, string location, string skills, string experience, string education, string sortBy, string sortOrder)
        {
            try
            {
                List<JobSearchResult> jobresults = new List<JobSearchResult>();
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlcmd = new SqlCommand("SP_GetFilteredJobs", Sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.StoredProcedure;

                        sqlcmd.Parameters.AddWithValue("@JobTitle", (object)jobTitle ?? DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@Location", (object)location ?? DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@Skills", (object)skills ?? DBNull.Value);
                        //sqlcmd.Parameters.AddWithValue("@Experience", !string.IsNullOrEmpty(experience) && experience != ""  ? experience : null);
                        sqlcmd.Parameters.AddWithValue("@EducationLevel", (object)education ?? DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@SortBy", (object)sortBy ?? DBNull.Value);
                        sqlcmd.Parameters.AddWithValue("@SortOrder", (object)sortOrder ?? DBNull.Value);

                        Sqlcon.Open();

                        using (SqlDataReader reader = sqlcmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                JobSearchResult result = new JobSearchResult
                                {
                                    JobID = Convert.ToInt32(reader["JobID"]),
                                    JobTitle = reader["JobTitle"].ToString(),
                                    JobDescription = reader["JobDescription"].ToString(),
                                    JobLocation = reader["Location"].ToString(),
                                    CompanyName = reader["CompanyName"].ToString(),
                                    Qualifications = reader["Qualifications"].ToString(),
                                    Experience = reader["Experience"].ToString(),
                                    RequiredSkills = reader["RequiredSkills"].ToString(),
                                    Salary = reader.GetDecimal(reader.GetOrdinal("Salary"))
                                };
                                jobresults.Add(result);
                            }
                        }
                        Sqlcon.Close();
                    }
                }


                return jobresults;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Insert_TrainingPrograms(TrainingPgmRequest program, int TrainerId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO TrainingPrograms (TrainerID,ProgramName, Description, StartDate, EndDate, DeliveryMode, Price, CreatedDate, ModifiedDate) " +
                               "VALUES (@TrainerID,@ProgramName, @Description, @StartDate, @EndDate, @DeliveryMode, @Price, @CreatedDate, @ModifiedDate)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TrainerID", TrainerId);
                    cmd.Parameters.AddWithValue("@ProgramName", program.ProgramName);
                    cmd.Parameters.AddWithValue("@Description", program.Description);
                    cmd.Parameters.AddWithValue("@StartDate", program.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", program.EndDate);
                    cmd.Parameters.AddWithValue("@DeliveryMode", program.DeliveryMode);
                    cmd.Parameters.AddWithValue("@Price", program.Price);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        return " Successfully inserted";
                    }
                    else return " error in inserting";

                }
            }

        }
        public bool SendMessages(int employerID, int candidateID, string messageBody)
        {
            try
            {
                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {

                    string query = "INSERT INTO Messages (SenderID, ReceiverID, MessageContent) " +
                           "VALUES (@SenderID, @ReceiverID, @MessageContent)";
                    SqlCommand cmd = new SqlCommand(query, Sqlcon);
                    cmd.Parameters.AddWithValue("@SenderID", employerID);
                    cmd.Parameters.AddWithValue("@ReceiverID", candidateID);
                    //   cmd.Parameters.AddWithValue("@JobID", JobId);
                    cmd.Parameters.AddWithValue("@MessageContent", messageBody);
                    Sqlcon.Open();
                    int rows = cmd.ExecuteNonQuery();
                    Sqlcon.Close();
                    if (rows > 0)
                    {
                        return true;
                    }
                    else return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable LoadConversation(int receiverId, int userId)
        {
            try
            {
                string query = "SELECT m.MessageContent, m.SenderID, " +
                    "  CASE WHEN m.SenderID = e.CompanyID THEN e.CompanyName" +
                    "   WHEN m.SenderID = c.CandidateID THEN c.FirstName + ' ' + c.LastName" +
                    "  ELSE 'Unknown'" +
                    "       END AS SenderName, " +
                    " m.SentDate FROM Messages m LEFT JOIN Employer e ON m.SenderID = e.CompanyID " +
                    "LEFT JOIN Candidate c ON m.SenderID = c.CandidateID " +
                    "WHERE(m.SenderID = @CurrentUser AND m.ReceiverID = @SelectedUser) " +
                    "   OR(m.SenderID = @SelectedUser AND m.ReceiverID = @CurrentUser) " +
                    "ORDER BY m.SentDate;";


                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@CurrentUser", userId);
                    cmd.Parameters.AddWithValue("@SelectedUser", receiverId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable LoadAllMessages(int userId)
        {
            try
            {
                string query = "SELECT DISTINCT m.SenderID," +
                    "CASE  WHEN m.SenderID = e.CompanyID THEN e.CompanyName" +
                    " WHEN m.SenderID = c.CandidateID THEN c.FirstName + ' ' + c.LastName   ELSE 'Unknown'" +
                    "    END AS SenderName FROM Messages m " +
                    "LEFT JOIN Employer e ON m.SenderID = e.CompanyID " +
                    "LEFT JOIN Candidate c ON m.SenderID = c.CandidateID " +
                    "LEFT JOIN JobPostings jp ON m.JobID = jp.JobID " +
                    "WHERE m.ReceiverID = @UserID OR m.SenderID = @UserID;";



                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetReceivedMessages(int userId)
        {
            try
            {
                string query = "SELECT m.MessageID, e.CompanyName,c.FirstName + ' ' + c.LastName AS CandidateName, m.MessageContent, m.SentDate, m.SenderID," +
                    " CASE WHEN m.SenderID = @CompanyID THEN e.CompanyName  ELSE c.FirstName + ' ' + c.LastName END AS SenderName " +
                    "FROM Messages m LEFT JOIN Employer e ON m.SenderID = e.CompanyID OR m.ReceiverID = e.CompanyID  " +
                    "LEFT JOIN Candidate c ON m.SenderID = c.CandidateID OR m.ReceiverID = c.CandidateID " +
                    "WHERE(m.SenderID = @CandidateID AND m.ReceiverID = @CompanyID) " +
                    "   OR(m.SenderID = @EmployerID AND m.ReceiverID = @CandidateID) " +
                    "ORDER BY m.SentDate ASC;";


                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public bool SendMessages(int SenderID, int ReceiverID,string messageBody )
        //{


        //    // Insert the new reply into the database
        //    string insertQuery = "INSERT INTO Messages (SenderID, ReceiverID, MessageContent, SentDate, IsRead) " +
        //                         "VALUES (@SenderID, @ReceiverID, @MessageContent, @SentDate, 0)";
        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand(insertQuery, connection);
        //        cmd.Parameters.AddWithValue("@SenderID", SenderID);
        //        cmd.Parameters.AddWithValue("@ReceiverID", ReceiverID);
        //        cmd.Parameters.AddWithValue("@MessageContent", messageBody);
        //        cmd.Parameters.AddWithValue("@SentDate", DateTime.Now);
        //        connection.Open();
        //        icmd.ExecuteNonQuery();
        //    }

        //    // Reload the conversation after the reply is sent

        //}
        public DataTable GetSentMessages(int userId)
        {
            try
            {
                string query = " SELECT  m.MessageID, e.CompanyName, c.FirstName + ' ' + c.LastName AS CandidateName,jp.JobTitle," +
                    "m.MessageContent,m.SentDate, m.IsRead FROM   Messages m LEFT JOIN  Employer e ON m.ReceiverID = e.CompanyID LEFT JOIN  Candidate c ON m.SenderID = c.CandidateID " +
                    "LEFT JOIN  JobPostings jp ON m.JobID = jp.JobID WHERE m.SenderID = @SenderID ORDER BY m.SentDate DESC;";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@SenderID", userId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<Skill> GetCoreSkillId(int candidateId)
        {
            List<Skill> skilllist = new List<Skill>();
            using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
            {
                Sqlcon.Open();

                string query = "Select CoreSkill from CandidateSkills where CandidateID=@CandidateID";
                using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                {
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@CandidateID", candidateId);
                    //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int coreskillid = reader["CoreSkill"] != DBNull.Value ? (int)reader["CoreSkill"] : 0;
                            string coreskill = GetSkillById(coreskillid, "CoreSkills");
                            int softskillid = reader["SoftSkill"] != DBNull.Value ? (int)reader["SoftSkill"] : 0;
                            string SoftSkills = GetSkillById(softskillid, "SoftSkills");
                            Skill skills = new Skill
                            {

                                CoreSkills = coreskill,
                                SoftSkills = SoftSkills,

                                CoreSkillPercentage = reader["CoreSkillPercentage"] != DBNull.Value ? reader["CoreSkillPercentage"].ToString() : null,
                                SoftSkillpercentage = reader["SoftSkillpercentage"] != DBNull.Value ? reader["SoftSkillpercentage"].ToString() : null,
                            };
                            skilllist.Add(skills);
                        }
                    }
                    else
                        return skilllist;
                }
                return skilllist;
            }
        }
        public List<TrainerResponse> SearchTrainers(string searchText)
        {
            List<TrainerResponse> employers = new List<TrainerResponse>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Create the SQL query
                string query = @"SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS SLNO , * FROM Trainer 
                         WHERE CompanyName LIKE @searchText 
                         OR CompanyPhoneNumber LIKE @searchText 
                         OR CompanyEmailAddress LIKE @searchText";
                ///*  OR WebsiteURL LIKE @searchText*/";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Use '%' to allow searching for substrings
                    command.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Assuming EmployerResponse has these properties
                            var employer = new TrainerResponse
                            {
                                SLNO = Convert.ToInt32(reader["SLNO"]),
                                TrainerId = reader.GetInt32(reader.GetOrdinal("TrainerID")),
                                CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                                CompanyRegistrationNumber = reader["CompanyRegistrationNumber"] != DBNull.Value ? reader["CompanyRegistrationNumber"].ToString() : null,
                                CompanyEmail = reader["CompanyEmailAddress"] != DBNull.Value ? reader["CompanyEmailAddress"].ToString() : null,
                                CompanyPhoneNumber = reader["CompanyPhoneNumber"] != DBNull.Value ? reader["CompanyPhoneNumber"].ToString() : null,
                                CompanyWebsiteUrl = reader["WebsiteUrl"] != DBNull.Value ? reader["WebsiteUrl"].ToString() : null,
                                PhysicalAddress = reader["PhysicalAddress"] != DBNull.Value ? reader["PhysicalAddress"].ToString() : null,
                                CompanyDescription = reader["CompanyDescription"] != DBNull.Value ? reader["CompanyDescription"].ToString() : null,
                                //IndustryType = reader["IndustryType"] != DBNull.Value ? reader["IndustryType"].ToString() : null,
                                CompanySize = reader["CompanySize"] != DBNull.Value ? reader["CompanySize"].ToString() : null,
                                ContactPersonName = reader["ContactPersonName"] != DBNull.Value ? reader["ContactPersonName"].ToString() : null,
                                ContactPersonEmail = reader["ContactPersonEmail"] != DBNull.Value ? reader["ContactPersonEmail"].ToString() : null,
                                ContactPersonPhoneNumber = reader["ContactPersonPhoneNumber"] != DBNull.Value ? reader["ContactPersonPhoneNumber"].ToString() : null,


                                // Map other properties as necessary
                            };
                            employers.Add(employer);
                        }
                    }
                }
            }

            return employers;
        }
        public List<EmployerResponse> SearchEmployees(string searchText)
        {
            List<EmployerResponse> employers = new List<EmployerResponse>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Create the SQL query
                string query = @"SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS SLNO , * FROM Employer 
                         WHERE CompanyName LIKE @searchText 
                         OR CompanyPhoneNumber LIKE @searchText 
                         OR CompanyEmailAddress LIKE @searchText"; 
                       ///*  OR WebsiteURL LIKE @searchText*/";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Use '%' to allow searching for substrings
                    command.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Assuming EmployerResponse has these properties
                            var employer = new EmployerResponse
                            {
                                SLNO = Convert.ToInt32(reader["SLNO"]),
                                EmployeeId = reader.GetInt32(reader.GetOrdinal("CompanyID")),
                                CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                                CompanyRegistrationNumber = reader["CompanyRegistrationNumber"] != DBNull.Value ? reader["CompanyRegistrationNumber"].ToString() : null,
                                CompanyEmail = reader["CompanyEmailAddress"] != DBNull.Value ? reader["CompanyEmailAddress"].ToString() : null,
                                CompanyPhoneNumber = reader["CompanyPhoneNumber"] != DBNull.Value ? reader["CompanyPhoneNumber"].ToString() : null,
                                CompanyWebsiteUrl = reader["WebsiteUrl"] != DBNull.Value ? reader["WebsiteUrl"].ToString() : null,
                                PhysicalAddress = reader["PhysicalAddress"] != DBNull.Value ? reader["PhysicalAddress"].ToString() : null,
                                CompanyDescription = reader["CompanyDescription"] != DBNull.Value ? reader["CompanyDescription"].ToString() : null,
                                //IndustryType = reader["IndustryType"] != DBNull.Value ? reader["IndustryType"].ToString() : null,
                                CompanySize = reader["CompanySize"] != DBNull.Value ? reader["CompanySize"].ToString() : null,
                                ContactPersonName = reader["ContactPersonName"] != DBNull.Value ? reader["ContactPersonName"].ToString() : null,
                                ContactPersonEmail = reader["ContactPersonEmail"] != DBNull.Value ? reader["ContactPersonEmail"].ToString() : null,
                                ContactPersonPhoneNumber = reader["ContactPersonPhoneNumber"] != DBNull.Value ? reader["ContactPersonPhoneNumber"].ToString() : null,


                                // Map other properties as necessary
                            };
                            employers.Add(employer);
                        }
                    }
                }
            }

            return employers;
        }
        //public List<EmployerResponse> SearchEmployees(string searchText)
        //{
        //    List<EmployerResponse> employers = new List<EmployerResponse>();

        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        connection.Open();

        //        // Create the SQL query
        //        string query = @"SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS SLNO , * FROM Employer 
        //                 WHERE CompanyName LIKE @searchText 
        //                 OR CompanyPhoneNumber LIKE @searchText 
        //                 OR CompanyEmailAddress LIKE @searchText";
        //        ///*  OR WebsiteURL LIKE @searchText*/";

        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            // Use '%' to allow searching for substrings
        //            command.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    // Assuming EmployerResponse has these properties
        //                    var employer = new EmployerResponse
        //                    {
        //                        SLNO = Convert.ToInt32(reader["SLNO"]),
        //                        EmployeeId = reader.GetInt32(reader.GetOrdinal("CompanyID")),
        //                        CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
        //                        CompanyRegistrationNumber = reader["CompanyRegistrationNumber"] != DBNull.Value ? reader["CompanyRegistrationNumber"].ToString() : null,
        //                        CompanyEmail = reader["CompanyEmailAddress"] != DBNull.Value ? reader["CompanyEmailAddress"].ToString() : null,
        //                        CompanyPhoneNumber = reader["CompanyPhoneNumber"] != DBNull.Value ? reader["CompanyPhoneNumber"].ToString() : null,
        //                        CompanyWebsiteUrl = reader["WebsiteUrl"] != DBNull.Value ? reader["WebsiteUrl"].ToString() : null,
        //                        PhysicalAddress = reader["PhysicalAddress"] != DBNull.Value ? reader["PhysicalAddress"].ToString() : null,
        //                        CompanyDescription = reader["CompanyDescription"] != DBNull.Value ? reader["CompanyDescription"].ToString() : null,
        //                        //IndustryType = reader["IndustryType"] != DBNull.Value ? reader["IndustryType"].ToString() : null,
        //                        CompanySize = reader["CompanySize"] != DBNull.Value ? reader["CompanySize"].ToString() : null,
        //                        ContactPersonName = reader["ContactPersonName"] != DBNull.Value ? reader["ContactPersonName"].ToString() : null,
        //                        ContactPersonEmail = reader["ContactPersonEmail"] != DBNull.Value ? reader["ContactPersonEmail"].ToString() : null,
        //                        ContactPersonPhoneNumber = reader["ContactPersonPhoneNumber"] != DBNull.Value ? reader["ContactPersonPhoneNumber"].ToString() : null,


        //                        // Map other properties as necessary
        //                    };
        //                    employers.Add(employer);
        //                }
        //            }
        //        }
        //    }

        //    return employers;
        //}

        public List<CandidateDetails> SearchCandidates(string searchText)
        {
            List<CandidateDetails> employers = new List<CandidateDetails>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Create the SQL query
                string query = @"SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS SLNO , * FROM Candidate 
                          WHERE FirstName LIKE @searchText 
            OR LastName LIKE @searchText 
            OR MobileNumber LIKE @searchText 
            OR EmailAddress LIKE @searchText";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Use '%' to allow searching for substrings
                    command.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Assuming EmployerResponse has these properties
                            var employer = new CandidateDetails
                            {
                                SLNO = reader["SLNO"] != DBNull.Value ? Convert.ToInt32(reader["SLNO"]) : 0,
                                CandidateId = reader["CandidateID"] != DBNull.Value ? Convert.ToInt32(reader["CandidateID"]) : 0,
                                ///PlanId = reader["PlanId"] != DBNull.Value ? (int)reader["PlanId"] : 0,
                                FirstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : null,
                                LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : null,
                                EmailAddress = reader["EmailAddress"] != DBNull.Value ? reader["EmailAddress"].ToString() : null,
                                MobileNumber = reader["MobileNumber"] != DBNull.Value ? reader["MobileNumber"].ToString() : null,
                                Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : null,
                                City = reader["City"] != DBNull.Value ? reader["City"].ToString() : null,
                                StateOrProvince = reader["State/Province"] != DBNull.Value ? reader["State/Province"].ToString() : null,
                                PostalOrZipCode = reader["Postal/ZipCode"] != DBNull.Value ? reader["Postal/ZipCode"].ToString() : null,
                                HighestEducationLevel = reader["HighestEducationLevel"] != DBNull.Value ? reader["HighestEducationLevel"].ToString() : null,

                            };
                            employers.Add(employer);
                        }
                    }
                }
            }

            return employers;
        }
        public List<Companies> GetAllCompany()
        {
            try
            {
                List<Companies> companiesList = new List<Companies>();

                using (SqlConnection Sqlcon = new SqlConnection(_connectionString))
                {
                    Sqlcon.Open();
                    string CompanyName = "";
                    string query = "Select CompanyID,CompanyName from Employer ";
                    using (SqlCommand sqlcmd = new SqlCommand(query, Sqlcon))
                    {
                        sqlcmd.CommandType = CommandType.Text;
                        // sqlcmd.Parameters.AddWithValue("@CountryID", countryid);
                        //sqlcmd.Parameters.AddWithValue("@Action", "SELECT");
                        SqlDataReader reader = sqlcmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Companies companies = new Companies();
                                companies.CompanyName = reader["CompanyName"]!=null? reader["CompanyName"].ToString(): null;
                                companies.CompanyID = (int)reader["CompanyID"];
                               
                                companiesList.Add(companies);
                            }

                        }
                    }
                    return companiesList;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}