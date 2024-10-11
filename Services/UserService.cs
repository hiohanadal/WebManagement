using WebManagement.Common;
using WebManagement.Entities;
using WebManagement.Models;
using WebManagement.Repositories;

namespace WebManagement.Services
{
    public class UserService
    {
        private string _connectionString;
        public UserService(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public LoginResult Login(string email, string password)
        {
            var result = new LoginResult();

            var userRepository = new UserRepository(_connectionString);
            var user = userRepository.GetUserByEmail(email);

            if (user != null)
            {                
                if (user.Password == password)
                {
                    result.success = true;
                    result.userGuid = user.UserGuid;
                    result.message = "Login efetuado com success.";
                }
                else
                {
                    result.success = false;
                    result.message = "Usuário ou password inválidos.";
                }
            }
            else
            {
                result.success = false;
                result.message = "Usuário ou password precisam ser cadastrados";
            }

            return result;

        }

        public RegistrationResult Registration(string name, string lasName, string phone, string email, string gender, string address, string cpf, string password)
        {
            var result = new RegistrationResult();

            var repository = new UserRepository(_connectionString);

            var userRepository = new UserRepository(_connectionString);
            var user = userRepository.GetUserByEmail(email);

            if (user != null)
            {
                result.success = false;
                result.message = "Usuário já possui cadastro.";
            }
            else
            {
                user = new User();
                user.Password = password;
                user.Name = name;
                user.LastName = lasName;
                user.Phone = phone;
                user.Email = email;
                user.Gender = gender;
                user.Address = address;
                user.Cpf = cpf;
                user.UserGuid = Guid.NewGuid();

                var affectedRows = repository.Insert(user);

                if (affectedRows > 0)
                {
                    result.success = true;
                    result.userGuid = user.UserGuid;

                }
                else
                {
                    result.success = false;
                    result.message = "Não foi possível inserir o usuário";

                }
            }

            return result;
        }

        public ForgotPasswordResult ForgotPassword(string email, string cpf)
        {
            var result = new ForgotPasswordResult();

            var userRepository = new UserRepository(_connectionString);
            var user = userRepository.GetUserByEmail(email);

            if (user == null)
            {
                result.success = false;
                result.message = "Usuário não cadastrado.";
            }
            else
            {
                result.success = true;
                var emailSender = new EmailSender();
                var subject = "Recuperação de Password";
                var body = "Sua password é " + user.Password;
                emailSender.Send(subject, body, user.Email);
            }

            return result;
        }

        public User GetUser(Guid userGuid)
        {
            var user = new UserRepository(_connectionString).GetByGuid(userGuid);

            return user;
        }
    }
}
