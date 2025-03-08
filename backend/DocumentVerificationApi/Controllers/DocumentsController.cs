using DocumentVerificationApi.Models;
using DocumentVerificationApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        public async Task<ActionResult<Document>> PostDocument([FromBody] Document document)
        {
            if (document == null)
            {
                return BadRequest("Document data is null.");
            }

            // Generate a unique VerificationCode
            document.VerificationCode = Guid.NewGuid().ToString();
            document.CreatedAt = DateTime.UtcNow;

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDocument), new { id = document.Id }, document);
        }

        // GET: api/documents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            return document;
        }
    }
}