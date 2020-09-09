using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eForm.MultiTenancy.HostDashboard.Dto;

namespace eForm.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}