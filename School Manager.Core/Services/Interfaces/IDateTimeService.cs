using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Manager.Core.ViewModels.Common;

namespace School_Manager.Core.Services.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
        string GetPersianMonthName(int day);
        int GetPersianYear(DateTime datetime);
        int GetPersianMonth(DateTime datetime);
        int GetPersianDayOfMonth(DateTime datetime);
        bool IsLastDayOfTheMonth(DateTime dateTime);
        string PersianDateTime(DateTime dateTime);
        Tuple<DateTime, DateTime> GetPersianYearStartAndEndDates(int year);
        PersianMonthViewModel GetPersianMonthInfo(int persianMonth);
        PersianMonthViewModel GetCurrentDaysPersianCalendarInfo(DateTime dateTime);
        int GetPersianDateNumerically();
        List<ComboBoxItemViewModel> GetPersianMonthNames();
        List<ComboBoxItemViewModel> GetPersianMonthInYearAsync();
        List<ComboBoxItemViewModel> GetActiveMonthsForAllocationAsync();
        List<ComboBoxItemViewModel> GetPersianDaysCurrentMonthWithGregorianDate();
    }
}
