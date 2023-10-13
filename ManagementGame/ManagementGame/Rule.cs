namespace ManagementGame
{
    public class Rule : IRule
    {
        public int InitialFunds { get; set; } = 300;
        public int TurnCount { get; set; } = 30;
        public int PurchasePrice { get; set; } = 10;
        public int SalePrice { get; set; } = 16;
        public int PartTimeHireCost { get; set; } = 10;
        public int FullTimeHireCost { get; set; } = 5;
        public int EndOfGameFullTimeHireCost { get; set; } = 25;
        public int MaxPartTimeEmployees { get; set; } = 6;
        public int MaxFullTimeEmployees { get; set; } = 9;
        public int MaxPartTimeEmployeesAtOnce { get; set; } = 3;
        public int PartTimeEmployeePower { get; set; } = 1;
        public int FullTimeEmployeePower { get; set; } = 2;
        public int GetInitialFunds()
        {
            return InitialFunds;
        }
        public int MaxPurchasableItems(int money, int currentEmployeeCapacity)
        {
            int value = money / PurchasePrice;
            return (value <= currentEmployeeCapacity) ? value : currentEmployeeCapacity;
        }

        public int Purchase(int funds, int quantity)
        {
            return funds - (PurchasePrice * quantity);
        }

        public int MaxSellableItems(int currentStock, int currentEmployeeCapacity)
        {
            return (currentStock <= currentEmployeeCapacity) ? currentStock : currentEmployeeCapacity;
        }

        public int Sell(int funds, int quantity)
        {
            return funds + (SalePrice * quantity);
        }

        public int HirePartTime(int funds, int quantity)
        {
            return funds - (PartTimeHireCost * quantity);
        }

        public int HireFullTime(int funds, int quantity)
        {
            return funds - (FullTimeHireCost * quantity);
        }

        public int MaxHireablePartTimeEmployees(int funds, int currentPartTimeEmployees)
        {
            int maxHirable = funds / PartTimeHireCost;
            int remainingHiringCapacity = MaxPartTimeEmployees - currentPartTimeEmployees;
            return Math.Min(Math.Min(maxHirable, remainingHiringCapacity), MaxPartTimeEmployeesAtOnce);
        }

        public int MaxHireableFullTimeEmployees(int funds, int currentFullTimeEmployees)
        {
            int maxHirable = funds / FullTimeHireCost;
            int remainingHiringCapacity = MaxFullTimeEmployees - currentFullTimeEmployees;
            return Math.Min(maxHirable, remainingHiringCapacity);
        }
        public int GetCurrentEmployeePower(int partTimeCount, int fullTimeCount)
        {
            return (partTimeCount * PartTimeEmployeePower) + (fullTimeCount * FullTimeEmployeePower);
        }
        public int FinalFullTimeEmployeeCost(int fullTimeCount)
        {
            return EndOfGameFullTimeHireCost * fullTimeCount;
        }
    }
}
