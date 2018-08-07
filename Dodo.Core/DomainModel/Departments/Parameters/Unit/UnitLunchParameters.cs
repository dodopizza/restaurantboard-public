using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Dodo.Core.DomainModel.Departments.Parameters.Unit
{
    [Serializable]
    public class UnitLunchParameters : UnitParameters
    {
        public Int32 MinimalShiftToKitchenWorker { get; private set; }
        public Int32 MinimalShiftToCashier { get; private set; }
        public Int32 MinimalShiftToCourier { get; private set; }
        public Int32 MinimalShiftToPersonalManager { get; private set; }

        public UnitLunchParameters() { }

        public UnitLunchParameters
        (
            Int32 minimalShiftToKitchenWorker,
            Int32 minimalShiftToCashier,
            Int32 minimalShiftToCourier,
            Int32 minimalShiftToPersonalManager
        )
        {
            MinimalShiftToKitchenWorker = minimalShiftToKitchenWorker;
            MinimalShiftToCashier = minimalShiftToCashier;
            MinimalShiftToCourier = minimalShiftToCourier;
            MinimalShiftToPersonalManager = minimalShiftToPersonalManager;
        }

        public static UnitLunchParameters ConvertToUnitLunchParameters(String xmlParameters)
        {
            Int32 minimalShiftToKitchenWorker = 8;
            Int32 minimalShiftToCashier = 8;
            Int32 minimalShiftToCourier = 8;
            Int32 minimalShiftToPersonalManager = 8;

            if (!String.IsNullOrWhiteSpace(xmlParameters))
            {
                XDocument document = XDocument.Load(XmlReader.Create(new StringReader(xmlParameters)));

                minimalShiftToKitchenWorker = GetElementValue(document, "MinimalShiftToKitchenWorker");
                minimalShiftToCashier = GetElementValue(document, "MinimalShiftToCashier");
                minimalShiftToCourier = GetElementValue(document, "MinimalShiftToCourier");
                minimalShiftToPersonalManager = GetElementValue(document, "MinimalShiftToPersonalManager");
            }

            return new UnitLunchParameters(minimalShiftToKitchenWorker, minimalShiftToCashier, minimalShiftToCourier, minimalShiftToPersonalManager);
        }

        private static int GetElementValue(XDocument document, string elementName)
        {
            var element = document
                .Descendants("Lunch")
                .Elements(elementName)
                .LastOrDefault(x => !String.IsNullOrWhiteSpace(x.Value));
            
            return element != null ? Convert.ToInt32(element.Value) : 8;
        }
    }
}
