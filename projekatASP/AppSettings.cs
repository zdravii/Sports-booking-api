using Application.Jwt;

namespace projekatASP.API
{
    public class AppSettings
    {
        public string ConnString { get; set; }
        public string HangfireDb { get; set; }
        public bool SeedOnStart { get; set; }
        public IEnumerable<string> ApiKeys { get; set; }
        public JwtSettings JwtSettings { get; set; }
    }
}
