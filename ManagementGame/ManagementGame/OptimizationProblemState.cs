using SimulatedAnnealing;

namespace ManagementGame
{
    public class OptimizationProblemState : IProblemState
    {
        public TradingAction[] TradingActions { get; }
        private static readonly Random Random = new Random();

        private readonly IRule Rule;
        private readonly SimulationResultAnalyzer Analyzer;

        public int minCount { get; set; } = 0;
        public int maxCount { get; set; } = 50;
        public OptimizationProblemState(TradingAction[] tradingActions, IRule rule)
        {
            TradingActions = tradingActions;
            Rule = rule;
            Analyzer = new SimulationResultAnalyzer(TradingActions, rule);
        }

        public double GetEnergy()
        {
            return -1 * Analyzer.Result();
        }

        public IProblemState GetNextState()
        {
            TradingAction[] newTradingActions = (TradingAction[])TradingActions.Clone();
            int indexToChange = Random.Next(TradingActions.Length);

            Action newAction;
            int newAmount;

            if(indexToChange == 0)
            {
                newAction = Action.Hire;
                newAmount = Random.Next(minCount, maxCount + 1);
            }
            else if(indexToChange == TradingActions.Length - 1)
            {
                newAction = Action.Sell;
                newAmount = Random.Next(minCount, maxCount + 1);
            }
            else if (TradingActions[indexToChange - 1].action == Action.Buy)
            {
                newAction = Action.Sell;
                newAmount = maxCount;
            }
            else
            {
                do
                {
                    newAction = (Action)Random.Next(Enum.GetNames(typeof(Action)).Length);
                } while (newAction == TradingActions[indexToChange - 1].action || newAction == TradingActions[indexToChange + 1].action);

                newAmount = Random.Next(minCount, maxCount + 1);
            }

            newTradingActions[indexToChange] = new TradingAction(newAction, newAmount);

            return new OptimizationProblemState(newTradingActions, Rule);
        }

        public TradingAction[] GetActions()
        {
            return TradingActions;
        }
    }
}
