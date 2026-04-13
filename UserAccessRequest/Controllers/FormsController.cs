using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAccessRequest.Data;
using UserAccessRequest.Model.Database;
using System.Security.Cryptography.Xml;
using UserAccessRequest.Model.Dto;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace UserAccessRequest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FormsController> _logger;  // ← Add generic type
        private readonly ApplicationDbContext _applicationDbContext;

        public FormsController(UserManager<Users> userManager, IConfiguration configuration, ILogger<FormsController> logger, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        // view forms by department
        [HttpGet("all")]
        public async Task<IActionResult> ListForm()
        {
            var forms = await _applicationDbContext.Forms
                .Select(s => new
                {
                    s.Id,
                    s.FormName,
                    s.FormDepartment,
                    Deployed  = s.IsActive,
                    Fields = s.FormFields != null ? s.FormFields.Count() : 0,
                    Requests = s.RequestForms != null ? s.RequestForms.Count() : 0,
                    s.FormAction,
                    actions = new List<object>{
                        new {method="get",url=$"Forms/get/{s.Id}",label="Detail"},
                        new {method="delete",url=$"Forms/delete/{s.Id}",label="Delete"},
                        new {method="Post",url=$"Request/create/{s.Id}",label="Request"}
                    }
                }).ToListAsync();
            return Ok(new { message = "", result = forms });
        }

        [HttpGet("department/{department}")]
        public async Task<IActionResult> ListFormByDepartment([FromRoute] string department)
        {
            var forms = await _applicationDbContext.Forms
                .Where(w => w.FormDepartment.Equals(department))
                .Select(s => new
                {
                    s.Id,
                    s.FormName,
                    s.FormDescription,
                    s.FormDepartment,
                    s.FormAction,
                    s.IsActive,
                    Fields = s.FormFields.Select(s => new
                    {
                        s.Field.Id,
                        s.Field.FiledName,
                        s.Field.FieldValue,
                        s.Field.FieldValueType,
                        s.Field.FieldType,
                        s.Field.FieldPlaceholder,
                        s.Field.FieldIsRequired,
                    }).ToList()
                }).ToListAsync();
            return Ok(new { message = "", result = forms });
        }

        [HttpGet("get/{formId}")]
        public async Task<IActionResult> ListFormById([FromRoute] string formId)
        {
            var forms = await _applicationDbContext.Forms
                .Where(w => w.Id.Equals(formId))
                .Select(s => new
                {
                    s.Id,
                    title = s.FormName,
                    description = s.FormDescription,
                    department = s.FormDepartment,
                    Sections = s.FormFields
                    .GroupBy(g=> g.Field.FieldSection)
                    .Select(s => new
                    {   
                        id= Guid.NewGuid().ToString(),
                        name = s.FirstOrDefault().Field.FieldSection ?? "Section",
                        fields = s.Select(s=> new
                        {
                            s.Field.Id,
                            label = s.Field.FiledName,
                            options = s.Field.FieldValue ,
                            valueType = s.Field.FieldValueType,
                            type = s.Field.FieldType,
                            placeholder = s.Field.FieldPlaceholder,
                            required = s.Field.FieldIsRequired,
                            s.Field.FieldSection
                        }
                     )
                    }).ToList(),
                    s.FormAction,
                    actions = new List<object>{
                        new {method="put",url=$"Forms/approve/{s.Id}",label="Approve", description="Approve form for deployment"},
                        new {method="delete",url=$"Forms/delete/{s.Id}",label="Delete", description="Delete form, form can only be deleted if there has not been any request"},
                        new {method="put",url=$"Forms/update/{s.Id}",label="Update", description="Update/Modify, "}
                    }
                }).ToListAsync();
            return Ok(new { message = "", result = forms });
        }

        [HttpGet("mine")]
        public async Task<IActionResult> ListFormByCreator()
        {
                var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
                var forms = _applicationDbContext.Forms.Where(w => w.CreatedBy.Equals(id)).Select(s => new
                {
                    s.Id,
                    s.FormName,
                    s.FormDescription,
                    s.FormDepartment,
                    s.FormAction,
                    s.IsActive,
                    Fields = s.FormFields.Select(s => new
                    {
                        s.Field.Id,
                        s.Field.FiledName,
                        s.Field.FieldValue,
                        s.Field.FieldValueType,
                        s.Field.FieldType,
                        s.Field.FieldPlaceholder,
                        s.Field.FieldIsRequired,
                    }).ToList()
                    , Actions = new List<object>
                    {
                    new {method="GET",url="",}
                    }
                });
                return Ok(new { message = "", result = forms });
            }
        // view forms by creator

        // create form
        [HttpPost("create")]
        public async Task<IActionResult> CreateForm([FromBody] DataObjects.CreateForm model)
        {
            var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            var department = User?.FindFirst("department")?.Value ?? "";
            var uid = Guid.NewGuid().ToString();
            var sections = model.sections.Select(s => s.Fields).ToList();

            Form form = new Form
            {
                Id = uid,
                FormName = model.Name,
                FormDescription = model.Description,
                FormDepartment = department,
                FormAction = model.Actions,
                IsActive = model.IsActive,
                FormFields = model.sections.SelectMany(section => section.Fields.Select(field => new FormFields
                {
                    FormId = uid,
                    FieldId = Guid.NewGuid().ToString(), // or should this come from field?
                    Field = new Fields
                    {
                        Id = Guid.NewGuid().ToString(),
                        FiledName = field.label,
                        DisplayName = field.displayName,
                        FieldType = field.type,
                        FieldValue = field.options,
                        FieldValueType = field.valueType,
                        FieldIsRequired = field.isRequired,
                        FieldPlaceholder = field.placeholder,
                        FieldSection = section.name // If you need to track which section
                    }
                })).ToList()
            };

            await _applicationDbContext.Forms.AddAsync(form);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(new { message = "success", model });
        }
        [HttpPut("update/{formId}")]
        public async Task<IActionResult> UpdateForm([FromRoute] string formId,[FromBody] DataObjects.UpdateCreateForm model)
        {
            var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            var department = User?.FindFirst("department")?.Value ?? "";
            var uid = Guid.NewGuid().ToString();
            var sections = model.sections.Select(s => s.Fields).ToList();

            var form = await _applicationDbContext.Forms.FindAsync(formId);
            if (form == null)
            {
                return NotFound($"Form with ID {formId} not found");
            }

            form.FormName = model.Name;
            form.FormDescription = model.Description;
            form.FormDepartment = department;
            form.FormAction = model.Actions;
            form.IsActive = model.IsActive;

            // Update Fields
            foreach (var section in model.sections)
            {
                foreach (var field in section.Fields)
                {
                    var existingField = await _applicationDbContext.Fields.FindAsync(field.id);

                    if (existingField == null)
                    {
                        // Add new field
                        var newField = new Fields
                        {
                            Id = field.id,
                            FiledName = field.label,
                            DisplayName = field.displayName,
                            FieldType = field.type,
                            FieldValue = field.options,
                            FieldValueType = field.valueType,
                            FieldIsRequired = field.isRequired,
                            FieldPlaceholder = field.placeholder,
                            FieldSection = section.name
                        };

                        await _applicationDbContext.Fields.AddAsync(newField);

                        // Add FormField relationship
                        var formField = new FormFields
                        {
                            FormId = formId,
                            FieldId = field.id
                        };
                        await _applicationDbContext.formFields.AddAsync(formField);
                    }
                    else
                    {
                        // Update existing field
                        existingField.FiledName = field.label;
                        existingField.DisplayName = field.displayName;
                        existingField.FieldType = field.type;
                        existingField.FieldValue = field.options;
                        existingField.FieldValueType = field.valueType;
                        existingField.FieldIsRequired = field.isRequired;
                        existingField.FieldPlaceholder = field.placeholder;
                        existingField.FieldSection = section.name;

                        _applicationDbContext.Fields.Update(existingField);
                    }
                }
            }

            try
            {
                await _applicationDbContext.SaveChangesAsync();
                return Ok(new { message = "Form updated successfully", formId });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(new { error = "Concurrency conflict", details = ex.Message });
            }
        }
        // approve form

        [HttpPut("approve/{formId}")]
            public async Task<IActionResult> ApproveForm([FromRoute] string formId)
            {
                var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
                var department = User?.FindFirst("department")?.Value ?? "";
                var uid = Guid.NewGuid().ToString();

                var form = await _applicationDbContext.Forms
                    .Where(w => w.FormDepartment.Equals(department) && w.Id.Equals(formId) && !w.IsActive)
                    //.Where(w =>w.Id.Equals(formId))
                    .FirstOrDefaultAsync();
                if (form == null)
                {
                    return NotFound("The recored does't exsit or is active!");
                }
                form.IsActive = true;
                _applicationDbContext.Update(form);
                await _applicationDbContext.SaveChangesAsync();
                return Ok(new { message = "success" });
            }

            [HttpDelete("delete/{formId}")]
            public async Task<IActionResult> DeleteForm([FromRoute] string formId)
            {
                var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
                var department = User?.FindFirst("department")?.Value ?? "";

                var form = await _applicationDbContext.Forms
                    // w.CreatedBy.Equals(id) &&
                    .Where(w => w.Id.Equals(formId))
                    .ExecuteDeleteAsync();
                if (form < 1)
                {
                    return NotFound("The recored does't exsit or is active!");
                }
                await _applicationDbContext.SaveChangesAsync();
                return Ok(new { message = "success" });
            }

        }
    }