using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Rv_WebAPI.Models;
using Rv_WebAPI.Utility;
using System.Data;

namespace Rv_WebAPI.Controllers
{
    [Route("api/Department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetAllDepartments")]
        public IActionResult GetAllDepartments()
        {
            List<DepartmentModel> response = new List<DepartmentModel>();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_GetAllDepartments", con) { CommandType = CommandType.StoredProcedure };
            con.Open();

            using (SqlDataReader sqlDtaReader = cmd.ExecuteReader())
            {
                while (sqlDtaReader.Read())
                {
                    DepartmentModel departmentModel = new DepartmentModel();
                    departmentModel.DepartmentId = Convert.ToInt32(sqlDtaReader["DepartmentId"]);
                    departmentModel.DepartmentName = sqlDtaReader["DepartmentName"].ToString();
                    departmentModel.DepartmentCode = sqlDtaReader["DepartmentCode"].ToString();
                    departmentModel.ManagerName = sqlDtaReader["ManagerName"].ToString();
                    departmentModel.ExtensionNumber = sqlDtaReader["ExtensionNumber"].ToString();
                    response.Add(departmentModel);
                }
            }
            con.Close();
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Department retrieved successfully.", Data = response });
        }

        [HttpPost("AddDepartment")]
        public ActionResult AddDepartment(DepartmentModel departmentModel)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_AddDepartment", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@DepartmentName", departmentModel.DepartmentName);
            cmd.Parameters.AddWithValue("@DepartmentCode", departmentModel.DepartmentCode);
            cmd.Parameters.AddWithValue("@ManagerName", departmentModel.ManagerName);
            cmd.Parameters.AddWithValue("@ExtensionNumber", departmentModel.ExtensionNumber);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error saving department data: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Department added successfully.", Data = departmentModel });
        }

        [HttpPut("UpdateDepartment")]
        public ActionResult UpdateDepartment(DepartmentModel departmentModel)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_UpdateDepartment", con) { CommandType = CommandType.StoredProcedure };
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@DepartmentName", departmentModel.DepartmentName);
                cmd.Parameters.AddWithValue("@DepartmentCode", departmentModel.DepartmentCode);
                cmd.Parameters.AddWithValue("@ManagerName", departmentModel.ManagerName);
                cmd.Parameters.AddWithValue("@ExtensionNumber", departmentModel.ExtensionNumber);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating department data: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Department updated successfully.", Data = departmentModel });
        }

        [HttpDelete("DeleteDepartmentId")]
        public ActionResult DeleteDepartmentId(int departmentId)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_DeleteDepartment", con) { CommandType = CommandType.StoredProcedure };
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@DepartmentId", departmentId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting department data: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Department deleted successfully.", Data = null });
        }
    }
}
