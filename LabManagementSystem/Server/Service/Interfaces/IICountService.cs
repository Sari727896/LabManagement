
namespace Service.Interfaces
{
    public interface IICountService
    {
        public Task<int> GetClientId(string phone = null, string email = null);

    }
}
