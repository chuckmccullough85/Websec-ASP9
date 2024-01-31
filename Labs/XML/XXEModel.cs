using Microsoft.AspNetCore.Http;

namespace AcmeWebsite.Models
{
    public class XXEModel
    {
        public XXEModel()
        {
            ParseDtd = true;
            MaxChars = long.MaxValue;
            ElapsedTime = 0;
        }
        public IFormFile File { get; set; }
        public bool ParseDtd { get; set; }
        public long MaxChars { get; set; }
        public double ElapsedTime { get; set; }
    }
}
