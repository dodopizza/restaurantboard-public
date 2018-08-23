using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dodo.Core.DomainModel.OrderProcessing;
using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;

namespace Dodo.RestaurantBoard.Domain.Services
{
    public class PizzeriaOrdersService : IPizzeriaOrdersService
    {
        private readonly IDepartmentsStructureService _departmentsStructureService;
        private readonly ITrackerClient _trackerClient;

        public PizzeriaOrdersService(IDepartmentsStructureService departmentsStructureService,
                                     ITrackerClient trackerClient)
        {           
            _departmentsStructureService = departmentsStructureService;
            _trackerClient = trackerClient;
        }

      
    }

}