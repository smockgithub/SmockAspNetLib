using System;
using System.Security.Cryptography;
using System.Text;

namespace SmockAspNetLib.Infrastructure.Utilities
{
    public class EncryptUtility
    {

        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        public static string GetSecret(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] palindata = Encoding.Default.GetBytes(text);//将要加密的字符串转换为字节数组
            byte[] encryptdata = md5.ComputeHash(palindata);//将字符串加密后也转换为字符数组

            return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为加密字符串
        }

        public static string GetMD5_32(string s, string _input_charset)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {

                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }

            return sb.ToString();
        }

        public static string MD5Encrypt32(string password)
        {
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                var convert = s[i].ToString("X");
                if(convert.Length==1)
                {
                    convert = "0" + convert;
                }
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + convert;
            }
            return pwd.ToLower();
        }


        public static string GetMd5_16(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider(); 
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8); 
            t2 = t2.Replace("-", "");

            return t2;
        }

        #region SHA加密
        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="_input_charset"></param>
        /// <returns></returns>
        public static string GetSHA256(string input, string _input_charset)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(input);
            SHA256 sha256 = new SHA256Managed();
            sha256.ComputeHash(clearBytes);
            byte[] hashedBytes = sha256.Hash;
            sha256.Clear();
            string output = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return output;
        }

        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="_input_charset"></param>
        /// <returns></returns>
        public static string GetSHA1(string input, string _input_charset)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(input);
            SHA1 sha1 = new SHA1Managed();
            sha1.ComputeHash(clearBytes);
            byte[] hashedBytes = sha1.Hash;
            sha1.Clear();
            string output = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return output;
        }
        #endregion


        private static string key = "abcdefgh";
        #region ========DES加密========

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string DESEncrypt(string Text)
        {
            return DESEncrypt(Text, key);
        }
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string DESEncrypt(string Text, string sKey)
        {
            return DESEncrypt(Text, sKey, CipherMode.CBC,PaddingMode.PKCS7);
        }

        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="sKey"></param>
        /// <param name="cipherMode"></param>
        /// <param name="paddingMode"></param>
        /// <returns></returns>
        public static string DESEncrypt(string Text, string sKey, CipherMode cipherMode, PaddingMode paddingMode)
        {
            if (Text ==null){
                return null;
            }
            if (Text == "")
                return "";
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(Md5Hash(sKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(Md5Hash(sKey).Substring(0, 8));
            des.Mode = cipherMode;
            des.Padding = paddingMode;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        private static string Md5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        #endregion

        #region ========DES解密========

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string DESDecrypt(string Text)
        {
            return DESDecrypt(Text, key);
        }
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string DESDecrypt(string Text, string sKey)
        {
            return DESDecrypt(Text, sKey, CipherMode.CBC,PaddingMode.PKCS7);
        }

        /// <summary>
        /// DES解密数据
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="sKey"></param>
        /// <param name="cipherMode"></param>
        /// <param name="paddingMode"></param>
        /// <returns></returns>
        public static string DESDecrypt(string Text, string sKey, CipherMode cipherMode, PaddingMode paddingMode)
        {
            if (Text == null)
            {
                return null;
            }
            if (Text == "")
                return "";
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(Md5Hash(sKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(Md5Hash(sKey).Substring(0, 8));
            des.Mode = cipherMode;
            des.Padding = paddingMode;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion

        #region ========AES加密========

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string AESEncrypt(string text)
        {
            return AESEncrypt(text, key);
        }
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string AESEncrypt(string text, string sKey)
        {
            return AESEncrypt(text, sKey, CipherMode.CBC, PaddingMode.PKCS7,true);
        }

        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="text">明文</param>
        /// <param name="sKey">必须为16的倍数</param>
        /// <param name="cipherMode">必须为16的倍数</param>
        /// <param name="paddingMode"></param>
        /// <param name="base64OrHex">输出为BASE64格式</param>
        /// <returns></returns>
        public static string AESEncrypt(string text, string sKey, CipherMode cipherMode, PaddingMode paddingMode,bool base64OrHex)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return "";
            }

            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(sKey);
            byte[] ivArray = UTF8Encoding.UTF8.GetBytes(sKey);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(text);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = cipherMode;
            rDel.Padding = paddingMode;
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            if (base64OrHex)
            {
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            else
            {
                string returnStr = "";
                if (resultArray != null)
                {
                    for (int i = 0; i < resultArray.Length; i++)
                    {
                        returnStr += resultArray[i].ToString("X2");
                    }
                }
                return returnStr;
            }
        }
        #endregion

        #region ========AES解密========

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string AESDecrypt(string text)
        {
            return AESDecrypt(text, key);
        }
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string AESDecrypt(string text, string sKey)
        {
            return AESDecrypt(text, sKey, CipherMode.CBC, PaddingMode.PKCS7,true);
        }

        /// <summary>
        /// 解密数据，经测试128位AES解密
        /// </summary>
        /// <param name="text">明文</param>
        /// <param name="sKey">必须为16的倍数</param>
        /// <param name="cipherMode">必须为16的倍数</param>
        /// <param name="paddingMode"></param>
        /// <returns></returns>
        /// <param name="isBase64">是否为base64格式，还是hex格式</param>
        /// <returns></returns>
        public static string AESDecrypt(string text, string sKey, CipherMode cipherMode, PaddingMode paddingMode,bool isBase64)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return "";
            }

            //RijndaelManaged rijndaelCipher = new RijndaelManaged();
            //rijndaelCipher.Mode = CipherMode.CBC;
            //rijndaelCipher.Padding = PaddingMode.PKCS7;
            //rijndaelCipher.KeySize = 128;
            //rijndaelCipher.BlockSize = 128;
            //byte[] encryptedData = Convert.FromBase64String(text);
            //byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(sKey);
            //byte[] keyBytes = new byte[16];
            //int len = pwdBytes.Length;
            //if (len > keyBytes.Length) len = keyBytes.Length;
            //System.Array.Copy(pwdBytes, keyBytes, len);
            //rijndaelCipher.Key = keyBytes;
            //byte[] ivBytes = System.Text.Encoding.UTF8.GetBytes(sKey);
            //rijndaelCipher.IV = ivBytes;
            //ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            //byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            //var result3 = UTF8Encoding.UTF8.GetString(plainText);
            //return Encoding.UTF8.GetString(plainText);
            

            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(sKey);
            byte[] ivArray = UTF8Encoding.UTF8.GetBytes(sKey);
            byte[] toEncryptArray;
            if (isBase64)
            {
                toEncryptArray = Convert.FromBase64String(text);
            }
            else
            {
                toEncryptArray = StrToHexByte(text);
            }
            
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = cipherMode;
            rDel.Padding = paddingMode;
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        #endregion


        /// <summary> 
        /// 字符串进行Hex解码(Hex.decodeHex())
        /// </summary> 
        /// <param name="hexString">需要进行解码的字符串</param> 
        /// <returns></returns> 
        private static byte[] StrToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
    }
}
