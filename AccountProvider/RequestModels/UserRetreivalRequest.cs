namespace AccountProvider.RequestModels;

public class UserRetreivalRequest
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
}