using Azure;
using BackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Rv_WebAPI.Utility;
using System.Text.Json.Serialization;

namespace BackEnd.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        [HttpGet("GetInventoryData")]
        public ActionResult GetInventoryData()
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=RAJU\\SQLEXPRESS;Database=InventoryDB; User Id=sa;Password=Raju@123;TrustServerCertificate=True;"
            };
            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_GetInventoryData",
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = connection
            };
                connection.Open();
                List<InventoryModelDto> response = new List<InventoryModelDto>();
                using (SqlDataReader sqlDtaReader = command.ExecuteReader())
                {
                    while (sqlDtaReader.Read())
                    {
                        InventoryModelDto inventoryModelDto = new InventoryModelDto();
                        inventoryModelDto.ProductId = Convert.ToInt32(sqlDtaReader["ProductId"]);
                        inventoryModelDto.ProductName = Convert.ToString(sqlDtaReader["ProductName"]);
                        inventoryModelDto.StockAvailable = Convert.ToInt32(sqlDtaReader["StockAvailable"]);
                        inventoryModelDto.ReorderStock = Convert.ToInt32(sqlDtaReader["ReorderStock"]);
                        response.Add(inventoryModelDto);
                    }
                }
            connection.Close();
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Inventory data retrieved successfully.", Data = response });
        }

        [HttpPost("AddInventoryData")]
        public ActionResult AddInventoryData(InventoryModelDto inventoryModelDto)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=RAJU\\SQLEXPRESS;Database=InventoryDB; User Id=sa;Password=Raju@123;TrustServerCertificate=True;"
            };
            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_AddInventoryData",
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = connection
            };
            command.Parameters.AddWithValue("@ProductId", inventoryModelDto.ProductId);
            command.Parameters.AddWithValue("@ProductName", inventoryModelDto.ProductName);
            command.Parameters.AddWithValue("@StockAvailable", inventoryModelDto.StockAvailable);
            command.Parameters.AddWithValue("@ReorderStock", inventoryModelDto.ReorderStock);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error saving inventory data: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Inventory data saved successfully.", Data = inventoryModelDto });
        }

        [HttpPut("UpdateInventoryData")]
        public ActionResult UpdateInventoryData(InventoryModelDto inventoryModelDto)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=RAJU\\SQLEXPRESS;Database=InventoryDB; User Id=sa;Password=Raju@123;TrustServerCertificate=True;"
            };
            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_UpdateInventoryData",
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = connection
            };
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@ProductId", inventoryModelDto.ProductId);
                command.Parameters.AddWithValue("@ProductName", inventoryModelDto.ProductName);
                command.Parameters.AddWithValue("@StockAvailable", inventoryModelDto.StockAvailable);
                command.Parameters.AddWithValue("@ReorderStock", inventoryModelDto.ReorderStock);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating inventory data: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Inventory data updated successfully.", Data = inventoryModelDto });
        }

        [HttpDelete("DeleteInventoryData")]
        public ActionResult DeleteInventoryData(int ProductId)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=RAJU\\SQLEXPRESS;Database=InventoryDB; User Id=sa;Password=Raju@123;TrustServerCertificate=True;"
            };
            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_DeleteInventoryData",
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = connection
            };
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@ProductId", ProductId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting inventory data: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Inventory data deleted successfully.", Data = null });
        }
    }
}
