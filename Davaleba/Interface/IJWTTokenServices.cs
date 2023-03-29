using Davaleba.Models;

namespace Davaleba.Interface
{
    public interface IJWTTokenServices
    {
        JWTTokens Authenticate(User users);
    }
}
