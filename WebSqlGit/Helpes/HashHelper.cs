using System.Security.Cryptography;
using System.Text;

namespace WebSqlGit.Helpes
{
    public static class HashHelper
    {
        public static string TextToHash( string text )
        {
            SHA1 sh = SHA1.Create();
            StringBuilder hash = new StringBuilder();
            byte[] bytes = Encoding.UTF8.GetBytes( text );
            byte[] b = sh.ComputeHash( bytes );
            foreach ( byte a in b )
            {
                var h = a.ToString( "x2" );
                hash.Append( h );
            }

            return hash.ToString();
        }
    }
}
