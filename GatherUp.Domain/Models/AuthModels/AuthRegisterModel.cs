namespace GatherUp.Domain.Models.AuthModels;

public class AuthRegisterModel
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public bool IsConfirmed { get; set; }
    public string Picture { get; set; }
}
