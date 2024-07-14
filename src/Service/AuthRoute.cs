using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteTecnicoCreatus.Data; // Importe o namespace correto para o contexto do banco de dados

namespace TesteTecnicoCreatus.Service
{
    public class AuthRoute
    {
        private readonly AppDbContext _context;

        public AuthRoute(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Auth(string email, string password, CancellationToken ct)
        {
            try
            {
                // Consulta o usuário pelo email e senha
                var user = await _context.Users
                    .SingleOrDefaultAsync(u => u.Email == email && u.Password == password, ct);

                if (user == null)
                {
                    // Se não encontrar o usuário, retorna Unauthorized
                    return new UnauthorizedResult();
                }

                // Gera o token JWT para o usuário encontrado
                var token = TokenService.GenerateToken(user);

                // Retorna os dados, como por exemplo o token gerado
                return new JsonResult(new { token });
            }
            catch (Exception ex)
            {
                // Em caso de erro, retorna um resultado de erro interno
                return new StatusCodeResult(500); // Pode ser ajustado para retornar detalhes do erro, conforme necessário
            }
        }
    }
}