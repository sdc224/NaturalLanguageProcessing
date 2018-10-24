using ESCommon.Rtf;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;

namespace LanguageProcessor.Rtf
{
    public class Report
    {
        private readonly RtfDocument _rtf;
        private readonly int _index;

        public Report(string textFolder)
        {
            var filePaths = Directory.GetFiles(textFolder, "*.txt");

            _rtf = new RtfDocument();
            _rtf.FontTable.Add(new RtfFont("Calibri"));
            _rtf.FontTable.Add(new RtfFont("Constantia"));
            _rtf.ColorTable.AddRange(new[]
            {
                new RtfColor(Color.Red),
                new RtfColor(0,0,255)
            });

            var header = new RtfFormattedParagraph(new RtfParagraphFormatting(16, RtfTextAlign.Center));
            var micHeader = new List<RtfFormattedParagraph>();
            var line = new RtfFormattedParagraph(new RtfParagraphFormatting(16, RtfTextAlign.Left));
            var endLine = new RtfFormattedParagraph(new RtfParagraphFormatting(16, RtfTextAlign.Left));
            var contents = new List<RtfFormattedParagraph>();

            header.AppendText(new RtfFormattedText(Dns.GetHostName(), RtfCharacterFormatting.Bold));
            header.AppendText(new RtfLineBreak());

            line.AppendText("---------------------------------------------------------------------------");
            endLine.AppendText("______________________________________________________");

            _rtf.Contents.Add(header);

            foreach (var path in filePaths)
            {
                micHeader.Add(new RtfFormattedParagraph(new RtfParagraphFormatting(12, RtfTextAlign.Left)));
                micHeader[_index].AppendText(Path.GetFileNameWithoutExtension(path));
                _rtf.Contents.Add(micHeader[_index]);
                _rtf.Contents.Add(line);
                using (var reader = new StreamReader(path))
                {
                    contents.Add(new RtfFormattedParagraph(new RtfParagraphFormatting(10, RtfTextAlign.Left)));
                    contents[_index].AppendText(reader.ReadLine());
                }
                contents[_index].AppendText(new RtfLineBreak());
                _rtf.Contents.Add(contents[_index]);
                _rtf.Contents.Add(endLine);
                _rtf.Contents.Add(new RtfPageBreak());
                _index++;
            }

            _rtf.Contents.Add(endLine);
        }

        public RtfDocument GetRtf()
        {
            return _rtf;
        }
    }
}
