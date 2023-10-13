namespace ManagementGame
{
    public class SimulationResultAnalyzer
    {
        private readonly IRule Rule;
        private readonly TradingAction[] TradingActions;
        public bool IsLoggingEnabled { get; set; }

        public SimulationResultAnalyzer(TradingAction[] tradingActions, IRule rule)
        {
            Rule = rule ?? throw new ArgumentNullException(nameof(rule));
            TradingActions = tradingActions ?? throw new ArgumentNullException(nameof(tradingActions));
            IsLoggingEnabled = false;
        }

        private void Log(string message)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine(message);
            }
        }

        public int Result()
        {
            int funds = Rule.GetInitialFunds();
            int partTimeCount = 0;
            int fullTimeCount = 0;
            int itemCount = 0;

            Log($"Initial funds: {funds} \n");

            foreach (var tradingAction in TradingActions)
            {
                Log($"Processing action: {tradingAction.action}, amount: {tradingAction.amount}");

                switch (tradingAction.action)
                {
                    case Action.Buy:
                        ProcessBuying(tradingAction.amount);
                        break;
                    case Action.Sell:
                        ProcessSelling(tradingAction.amount);
                        break;
                    case Action.Hire:
                        ProcessHiring(tradingAction.amount);
                        break;
                    default:
                        throw new InvalidOperationException("Invalid action type.");
                }

                Log($"Funds after action: {funds}, Item count: {itemCount}, Part-time: {partTimeCount}, Full-time: {fullTimeCount}\n");
            }

            funds -= Rule.FinalFullTimeEmployeeCost(fullTimeCount);

            Log($"Final funds after settlement: {funds}");

            return funds;

            void ProcessBuying(int requestedPurchaseCount)
            {
                int employeePower = Rule.GetCurrentEmployeePower(partTimeCount, fullTimeCount);
                int maxPurchaseCount = Rule.MaxPurchasableItems(funds, employeePower);
                int actualPurchaseCount = Math.Min(maxPurchaseCount, requestedPurchaseCount);
                funds = Rule.Purchase(funds, actualPurchaseCount);
                itemCount += actualPurchaseCount;

                Log($"Bought {actualPurchaseCount} items");
            }

            void ProcessSelling(int requestedSellCount)
            {
                int employeePower = Rule.GetCurrentEmployeePower(partTimeCount, fullTimeCount);
                int maxSellCount = Rule.MaxSellableItems(itemCount, employeePower);
                int actualSellCount = Math.Min(maxSellCount, requestedSellCount);
                funds = Rule.Sell(funds, actualSellCount);
                itemCount -= actualSellCount;

                Log($"Sold {actualSellCount} items");
            }

            void ProcessHiring(int requestedHireCount)
            {
                int maxPartTimeHireCount = Rule.MaxHireablePartTimeEmployees(funds, partTimeCount);
                int actualPartTimeHireCount = Math.Min(maxPartTimeHireCount, requestedHireCount);

                HirePartTimeEmployees(actualPartTimeHireCount);

                int remainingHireCount = requestedHireCount - actualPartTimeHireCount;
                HireFullTimeEmployees(remainingHireCount);
            }

            void HirePartTimeEmployees(int count)
            {
                if (count <= 0) return;

                funds = Rule.HirePartTime(funds, count);
                partTimeCount += count;

                Log($"Hired {count} part-time employees");
            }

            void HireFullTimeEmployees(int count)
            {
                if (count <= 0) return;

                int maxFullTimeHireCount = Rule.MaxHireableFullTimeEmployees(funds,fullTimeCount);
                int actualFullTimeHireCount = Math.Min(maxFullTimeHireCount, count);

                funds = Rule.HireFullTime(funds, actualFullTimeHireCount);
                fullTimeCount += actualFullTimeHireCount;

                Log($"Hired {actualFullTimeHireCount} full-time employees");
            }
        }
    }
}
