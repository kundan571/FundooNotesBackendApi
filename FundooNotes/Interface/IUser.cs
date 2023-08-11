using FundooNotes.Entity;
using FundooNotes.Models;

namespace FundooNotes.Interface;

public interface IUser
{
    string LogIn(string email,  string password);
    User Register(UserRegistration userModel);
    bool ForgetPassWord(string email);
    bool ResetPassword(string emailId, string newPassword, string confirmPassword);
}
