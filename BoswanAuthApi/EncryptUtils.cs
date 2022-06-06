using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuickSecureLib
{
    public class EncryptUtils
    {
        private static readonly int RecurseCount = 5;

        public EncryptUtils()
        {
            
        }

        public static string aesEncrypt(string content)
        {
            int RecurseCount = 5;
            string encryptedText = RecursiveEncryptAES(content, RecurseCount);

            //if (encryptedText.EndsWith("=="))
            //{
            //    encryptedText = encryptedText.Substring(0, encryptedText.Length - 2);
            //}

            return encryptedText;
        }

        public static string aesDecrypt(string content)
        {
            return RecursiveDecryptAES(content, RecurseCount);
        }

        private static string InternalEncryptAES(string content)
        {
            string KeyToday = GetKeyToday();
            byte[] key = Encoding.UTF8.GetBytes(GetKeyToday());
            byte[] iv = new byte[key.Length];

            byte[] encrypted;

            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(content);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        private static string InternalDecryptAES(string content)
        {
            string KeyToday = GetKeyToday();
            byte[] key = Encoding.UTF8.GetBytes(GetKeyToday());
            byte[] iv = new byte[key.Length];

            byte[] cipherText = Convert.FromBase64String(content);
            string plaintext = null;

            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        private static string RecursiveEncryptAES(string content, int count)
        {
            if (count > 0)
            {
                return RecursiveEncryptAES(InternalEncryptAES(content), count - 1);
            }
            else
            {
                return InternalEncryptAES(content);
            }
        }

        private static string RecursiveDecryptAES(string content, int count)
        {
            if (count > 0)
            {
                return RecursiveDecryptAES(InternalDecryptAES(content), count - 1);
            }
            else
            {
                return InternalDecryptAES(content);
            }
        }

        private static string GetKeyToday()
        {
            return DateTime.Now.ToString("yyyyMMddHH") + DateTime.Now.DayOfWeek.ToString().ToUpper().Substring(0, 6);
        }

        public string sha256Encrypt(string password, string salt)
        {
            string saltAndPwd = String.Concat(password, salt);
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(saltAndPwd));
            string _newPasswordHash = Convert.ToBase64String(hashedDataBytes);
            return _newPasswordHash;
        }
    }
}
