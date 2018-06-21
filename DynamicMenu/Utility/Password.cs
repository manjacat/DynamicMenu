using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Cryptography;
using BCrypt.Net;


namespace DynamicMenu.Utility
{
    public class Password
    {
        #region "Help and Validation"


        string LeadingKey = "0x";
        public Password()
        {
        }

        public byte[] CreateBinaryPwd(string password, string username)
        {

            //Encrypt the password
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedBytes = null;
            UTF8Encoding encoder = new UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(password + username));

            return hashedBytes;
        }

        public string GetEncrypt(string password, string username)
        {
            byte[] byteArray = CreateBinaryPwd(password, username.ToLower());
            return (LeadingKey + BitConverter.ToString(byteArray).Replace("-", ""));
        }

        public string RandomPwdGenerator(int numChar)
        {
            string listOfChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string randomPassword = "";
            Random r = new Random();
            int x = 0;
            for (x = 0; x <= numChar - 1; x += x + 1)
            {
                int position = Convert.ToInt32(Math.Floor(r.NextDouble() * 62));
                randomPassword += listOfChars.Substring(position, 1);
            }
            return randomPassword;
        }

        ///*********************************************************************
        ///
        /// PwdHasher.Encrypt() Method
        ///
        /// The Encrypt method encryts a clean string into a hashed string
        ///
        ///*********************************************************************
        public string Encrypt(string cleanString)
        {

            string hashedText = null;
            //Dim clearBytes As Byte

            Encoding enc = Encoding.Unicode;

            // Encode the entire string.

            byte[] bytes = enc.GetBytes(cleanString);
            hashedText = BitConverter.ToString(bytes);

            return hashedText;
        }








        #endregion




        private string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);


        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        public bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }



        public static string EncryptString(string Message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }
        public static string DecryptString(string Message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            return UTF8.GetString(Results);
        }
    }
}