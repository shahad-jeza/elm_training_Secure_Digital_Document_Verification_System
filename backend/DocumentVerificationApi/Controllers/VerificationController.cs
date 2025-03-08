using DocumentVerificationApi.Models;
using DocumentVerificationApi.Data;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics; // For Stopwatch

namespace DocumentVerificationApi.Controllers
{
    [Route("api/verify")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly AppDbContext _context;

        public VerificationController(AppDbContext context)
        {
            _connectionString = context.Database.GetDbConnection().ConnectionString;
            _context = context;
        }

        // POST: api/verify
        [HttpPost]
        public async Task<ActionResult> VerifyDocument([FromBody] VerificationLogDto logDto)
        {
            try
            {
                if (logDto == null)
                {
                    return BadRequest("Verification log data is null.");
                }

                // Validate the required fields
                if (string.IsNullOrEmpty(logDto.Status))
                {
                    return BadRequest("The Status field is required.");
                }

                // Map VerificationLogDto to VerificationLog
                var log = new VerificationLog
                {
                    DocumentId = logDto.DocumentId,
                    VerifiedByUserId = logDto.VerifiedByUserId,
                    Timestamp = logDto.Timestamp,
                    Status = logDto.Status
                };

                // Log time for Dapper
                var stopwatchDapper = Stopwatch.StartNew();
                using (IDbConnection dbConnection = new SqliteConnection(_connectionString))
                {
                    dbConnection.Open();

                    // Insert the verification log using Dapper
                    var query = @"
                        INSERT INTO VerificationLogs (DocumentId, VerifiedByUserId, Timestamp, Status)
                        VALUES (@DocumentId, @VerifiedByUserId, @Timestamp, @Status)";

                    await dbConnection.ExecuteAsync(query, new
                    {
                        log.DocumentId,
                        log.VerifiedByUserId,
                        log.Timestamp,
                        log.Status
                    });
                }
                stopwatchDapper.Stop();
                var dapperTime = stopwatchDapper.ElapsedMilliseconds;

                // Log time for EF Core
                var stopwatchEf = Stopwatch.StartNew();
                _context.VerificationLogs.Add(log);
                await _context.SaveChangesAsync();
                stopwatchEf.Stop();
                var efTime = stopwatchEf.ElapsedMilliseconds;

                // Log the results
                Console.WriteLine($"Dapper Execution Time: {dapperTime} ms");
                Console.WriteLine($"EF Core Execution Time: {efTime} ms");

                return Ok(new
                {
                    Message = "Document verified successfully.",
                    DapperTime = dapperTime,
                    EfCoreTime = efTime
                });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error verifying document: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                // Return a 500 error with a meaningful message
                return StatusCode(500, new { Message = "An error occurred while verifying the document.", Error = ex.Message });
            }
        }
    }
}