using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace iMU_Tool
{
    public class Crypto
    {
        private static readonly string key = "imu8020!";

        public static void EncryptFile(string ReadFilename, string WriteFilename)  //파일 암호화
        {
            FileStream fsInput = new FileStream(ReadFilename, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypted = new FileStream(WriteFilename, FileMode.Create, FileAccess.Write);

            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = Encoding.ASCII.GetBytes(key);
            DES.IV = Encoding.ASCII.GetBytes(key);

            ICryptoTransform Encrypt = DES.CreateEncryptor();
            CryptoStream cryptostream = new CryptoStream(fsEncrypted, Encrypt, CryptoStreamMode.Write);

            byte[] bytearrayinput = new byte[fsInput.Length];
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);

            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            fsEncrypted.Flush();
            cryptostream.Close();
            fsInput.Close();
            fsEncrypted.Close();
        }

        public static void DecryptFile(string ReadFilename, string WriteFilename)//파일 복호화
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = Encoding.ASCII.GetBytes(key);
            DES.IV = Encoding.ASCII.GetBytes(key);

            ICryptoTransform Decrypt = DES.CreateDecryptor();
            FileStream fsread = new FileStream(ReadFilename, FileMode.Open, FileAccess.Read);
            CryptoStream cryptostreamDecr = new CryptoStream(fsread, Decrypt, CryptoStreamMode.Read);

            // ************************
            // 인코딩 문제 해결: https://www.codeproject.com/Articles/26085/File-Encryption-and-Decryption-in-C
            // ************************
            FileStream fsOut = new FileStream(WriteFilename, FileMode.Create);

            int data;
            while ((data = cryptostreamDecr.ReadByte()) != -1)
                fsOut.WriteByte((byte)data);

            fsread.Close();
            cryptostreamDecr.Close();
            fsOut.Close();

            /*
            StreamWriter fsDecrypted = new StreamWriter(WriteFilename);
            fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
            fsDecrypted.Flush();
            fsDecrypted.Close();
            */
        }
    }
}
