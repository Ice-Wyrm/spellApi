﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ORM_example.Entity
{
    public class AuthOptions
    {
        public const string ISSUER = "Denis"; // издатель токена
        public const string AUDIENCE = "Client"; // потребитель токена
        const string KEY = "longKeyNeededInOrderToAvoidException";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
