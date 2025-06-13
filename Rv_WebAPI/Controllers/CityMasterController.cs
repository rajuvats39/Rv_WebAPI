using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Rv_WebAPI.Models;
using Rv_WebAPI.Utility;
using System.Data;

namespace Rv_WebAPI.Controllers
{
    [Route("api/CityMaster")]
    [ApiController]
    public class CityMasterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CityMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetCities")]
        public ActionResult GetCities()
        {
            List<CityMasterModel> response = new List<CityMasterModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_GetCities", con) { CommandType = CommandType.StoredProcedure };
            con.Open();
            using (SqlDataReader sqlDtaReader = cmd.ExecuteReader())
            {
                while (sqlDtaReader.Read())
                {
                    CityMasterModel cityMasterModel = new CityMasterModel();
                    cityMasterModel.CityId = Convert.ToInt32(sqlDtaReader["CityId"]);
                    cityMasterModel.StateId = Convert.ToInt32(sqlDtaReader["StateId"]);
                    cityMasterModel.CityName = sqlDtaReader["CityName"].ToString();
                    response.Add(cityMasterModel);
                }
            }
            con.Close();
            return Ok(new ResponseMessage { IsSuccess = true, Message = "City retrieved successfully.", Data = response });
        }

        [HttpPost("AddCity")]
        public ActionResult AddCity(CityMasterModel cityMasterModel)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_AddCity", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@StateId", cityMasterModel.StateId);
            cmd.Parameters.AddWithValue("@CityName", cityMasterModel.CityName);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error saving city data: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "City added successfully.", Data = cityMasterModel });
        }

        [HttpPut("UpdateCity")]
        public ActionResult UpdateCity(CityMasterModel cityMasterModel)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_UpdateCity", con) { CommandType = CommandType.StoredProcedure };
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@CityId", cityMasterModel.CityId);
                cmd.Parameters.AddWithValue("@StateId", cityMasterModel.StateId);
                cmd.Parameters.AddWithValue("@CityName", cityMasterModel.CityName);
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
            return Ok(new ResponseMessage { IsSuccess = true, Message = "City updated successfully.", Data = cityMasterModel });
        }

        [HttpDelete("DeleteCity")]
        public ActionResult DeleteCity(int cityId)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_DeleteCity", con) { CommandType = CommandType.StoredProcedure };
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@CityId", cityId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting city data: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "City deleted successfully.", Data = null });
        }
    }
}