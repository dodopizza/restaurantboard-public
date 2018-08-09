using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Dodo.Core.DomainModel.Departments.Parameters.Department;
using Dodo.Core.DomainModel.Localization;
using Dodo.Core.Common;

namespace Dodo.Core.DomainModel.Departments
{
    [Serializable]
    public abstract class Department : Entity
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

        public virtual String TimeZoneShiftString =>
            String.Format("{0}{1}", GetTimeZoneShiftSymbol(), Math.Abs(TimeZoneShift));

        private Char GetTimeZoneShiftSymbol()
        {
            if (TimeZoneShift > 0) return '+';
            if (TimeZoneShift < 0) return '-';
            return  ' ';
        }

        public virtual TimeSpan CurrentTimeZoneUTCOffset => TimeSpan.FromMinutes(TimeZoneUTCOffset);

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

        public virtual Cultures[] AvailableCultures
        {
            get => availableCultures;
            set
            {
                availableCultures = value;
                departmentCultureData = GetCultureData(DepartmentCultureData);
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
            get => departmentCultureData;
            set => departmentCultureData = GetCultureData(value);
        }

        private DepartmentCultureData[] GetCultureData(DepartmentCultureData[] cultureData)
        {
            var result = new List<DepartmentCultureData>();

            if (AvailableCultures == null || cultureData == null)
                return cultureData;
            

            result.AddRange(GetAvailablesCulturesByCultureNameFrom(cultureData));
            result.AddRange(GetCulturesWithNativeCultureNameWithout(result));
            
            return result.ToArray();
        }

        private IEnumerable<DepartmentCultureData> GetCulturesWithNativeCultureNameWithout(
            IEnumerable<DepartmentCultureData> cultureData)
        {
            var intersected = 
                from ac in AvailableCultures
                join cd in cultureData on ac.CultureName equals cd.CultureName
                select ac;

            return
                from culture in AvailableCultures.Except(intersected)
                select new DepartmentCultureData
                (
                    0, 
                    culture.CultureName, 
                    Id,
                    String.Equals(NativeCultureName, culture.CultureName) ? Name : ""
                );
        }

        private IEnumerable<DepartmentCultureData> GetAvailablesCulturesByCultureNameFrom(DepartmentCultureData[] cultureData)
        {
            return 
                from culture in AvailableCultures 
                where cultureData.Any(c => c.CultureName == culture.CultureName) 
                select cultureData.FirstOrDefault(c => c.CultureName == culture.CultureName);
        }
        #endregion		
    }
}