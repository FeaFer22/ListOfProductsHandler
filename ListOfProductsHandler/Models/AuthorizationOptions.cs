using Microsoft.Build.Framework;
using Newtonsoft.Json;

namespace ListOfProductsHandler.Models
{
    public record AuthorizationOptions
    {
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
