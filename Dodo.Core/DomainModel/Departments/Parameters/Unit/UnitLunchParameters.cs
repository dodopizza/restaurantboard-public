using System;
using System.IO;
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

		public UnitLunchParameters() {}

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

			if (String.IsNullOrWhiteSpace(xmlParameters))
			{
				return new UnitLunchParameters(minimalShiftToKitchenWorker, minimalShiftToCashier, minimalShiftToCourier, minimalShiftToPersonalManager);
			}

			XmlReader xmlReader = XmlReader.Create(new StringReader(xmlParameters));
			XDocument document = XDocument.Load(xmlReader);

			foreach (XElement item in document.Descendants("Lunch"))
			{
				if (item.Element("MinimalShiftToKitchenWorker") != null && !String.IsNullOrWhiteSpace(item.Element("MinimalShiftToKitchenWorker").Value))
					minimalShiftToKitchenWorker = Convert.ToInt32(item.Element("MinimalShiftToKitchenWorker").Value);

				if (item.Element("MinimalShiftToCashier") != null && !String.IsNullOrWhiteSpace(item.Element("MinimalShiftToCashier").Value))
					minimalShiftToCashier = Convert.ToInt32(item.Element("MinimalShiftToCashier").Value);

				if (item.Element("MinimalShiftToCourier") != null && !String.IsNullOrWhiteSpace(item.Element("MinimalShiftToCourier").Value))
					minimalShiftToCourier = Convert.ToInt32(item.Element("MinimalShiftToCourier").Value);

				if (item.Element("MinimalShiftToPersonalManager") != null && !String.IsNullOrWhiteSpace(item.Element("MinimalShiftToPersonalManager").Value))
					minimalShiftToPersonalManager = Convert.ToInt32(item.Element("MinimalShiftToPersonalManager").Value);
			}

			return new UnitLunchParameters(minimalShiftToKitchenWorker, minimalShiftToCashier, minimalShiftToCourier, minimalShiftToPersonalManager);
		}
	}
}
