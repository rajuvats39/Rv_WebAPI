using Azure;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Rv_WebAPI.Utility;

namespace BackEnd.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet("GetAllCustomer")]
        public ActionResult GetAllCustomer()
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=RAJU\\SQLEXPRESS;Database=RV_WebAPI_DB; User Id=sa;Password=Raju@123;TrustServerCertificate=True;"
            };
            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_GetCustomerData",
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = connection
            };
            connection.Open();
            List<CustomerModel> response = new List<CustomerModel>();
            using (SqlDataReader sqlDtaReader = command.ExecuteReader())
            {
                while (sqlDtaReader.Read())
                {
                    CustomerModel customerModelDto = new CustomerModel();
                    customerModelDto.CustomerId = Convert.ToInt32(sqlDtaReader["CustomerId"]);
                    customerModelDto.FirstName = Convert.ToString(sqlDtaReader["FirstName"]);
                    customerModelDto.LastName = Convert.ToString(sqlDtaReader["LastName"]);
                    customerModelDto.Email = Convert.ToString(sqlDtaReader["Email"]);
                    customerModelDto.Mobile = Convert.ToString(sqlDtaReader["Mobile"]);
                    customerModelDto.RegistrationDate = Convert.ToDateTime(sqlDtaReader["RegistrationDate"]);
                    response.Add(customerModelDto);
                }
            }
            connection.Close();
            return Ok(new ResponseMessage {IsSuccess = true, Message = "Customer data retrieved successfully.", Data = response });
        }

        [HttpPost("AddCustomer")]
        public ActionResult AddCustomer(CustomerModel customerModelDto)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=RAJU\\SQLEXPRESS;Database=RV_WebAPI_DB; User Id=sa;Password=Raju@123;TrustServerCertificate=True;"
            };
            SqlCommand cmd = new SqlCommand
            {
                CommandText = "sp_SaveCustomerData",
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = connection
            };
            cmd.Parameters.AddWithValue("@CustomerId", customerModelDto.CustomerId);
            cmd.Parameters.AddWithValue("@FirstName", customerModelDto.FirstName);
            cmd.Parameters.AddWithValue("@LastName", customerModelDto.LastName);
            cmd.Parameters.AddWithValue("@Email", customerModelDto.Email);
            cmd.Parameters.AddWithValue("@Mobile", customerModelDto.Mobile);
            cmd.Parameters.AddWithValue("@RegistrationDate", customerModelDto.RegistrationDate);
            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { IsSuccess = false, Message = $"Error saving customer data: {ex.Message}" });
            }
            finally
            {
                connection.Close();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Customer data saved successfully.", Data = customerModelDto });
        }

        [HttpPut("UpdateCustomerData")]
        public ActionResult UpdateCustomerData(CustomerModel customerModelDto)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=RAJU\\SQLEXPRESS;Database=InventoryDB; User Id=sa;Password=Raju@123;TrustServerCertificate=True;"
            };
            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_UpdateCustomerData",
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = connection
            };
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@CustomerId", customerModelDto.CustomerId);
                command.Parameters.AddWithValue("@FirstName", customerModelDto.FirstName);
                command.Parameters.AddWithValue("@LastName", customerModelDto.LastName);
                command.Parameters.AddWithValue("@Email", customerModelDto.Email);
                command.Parameters.AddWithValue("@Mobile", customerModelDto.Mobile);
                command.Parameters.AddWithValue("@RegistrationDate", customerModelDto.RegistrationDate);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { IsSuccess = false, Message = $"Error updating customer data: {ex.Message}" });
            }
            finally
            {
                connection.Close();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Customer data updated successfully.", Data = customerModelDto });
        }

        [HttpDelete("DeleteCustomerData")]
        public ActionResult DeleteCustomerData(int customerId)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=RAJU\\SQLEXPRESS;Database=RV_WebAPI_DB; User Id=sa;Password=Raju@123;TrustServerCertificate=True;"
            };
            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_DeleteCustomerData",
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = connection
            };
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@CustomerId", customerId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { IsSuccess = false, Message = $"Error deleting customer data: {ex.Message}" });
            }
            finally
            {
                connection.Close();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Customer data saved successfully.", Data = null });
        }
    }
}
