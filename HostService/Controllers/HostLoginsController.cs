using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HostService.Data;
using HostService.Models;
using HostService.DTOs;
using RestSharp;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using Newtonsoft.Json;

namespace HostService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowCrossSite]

    public class LoginsController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        private readonly HostServiceContext _context;

        public LoginsController(HostServiceContext context)
        {
            _context = context;
        }





        [Route("ByOfficer")]
        [HttpGet]
        public async Task<Object> GetPensionStatusByOfficer()
        {
            var responseString = await client.GetStringAsync("https://localhost:7071/api/PensionStatus/ByOfficer");
            return responseString;



        }





        [Route("ByAdmin")]
        [HttpGet]
        public async Task<Object> GetPensionStatusByAdmin()
        {

            var responseString = await client.GetStringAsync("https://localhost:7071/api/PensionStatus/ByAdmin");
            return responseString;

        }




        [Route("ShowStatus/{pensionId}")]
        [HttpPut]
        public async Task<Object> UpdateStatusByAdmin(string pensionId)
        {
            // Setup initial conditions.
            if (pensionId != null)
            {
                var client = new RestClient("https://localhost:7071/api/PensionStatus/ShowStatus/" + pensionId);
                var request = new RestRequest(Method.PUT);
                request.Timeout = 500000;
                IRestResponse response = client.Execute(request);
                /*Console.WriteLine(response.Content);*/
                return response.Content;

            }
            return NoContent();

        }





        [Route("UpdateStatus/{pensionId}")]
        [HttpPut]
        public async Task<Object> UpdateStatusByOfficer(string pensionId)
        {
            // Setup initial conditions.
            if (pensionId != null)
            {
                var client = new RestClient("https://localhost:7071/api/PensionStatus/UpdateStatus/" + pensionId);
                var request = new RestRequest(Method.PUT);
                request.Timeout = 500000;
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
                return response.Content;

            }
            return NoContent();

        }




        [Route("AddPension")]
        [HttpPost]
        public async Task<Object> PostPensionScheme( PensionSchemeValidate pensionScheme)
        {



            var person = new Person(pensionScheme.PensionName, pensionScheme.Description);
            
            var json = JsonConvert.SerializeObject(person);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://localhost:44342/api/PensionSchemes";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);


            return result;





        }









        // GET: api/Logins

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HostLogin>>> GetLogin()
        {
            if (_context.HostLogin == null)
            {
                return NotFound();
            }
            return await _context.HostLogin.ToListAsync();
        }




        // GET: api/Logins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HostLogin>> GetLoginById(int id)
        {
            if (_context.HostLogin == null)
            {
                return NotFound();
            }
            var login = await _context.HostLogin.FindAsync(id);

            if (login == null)
            {
                return NotFound();
            }

            return login;
        }

        // PUT: api/Logins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{email}")]
        public async Task<IActionResult> PutLogin(string email, HostLogin login)
        {
            if (email != login.Email)
            {
                return BadRequest();
            }

            _context.Entry(login).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Logins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HostLogin>> PostLogin(HostLogin login)
        {
            if (_context.HostLogin == null)
            {
                return Problem("Entity set 'PensionManagementSystemContext.Login'  is null.");
            }
            _context.HostLogin.Add(login);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogin", login);
        }


        [Route("LoginValidate")]
        [HttpPost]
        public async Task<ActionResult<LoginValidate>> Login(LoginValidate login)
        {
            HostLogin user = new HostLogin();
            if (login.Email == "" || login.Password == "")
            {
                return BadRequest(new { Status = "fail", Message = "Email and Password cannot be empty" });
            }
            var users = _context.HostLogin.Where(d => d.Email == login.Email).FirstOrDefault();

            if (users == null)
            {
                return BadRequest(new { Status = "fail", Message = "Email not found" });
            }
            if (login.Password == login.Password)
            {
                return Ok(new { Status = "success", Message = "Login Successful", User = users });
            }
            else
            {
                return BadRequest(new { Status = "fail", Message = "Password doesn't match" });
            }

        }





        // DELETE: api/Logins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogin(int id)
        {
            if (_context.HostLogin == null)
            {
                return NotFound();
            }
            var login = await _context.HostLogin.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }

            _context.HostLogin.Remove(login);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }

    internal class Person
    {
        public string PensionName { get; set; }
        public string Description { get; set; }
        public Person(string pensionName, string description)
        {
            PensionName = pensionName;
            Description = description;
        }
    }
}
