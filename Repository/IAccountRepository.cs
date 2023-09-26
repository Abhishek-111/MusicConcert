using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicConcert.Models;
using Microsoft.AspNetCore.Identity;

namespace MusicConcert.Repository
{
    public interface IAccountRepository
    {
        
        Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);
        Task<SignInResult> PasswordSignInAsync(SignInModel signInModel);

        Task SignOutAsync();
    }
}
