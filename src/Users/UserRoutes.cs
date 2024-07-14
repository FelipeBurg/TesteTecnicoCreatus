using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using TesteTecnicoCreatus.Data;
using TesteTecnicoCreatus.Service;

namespace TesteTecnicoCreatus.Users
{
    public static class UserRoutes
    {
        public static void MapUserRoutes(this WebApplication app) //this faz com que se torna um método de extensão
        {
            
            
            var routeUsers = app.MapGroup("/users");
            
            routeUsers.MapPost("/login", PostAuthRoute);
            routeUsers.MapPost("", CreateUserAsync);
            routeUsers.MapGet("", GetUsersAsync);
            routeUsers.MapGet("{id:guid}", GetSpecificUser);
            routeUsers.MapPut("{id:guid}", UpdateUserAsync);
            routeUsers.MapDelete("{id:guid}", DeleteUserAsync);
            routeUsers.MapGet("/report", GenerateReport).RequireAuthorization("Admin");
        }

        private static async Task<IResult> PostAuthRoute(string email, string password, AppDbContext context, CancellationToken ct)
        {
            var user = await context.Users
                .SingleOrDefaultAsync(u => u.Email == email && u.Password == password, ct);
            if (user == null)
            {
                return Results.NotFound("Email or Password invalid");
            }
            
            var token = TokenService.GenerateToken(user);
            return Results.Ok(new { Token = token });
        }
        private static async Task<IResult> CreateUserAsync(UserRequest request, AppDbContext context, CancellationToken ct)
        {
            var existingUser = await context.Users.AnyAsync(user => user.Email == request.Email,ct);
            if (existingUser)
            {
                return Results.Conflict("Email já cadastrado");
            }
            
            var newUser = new User(request.Name, request.Email, request.Password, request.Level);

            await context.Users.AddAsync(newUser, ct);
            await context.SaveChangesAsync(ct);

            var userReturn = new UserDetailsDto(newUser.Id, newUser.Name, newUser.Email, newUser.Level);
            return Results.Ok(userReturn);
        }
        private static async Task<IResult> GetUsersAsync(AppDbContext context, CancellationToken ct)
        {
            var users = await context.Users
                .Where(user => user.IsActive)
                .Select(user => new UserDetailsDto(user.Id, user.Name, user.Email, user.Level))
                .ToListAsync(ct);
            return Results.Ok(users);
        }

        private static async Task<IResult> GetSpecificUser(Guid id, AppDbContext context, CancellationToken ct)
        {
            var user = await context.Users
                .FindAsync(id);
            if (user == null)
            {
                return Results.NotFound();
            }

            var newSpecificUser =  new UserDetailsDto(user.Id, user.Name, user.Email, user.Level);
            return Results.Ok(newSpecificUser);

        }
        private static async Task<IResult> UpdateUserAsync(Guid id, UpdateUserRequest request, AppDbContext context, CancellationToken ct)
        {
            var user = await context.Users.SingleOrDefaultAsync(user => user.Id == id, ct);
            if (user == null)
            {
                return Results.NotFound("Usuário não encontrado");
            }
            user.UpdateUser(request.Name, request.Email, request.Password, request.Level);
            await context.SaveChangesAsync(ct);
            return Results.Ok(new UserDetailsDto(user.Id, user.Name, user.Email, user.Level));
        }

        private static async Task<IResult> DeleteUserAsync(Guid id, AppDbContext context, CancellationToken ct)
        {
            var user = await context.Users
                .SingleOrDefaultAsync(user => user.Id == id, ct);
            if (user == null)
            {
                return Results.NotFound();
            }

            user.DeleteUser();
            await context.SaveChangesAsync(ct);
            return Results.Ok(); 
        }

        private static async Task<IResult> GenerateReport(AppDbContext context, CancellationToken ct, ClaimsPrincipal claimUser)
        {
            // Verifica se o usuário está autenticado
            if (!claimUser.Identity.IsAuthenticated)
            {
                return Results.Unauthorized();
            }

            // Verifica se o usuário tem permissão de Admin
            var isAdmin = claimUser.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
            if (!isAdmin)
            {
                return Results.Forbid();
            }

            // Busca todos os usuários ativos
            var users = await context.Users
                .Where(user => user.IsActive)
                .Select(user => new UserDetailsDto(user.Id, user.Name, user.Email, user.Level))
                .ToListAsync(ct);

            // Gera o relatório em PDF
            var title = "Relatório de Usuários";
            var relatory = new Relatory(title, users);
            var pdfBytes = relatory.GeneratePdf();

            return Results.File(pdfBytes, "application/pdf", "Relatorio.pdf");
        }


    }
}