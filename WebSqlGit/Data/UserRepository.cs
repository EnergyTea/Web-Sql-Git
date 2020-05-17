using Dapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WebSqlGit.Data.Interface;
using WebSqlGit.Data.Model;

namespace WebSqlGit.Data
{
    public class UserRepository : IUserInterface
    {
        readonly string connectionString;
        const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1; // default for Rfc2898DeriveBytes
        const int Pbkdf2IterCount = 1000; // default for Rfc2898DeriveBytes
        const int Pbkdf2SubkeyLength = 256 / 8; // 256 bits
        const int SaltSize = 128 / 8; // 128 bits

        public UserRepository(string conn)
        {
            connectionString = conn;
        }

        public void CreateUser(User user)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            string Login = user.Email;
            string Password = user.Email;
            db.Execute("INSERT INTO  Scripts (Login, Password) VALUES(@Login,  @Password )", new { Login, Password });
        }

        public ClaimsIdentity IdentityUser(User user)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            string Login = user.Email;
            string Password = user.Email;
            User AuthorizUser = db.Query<User>(
                "SELECT * FROM User WHERE Login = @Login AND Password = @Password", new { Login, Password }).FirstOrDefault();
            if (AuthorizUser != null && VerifyHashedPassword(Convert.FromBase64String(AuthorizUser.PasswordHash), user.PasswordHash))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, AuthorizUser.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, AuthorizUser.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }

        public byte[] HashPassword(string password, RandomNumberGenerator rng)
        {
            // Produce a version 2 (see comment above) text hash.
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);

            var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
            outputBytes[0] = 0x00; // format marker
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);
            return outputBytes;
        }

        private bool VerifyHashedPassword(byte[] hashedPassword, string password)
        {
            // We know ahead of time the exact length of a valid hashed password payload.
            if (hashedPassword.Length != 1 + SaltSize + Pbkdf2SubkeyLength)
            {
                return false; // bad size
            }

            byte[] salt = new byte[SaltSize];
            Buffer.BlockCopy(hashedPassword, 1, salt, 0, salt.Length);

            byte[] expectedSubkey = new byte[Pbkdf2SubkeyLength];
            Buffer.BlockCopy(hashedPassword, 1 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            // Hash the incoming password and verify it
            byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);

            return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
        }

        public User CheckUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
