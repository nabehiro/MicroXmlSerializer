using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroXmlSerializer.Tests
{
    [TestClass]
    public class UnitTest1
    {
        string xml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<perls>
    
    <article name = ""backgroundworker"" >
        Example text.
    </article>
    <article/>
    <article name = ""threadpool"" >
        More text.
    </article>
    <article></article>
    <article>Final text.</article>
</perls>";

        [TestMethod]
        public void TestMethod1()
        {
            var elm = XElement.Load(new StringReader(xml));
        }

        [TestMethod]
        public void TestMethod2()
        {
            
        }
    }
}
