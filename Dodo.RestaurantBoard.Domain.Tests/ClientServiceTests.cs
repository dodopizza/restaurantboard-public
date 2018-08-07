using System;
using System.Collections.Generic;
using System.Text;
using Dodo.Core.DomainModel.Clients;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.RestaurantBoard.Domain.Services;
using NUnit.Framework;

namespace Dodo.RestaurantBoard.Domain.Tests
{
    public class ClientServiceTests
    {
        [Test]
        public void GetIcons_ReturnIcon_WhenClientTreatmentIsRandomImage()
        {
            var clientTreatment = ClientTreatment.RandomImage;
            var clientService = new ClientService();

            var icons = clientService.GetIcons(clientTreatment);

            Assert.AreEqual(1, icons.Length);
        }

        [Test]
        public void GetIcons_ReturnEmptyIcons_WhenClientTreatmentIsNotRandomImage()
        {
            var clientTreatment = ClientTreatment.DefaultName;
            var clientService = new ClientService();
            
            var icons = clientService.GetIcons(clientTreatment);

            Assert.AreEqual(0, icons.Length);
        }
    }
}


