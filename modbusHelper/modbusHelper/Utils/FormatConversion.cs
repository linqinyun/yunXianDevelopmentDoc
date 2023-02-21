using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modbusHelper.Utils
{
    internal class FormatConversion
    {
        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// 去除byte[]数组缓冲区内的尾部空白区;从末尾向前判断;
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] bytesTrimEnd(byte[] bytes)
        {
            List<byte> list = bytes.ToList();
            for (int i = bytes.Length - 1; i >= 0; i--)
            {
                if (bytes[i] == 0x00)
                {
                    list.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }
            return list.ToArray();
        }
        /// <summary>
        /// 将数组a,b进行合并
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public static byte[] Combine2(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length];

            Array.Copy(a, 0, c, 0, a.Length);
            Array.Copy(b, 0, c, a.Length, b.Length);

            return c;
        }

        /// <summary>
        /// 字符串ASCIIstr2 为对应的ASCII字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetAscllByString(String str) {
            string str2 = str;
            byte[] array = System.Text.Encoding.ASCII.GetBytes(str2);  //数组array为对应的ASCII数组     
            string ASCIIstr2 = null;
            for (int i = 0; i < array.Length; i++)
            {
                int asciicode = (int)(array[i]);
                ASCIIstr2 += Convert.ToString(asciicode);//字符串ASCIIstr2 为对应的ASCII字符串
            }
            return ASCIIstr2;
        }
    }
}
