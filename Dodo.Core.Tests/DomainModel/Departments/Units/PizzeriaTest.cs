using System;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Xunit;

namespace Dodo.Core.Tests.DomainModel.Departments
{
    public class PizzeriaTest
    {
        [Fact]
        public void IsZeroYearsOld_WhenPizzeriaIsNotOpened()
        {
            Pizzeria pizzeria = CreatePizzeria()
                .ThatIsNotOpened()
                .Please();
            
            PizzeriaAssert.That(pizzeria).At(4.JulyOf(1856)).HasAgeOf(0, In.Years);
        }

        [Fact]
        public void IsOneYearOld_WhenOpenedOneYearFromViewDate()
        {
            Pizzeria pizzeria = CreatePizzeria()
                .ThatIsOpened(14.JulyOf(2019))
                .Please();
            
            PizzeriaAssert.That(pizzeria).At(14.JulyOf(2020)).HasAgeOf(1, In.Years);
        }
        
        [Fact]
        public void IsOneMonthsOld_WhenOpenedOneMonthFromViewDate()
        {
            Pizzeria pizzeria = CreatePizzeria()
                .ThatIsOpened(14.JuneOf(2018))
                .Please();
            
            PizzeriaAssert.That(pizzeria).At(14.JulyOf(2018)).HasAgeOf(1, In.Months);
        }   
        
        [Fact]
        public void IsZeroMonthsOld_WhenPizzeriaIsNotOpened()
        {
            Pizzeria pizzeria = CreatePizzeria()
                .ThatIsNotOpened()
                .Please();
            
            PizzeriaAssert.That(pizzeria).At(14.JulyOf(2018)).HasAgeOf(0, In.Months);
        }      
        
        [Fact]
        public void HasNoFormat_WhenPizzeriaIsCreatedWithoutFormat()
        {
            Pizzeria pizzeria = CreatePizzeria()
                .WithoutFormat()
                .Please();

            PizzeriaAssert.That(pizzeria).HasNoFormat();
        }

        private PizzeriaBuilder CreatePizzeria()
        {
            return new PizzeriaBuilder();
        }

        internal class PizzeriaBuilder
        {
            private DateTime? openingDate;
            private PizzeriaFormat pizzeriaFormat;
    
            public PizzeriaBuilder ThatIsNotOpened()
            {
                openingDate = null;
                return this;
            }
            
            public PizzeriaBuilder WithoutFormat()
            {
                pizzeriaFormat = null;
                return this;
            }
    
            public PizzeriaBuilder ThatIsOpened(DateTime date)
            {
                openingDate = date;
                return this;
            }
    
            public Pizzeria Please()
            {
                return new Pizzeria(0, null, "", null, "", UnitApprove.Approved, UnitState.Close, 0, null, 0, null, 0.0,
                    openingDate,
                    "", null, null, null, ClientTreatment.DefaultName, false, pizzeriaFormat);
            }
        }
    }
}