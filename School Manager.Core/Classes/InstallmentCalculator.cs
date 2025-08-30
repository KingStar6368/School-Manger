using DNTPersianUtils.Core;
using School_Manager.Core.Classes;
using School_Manager.Domain.Entities.Catalog.Enums;
using School_Manager.Domain.Entities.Catalog.Operation;

public static class InstallmentCalculator
{
    public static List<Bill> CalculateInstallments(
        int installmentCount,
        int price,
        int monthCount,
        long totalPaid,
        DateTime startDate,
        DateTime endDate,
        int deadLine,
        long serviceContractRef)
    {
        var result = new List<Bill>();
        var totalPrice = (price * monthCount) - totalPaid;
        var perInstallmentUnrounded = totalPrice / installmentCount;
        var perInstallmentRounded = (totalPrice / (installmentCount * 1000000)) * 1000000;
        var perInstallment = perInstallmentRounded;
        var remainInstallment = totalPrice % installmentCount + ((perInstallmentUnrounded - perInstallmentRounded) * installmentCount);

        int totalMonths = (endDate.Year - startDate.Year) * 12 + (endDate.Month - startDate.Month) + 1;
        if (installmentCount > totalMonths)
            return result;

        double step = (double)totalMonths / installmentCount;

        for (int i = 0; i < installmentCount; i++)
        {
            int offsetMonth = (int)Math.Round(i * step);
            var current = new DateTime(startDate.Year, startDate.Month, 1).AddMonths(offsetMonth);

            var estimateTime = PersianDateHelper.GetBillEstimateTime(current, deadLine);
            var monthName = PersianDateHelper.PersianMonthNames[current.GetPersianMonth()];
            var bill = new Bill
            {
                Name = $"قسط {monthName}",
                ServiceContractRef = serviceContractRef,
                Price = (i == installmentCount - 1) ? perInstallment + remainInstallment : perInstallment,
                EstimateTime = estimateTime,
                Type = BillType.Normal
            };

            result.Add(bill);
        }
        return result;
    }
}