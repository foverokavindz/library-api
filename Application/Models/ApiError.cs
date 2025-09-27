using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace library_api.Application.Models
{
    public class ApiError
    {
        public string Message { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public Dictionary<string, object>? Details { get; set; }
        public List<ValidationError>? ValidationErrors { get; set; }
    }
}