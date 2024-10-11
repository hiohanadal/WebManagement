using WebManagement.Entities;
using MySql.Data.MySqlClient;

namespace WebManagement.Repositories
{
        public class UserRepository
    {
        private string _connectionString;
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int Insert(User user)
        {
            var cnn = new MySqlConnection(_connectionString);

            var cmd = new MySqlCommand();

            cmd.Connection = cnn;

            cmd.CommandText = @"INSERT INTO  user(name, lastName, phone, email, gender, password, cpf, userGuid)VALUES
(@name, @lastName, @phone, @email, @gender, @password, @cpf, @userGuid);";

            cmd.Parameters.AddWithValue("name", user.Name);
            cmd.Parameters.AddWithValue("lastName", user.LastName);
            cmd.Parameters.AddWithValue("phone", user.Phone);
            cmd.Parameters.AddWithValue("email", user.Email);
            cmd.Parameters.AddWithValue("gender", user.Gender);
            cmd.Parameters.AddWithValue("password", user.Password);
            cmd.Parameters.AddWithValue("cpf", user.Cpf);
            cmd.Parameters.AddWithValue("userGuid", user.UserGuid);

            cnn.Open();

            var affectedRows = cmd.ExecuteNonQuery();
            cnn.Close();

            return affectedRows;
        }

        public User GetUserByEmail(string email)
        {
            User user = null;

            var cnn = new MySqlConnection(_connectionString);
            var cmd = new MySqlCommand();
            cmd.Connection = cnn;

            cmd.CommandText = "SELECT * FROM user WHERE email=@email";

            cmd.Parameters.AddWithValue("email", email);

            cnn.Open();

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                user = new User();
                user.Name = reader["name"].ToString();
                user.LastName = reader["lastName"].ToString();
                user.Phone = reader["phone"].ToString();
                user.Email = reader["email"].ToString();
                user.Cpf = reader["cpf"].ToString();
                user.Password = reader["password"].ToString();
                user.Gender = reader["gender"].ToString();
                user.UserGuid = new Guid(reader["userGuid"].ToString()); 
            }

            cnn.Close();

            return user;           
        }

        public User GetByGuid(Guid userGuid)
        {
            User user = null;

            var cnn = new MySqlConnection(_connectionString);

            var cmd = new MySqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = "SELECT * FROM user WHERE userGuid = @userGuid";
            cmd.Parameters.AddWithValue("userGuid", userGuid);
            cnn.Open();
            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                user = new User();
                user.Name = reader["name"].ToString();
                user.LastName = reader["lastName"].ToString();
                user.Phone = reader["phone"].ToString();
                user.Email = reader["email"].ToString();
                user.Cpf = reader["cpf"].ToString();
                user.Password = reader["password"].ToString();
                user.Gender = reader["gender"].ToString();
                user.UserGuid = new Guid(reader["userGuid"].ToString());
            }

            cnn.Close();

            return user;
        }
    }


}
