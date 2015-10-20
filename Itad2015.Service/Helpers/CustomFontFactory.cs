using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Itad2015.Service.Helpers
{
    public class CustomFontFactory : FontFactoryImp
    {
        private const float DEFAULT_FONT_SIZE = 12;
        private const int DEFAULT_FONT_STYLE = 0;
        private static readonly BaseColor DEFAULT_FONT_COLOR = BaseColor.BLACK;

        public string DefaultFontPath { get; private set; }
        public string DefaultFontEncoding { get; private set; }
        public bool DefaultFontEmbedding { get; private set; }
        public float DefaultFontSize { get; private set; }
        public int DefaultFontStyle { get; private set; }
        public BaseColor DefaultFontColor { get; private set; }

        public bool ReplaceEncodingWithDefault { get; set; }
        public bool ReplaceEmbeddingWithDefault { get; set; }
        public bool ReplaceFontWithDefault { get; set; }
        public bool ReplaceSizeWithDefault { get; set; }
        public bool ReplaceStyleWithDefault { get; set; }
        public bool ReplaceColorWithDefault { get; set; }

        public BaseFont DefaultBaseFont { get; protected set; }

        public CustomFontFactory(
            string defaultFontFilePath,
            string defaultFontEncoding = BaseFont.IDENTITY_H,
            bool defaultFontEmbedding = BaseFont.EMBEDDED,
            float? defaultFontSize = null,
            int? defaultFontStyle = null,
            BaseColor defaultFontColor = null,
            bool automaticalySetReplacementForNullables = true)
        {
            //set default font properties
            DefaultFontPath = defaultFontFilePath;
            DefaultFontEncoding = defaultFontEncoding;
            DefaultFontEmbedding = defaultFontEmbedding;
            DefaultFontColor = defaultFontColor ?? DEFAULT_FONT_COLOR;
            DefaultFontSize = defaultFontSize ?? DEFAULT_FONT_SIZE;
            DefaultFontStyle = defaultFontStyle ?? DEFAULT_FONT_STYLE;

            //set default replacement options
            ReplaceFontWithDefault = false;
            ReplaceEncodingWithDefault = true;
            ReplaceEmbeddingWithDefault = false;

            if (automaticalySetReplacementForNullables)
            {
                ReplaceSizeWithDefault = defaultFontSize.HasValue;
                ReplaceStyleWithDefault = defaultFontStyle.HasValue;
                ReplaceColorWithDefault = defaultFontColor != null;
            }

            //define default font
            DefaultBaseFont = BaseFont.CreateFont(DefaultFontPath, DefaultFontEncoding, DefaultFontEmbedding);

            //register system fonts
            FontFactory.RegisterDirectories();
        }

        private Font GetBaseFont(float size, int style, BaseColor color)
        {
            var baseFont = new Font(DefaultBaseFont, size, style, color);

            return baseFont;
        }

        public override Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color, bool cached)
        {
            //eventually replace expected font properties
            size = ReplaceSizeWithDefault
                ? DefaultFontSize
                : size;
            style = ReplaceStyleWithDefault
                ? DefaultFontStyle
                : style;
            encoding = ReplaceEncodingWithDefault
                ? DefaultFontEncoding
                : encoding;
            embedded = ReplaceEmbeddingWithDefault
                ? DefaultFontEmbedding
                : embedded;

            //get font
            Font font;
            if (ReplaceFontWithDefault)
            {
                font = GetBaseFont(
                    size,
                    style,
                    color);
            }
            else
            {
                font = FontFactory.GetFont(
                    fontname,
                    encoding,
                    embedded,
                    size,
                    style,
                    color,
                    cached);

                if (font.BaseFont == null)
                    font = GetBaseFont(
                        size,
                        style,
                        color);
            }

            return font;
        }
    }
}
