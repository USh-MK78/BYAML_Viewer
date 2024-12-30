using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYAML_Library.Node
{
    public class NodeIdentifier
    {
        public enum BYAMLNodeIdentifier
        {
            StringValueTable = 0xA0,
            PathValueTable = 0xA1,
            ArrayNode = 0xC0,
            DictionaryNode = 0xC1,
            StringTable = 0xC2,
            PathTable = 0xC3,
            Boolean = 0xD0,
            Integer = 0xD1,
            Float = 0xD2
        }

        public enum BYAMLNodeTypeIdentifier
        {
            IndexValue = 0,
            Full = 1,
            Full_Table = 2,
            Value = 3
        }

        /// <summary>
        /// BYAMLIdentifier, Used Type
        /// </summary>
        public struct BYAMLIdentifier
        {
            public BYAMLNodeIdentifier BYAMLNodeIdentifier;
            public BYAMLNodeTypeIdentifier BYAMLNodeTypeIdentifier;

            public BYAMLIdentifier(BYAMLNodeIdentifier bYAMLNodeIdentifier, BYAMLNodeTypeIdentifier bYAMLNodeTypeIdentifier)
            {
                BYAMLNodeIdentifier = bYAMLNodeIdentifier;
                BYAMLNodeTypeIdentifier = bYAMLNodeTypeIdentifier;
            }
        }

        public static BYAMLNodeIdentifier GetNodeType(byte b)
        {
            return (BYAMLNodeIdentifier)Enum.ToObject(typeof(BYAMLNodeIdentifier), b);
        }

        public static BYAMLIdentifier GetBYAMLNodeType(BYAMLNodeIdentifier bYAMLNodeIdentifier)
        {
            BYAMLIdentifier bYAMLIdentifier = new BYAMLIdentifier();
            if (bYAMLNodeIdentifier == BYAMLNodeIdentifier.StringValueTable || bYAMLNodeIdentifier == BYAMLNodeIdentifier.PathValueTable)
            {
                bYAMLIdentifier = new BYAMLIdentifier(bYAMLNodeIdentifier, BYAMLNodeTypeIdentifier.IndexValue);
            }
            else if (bYAMLNodeIdentifier == BYAMLNodeIdentifier.Boolean || bYAMLNodeIdentifier == BYAMLNodeIdentifier.Integer || bYAMLNodeIdentifier == BYAMLNodeIdentifier.Float)
            {
                bYAMLIdentifier = new BYAMLIdentifier(bYAMLNodeIdentifier, BYAMLNodeTypeIdentifier.Value);
            }
            else if (bYAMLNodeIdentifier == BYAMLNodeIdentifier.ArrayNode || bYAMLNodeIdentifier == BYAMLNodeIdentifier.DictionaryNode)
            {
                bYAMLIdentifier = new BYAMLIdentifier(bYAMLNodeIdentifier, BYAMLNodeTypeIdentifier.Full);
            }
            else if (bYAMLNodeIdentifier == BYAMLNodeIdentifier.StringTable || bYAMLNodeIdentifier == BYAMLNodeIdentifier.PathTable)
            {
                bYAMLIdentifier = new BYAMLIdentifier(bYAMLNodeIdentifier, BYAMLNodeTypeIdentifier.Full_Table);
            }
            return bYAMLIdentifier;
        }
    }
}
