﻿using Catalog.Auth.Domain.Configurations;
using Catalog.Auth.Domain.Repositories;
using Catalog.Auth.Domain.Requests;
using Catalog.Auth.Domain.Responses;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Auth.Domain.Services
{
	public class UserService : IUserService
	{
		private readonly AuthenticationSettings _authenticationSettings;
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository,
			IOptions<AuthenticationSettings> authenticationSettings)
		{
			_userRepository = userRepository;
			_authenticationSettings = authenticationSettings.Value;
		}

		public async Task<UserResponse> GetUserAsync(GetUserRequest request, CancellationToken cancellationToken = default)
		{
			var response = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

			return new UserResponse
			{
				Name = response.Name,
				Email = response.Email
			};
		}

		public async Task<TokenResponse> SignInAsync(SignInRequest request, CancellationToken cancellationToken = default)
		{
			bool isAuthenticated = await _userRepository
				.AuthenticateAsync(request.Email, request.Password,
				cancellationToken);

			return !isAuthenticated ? null : new TokenResponse
			{
				Token = GenerateSecurityToken(request)
			};
		}

		public async Task<UserResponse> SignUpAsync(SignUpRequest request, CancellationToken cancellationToken = default)
		{
			var user = new Entities.User
			{
				Email = request.Email,
				UserName = request.Email,
				Name = request.Name
			};

			bool result = await _userRepository.SignUpAsync(user, request.Password, cancellationToken);

			return !result ? null : new UserResponse
			{
				Name = request.Name,
				Email = request.Email
			};
		}

		private string GenerateSecurityToken(SignInRequest request)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_authenticationSettings.Secret);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Email, request.Email)
				}),
				Expires = DateTime.UtcNow.AddDays(_authenticationSettings.ExpirationDays),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
					SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
