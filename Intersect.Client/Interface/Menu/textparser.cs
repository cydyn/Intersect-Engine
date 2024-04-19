using System.Collections.Generic;

using Intersect.Client.Framework.Gwen;

namespace Intersect.Client.Interface.Menu
{

    partial class Textparser
    {

        public List<TextLine> Lines = new List<TextLine>();

        public partial struct TextLine
        {

            public string Text;

            public string Font;

            public int Size;

            public string Alignment;

            public Color Clr;

            public Alignments GetAlignment()
            {
                switch (Alignment)
                {
                    case "center":
                        return Alignments.CenterH;
                    case "right":
                        return Alignments.Right;
                    default:
                        return Alignments.Left;
                }
            }

        }

    }

}

