using Application;
using Application.Commands;
using Application.DTO.Auth;
using Application.Exceptions;
using Application.Jwt;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators.Auth;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Implementation.UseCases.Commands
{
    public class LoginUserCommand : UseCase, ILoginUserCommand
    {
        private readonly LoginUserValidator _validator;
        private readonly IJwtHandler _jwtHandler;
        public LoginUserCommand(DataBaseContext _db, LoginUserValidator validator, IJwtHandler jwtHandler) : base(_db)
        {
            _validator = validator;
            _jwtHandler = jwtHandler;
        }

        public string Id => "login-user";

        public string Name => "Login user";

        public LoginResponseDTO Execute(LoginUserDTO request)
        {
            _validator.ValidateAndThrow(request);

            User? user = _db.Users.FirstOrDefault(u => u.Email == request.Email);

            if (user == null || user.PasswordHash != Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(request.Password))))
            {
                throw new BadRequestException("Email or password are incorrect");
            }

            var jwt = _jwtHandler.MakeToken(user);

            LoginResponseDTO response = new LoginResponseDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role.Name,
                UserId = user.Id,
                Token = jwt.Token,
                RefreshToken = jwt.RefreshToken
            };

            return response;
        }
    }
}
