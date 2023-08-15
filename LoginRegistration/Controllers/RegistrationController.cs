using LoginRegistration.Models;
using LoginRegistration.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace LoginRegistration.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public readonly DbContextClass RegContextClass;

        public RegistrationController(IConfiguration configuration, DbContextClass _db)
        {
            _configuration = configuration;
            RegContextClass = _db;
        }
        

        /*public RegistrationController()

        {

            

        }*/
        [HttpGet]

        public async Task<ActionResult> GetAdminUsers()

        {

            return Ok(await RegContextClass.Registration.ToListAsync());
        }
        [HttpPost]
        [Route("registration")]
        public string registration(Registration registration)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection").ToString());
            SqlCommand cmd = new SqlCommand("INSERT INTO Registration(UserName,Password,Email,IsActive) VALUES('"+registration.UserName+ "','"+registration.Password+ "','"+registration.Email+ "','"+registration.IsActive+ "')", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if(i>0)
            {
                return "Data Inserted";
            }
            else
            {
                return "Error";
            }
            return "";
        }
        [HttpPost]
        [Route("login")]

        public string login(Registration registration)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection").ToString());
            SqlDataAdapter da= new SqlDataAdapter("SELECT * FROM Registration WHERE Email = '"+registration.Email+ "' AND Password='"+registration.Password+ "' ", con);
            DataTable dt=new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count>0)
            {
                return "Data Found";
            }
            else 
            { 
                return "Error:Invalid Data"; 
            }

        }

    }
}
