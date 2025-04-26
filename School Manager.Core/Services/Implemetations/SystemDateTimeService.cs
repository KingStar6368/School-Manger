using AutoMapper;
using DNTPersianUtils.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Manager.Core.Services.Interfaces;
using School_Manager.Core.Utilities;
using School_Manager.Core.ViewModels.Common;

namespace School_Manager.Core.Services.Implemetations
{
    public class SystemDateTimeService : IDateTimeService
    {
        private readonly IMapper _mapper;
        public SystemDateTimeService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public DateTime NowUtc => DateTime.UtcNow;

        public int GetPersianDayOfMonth(DateTime datetime)
        {
            return datetime.GetPersianDayOfMonth();
        }

        public string GetPersianMonthName(int day)
        {
            return day.GetPersianMonthName();
        }

        public int GetPersianMonth(DateTime datetime)
        {
            return datetime.GetPersianMonth();
        }

        public int GetPersianYear(DateTime datetime)
        {
            return datetime.GetPersianYear();
        }

        public Tuple<DateTime, DateTime> GetPersianYearStartAndEndDates(int year)
        {
            var persianMonth = year.GetPersianMonthStartAndEndDates(year);
            var dates = Tuple.Create(persianMonth.StartDate, persianMonth.EndDate);
            return dates;
        }

        public bool IsLastDayOfTheMonth(DateTime dateTime)
        {
            return dateTime.IsLastDayOfTheMonth();
        }

        public PersianMonthViewModel GetPersianMonthInfo(int persianMonth)
        {
            var persianCalendar = new PersianCalendar();
            var date = new DateTime(DateTime.Now.GetPersianYear(), persianMonth, 1, persianCalendar);
            var persianDate = date.GetPersianMonthStartAndEndDates();
            return _mapper.Map<PersianMonthViewModel>(persianDate);
        }

        public PersianMonthViewModel GetCurrentDaysPersianCalendarInfo(DateTime dateTime)
        {
            var persianCalendar = new PersianCalendar();
            var date = new DateTime(dateTime.GetPersianYear(), dateTime.GetPersianMonth(), dateTime.GetPersianDayOfMonth(), persianCalendar);
            var persianDate = date.GetPersianMonthStartAndEndDates();
            return _mapper.Map<PersianMonthViewModel>(persianDate);
        }

        public int GetPersianDateNumerically()
        {
            string persianDate = string.Concat(DateTime.Now.GetPersianYear(), DateTime.Now.GetPersianMonth().ToString("D2"), DateTime.Now.GetPersianDayOfMonth().ToString("D2"));
            return persianDate.ToInt();
        }

        public string PersianDateTime(DateTime dateTime)
        {
            string persianDate = string.Concat(dateTime.GetPersianYear(), "/", dateTime.GetPersianMonth().ToString("D2"), "/", dateTime.GetPersianDayOfMonth().ToString("D2"));
            return persianDate;
        }

        public List<ComboBoxItemViewModel> GetPersianMonthInYearAsync()
        {
            DateTime dateTime = DateTime.Now;
            var persianCalendar = new PersianCalendar();

            List<ComboBoxItemViewModel> comboBoxItemsViewModels = new List<ComboBoxItemViewModel>();
            for (int i = 1; i <= 12; i++)
            {
                var date = new DateTime(dateTime.GetPersianYear(false), i, dateTime.GetPersianDayOfMonth(false), persianCalendar);
                var persianDate = date.GetPersianMonthStartAndEndDates(false);
                var item = new ComboBoxItemViewModel(i.GetPersianMonthName(), $"{persianDate.StartDate.ToString("yyyyMMdd", new CultureInfo("en-US"))}_{persianDate.EndDate.ToString("yyyyMMdd", new CultureInfo("en-US"))}");
                comboBoxItemsViewModels.Add(item);
            }
            return comboBoxItemsViewModels;
        }

        /// <summary>
        /// لیست ماه های سال  به صورت فعال و غیر فعال جهت استفاده در تخصیص
        /// </summary>
        /// <returns></returns>
        public List<ComboBoxItemViewModel> GetActiveMonthsForAllocationAsync()
        {
            DateTime dateTime = DateTime.Now;
            var persianCalendar = new PersianCalendar();
            int currentMonth = dateTime.GetPersianMonth(false);

            List<ComboBoxItemViewModel> comboBoxItemsViewModels = new List<ComboBoxItemViewModel>();
            for (int i = 1; i <= 12; i++)
            {
                var date = new DateTime(dateTime.GetPersianYear(false), i, dateTime.GetPersianDayOfMonth(false), persianCalendar);
                var persianDate = date.GetPersianMonthStartAndEndDates(false);
                var item = new ComboBoxItemViewModel(
                    i.GetPersianMonthName(),
                    i.ToString(), currentMonth <= i ? true : false
                    );
                comboBoxItemsViewModels.Add(item);
            }
            return comboBoxItemsViewModels;
        }

        /// <summary>
        /// دریافت روزهای ماه جاری به همراه تاریخ میلادی
        /// </summary>
        /// <returns></returns>
        public List<ComboBoxItemViewModel> GetPersianDaysCurrentMonthWithGregorianDate()
        {
            DateTime dateTime = DateTime.Now;
            var persianCalendar = new PersianCalendar();
            List<ComboBoxItemViewModel> comboBoxItemsDtos = new List<ComboBoxItemViewModel>();
            int lastDayOfMonth = dateTime.GetPersianMonth().GetPersianMonthLastDay(dateTime.GetPersianMonth());
            int currentMonth = dateTime.GetPersianMonth();
            int currentYear = dateTime.GetPersianYear(false);
            //var date = new DateTime(dateTime.GetPersianYear(false),, dateTime.GetPersianDayOfMonth(false), persianCalendar);
            for (int day = 1; day <= lastDayOfMonth; day++)
            {
                var date = new DateTime(currentYear, currentMonth, day, persianCalendar);
                var item = new ComboBoxItemViewModel(day.ToString(), $"{date.ToString("yyyyMMdd", new CultureInfo("en-US"))}");
                comboBoxItemsDtos.Add(item);
            }
            return comboBoxItemsDtos;
        }

        public List<ComboBoxItemViewModel> GetPersianMonthNames()
        {
            DateTime dateTime = DateTime.Now;
            var persianCalendar = new PersianCalendar();

            List<ComboBoxItemViewModel> comboBoxItemsDtos = new List<ComboBoxItemViewModel>();
            for (int i = 1; i <= 12; i++)
            {
                var date = new DateTime(dateTime.GetPersianYear(false), i, dateTime.GetPersianDayOfMonth(false), persianCalendar);
                var persianDate = date.GetPersianMonthStartAndEndDates(false);
                var item = new ComboBoxItemViewModel(i.GetPersianMonthName(), $"{persianDate.StartDate.ToString("MM")}");
                comboBoxItemsDtos.Add(item);
            }
            return comboBoxItemsDtos;
        }
    }
}
