using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace restrauntBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RestaurantUser user)
        {
            if (await _context.RestaurantUsers.AnyAsync(u => u.email == user.email))
            {
                return BadRequest("User with this email already exists.");
            }

            _context.RestaurantUsers.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { UserId = user.userid });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RestaurantUser loginRequest)
        {
            var user = await _context.RestaurantUsers
                .FirstOrDefaultAsync(u => u.email == loginRequest.email && u.password == loginRequest.password && u.usertype==loginRequest.usertype);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(new { UserId = user.userid });
        }

        [HttpGet("users")] 
        public async Task<IActionResult> GetAllUsers() 
        {
            var users = await _context.RestaurantUsers.ToListAsync(); 
            return Ok(users);
        }


        [HttpPost("addTable")] public async Task<IActionResult> AddTable([FromBody] TableRequest request)
        { 

            var table = new Table
            { 
                tablename = request.tablename,
                status = request.status
            }; 
            _context.Tables.Add(table);
            await _context.SaveChangesAsync();
            return Ok(new { newtableid = table.tableid }); 
        }

       

        [HttpGet("alltables")] public async Task<IActionResult> GetAllTables()
        { 
            var tables = await _context.Tables.ToListAsync(); 
            return Ok(tables);
        }


        [HttpPost("bookTable")]
        public async Task<IActionResult> CreateBooking([FromBody] TableBooking tbl)
        {
            _context.TableBookings.Add(tbl);

            var table = await _context.Tables.FindAsync(tbl.tableid);
            if (table != null)
            {
                table.status = "2";
                _context.Tables.Update(table);
            }

            await _context.SaveChangesAsync();
            return Ok(new { newbookingid = tbl.bookingid });
        }



        //[HttpPut("resetBookStatus/{tableid}")]
        //public async Task<IActionResult> ResetBookStatus(int tableid)
        //{
        //    var table = await _context.Tables.FindAsync(tableid);
        //    if (table == null)
        //    {
        //        return NotFound(new { message = "table not found" });
        //    }

        //    table.status = "1";
        //    _context.Tables.Update(table);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { message = "table status reset to 1 successfully", changedtableid = tableid });
        //}

        [HttpPut("resetBookStatus/{tableid}")]
        public async Task<IActionResult> ResetBookStatus(int tableid)
        {
            var table = await _context.Tables.FindAsync(tableid);
            if (table == null)
            {
                return NotFound(new { message = "table not found" });
            }

            table.status = "1";
            _context.Tables.Update(table);
            await _context.SaveChangesAsync();

            // Delete records from TableBookings table using tableid
            var bookings = _context.TableBookings.Where(b => b.tableid == tableid);
            _context.TableBookings.RemoveRange(bookings);
            await _context.SaveChangesAsync();

            return Ok(new { message = "table status reset to 1 successfully and bookings deleted", changedtableid = tableid });
        }


        [HttpGet("allBookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _context.TableBookings
                .Join(_context.Tables,
                      booking => booking.tableid,
                      table => table.tableid,
                      (booking, table) => new
                      {
                          booking.bookingid,
                          booking.custname,
                          table.tablename,
                          table.tableid
                      })
                .ToListAsync();

            return Ok(bookings);
        }






    }



}
