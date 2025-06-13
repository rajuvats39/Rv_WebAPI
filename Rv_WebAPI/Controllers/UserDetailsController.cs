using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Rv_WebAPI.Models;
using Rv_WebAPI.Utility;
using System.Data;

namespace Rv_WebAPI.Controllers
{
    [Route("api/UserDetails")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserDetailsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetAllUser")]
        public ActionResult GetAllUserDetails()
        {
            List<UserDetailsModel> response = new List<UserDetailsModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_GetUserDetails", con) { CommandType = CommandType.StoredProcedure };
            con.Open();
            using (SqlDataReader sqlDtaReader = cmd.ExecuteReader())
            {
                while (sqlDtaReader.Read())
                {
                    UserDetailsModel userDetailsModel = new UserDetailsModel();
                    userDetailsModel.UserId = Convert.ToInt32(sqlDtaReader["UserId"]);
                    userDetailsModel.Name = sqlDtaReader["Name"].ToString();
                    userDetailsModel.Email = sqlDtaReader["Email"].ToString();
                    userDetailsModel.Password = sqlDtaReader["Password"].ToString();
                    userDetailsModel.Number = sqlDtaReader["Number"].ToString();
                    userDetailsModel.DOB = Convert.ToDateTime(sqlDtaReader["DOB"]);
                    userDetailsModel.Gender = sqlDtaReader["Gender"].ToString();
                    userDetailsModel.CountryId = Convert.ToInt32(sqlDtaReader["CountryId"]);
                    userDetailsModel.StateId = Convert.ToInt32(sqlDtaReader["StateId"]);
                    userDetailsModel.CityId = Convert.ToInt32(sqlDtaReader["CityId"]);
                    userDetailsModel.AcceptTerms = Convert.ToBoolean(sqlDtaReader["AcceptTerms"]);
                    userDetailsModel.Feedback = sqlDtaReader["Feedback"].ToString();
                    userDetailsModel.Image = sqlDtaReader["Image"].ToString();
                    userDetailsModel.Score = Convert.ToInt32(sqlDtaReader["Score"]);
                    userDetailsModel.CountryName = sqlDtaReader["CountryName"].ToString();
                    userDetailsModel.StateName = sqlDtaReader["StateName"].ToString();
                    userDetailsModel.CityName = sqlDtaReader["CityName"].ToString();
                    response.Add(userDetailsModel);
                }
            }
            con.Close();
            return Ok(new ResponseMessage { IsSuccess = true, Message = "User retrieved successfully.", Data = response });
        }

        [HttpPost("AddUserDetails")]
        public IActionResult AddUserDetails(UserDetailsModel model)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("sp_AddUserDetails", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                cmd.Parameters.AddWithValue("@Number", model.Number);
                cmd.Parameters.AddWithValue("@DOB", model.DOB);
                cmd.Parameters.AddWithValue("@Gender", model.Gender);
                cmd.Parameters.AddWithValue("@CountryId", model.CountryId);
                cmd.Parameters.AddWithValue("@StateId", model.StateId);
                cmd.Parameters.AddWithValue("@CityId", model.CityId);
                cmd.Parameters.AddWithValue("@AcceptTerms", model.AcceptTerms);
                cmd.Parameters.AddWithValue("@Feedback", model.Feedback);
                cmd.Parameters.AddWithValue("@Image", model.Image);
                cmd.Parameters.AddWithValue("@Score", model.Score);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "User added successfully" });
        }

        [HttpPut("UpdateUserDetails")]
        public IActionResult UpdateUserDetails(UserDetailsModel model)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateUserDetails", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                cmd.Parameters.AddWithValue("@Number", model.Number);
                cmd.Parameters.AddWithValue("@DOB", model.DOB);
                cmd.Parameters.AddWithValue("@Gender", model.Gender);
                cmd.Parameters.AddWithValue("@CountryId", model.CountryId);
                cmd.Parameters.AddWithValue("@StateId", model.StateId);
                cmd.Parameters.AddWithValue("@CityId", model.CityId);
                cmd.Parameters.AddWithValue("@AcceptTerms", model.AcceptTerms);
                cmd.Parameters.AddWithValue("@Feedback", model.Feedback);
                cmd.Parameters.AddWithValue("@Image", model.Image);
                cmd.Parameters.AddWithValue("@Score", model.Score);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "User updated successfully" });
        }

        [HttpDelete("DeleteUserDetails")]
        public IActionResult DeleteUserDetails(int userId)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteUserDetails", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@UserId", userId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "User deleted successfully" });
        }
    }
}
