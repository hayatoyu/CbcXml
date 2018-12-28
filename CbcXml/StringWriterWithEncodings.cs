using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CbcXml
{
    public sealed class StringWriterWithEncodings : StringWriter
    {
        private readonly Encoding encoding;

        public StringWriterWithEncodings() : this(Encoding.UTF8) { }

        public StringWriterWithEncodings(Encoding en)
        {
            this.encoding = en;
        }

        public override Encoding Encoding
        {
            get { return encoding; }
        }
    }
}
