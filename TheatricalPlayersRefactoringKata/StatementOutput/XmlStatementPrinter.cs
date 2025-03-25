using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Pricing;

namespace TheatricalPlayersRefactoringKata.StatementOutput
{
    public class XmlStatementPrinter
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            CultureInfo culture = new CultureInfo("en-US");
            int totalAmount = 0;
            int totalCredits = 0;
            var items = new List<XElement>();

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var calculator = GetPriceCalculator(play.Type);

                int amount = calculator.CalculateAmount(perf, play);
                int credits = calculator.CalculateVolumeCredits(perf, play);

                totalAmount += amount;
                totalCredits += credits;

                decimal displayAmount = amount / 100m; 

                items.Add(
                    new XElement("Item",
                        new XElement("AmountOwed", displayAmount),
                        new XElement("EarnedCredits", credits),
                        new XElement("Seats", perf.Audience)
                    )
                );
            }

            decimal totalDisplayAmount = totalAmount / 100m;

            var statement = new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                new XElement("Customer", invoice.Customer),
                new XElement("Items", items),
                new XElement("AmountOwed", totalDisplayAmount),
                new XElement("EarnedCredits", totalCredits)
            );

            var doc = new XDocument(new XDeclaration("1.0", "utf-8", null), statement);
            return doc.ToString();
        }

        private IPriceCalculator GetPriceCalculator(string playType)
        {
            switch (playType)
            {
                case "tragedy":
                    return new TragedyPriceCalculator();
                case "comedy":
                    return new ComedyPriceCalculator();
                case "history":
                    return new HistoryPriceCalculator();
                default:
                    throw new Exception("unknown type: " + playType);
            }
        }
    }
}
