namespace PancakeAuthBackend.Services {
    public interface IAccountService {
        Task<string> GetToken();
        Task<bool> SignIn(LoginDTO loginDetails);
    }
}
