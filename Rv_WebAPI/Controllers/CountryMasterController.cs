using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Rv_WebAPI.Models;
using Rv_WebAPI.Utility;
using System.Data;

namespace Rv_WebAPI.Controllers
{
    [Route("api/CountryMaster")]
    [ApiController]
    public class CountryMasterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CountryMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetCountries")]
        public ActionResult GetCountries()
        {
            List<CountryMasterModel> response = new List<CountryMasterModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_GetCountries", con) { CommandType = CommandType.StoredProcedure };
            con.Open();
            using (SqlDataReader sqlDtaReader = cmd.ExecuteReader())
            {
                while (sqlDtaReader.Read())
                {
                    CountryMasterModel countryMasterModel = new CountryMasterModel();
                    countryMasterModel.CountryId = Convert.ToInt32(sqlDtaReader["CountryId"]);
                    countryMasterModel.CountryName = sqlDtaReader["CountryName"].ToString();
                    response.Add(countryMasterModel);
                }
            }
            con.Close();
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Country retrieved successfully.", Data = response });
        }

        [HttpPost("AddCountry")]
        public ActionResult AddCountry(CountryMasterModel countryMasterModel)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_AddCountry", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@CountryName", countryMasterModel.CountryName);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error saving country data: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Country added successfully.", Data = countryMasterModel });
        }

        [HttpPut("UpdateCountry")]
        public ActionResult UpdateCountry(CountryMasterModel countryMasterModel)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_UpdateCountry", con) { CommandType = CommandType.StoredProcedure };
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@CountryId", countryMasterModel.CountryId);
                cmd.Parameters.AddWithValue("@CountryName", countryMasterModel.CountryName);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating country data: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Country updated successfully.", Data = countryMasterModel });
        }

        [HttpDelete("DeleteCountry")]
        public ActionResult DeleteCountry(int countryId)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_DeleteCountry", con) { CommandType = CommandType.StoredProcedure };
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@CountryId", countryId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting country data: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "CountryId deleted successfully.", Data = null });
        }
    }
}