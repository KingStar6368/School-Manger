using DNTPersianUtils.Core;
using School_Manager.Core.Classes;
using School_Manager.Domain.Entities.Catalog.Enums;
using School_Manager.Domain.Entities.Catalog.Operation;

public static class InstallmentCalculator
{
    public static List<Bill> CalculateInstallments(
    int installmentCount,
    long price,
    int monthCount,
    long totalPaid,
    DateTime startDate,
    DateTime endDate,
    int deadLine,
    long serviceContractRef,
    bool AddRoundedToFirst = false,
    long RoundedPrice = 1000000)
    {
        if (installmentCount <= 0) throw new ArgumentException(nameof(installmentCount));
        var result = new List<Bill>();

        long totalPrice = (price * (long)monthCount) - totalPaid;
        if (totalPrice <= 0) return result;

        long perInstallmentUnrounded = totalPrice / installmentCount;

        long perInstallmentRounded;
        long remainInstallment;

        if (RoundedPrice <= 0)
        {
            perInstallmentRounded = perInstallmentUnrounded;
            remainInstallment = totalPrice - perInstallmentRounded * installmentCount;
        }
        else
        {

            perInstallmentRounded = (perInstallmentUnrounded / RoundedPrice) * RoundedPrice;

            remainInstallment = totalPrice - perInstallmentRounded * installmentCount;
        }

        int totalMonths = (endDate.Year - startDate.Year) * 12 + (endDate.Month - startDate.Month) + 1;
        if (installmentCount > totalMonths) return result;

        double step = (double)totalMonths / installmentCount;

        for (int i = 0; i < installmentCount; i++)
        {
            int offsetMonth = (int)Math.Floor(i * step); // استفاده از Floor پایدارتر است
            var current = new DateTime(startDate.Year, startDate.Month, 1).AddMonths(offsetMonth);

            var estimateTime = PersianDateHelper.GetBillEstimateTime(current, deadLine);
            var monthName = PersianDateHelper.PersianMonthNames[current.GetPersianMonth()];

            long priceInstallment = perInstallmentRounded;

            if ((AddRoundedToFirst && i == 0) || (!AddRoundedToFirst && i == installmentCount - 1))
                priceInstallment += remainInstallment;

            var bill = new Bill
            {
                Name = $"قسط {monthName}",
                ServiceContractRef = serviceContractRef,
                Price = priceInstallment,
                EstimateTime = estimateTime,
                Type = BillType.Normal
            };

            result.Add(bill);
        }

        return result;
    }
}