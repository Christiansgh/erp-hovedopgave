namespace erp.Services;

public interface IAuthenticationService<T> {
    bool Authenticate(T login);
}
