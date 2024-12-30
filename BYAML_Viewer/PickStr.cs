using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYAML_Viewer
{
    public class PickStr
    {
        /// <summary>
        /// 文字列の終わりがNull終端文字列で構成されたバイナリデータを文字列に変換するメソッド
        /// </summary>
        /// <param name="BinReader">BinaryReader</param>
        /// <param name="OutputStr">String</param>
        public static string PickByteStr2String(BinaryReader BinReader)
        {
            List<byte> PickStrList = new List<byte>();
            var bs = BinReader.BaseStream;
            while (bs.Position != bs.Length)
            {
                byte PickStr = BinReader.ReadByte();
                PickStrList.Add(PickStr);
                if (PickStr == 0x00)
                {
                    break;
                }
            }

            byte[] PickStrAry = PickStrList.ToArray();

            return System.Text.Encoding.GetEncoding("utf-8").GetString(PickStrAry);
        }
    }
}
