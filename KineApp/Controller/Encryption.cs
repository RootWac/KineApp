using KineApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.Controller
{
    internal class Encryption
    {
        // Local variables
        // ====================================================================================================================================================================================================================

        /// <summary>
        /// 
        /// </summary>
        private static readonly byte[] IV = { 5, 153, 201, 215, 105, 79, 213, 247 };

        /// <summary>
        /// 
        /// </summary>
        private static readonly byte[] KEY = { 28, 165, 201, 94, 207, 33, 154, 128, 216, 24, 81, 155, 91, 120, 67, 65, 35, 157, 18, 9, 18, 144, 9, 111 };

        // IO
        // ====================================================================================================================================================================================================================

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="Data"></param>
        /// <param name="Crypt"></param>
        public static void Write(string Path, string Data, bool Crypt = true)
        {
            string _Data = Data;
            if (Crypt) _Data = EnCrypt(Data);
            File.WriteAllText(Path, _Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static string Read(string Path)
        {
            string Data = File.ReadAllText(Path);
            return Decrypt(Data);
        }


        // Local methods
        // ====================================================================================================================================================================================================================

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        internal static string EnCrypt(string Text)
        {
            try
            {
                byte[] textAsByte = Encoding.Default.GetBytes(Text);
                TripleDESCryptoServiceProvider TDES = new TripleDESCryptoServiceProvider();
                var encryptor = TDES.CreateEncryptor(KEY, IV);
                byte[] cryptedTextAsByte = encryptor.TransformFinalBlock(textAsByte, 0, textAsByte.Length);
                Text = Convert.ToBase64String(cryptedTextAsByte);
            }
            catch (Exception ex)
            {
                Log.Write("Error while reading the encryption file :" + ex.ToString(), LogStatus.Critical);
            }
            return Text;
        }

        /// <summary>
        /// 
        /// </summary>
        internal static string Decrypt(string Text)
        {
            try
            {
                byte[] cryptedTextAsByte = Convert.FromBase64String(Text);
                TripleDESCryptoServiceProvider TDES = new TripleDESCryptoServiceProvider();
                var decryptor = TDES.CreateDecryptor(KEY, IV);
                byte[] decryptedTextAsByte = decryptor.TransformFinalBlock(cryptedTextAsByte, 0, cryptedTextAsByte.Length);
                Text = Encoding.Default.GetString(decryptedTextAsByte);
            }
            catch (Exception ex)
            {
                Log.Write("Error while reading the decryption file :" + ex.ToString(), LogStatus.Critical);
            }
            return Text;
        }
    }
}

