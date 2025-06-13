using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Rv_WebAPI.Models;
using Rv_WebAPI.Utility;
using System.Data;

namespace Rv_WebAPI.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetAllEmployees")]
        public ActionResult GetAllEmployees()
        {
            List<EmployeeModel> response = new List<EmployeeModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_GetAllEmployees", con) { CommandType = CommandType.StoredProcedure };
            con.Open();
            using (SqlDataReader sqlDtaReader = cmd.ExecuteReader())
            {
                while (sqlDtaReader.Read())
                {
                    EmployeeModel employeeModel = new EmployeeModel();
                    employeeModel.EmployeeId = Convert.ToInt32(sqlDtaReader["EmployeeId"]);
                    employeeModel.FirstName = sqlDtaReader["FirstName"].ToString();
                    employeeModel.LastName = sqlDtaReader["LastName"].ToString();
                    employeeModel.Email = sqlDtaReader["Email"].ToString();
                    employeeModel.PhoneNumber = sqlDtaReader["PhoneNumber"].ToString();
                    employeeModel.DateOfBirth = sqlDtaReader["DateOfBirth"] as DateTime?;
                    employeeModel.JoiningDate = sqlDtaReader["JoiningDate"] as DateTime?;
                    employeeModel.DepartmentId = Convert.ToInt32(sqlDtaReader["DepartmentId"]);
                    employeeModel.DepartmentName = sqlDtaReader["DepartmentName"].ToString();
                    employeeModel.Gender = sqlDtaReader["Gender"].ToString();
                    employeeModel.Skills = sqlDtaReader["Skills"].ToString();
                    employeeModel.ResumePath = sqlDtaReader["ResumePath"].ToString();
                    employeeModel.ProfileImage = sqlDtaReader["ProfileImage"].ToString();
                    employeeModel.IsActive = Convert.ToBoolean(sqlDtaReader["IsActive"]);
                    employeeModel.Salary = Convert.ToDecimal(sqlDtaReader["Salary"]);
                    employeeModel.ExperienceYears = Convert.ToSingle(sqlDtaReader["ExperienceYears"]);
                    employeeModel.PasswordHash = sqlDtaReader["PasswordHash"].ToString();
                    employeeModel.Bio = sqlDtaReader["Bio"].ToString();
                    response.Add(employeeModel);
                }
            }
            con.Close();
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Employee retrieved successfully.", Data = response });
        }

        [HttpPost("AddEmployee")]
        public ActionResult AddEmployee(EmployeeModel employeeModel)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_AddEmployee", con) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@FirstName", employeeModel.FirstName);
            cmd.Parameters.AddWithValue("@LastName", employeeModel.LastName);
            cmd.Parameters.AddWithValue("@Email", employeeModel.Email);
            cmd.Parameters.AddWithValue("@PhoneNumber", employeeModel.PhoneNumber);
            cmd.Parameters.AddWithValue("@DateOfBirth", employeeModel.DateOfBirth);
            cmd.Parameters.AddWithValue("@JoiningDate", employeeModel.JoiningDate);
            cmd.Parameters.AddWithValue("@DepartmentId", employeeModel.DepartmentId);
            cmd.Parameters.AddWithValue("@Gender", employeeModel.Gender);
            cmd.Parameters.AddWithValue("@Skills", employeeModel.Skills);
            cmd.Parameters.AddWithValue("@ResumePath", employeeModel.ResumePath);
            cmd.Parameters.AddWithValue("@ProfileImage", employeeModel.ProfileImage);
            cmd.Parameters.AddWithValue("@IsActive", employeeModel.IsActive);
            cmd.Parameters.AddWithValue("@Salary", employeeModel.Salary);
            cmd.Parameters.AddWithValue("@ExperienceYears", employeeModel.ExperienceYears);
            cmd.Parameters.AddWithValue("@PasswordHash", employeeModel.PasswordHash);
            cmd.Parameters.AddWithValue("@Bio", employeeModel.Bio);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error saving employee data: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Employee added successfully.", Data = employeeModel });
        }

        [HttpPut("UpdateEmployee")]
        public ActionResult UpdateEmployee(EmployeeModel employeeModel)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_UpdateEmployee", con) { CommandType = CommandType.StoredProcedure };
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@EmployeeId", employeeModel.EmployeeId);
                cmd.Parameters.AddWithValue("@FirstName", employeeModel.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employeeModel.LastName);
                cmd.Parameters.AddWithValue("@Email", employeeModel.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", employeeModel.PhoneNumber);
                cmd.Parameters.AddWithValue("@DateOfBirth", employeeModel.DateOfBirth);
                cmd.Parameters.AddWithValue("@JoiningDate", employeeModel.JoiningDate);
                cmd.Parameters.AddWithValue("@DepartmentId", employeeModel.DepartmentId);
                cmd.Parameters.AddWithValue("@Gender", employeeModel.Gender);
                cmd.Parameters.AddWithValue("@Skills", employeeModel.Skills);
                cmd.Parameters.AddWithValue("@ResumePath", employeeModel.ResumePath);
                cmd.Parameters.AddWithValue("@ProfileImage", employeeModel.ProfileImage);
                cmd.Parameters.AddWithValue("@IsActive", employeeModel.IsActive);
                cmd.Parameters.AddWithValue("@Salary", employeeModel.Salary);
                cmd.Parameters.AddWithValue("@ExperienceYears", employeeModel.ExperienceYears);
                cmd.Parameters.AddWithValue("@PasswordHash", employeeModel.PasswordHash);
                cmd.Parameters.AddWithValue("@Bio", employeeModel.Bio);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating employee data: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Employee updated successfully.", Data = employeeModel });
        }

        [HttpDelete("DeleteEmployee")]
        public ActionResult DeleteEmployee(int employeeId)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_DeleteEmployee", con) { CommandType = CommandType.StoredProcedure };
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting employee data: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Employee deleted successfully.", Data = null });
        }
    }
}
