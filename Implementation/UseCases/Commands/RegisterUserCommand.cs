using DataAccess;
using Domain.Entities;
using FluentValidation;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Collections.Generic;
using Application.Exceptions;
using Application.Commands;
using Implementation.Validators.Auth;
using Application.DTO.Auth;

namespace Implementation.UseCases.Commands
{
    public class RegisterUserCommand :  UseCase,IRegisterUserCommand
    {
        public string Id => "register-user";
        public string Name => "Register new user";
        private RegisterUserValidator _validator;

        public RegisterUserCommand(RegisterUserValidator validator,DataBaseContext _db) : base(_db)
        {
            _validator = validator;
        }

        public int Execute(RegisterUserDTO request)
        {
            _validator.ValidateAndThrow(request);

            if (_db.Users.Any(u => u.Email == request.Email))
                throw new ConflictException("Email already in use");

            Role? role = _db.Roles.FirstOrDefault(r => r.Name == "User");

            if (role == null)
                throw new InternalServerException("Server error,contact the admin");

            User newUser = new User();
            newUser.FirstName = request.FirstName;
            newUser.LastName = request.LastName;
            newUser.Email = request.Email;
            newUser.PasswordHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(request.Password)));
            newUser.RoleId = role.Id;
            newUser.IsActive = true;

            _db.Users.Add(newUser);
            _db.SaveChanges();

            return newUser.Id;
        }
    }
}
