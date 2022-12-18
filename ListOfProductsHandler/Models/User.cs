using Microsoft.Build.Framework;
using Newtonsoft.Json;

namespace ListOfProductsHandler.Models
{
    public class User
    {
        [Required]
        [JsonProperty("userName")]
        public string userName { get; set; } = string.Empty;

        [Required]
        [JsonProperty("userPassword")]
        public string userPassword { get; set; } = string.Empty;
    }
}
