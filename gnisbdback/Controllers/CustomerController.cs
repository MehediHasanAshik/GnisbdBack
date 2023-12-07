using gnisbdback.Data;
using gnisbdback.Migrations;
using gnisbdback.Models.Domain;
using gnisbdback.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;

namespace gnisbdback.Controllers
{
    [ApiController]
    [Route("api/")]
    public class CustomerController : Controller
    {
        private readonly AppDbContext appDbContext;

        public CustomerController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [HttpGet("Customer/Corporate")]
        public async Task<IActionResult> GetCorporateCustomerAsync()
        {
            var customers = await appDbContext.Corporate_Customer_Tbl.ToListAsync();
            return Ok(customers);
        }

        [HttpGet("Customer/Individual")]
        public async Task<IActionResult> GetIndividualCustomerAsync()
        {
            var customers = await appDbContext.Individual_Customer_Tbl.ToListAsync();
            return Ok(customers);
        }

        [HttpGet("ProductDetails")]
        public async Task<IActionResult> GetProductDataAsync()
        {
            var products = await appDbContext.Products_Service_tbl.ToListAsync();
            return Ok(products);
        }

        [HttpPost("ProductDetails")]
        public async Task<IActionResult> AddProduct([FromBody]ProductServicesReqDTO request)
        {
            var product = new ProductServices
            {
                ServiceName = request.ServiceName,
                Unit = request.Unit,
                Quantity = request.Quantity
            };

            if(product != null)
            {
                await appDbContext.Products_Service_tbl.AddAsync(product);
                await appDbContext.SaveChangesAsync();
            }

            return Ok(request);

        }

        [HttpGet]
        [Route("ProductDetails/{id:int}")]
        public async Task<IActionResult> GetSingleProduct([FromRoute] int id)
        {
            var product = await appDbContext.Products_Service_tbl.FindAsync(id);

            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut]
        [Route("ProductDetails/{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, ProductServicesReqDTO updatedProduct)
        {
            var product = await appDbContext.Products_Service_tbl.FirstOrDefaultAsync(x => x.Id == id);

            if(product == null)
            {
                return NotFound();
            }

            product.ServiceName = updatedProduct.ServiceName;
            product.Unit = updatedProduct.Unit;
            product.Quantity = updatedProduct.Quantity;

            await appDbContext.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete]
        [Route("ProductDetails/{id:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var product = await appDbContext.Products_Service_tbl.FirstOrDefaultAsync(x => x.Id == id);

            if(product == null)
            {
                return NotFound();
            }

            appDbContext.Products_Service_tbl.Remove(product);
            await appDbContext.SaveChangesAsync();

            return Ok(product);
        }

        //for store procedure
        [HttpPost]
        [Route("Insert/InsertIntoDetail")]
        public async Task<IActionResult> InsertIntoMinutesDetailsync([FromBody]MeetingMinutesDetailsDTO meetingsMinutes)
        {
            try
            {
                int rowsAffected = await appDbContext.Database.
                ExecuteSqlInterpolatedAsync($"EXEC Meeting_Minutes_Details_Save_SP @MeetingAgenda={meetingsMinutes
                .MeetingAgenda}, @MeetingDiscussion={meetingsMinutes
                .MeetingDiscussion}, @MeetingDecision={meetingsMinutes.MeetingDecision} ");

                if(rowsAffected > 0 )
                {
                    return Ok("Meeting minutes added successfully.");
                }
                else
                {
                    return BadRequest("Failed to add meeting minutes.");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
            
        }

        //for store procedure
        [HttpPost]
        [Route("Insert/InsertIntoMaster")]
        public async Task<IActionResult> InsertIntoMinutesMasterAsync([FromBody] MeetingMInutesMasterDTO meetingsMinutes)
        {
            try
            {
                int rowsAffected = await appDbContext.Database.
                ExecuteSqlInterpolatedAsync($"EXEC Meeting_Minutes_Master_Save_SP @CustomerName={meetingsMinutes
                .CustomerName}, @DateandTime={meetingsMinutes
                .DateandTime}, @MeetingPlace={meetingsMinutes
                .MeetingPlace}, @ClientSide={meetingsMinutes
                .ClientSide}, @HostSide={meetingsMinutes.HostSide} ");

                if (rowsAffected > 0)
                {
                    return Ok("Meeting minutes added successfully.");
                }
                else
                {
                    return BadRequest("Failed to add meeting minutes.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

        }

    }
}



//make a function to call the store procedure function
/*public int Meeting_Minutes_Master_Save_SP(MeetingMinutesDetailsDTO meetingMinutes)
{
    var meetingAgendaParam = new SqlParameter("@MeetingAgenda", meetingMinutes.MeetingAgenda);
    var meetingDiscussionParam = new SqlParameter("@MeetingDiscussion", meetingMinutes.MeetingDiscussion);
    var meetingDecisionParam = new SqlParameter("@MeetingDecision", meetingMinutes.MeetingDecision);

    // Execute the raw SQL query to call the stored procedure
    return Database.ExecuteSqlRaw("EXEC Meeting_Minutes_Master_Save_SP @MeetingAgenda, @MeetingDiscussion, @MeetingDecision",
        meetingAgendaParam, meetingDiscussionParam, meetingDecisionParam);
}
*/