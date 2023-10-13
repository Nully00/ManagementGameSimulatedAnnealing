namespace ManagementGame
{
    public interface IRule
    {
        int GetInitialFunds();
        int MaxPurchasableItems(int money, int currentEmployeeCapacity);
        int Purchase(int funds, int quantity);
        int MaxSellableItems(int currentStock, int currentEmployeeCapacity);
        int Sell(int funds, int quantity);
        int HirePartTime(int funds, int quantity);
        int HireFullTime(int funds, int quantity);
        int MaxHireablePartTimeEmployees(int funds, int currentPartTimeEmployees);
        int MaxHireableFullTimeEmployees(int funds, int currentFullTimeEmployees);
        int GetCurrentEmployeePower(int partTimeCount, int fullTimeCount);
        int FinalFullTimeEmployeeCost(int fullTimeCount);
    }
}
