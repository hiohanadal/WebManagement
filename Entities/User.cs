﻿namespace WebManagement.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Cpf { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Gender { get; set; }

        public string Password { get; set; }

        public Guid UserGuid { get; set; } 
    }
}
