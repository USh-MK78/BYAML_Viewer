using BYAML_Library.Component;
using BYAML_Library.Node;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BYAML_Library
{
    public class BYAML
    {
        public char[] BYAMLHeader { get; set; } //0x2
        public byte[] Version { get; set; } //0x2, 0x0001 = MK8
        public enum BYAMLVersion
        {
            MK8 = 0x0001,
            Unknown
        }

        public int NodeNameTableOffset { get; set; }
        public Node.BYAMLNode NodeNameTable_BYAMLNode { get; set; }

        public int StringValueTableNode_Offset { get; set; }
        public Node.BYAMLNode StringValueTable_BYAMLNode { get; set; }

        public int PathValueTableNode_Offset { get; set; }
        public Node.BYAMLNode PathValueTable_BYAMLNode { get; set; }

        public int RootNode_Offset { get; set; }
        public Node.BYAMLNode RootNode_BYAMLNode { get; set; }

        public void ReadBYAML(BinaryReader br, byte[] BOM)
        {
            long BYAMLPos = br.BaseStream.Position;

            EndianConvert endianConvert = new EndianConvert(BOM);

            BYAMLHeader = br.ReadChars(2);

            Version = endianConvert.Convert(br.ReadBytes(2)); //0x0001 => Mario Kart 8

            NodeNameTableOffset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            if (NodeNameTableOffset != 0)
            {
                long CurrentPos = br.BaseStream.Position;
                br.BaseStream.Position = BYAMLPos;

                br.BaseStream.Seek(NodeNameTableOffset, SeekOrigin.Current);

                //BYAMLNode
                NodeNameTable_BYAMLNode.Read_BYAMLNode(br, BOM);

                br.BaseStream.Position = CurrentPos;
            }

            StringValueTableNode_Offset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            if (StringValueTableNode_Offset != 0)
            {
                long CurrentPos = br.BaseStream.Position;
                br.BaseStream.Position = BYAMLPos;

                br.BaseStream.Seek(StringValueTableNode_Offset, SeekOrigin.Current);

                //BYAMLNode
                StringValueTable_BYAMLNode.Read_BYAMLNode(br, BOM);

                br.BaseStream.Position = CurrentPos;
            }

            PathValueTableNode_Offset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            if (PathValueTableNode_Offset != 0)
            {
                long CurrentPos = br.BaseStream.Position;
                br.BaseStream.Position = BYAMLPos;

                br.BaseStream.Seek(PathValueTableNode_Offset, SeekOrigin.Current);

                //BYAMLNode
                PathValueTable_BYAMLNode.Read_BYAMLNode(br, BOM);

                br.BaseStream.Position = CurrentPos;
            }

            RootNode_Offset = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            if (RootNode_Offset != 0)
            {
                long CurrentPos = br.BaseStream.Position;
                br.BaseStream.Position = BYAMLPos;

                br.BaseStream.Seek(RootNode_Offset, SeekOrigin.Current);

                //BYAMLNode
                RootNode_BYAMLNode.Read_BYAMLNode(br, BOM);

                br.BaseStream.Position = CurrentPos;
            }

        }

        public BYAML()
        {
            BYAMLHeader = "NN".ToCharArray(); //Default
            Version = new byte[2];

            NodeNameTableOffset = 0;
            NodeNameTable_BYAMLNode = new Node.BYAMLNode();
            StringValueTableNode_Offset = 0;
            StringValueTable_BYAMLNode = new Node.BYAMLNode();
            PathValueTableNode_Offset = 0;
            PathValueTable_BYAMLNode = new Node.BYAMLNode();
            RootNode_Offset = 0;
            RootNode_BYAMLNode = new Node.BYAMLNode();
        }
    }
}
