using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Rv_WebAPI.Models;
using Rv_WebAPI.Utility;
using System.Data;

namespace Rv_WebAPI.Controllers
{
    [Route("api/StateMaster")]
    [ApiController]
    public class StateMasterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public StateMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetStates")]
        public ActionResult GetStates()
        {
            List<StateMasterModel> response = new List<StateMasterModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_GetStates", con) { CommandType = CommandType.StoredProcedure };
            con.Open();
            using (SqlDataReader sqlDtaReader = cmd.ExecuteReader())
            {
                while (sqlDtaReader.Read())
                {
                    StateMasterModel stateMasterModel = new StateMasterModel();
                    stateMasterModel.CountryId = Convert.ToInt32(sqlDtaReader["CountryId"]);
                    stateMasterModel.StateId = Convert.ToInt32(sqlDtaReader["StateId"]);
                    stateMasterModel.StateName = sqlDtaReader["StateName"].ToString();
                    response.Add(stateMasterModel);
                }
            }
            con.Close();
            return Ok(new ResponseMessage { IsSuccess = true, Message = "State retrieved successfully.", Data = response });
        }

        [HttpPost("AddState")]
        public ActionResult AddState(StateMasterModel stateMasterModel)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_AddState", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@CountryId", stateMasterModel.CountryId);
            cmd.Parameters.AddWithValue("@StateName", stateMasterModel.StateName);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error saving state data: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "State added successfully.", Data = stateMasterModel });
        }

        [HttpPut("UpdateState")]
        public ActionResult UpdateState(StateMasterModel stateMasterModel)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_UpdateState", con) { CommandType = CommandType.StoredProcedure };
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@StateId", stateMasterModel.StateId);
                cmd.Parameters.AddWithValue("@CountryId", stateMasterModel.CountryId);
                cmd.Parameters.AddWithValue("@StateName", stateMasterModel.StateName);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating city data: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "City updated successfully.", Data = stateMasterModel });
        }

        [HttpDelete("DeleteState")]
        public ActionResult DeleteState(int stateId)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_DeleteState", con) { CommandType = CommandType.StoredProcedure };
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@StateId", stateId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting state data: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "State deleted successfully.", Data = null });
        }
    }
}