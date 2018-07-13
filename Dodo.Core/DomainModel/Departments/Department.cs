﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dodo.Core.DomainModel.Departments.Parameters.Department;
using Dodo.Core.DomainModel.Localization;
using Dodo.Core.Common;

namespace Dodo.Core.DomainModel.Departments
{
	[Serializable]
	public class Department : Entity
	{
		public virtual Uuid Uuid { get; set; }
		public virtual String Name { get; set; }

		public virtual DepartmentType Type { get; set; }

		public virtual DepartmentState State { get; set; }

		/// <summary>
		/// Отклонение от UTC в минутах
		/// </summary>
		public virtual Int32 TimeZoneUTCOffset { get; set; }

		/// <summary>
		/// Отклонение от серверного времени(Москва) в часах
		/// </summary>
		public virtual Int16 TimeZoneShift
		{
			get
			{
				Double currentTimeZoneUTCOffset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow).TotalMinutes;
				return (Int16)Math.Round(((Double)TimeZoneUTCOffset - currentTimeZoneUTCOffset) / 60);
			}
		}

		public int ManagedTimeZoneShift { get; set; }

		public virtual Country Country { get; set; }

		public virtual String OwnerName  { get; set; }
		public virtual String OwnerPhone  { get; set; }
		public virtual String OwnerEMail  { get; set; }

		public virtual String TimeZoneUTCOffsetString
		{
			get
			{
				Char mathSimbol;
				if (TimeZoneUTCOffset > 0)
					mathSimbol = '+';
				else if (TimeZoneShift < 0)
					mathSimbol = '-';
				else
					mathSimbol = ' ';

				TimeSpan timeSpan = TimeSpan.FromMinutes(Math.Abs(TimeZoneUTCOffset));
				String fromTimeString = timeSpan.ToString(@"hh\:mm");

				return String.Format("{0}{1}", mathSimbol, fromTimeString);
			}
		}

		public virtual String TimeZoneShiftString => getTimeZoneShiftString(TimeZoneShift);
		public virtual String ManagedTimeZoneShiftString => getTimeZoneShiftString(ManagedTimeZoneShift);

		private string getTimeZoneShiftString(int timeZoneShift)
		{
			Char mathSimbol;
			if (timeZoneShift > 0)
				mathSimbol = '+';
			else if (timeZoneShift < 0)
				mathSimbol = '-';
			else
				mathSimbol = ' ';

			return String.Format("{0}{1}", mathSimbol, Math.Abs(timeZoneShift));	
		}

		public virtual TimeSpan CurrentTimeZoneUTCOffset
		{
			get
			{
				TimeSpan result = TimeSpan.FromMinutes(TimeZoneUTCOffset);
				return result;
			}
		}

		/// <summary>
		/// Приводит dateTime ко времени в UTC с учетом часового пояса департамента
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns></returns>
		public DateTime GetUtcDateTime(DateTime dateTime)
		{
			return DateTime.SpecifyKind(dateTime.AddMinutes(-TimeZoneUTCOffset), DateTimeKind.Utc);
		}

		public DateTime? ToLocalDateTime(DateTime? utcDateTime)
		{
			return utcDateTime == null
				? (DateTime?)null
				: DateTime.SpecifyKind(utcDateTime.Value.AddMinutes(TimeZoneUTCOffset), DateTimeKind.Unspecified);
		}

		/// <summary>
		/// Текущее время подразделения (с учетом часового пояса)
		/// </summary>
		public virtual DateTime CurrentDateTime => DateTime.SpecifyKind( DateTime.UtcNow.AddMinutes(TimeZoneUTCOffset), DateTimeKind.Unspecified);

		public virtual DateTime CurrentDateTimeUtc => GetUtcDateTime(CurrentDateTime);

		public virtual DateTime CurrentDate => CurrentDateTime.Date;

		public readonly List<Unit> Units = new List<Unit>();
		
		protected Department(Int32 id, Uuid uuid, String name, DepartmentType type,  DepartmentState state, Int32 timeZoneUTCOffset,  Country country)
		{
			Id = id;
			Uuid = uuid;
			Name = name;
			Type = type;
			State = state;
			Country = country;

			TimeZoneUTCOffset = timeZoneUTCOffset;
		}

		protected Department(Int32 id, Uuid uuid, String name, DepartmentType type,  DepartmentState state,
			Int32 timeZoneUTCOffset,  Country country, String ownerName, String ownerPhone, String ownerEMail)
		{
			Id = id;
			Uuid = uuid;
			Name = name;
			Type = type;
			State = state;
			Country = country;
			OwnerName = ownerName;
			OwnerPhone = ownerPhone;
			OwnerEMail = ownerEMail;

			TimeZoneUTCOffset = timeZoneUTCOffset;
		}

		public Department ( )
		{
		}

		public void AddUnit(Unit unit)
		{
			Units.Add(unit);
		}

		public List<string> GetAllUnitNames()
		{
			var unitsNames = new List<string>();
			foreach (var unit in Units)
			{
				unitsNames.Add(unit.GetName());
			}

			return unitsNames;
		}
		
		public static DepartmentParameters GetDepartmentParametersFromXmlString(String value, DepartmentType departmentType)
		{
			if (String.IsNullOrEmpty(value)) return null;

			switch (departmentType)
			{
				case DepartmentType.Department:
					return CityParameters.FromXmlString(value);

				default:
					return null;
			}
		}

		public override String ToString()
		{
			return $"{Name} Type: {Type} State: {State}";
		}

		#region мультиязычность

		private Cultures[] availableCultures;

		public Cultures[] AvailableCultures
		{
			get { return availableCultures; }
			set
			{
				availableCultures = value;
				departmentCultureData = GetCultureData(CurrentCultureName, DepartmentCultureData, availableCultures);
			}
		}

		public String CurrentCultureName { get; set; }

		private String nativeCultureName;

		public String NativeCultureName
		{
			get
			{
				if (AvailableCultures != null && AvailableCultures.Any(c => (c.IsNative)))
					return AvailableCultures.FirstOrDefault(c => c.IsNative).CultureName;
				else
					return nativeCultureName;
			}
			set { nativeCultureName = value; }
		}

		private DepartmentCultureData[] departmentCultureData;

		public DepartmentCultureData[] DepartmentCultureData
		{
			get { return departmentCultureData; }
			set
			{
				departmentCultureData = GetCultureData(CurrentCultureName, value, AvailableCultures);
			}
		}

		private DepartmentCultureData[] GetCultureData(String selectedCultureName, DepartmentCultureData[] cultureData, Cultures[] availableCultures)
		{
			List<DepartmentCultureData> result = new List<DepartmentCultureData>();

			if (availableCultures == null || cultureData == null)
				return cultureData;

			foreach (var culture in availableCultures)
			{
				if (cultureData.Any(c => (c.CultureName == culture.CultureName)))
					result.Add(cultureData.FirstOrDefault(c => (c.CultureName == culture.CultureName)));
				else if (String.Equals(NativeCultureName, culture.CultureName))
					result.Add(new DepartmentCultureData(0, culture.CultureName, this.Id, this.Name));
				else
					result.Add(new DepartmentCultureData(0, culture.CultureName, this.Id, ""));
			}

			return result.ToArray();
		}

		#endregion		
	}
}