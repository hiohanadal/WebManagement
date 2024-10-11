using Microsoft.AspNetCore.Mvc;
using WebManagement.Models;
using WebManagement.Services;


namespace WebManagement.Controllers
{
    [Route("api/user")]

    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("login")]
        [HttpPost]
        public LoginResult Login(LoginRequest request)
     
        {
            var result = new LoginResult();

            if (request == null)
            {
                result.success = false;
                result.message = "Pàrâmetro request nulo";
            }

            else if (request.email == "")
            {
                result.success = false;
                result.message = "E-mail obrigatório";
            }

            else if (request.password == "")
            {
                result.success = false;
                result.message = "Password obrigatória";
            }
            else
            {
                var connectionString = _configuration.GetConnectionString("cursoRenatoGavaDb"); //TALVEZ TENHA QUE ARRUMAR AQUI O BANCO DE DADOS
                var userService = new UserService(connectionString);
                result = userService.Login(request.email, request.password);                
            }

            return result;
        }

        [Route("registration")]
        [HttpPost]
        public RegistrationResult Registration(RegistrationRequest request)
        {
            var result = new RegistrationResult();

            if (request == null ||
                    string.IsNullOrWhiteSpace(request.name) ||
                    string.IsNullOrWhiteSpace(request.lastName) ||
                    string.IsNullOrWhiteSpace(request.phone) ||
                    string.IsNullOrWhiteSpace(request.address) ||
                    string.IsNullOrWhiteSpace(request.gender) ||
                    string.IsNullOrWhiteSpace(request.cpf) ||
                    string.IsNullOrWhiteSpace(request.email) ||
                    string.IsNullOrWhiteSpace(request.password))
            {

                result.success = false;
                result.message = "Preencha todos os campos.";

                return result;

            }

            else
            {
                var connectionString = _configuration.GetConnectionString("cursoRenatoGavaDb"); //ARRUMAR BANCO DE DADOS AQUI
                var usuarioService = new UserService(connectionString);
                result = usuarioService.Registration(request.name, request.lastName, request.phone, request.email, request.gender, request.address, request.cpf, request.password);            
            }
            return result;

        }

        [Route("forgotPassword")]
        [HttpPost]
        public ForgotPasswordResult ForgotPassword(ForgotPasswordRequest request)
        {
            var result = new ForgotPasswordResult();

            if (request == null ||
                string.IsNullOrWhiteSpace(request.email) ||
                string.IsNullOrWhiteSpace(request.cpf))
            {
                result.success = false;
                result.message = "Todos os campos devem ser preenchidos.";

                return result;
            }

            else
            {
                var connectionString = _configuration.GetConnectionString("cursoRenatoGavaDb"); //ARRUMAR BANCO DE DADOS AQUI
                var usuarioService = new UserService(connectionString);
                result = usuarioService.ForgotPassword(request.email, request.cpf);                
            }
            return result;
        }

        [HttpGet]
        [Route("getUser")]
        public GetUserResult GetUser(Guid userGuid)
        {
            var result = new GetUserResult();

            if (userGuid == null)
            {
                result.message = "Guid vazio.";
            }

            else
            {
                var connectionString = _configuration.GetConnectionString("cursoRenatoGavaDb"); //ARRUMAR BANCO DE DADOS AQUI

                var user = new UserService(connectionString).GetUser(userGuid);

                if (user == null)
                {
                    result.message = "Usuário não existe.";
                }
                else
                {
                    result.success = true;
                    result.name = user.Name;
                }
            }
            return result;
        }

    }
}
