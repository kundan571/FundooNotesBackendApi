using FundooNotes.Context;
using FundooNotes.Entity;
using FundooNotes.Interface;
using FundooNotes.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reactive.Subjects;
using System.Security.Claims;
using System.Text;

namespace FundooNotes.Service;

public class UserServices : IUser
{
    private readonly IConfiguration _configuration;
    private readonly FundooContext _db;
    public UserServices(IConfiguration configuration,FundooContext db)
    {
        _configuration = configuration;
        _db = db;
        _db.Database.EnsureCreated();
    }
    public User Register(UserRegistration userModel)
    {
        User newUser = new User()
        {
            EmailId = userModel.Email.ToLower(),
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
            Password = userModel.Password,
            RegisterAt = DateTime.Now,
        };

        _db.Users.Add(newUser);
        int result = _db.SaveChanges();
        if (result > 0)
        {
            return newUser;
        }
        else
        {
            return null;
        }
        
    }
    public string LogIn(string email, string password)
    {
        email = email.ToLower();
        User someUser = _db.Users.FirstOrDefault(x => x.EmailId == email && x.Password == password);
        if(someUser != null)
        {
            string token = GenerateToken(someUser.EmailId);
            return token;
        }
        else
        {
            return null;
        }
    }
    public bool ForgetPassWord(string email)
    {
        email = email.ToLower();
        User someUser = _db.Users.FirstOrDefault(x => x.EmailId == email);
        if (someUser != null)
        {
            string token = GenerateToken(someUser.EmailId);
            MessageService msgService = new MessageService(_configuration);
            msgService.SendMessageToQueue(someUser.EmailId, token);
            return true;
        }
        else 
        {
            return false;
        }
    }
    public bool ResetPassword(string emailId, string newPassword, string confirmPassword)
    {

        User someUser = _db.Users.FirstOrDefault(x => x.EmailId == emailId);
        if (someUser != null && newPassword == confirmPassword)
        {
            someUser.Password = newPassword;
            _db.Users.Update(someUser);
            _db.SaveChanges();
            return true;
        }
        else
        {
            return false;
        }
    }

    private string GenerateToken(string email)
    {
        Byte[] key = Encoding.ASCII.GetBytes(_configuration["JWT-Key"]);
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, email),
            }),
            Expires = DateTime.UtcNow.AddMinutes(60),
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);

    }
}
