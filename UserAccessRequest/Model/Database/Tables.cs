using Microsoft.AspNetCore.Identity;
using Npgsql.Internal.Postgres;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAccessRequest.Model.Database
{
    public class Users : IdentityUser
    {
        public DateTime LoggedAt { get; set; } = DateTime.UtcNow;
        public string Autheticator { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }

    public class Form
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FormName { get; set; } = string.Empty;
        public string FormDescription { get; set; } = string.Empty;
        public string FormDepartment { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string FormAction { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreateOn { get; set; } = DateTime.UtcNow;
        public virtual ICollection<RequestForms>? RequestForms { get; set; }
        public virtual ICollection<FormFields>? FormFields { get; set; }
    }

    public class Fields
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FiledName { get; set; } = string.Empty;// form submition name
        public string DisplayName { get; set; } = string.Empty; // label
        public string FieldType { get; set; } = string.Empty; // datatype
        public string FieldValue { get; set; } = string.Empty;
        public string FieldValueType { get; set; } = string.Empty;
        public bool FieldIsRequired { get; set; } = false;
        public string FieldPlaceholder { get; set; } = string.Empty;
        public string FieldSection { get; set; } = string.Empty;
        public virtual ICollection<RequestForms>? RequestForms { get; set; }
        public virtual ICollection<FormFields>? FieldForms { get; set; }
        public virtual ICollection<RequestFormsResult>? ResultFields { get; set; }
    }

    public class FormFields
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FormId { get; set; }
        public string FieldId { get; set; }
        public bool Inactive { get; set; }

        [ForeignKey("FormId")]
        public virtual Form Form { get; set; }

        [ForeignKey("FieldId")]
        public virtual Fields Field { get; set; }
    }

    public class RequestForms
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required] public string FormId { get; set; }
        public string? RequestStatus { get; set; } = "pending"; // pending,approved,rejected
        public bool IsActive { get; set; } = false;
        public bool IsRejected { get; set; } = false;
        public bool IsApproved { get; set; } = false;
        public DateTime? ApprovedAt { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? RejectedAt { get; set; }
        public string? RejectedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }

        [ForeignKey("FormId")]
        public virtual Form Form { get; set; }
        public virtual ICollection<RequestFormsResult>? RequestFormsResult { get; set; }
    }

    public class RequestFormsResult
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required] public string RequestFormId { get; set; }
        [Required] public string FieldId { get; set; }
        public string? FieldValue { get; set; }
        
        [ForeignKey("RequestId")]
        public virtual RequestForms RequestForms { get; set; }

        [ForeignKey("FieldId")]
        public virtual Fields Fields { get; set; }

    }
}
