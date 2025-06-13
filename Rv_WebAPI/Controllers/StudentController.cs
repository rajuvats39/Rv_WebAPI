using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Rv_WebAPI.Models;
using Rv_WebAPI.Utility;
using System.Data;

namespace Rv_WebAPI.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            List<StudentModel> response = new List<StudentModel>();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("sp_GetAllStudents", conn) { CommandType = CommandType.StoredProcedure };
            conn.Open();
            using (SqlDataReader sqlDtaReader = cmd.ExecuteReader())
            {
                while (sqlDtaReader.Read())
                {
                    StudentModel studentModel = new StudentModel();
                    studentModel.ID = Convert.ToInt32(sqlDtaReader["ID"]);
                    studentModel.Name = sqlDtaReader["Name"].ToString();
                    studentModel.Age = Convert.ToInt32(sqlDtaReader["Age"]);
                    studentModel.DOB = Convert.ToDateTime(sqlDtaReader["DOB"]);
                    studentModel.Email = sqlDtaReader["Email"].ToString();
                    studentModel.Mobile = sqlDtaReader["Mobile"].ToString();
                    studentModel.Address = sqlDtaReader["Address"].ToString();
                    response.Add(studentModel);
                }
            }
            conn.Close();
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Students retrieved successfully.", Data = response });
        }

        [HttpPost("AddStudent")]
        public IActionResult AddStudent(StudentModel studentModel)
        {
            using SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using SqlCommand cmd = new SqlCommand("sp_AddStudent", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Name", studentModel.Name);
            cmd.Parameters.AddWithValue("@Age", studentModel.Age);
            cmd.Parameters.AddWithValue("@DOB", studentModel.DOB);
            cmd.Parameters.AddWithValue("@Email", studentModel.Email);
            cmd.Parameters.AddWithValue("@Mobile", studentModel.Mobile);
            cmd.Parameters.AddWithValue("@Address", studentModel.Address);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Student added successfully.", Data = studentModel });
        }

        [HttpPut("UpdateStudent")]
        public IActionResult UpdateStudent(StudentModel studentModel)
        {
            using SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using SqlCommand cmd = new SqlCommand("sp_UpdateStudent", conn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@ID", studentModel.ID);
            cmd.Parameters.AddWithValue("@Name", studentModel.Name);
            cmd.Parameters.AddWithValue("@Age", studentModel.Age);
            cmd.Parameters.AddWithValue("@DOB", studentModel.DOB);
            cmd.Parameters.AddWithValue("@Email", studentModel.Email);
            cmd.Parameters.AddWithValue("@Mobile", studentModel.Mobile);
            cmd.Parameters.AddWithValue("@Address", studentModel.Address);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Student updated successfully.", Data = studentModel });
        }

        [HttpDelete("DeleteStudent")]
        public IActionResult DeleteStudent(int id)
        {
            using SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            using SqlCommand cmd = new SqlCommand("sp_DeleteStudent", conn) { CommandType = CommandType.StoredProcedure };
            conn.Open();
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.ExecuteNonQuery();
            conn.Close();
            return Ok(new ResponseMessage { IsSuccess = true, Message = "Student deleted successfully.", Data = null });
        }
    }
}
