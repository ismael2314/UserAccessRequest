using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using UserAccessRequest.Data;
using UserAccessRequest.Model.Database;
using UserAccessRequest.Model.Dto;
namespace UserAccessRequest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _applicationDbContext;

        public RequestController(UserManager<Users> userManager,IConfiguration configuration,ILogger logger,ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        // Request view by department
        [HttpGet("all")]
        public async Task<IActionResult> ListRequests()
        {
            var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            var department = User?.FindFirst("department")?.Value ?? "";
            var uid = Guid.NewGuid().ToString();

            var request = await _applicationDbContext.RequestForms
                        .Where(w => w.Form.FormDepartment.Equals(department))
                        .Select(s => new
                        {
                            name = s.Form.FormName,
                            description = s.Form.FormDescription,
                            department = s.Form.FormDepartment,
                            status = s.RequestStatus,
                            Fields = s.RequestFormsResult.Select(s=> 
                            new {
                                name = s.Fields.FiledName,
                                type = s.Fields.FieldType,
                                //s.RequestForms.Fields.FieldValue,
                                //s.RequestForms.Fields.FieldValueType,
                                value = s.FieldValue
                            })
                        }).ToListAsync();
            return Ok(new { message = "", result = request });
        }
        // Request view by self
        [HttpGet("mine")]
        public async Task<IActionResult> ListRequestsByCreator()
        {
            var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            var department = User?.FindFirst("department")?.Value ?? "";
            var uid = Guid.NewGuid().ToString();

            var request = await _applicationDbContext.RequestForms
                        .Where(w => w.RequestFormsResult != null && w.CreatedBy.Equals(id))
                        .Select(s => new
                        {
                            name = s.Form.FormName,
                            description = s.Form.FormDescription,
                            department = s.Form.FormDepartment,
                            status = s.RequestStatus,
                            Fields = s.RequestFormsResult.Select(s =>
                            new {
                                name = s.Fields.FiledName,
                                type = s.Fields.FieldType,
                                //s.RequestForms.Fields.FieldValue,
                                //s.RequestForms.Fields.FieldValueType,
                                value = s.FieldValue
                            })
                        }).ToListAsync();
            return Ok(new { message = "", result = request });
        }
        // Request view by ticket
        [HttpGet("tiket/{requestId}")]
        public async Task<IActionResult> ListRequestsByTicket([FromRoute] string requestId)
        {
            var request = await _applicationDbContext.RequestForms
                        .Where(w => w.Id.Equals(requestId))
                        .Select(s => new
                        {
                            name = s.Form.FormName,
                            description = s.Form.FormDescription,
                            department = s.Form.FormDepartment,
                            status = s.RequestStatus,
                            Fields = s.RequestFormsResult.Select(s =>
                            new {
                                name = s.Fields.FiledName,
                                type = s.Fields.FieldType,
                                //s.RequestForms.Fields.FieldValue,
                                //s.RequestForms.Fields.FieldValueType,
                                value = s.FieldValue
                            })
                        }).ToListAsync();
            return Ok(new { message = "", result = request });
        }

        [HttpPost("create/{formId}")]
        public async Task<IActionResult> CreateReqeust([FromRoute] string? formId, [FromBody] DataObjects.CreateRequest model)
        {
            var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            var department = User?.FindFirst("department")?.Value ?? "";
            var uid = Guid.NewGuid().ToString();

            if (formId!=null && !formId.IsNullOrEmpty())
            {
                model.formId = formId;
            }
            
            RequestForms form = new RequestForms
            {
                FormId=model.formId,
                RequestFormsResult = model.Field.Select(s => new RequestFormsResult
                {
                    FieldId = s.Id,
                    RequestFormId = model.formId,
                    FieldValue = s.value
                }).ToList()
            };

            await _applicationDbContext.RequestForms.AddAsync(form);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(new {message = "Success"});
        }
        // -----
        // Request Create
        // Request update
        // request approve
        [HttpPut("approve/{requestId}")]
        public async Task<IActionResult> ApproveRequest([FromRoute] string requestId)
        {
            var request = await _applicationDbContext.RequestForms
                        .Where(w => w.Id.Equals(requestId)).FirstOrDefaultAsync();
            if (request == null)
            {
                return NotFound("Request Not found");
            }

            request.IsApproved = true;

            _applicationDbContext.Update(request);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(new { message = "Approved", result = request });
        }
        // request decline

        [HttpPut("decline/{requestId}")]
        public async Task<IActionResult> DeclineRequest([FromRoute] string requestId)
        {
            var request = await _applicationDbContext.RequestForms
                        .Where(w => w.Id.Equals(requestId)).FirstOrDefaultAsync();
            if (request == null)
            {
                return NotFound("Request Not found");
            }

            request.IsRejected = true;

            _applicationDbContext.Update(request);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(new { message = "Approved", result = request });
        }

        [HttpPut("action/{requestId}")]
        public async Task<IActionResult> ActionRequest([FromRoute] string requestId)
        {
            var request = await _applicationDbContext.RequestForms
                        .Where(w => w.Id.Equals(requestId)).FirstOrDefaultAsync();
            if (request == null)
            {
                return NotFound("Request Not found");
            }

            request.IsApproved = true;

            _applicationDbContext.Update(request);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(new { message = "Approved", result = request });
        }

    }
}
