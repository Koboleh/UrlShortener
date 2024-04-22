using AutoMapper;
using UrlShortener.DataAccess.Entities;
using UrlShortener.DTOs.Request;
using UrlShortener.DTOs.Response;
using UrlShortener.Interfaces;

namespace UrlShortener.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public UserService(IMapper mapper, ITokenService tokenService, IUserRepository userRepository)
    {
        _mapper = mapper;
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    public async Task<UserResponse?> UserLogin(LoginRequest request)
    {

        var user = await _userRepository.GetUserByUsernameAsync(request.Username);

        if (user == null)
        {
            return null;
        }

        var passwordCheck = VerifyPassword(request.Password, user.Password);
        if (!passwordCheck) return null;

        var accessToken = _tokenService.CreateToken(user);

        user.AccessToken = accessToken;
        await _userRepository.UpdateUser(user);

        var userResponse = _mapper.Map<User, UserResponse>(user);

        return userResponse;
    }

    private bool VerifyPassword(string inputPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
    }
}