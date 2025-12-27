using Microsoft.AspNetCore.Mvc; // Yeh line 'Waiter' wali powers laati hai
using FreshDeskAPI; // <--- YEH LINE ZAROORI HAI (Isse Ticket file dikhegi)
using Microsoft.EntityFrameworkCore;


namespace FreshDeskAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketsController : ControllerBase
    {
        //database connection 
        private readonly AppDbContext _context;

        //Constructor : Servwr se data maang raha hai ,Dependency Injection
        public TicketsController(AppDbContext context)
        {
            _context = context;
        }


        //GET : /tickets (database se sare tickets lao )
        public IActionResult Get()
        {
            //List ki jagah _context.Tickets use kregei 
            var allTickets = _context.Tickets.ToList();
            return Ok(allTickets);
        }

        //POST: /tickets (Naye tickets save kro )
        [HttpPost]
        public IActionResult Post(Ticket ticket)
        {
            //Note : ID generate karne ki jarurat nahi , SQL server khud karega !

            //1. Database mei add kro 
            _context.Tickets.Add(ticket);

            //2. SAVE BUTTON : ye line sabse zaroori hai. Iske bina data save nahi hoga 
            _context.SaveChanges();

            //3. Update List Wapas bhejo 
            return Ok(_context.Tickets.ToList());
        }

        //PUT : /tickets/5 (Update kro )
        [HttpPut("{id}")]
        public IActionResult Update(int id, Ticket ticket)
        {
            //1. Database mei dhundo 
            var existingTicket = _context.Tickets.FirstOrDefault(t => t.Id == id);

            if (existingTicket != null)
            {
                //2. status update kro 
                existingTicket.Status = "Closed";

                //3. Changes save kro 
                _context.SaveChanges();
            }
            return Ok(_context.Tickets.ToList());

        }


        //DELETE: /tickets/5 (delee krdo 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)

        {

            //1. database mei dhundo 
            var ticketToRemove = _context.Tickets.FirstOrDefault(t => t.Id == id);

            if (ticketToRemove != null)
            {

                //remove  kro 
                _context.Tickets.Remove(ticketToRemove);

                //3. changes save kro 
                _context.SaveChanges();

            }
            return Ok(_context.Tickets.ToList());

        }
        
    }
}