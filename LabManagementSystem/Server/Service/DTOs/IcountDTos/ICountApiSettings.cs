
namespace Service.DTOs
{
    public class ICountApiSettings
    {
        public string HostURL {  get; set; }
        public Credentials Credentials { get; set; }
    }

    public class Credentials 
    {
        public string Cid { get; set; }
        public string User { get; set; }
        public string Password { get; set; } 
    }

}
