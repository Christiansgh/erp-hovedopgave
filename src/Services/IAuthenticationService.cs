namespace erp.Services;

public interface IAuthenticationService {
  bool Authenticate(string username, string password);
}
