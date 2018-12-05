using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Encryption
    {

        public static string HashPassword(string password)
        {
            
            byte[] myPasswordAsBytes = Encoding.UTF8.GetBytes(password);

            var myalg = SHA512.Create();
            byte[] digestAsBytes = myalg.ComputeHash(myPasswordAsBytes);


            return Convert.ToBase64String(digestAsBytes);

        }


        

        public static string SymmetricEncrypt(string input)
        {
           
            Rijndael myAlg = Rijndael.Create();

            byte[] inputAsBytes = Encoding.UTF32.GetBytes(input);

            string password = ConfigurationManager.AppSettings["password"];

            Rfc2898DeriveBytes myKeyGenerator = new Rfc2898DeriveBytes(password,
                new byte[] { 48, 23, 1, 07, 200, 204, 45, 145, 123 });

            myAlg.Key = myKeyGenerator.GetBytes(myAlg.KeySize / 8);
            myAlg.IV = myKeyGenerator.GetBytes(myAlg.BlockSize / 8);

            MemoryStream msInput = new MemoryStream(inputAsBytes);

            CryptoStream cs = new CryptoStream(msInput, myAlg.CreateEncryptor(), CryptoStreamMode.Read);

            MemoryStream msOutput = new MemoryStream();

            cs.CopyTo(msOutput);

            

            return Convert.ToBase64String(msOutput.ToArray());
        }




        public static string EncryptQueryString(string input)
        {
            string cipher = SymmetricEncrypt(input);

            cipher = cipher.Replace('/', '|');
            cipher = cipher.Replace('+', '!');
            cipher = cipher.Replace('=', '$');

            return cipher;
        }

        public static string SymmetricDecrypt(string input)
        {
            
            Rijndael myAlg = Rijndael.Create();

            byte[] inputAsBytes = Convert.FromBase64String(input);

            string password = ConfigurationManager.AppSettings["password"];

            Rfc2898DeriveBytes myKeyGenerator = new Rfc2898DeriveBytes(password,
                new byte[] { 48, 23, 1, 07, 200, 204, 45, 145, 123 });

            myAlg.Key = myKeyGenerator.GetBytes(myAlg.KeySize / 8);
            myAlg.IV = myKeyGenerator.GetBytes(myAlg.BlockSize / 8);

            
            MemoryStream msInput = new MemoryStream(inputAsBytes);

            CryptoStream cs = new CryptoStream(msInput, myAlg.CreateDecryptor(), CryptoStreamMode.Read);

            MemoryStream msOutput = new MemoryStream();

            cs.CopyTo(msOutput);

           

            return Encoding.UTF32.GetString(msOutput.ToArray());
        }

        public static string DecryptQueryString(string input)
        {

            input = input.Replace('|', '/');
            input = input.Replace('!', '+');
            input = input.Replace('$', '=');

            string cipher = SymmetricDecrypt(input);

            return cipher;
        }


        public static AsymmetricKeys GenerateAsymmetricKeys()
        {
            RSA myAlg = RSA.Create();  

            AsymmetricKeys myKeys = new AsymmetricKeys();
            myKeys.PublicKey = myAlg.ToXmlString(false);
            myKeys.PrivateKey = myAlg.ToXmlString(true);

            return myKeys;
        }


        public static byte[] AsymmetricallyEncrypt(byte[] input, string publicKey)
        {
            var rsa = RSA.Create();
            rsa.FromXmlString(publicKey);


            byte[] output = rsa.EncryptValue(input);

            return output;
        }

        public static byte[] AsymmetricallyDecrypt(byte[] input, string privateKey)
        {
            var rsa = RSA.Create();
            rsa.FromXmlString(privateKey);


            byte[] output = rsa.DecryptValue(input);

            return output;
        }

       

        public static MemoryStream HybridEncrypt(Stream input, string publicKey)
        {
            
            input.Position = 0;

            

            var myAlg = Rijndael.Create();
            myAlg.GenerateKey();

           
            byte[] key = myAlg.Key;
            byte[] encryptedkey = AsymmetricallyEncrypt(key, publicKey);

            
            MemoryStream msOutput = new MemoryStream();

            
            msOutput.Write(encryptedkey, 0, encryptedkey.Length); 

            return msOutput;


        }

        public static MemoryStream HybridDecrypt(Stream input, string privateKey)
        {
           
            byte[] encKey = new byte[128];
            input.Read(encKey, 0, 128);
           


            MemoryStream remainingFilecontent = new MemoryStream();
            input.CopyTo(remainingFilecontent);
            
            MemoryStream decryptedFile = new MemoryStream();

            return decryptedFile;


        }

        

        public string SignData(byte[] input, string privateKey)
        {
            RSA myAlg = RSA.Create();
            myAlg.FromXmlString(privateKey);

            byte[] signature = myAlg.SignData(input, new HashAlgorithmName("SHA512"), RSASignaturePadding.Pss);

            return Convert.ToBase64String(signature);

        }


        public bool VerifyData(byte[] input, string publicKey, string signature)
        {
            RSA myAlg = RSA.Create();
            myAlg.FromXmlString(publicKey);

            bool result = myAlg.VerifyData(input, Convert.FromBase64String(signature), new HashAlgorithmName("SHA512"), RSASignaturePadding.Pss);
           

            return result;
        }



    }

    public class AsymmetricKeys
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }

}

