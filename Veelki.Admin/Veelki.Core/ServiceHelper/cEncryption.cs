using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Veelki.Core.ServiceHelper
{
    public static class cEncryption
    {
        /// <summary>
        /// Base64 Encoding
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToBase64Encode(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return text;
            }
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }

        /// <summary>
        /// Base64 Decoding
        /// </summary>
        /// <param name="base64EncodedText"></param>
        /// <returns></returns>
        public static string ToBase64Decode(string base64EncodedText)
        {
            if (String.IsNullOrEmpty(base64EncodedText))
            {
                return base64EncodedText;
            }
            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedText);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// MD5 Encode
        /// </summary>
        /// <param name="encryptionText"></param>
        /// <returns></returns>
        public static string MD5Encryption(string text)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] array = Encoding.UTF8.GetBytes(text);
            array = md5.ComputeHash(array);
            StringBuilder sb = new StringBuilder();
            foreach (byte ba in array)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }
            //We returned the hexadecimal string.
            return sb.ToString();
        }        
    }
}

