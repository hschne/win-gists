using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace GistClient.FileSystem
{
    public static class StringProtector
    {
        private static readonly byte[] entropy = Encoding.UTF8.GetBytes("xcva123cdd");

        public static string Encrypt(this string input, DataProtectionScope scope = DataProtectionScope.CurrentUser){
            byte[] clearBytes = Encoding.UTF8.GetBytes(input);
            byte[] encryptedBytes = ProtectedData.Protect(clearBytes, entropy, scope);
            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(this string encryptedText,
            DataProtectionScope scope = DataProtectionScope.CurrentUser){
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] clearBytes = ProtectedData.Unprotect(encryptedBytes, entropy, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(clearBytes);
        }

        public static SecureString ToSecureString(string input){
            var secure = new SecureString();
            foreach (char c in input){
                secure.AppendChar(c);
            }
            secure.MakeReadOnly();
            return secure;
        }

        public static string ToInsecureString(SecureString input){
            string returnValue = string.Empty;
            IntPtr ptr = Marshal.SecureStringToBSTR(input);
            try{
                returnValue = Marshal.PtrToStringBSTR(ptr);
            }
            finally{
                Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }
    }
}