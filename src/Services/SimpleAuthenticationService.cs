namespace erp.Services;

public class SimpleAuthenticationService : IAuthenticationService {
  private readonly string _username = "Hovedopgave";
  private readonly string _password = "TtXr@oA7M.PaNxCmsvL2WBMw";

  public bool Authenticate(string username, string password) {
    return string.Equals(_username, username) && string.Equals(_password, password);
  }
}
