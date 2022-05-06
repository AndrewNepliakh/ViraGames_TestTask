using System;

namespace Managers
{
    public interface IUser
    {
        float Timer {get; set;}
        int Health {get; set;}
        int Coins {get; set;}
    }
}