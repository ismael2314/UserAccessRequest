namespace UserAccessRequest.Model.Dto
{
    public class DataObjects
    {
       public class CreateForm
        {
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public bool IsActive { get; set; } = true;
            public string Actions { get; set; } = string.Empty;
            public List<FormFieldSection> sections { get; set; } = new List<FormFieldSection>();
        }
        public class FormFieldSection
        {
            public string name { get; set; }
            public List<CreateFormField> Fields { get; set; } = new List<CreateFormField>();

        }
        public class CreateFormField
        {
            public string label { get; set; } = string.Empty;// form submition name
            public string displayName { get; set; } = string.Empty; // label
            public string type { get; set; } = string.Empty; // datatype
            public string options { get; set; } = string.Empty;
            public string valueType { get; set; } = string.Empty;
            public bool isRequired { get; set; } = false;
            public string placeholder { get; set; } = string.Empty;
        }
        public class UpdateFromField:CreateFormField
        {
            public string id { get; set; } = string.Empty; 
        }
        public class UpdateFormFieldSection
        {
            public string name { get; set; }
            public List<UpdateFromField> Fields { get; set; } = new List<UpdateFromField>();

        }
        public class UpdateCreateForm
        {
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public bool IsActive { get; set; } = true;
            public string Actions { get; set; } = string.Empty;
            public List<UpdateFormFieldSection> sections { get; set; } = new List<UpdateFormFieldSection>();
        }
        public class CreateRequest
        {
            public string formId { get; set; }
            public List<CreateRequestField> Field {get;set;}
        }
        public class CreateRequestField
        {
            public string Id { get; set; }
            public string value { get; set; }
        }

    }
}
