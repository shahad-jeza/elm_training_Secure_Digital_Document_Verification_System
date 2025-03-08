using DocumentVerificationApi.Models;
using DocumentVerificationApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentVerificationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocumentsController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/documents
        [HttpPost]
        public async Task<ActionResult<Document>> PostDocument([FromForm] string title, [FromForm] IFormFile file, [FromForm] int userId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Save the file to a specific location
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Create a new Document object
            var document = new Document
            {
                Title = title,
                FilePath = filePath,
                VerificationCode = Guid.NewGuid().ToString(), // Generate a unique VerificationCode
                Status = "Pending", // Default status
                CreatedAt = DateTime.UtcNow,
                UserId = userId // Assign the UserId
            };

            // Save the document to the database
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDocument), new { id = document.Id }, document);
        }

[HttpGet]
public async Task<ActionResult<IEnumerable<Document>>> GetDocuments()
{
    try
    {
        var documents = await _context.Documents
            .Include(d => d.User) // Include the User
            .Include(d => d.VerificationLogs) // Include the VerificationLogs
            .ToListAsync();

        return Ok(documents);
    }
    catch (Exception ex)
    {
        // Log the exception
        Console.WriteLine($"Error fetching documents: {ex.Message}");
        Console.WriteLine($"Stack Trace: {ex.StackTrace}");

        // Return a 500 error with a meaningful message
        return StatusCode(500, new { Message = "An error occurred while fetching documents.", Error = ex.Message });
    }
}

        // GET: api/documents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocument(int id)
        {
            var document = await _context.Documents
                .Include(d => d.User) // Include the User
                .Include(d => d.VerificationLogs) // Include the VerificationLogs
                .FirstOrDefaultAsync(d => d.Id == id);

            if (document == null)
            {
                return NotFound();
            }

            return document;
        }
    }
}