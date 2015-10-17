using System;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using Itad2015.Contract.Service;
using Itad2015.Service.Helpers;

namespace Itad2015.Service.Concrete
{
    public class PdfService : IPdfService
    {
        private static DefaultTagProcessorFactory InitializeTagProcessor()
        {
            var tagProcessors = (DefaultTagProcessorFactory)Tags.GetHtmlTagProcessorFactory();
            tagProcessors.RemoveProcessor(HTML.Tag.IMG);
            tagProcessors.AddProcessor(HTML.Tag.IMG, new CustomImageTagProcessor());

            return tagProcessors;
        }

        private StyleAttrCSSResolver InitializeCssFiles(string[] cssPaths)
        {
            var cssFiles = new CssFilesImpl();
            cssFiles.Add(XMLWorkerHelper.GetInstance().GetDefaultCSS());

            var cssResolver = new StyleAttrCSSResolver(cssFiles);
            cssResolver.AddCss(@"code { padding: 2px 4px; }", "utf-8", true);

            foreach (var cssPath in cssPaths)
            {
                cssResolver.AddCssFile(cssPath, false);
            }

            return cssResolver;
        }
        public byte[] GeneratePdfFromView(string viewString, string[] cssPaths)
        {
            using (var memoryStream = new MemoryStream())
            {
                var doc = new Document(PageSize.A4);

                var writer = PdfWriter.GetInstance(doc, memoryStream);

                doc.Open();

                var tagProcessors = InitializeTagProcessor();
                var cssResolver = InitializeCssFiles(cssPaths);

                var hpc = new HtmlPipelineContext(new CssAppliersImpl(new XMLWorkerFontProvider()));
                hpc.SetAcceptUnknown(true).AutoBookmark(true).SetTagFactory(tagProcessors); // inject the tagProcessors

                var htmlPipeline = new HtmlPipeline(hpc, new PdfWriterPipeline(doc, writer));
                var pipeline = new CssResolverPipeline(cssResolver, htmlPipeline);

                var worker = new XMLWorker(pipeline, true);
                var xmlParser = new XMLParser(true, worker, Encoding.UTF8);
                xmlParser.Parse(new StringReader(viewString));

                doc.Close();
                return memoryStream.ToArray();
            }
        }
}
}
