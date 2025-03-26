using System.Collections.Generic;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata;
using TheatricalPlayersRefactoringKata.StatementOutput;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class XmlStatementPrinterTests
    {
        [Fact]
        public void XmlStatementPrinter_ShouldGenerateValidXml()
        {
            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, "tragedy") },
                { "as-like", new Play("As You Like It", 2670, "comedy") },
                { "othello", new Play("Othello", 3560, "tragedy") }
            };

            var invoice = new Invoice("BigCo", new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40)
            });

            var xmlPrinter = new XmlStatementPrinter();
            string xmlContent = xmlPrinter.Print(invoice, plays);

            var doc = XDocument.Parse(xmlContent);
            Assert.NotNull(doc.Element("Statement"));
            Assert.NotNull(doc.Element("Statement").Element("Customer"));
            Assert.NotNull(doc.Element("Statement").Element("Items"));
        }
    }
}