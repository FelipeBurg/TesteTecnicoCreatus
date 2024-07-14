namespace TesteTecnicoCreatus.Users;

public record UpdateUserRequest(string Name, string Email, string Password, int Level);
