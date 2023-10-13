using SimulatedAnnealing;

namespace ManagementGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var numberOfAttempts = 1000;
            var tempRange = (min: 1000.0, max: 10000.0);
            var coolingRateRange = (min: 0.0001, max: 0.05);
            var iterationsRange = (min: 500, max: 10000);
            Random random = new Random();


            TradingAction[] initialActions = new TradingAction[35];
            for (int i = 0; i < initialActions.Length; i++)
            {
                initialActions[i] = new TradingAction(Action.Hire, 1);
            }

            IRule rule = new Rule();
            OptimizationProblemState initialState = new OptimizationProblemState(initialActions, rule);

            SimulatedAnnealingSolver simulatedAnnealingSolver = new SimulatedAnnealingSolver();

            IProblemState? bestState = null;
            double bestEnergy = double.MaxValue;

            for (int attempt = 0; attempt < numberOfAttempts; attempt++)
            {
                double initialTemperature = random.NextDouble() * (tempRange.max - tempRange.min) + tempRange.min;
                double coolingRate = random.NextDouble() * (coolingRateRange.max - coolingRateRange.min) + coolingRateRange.min;
                int numberOfIterations = random.Next(iterationsRange.min, iterationsRange.max + 1);

                IProblemState optimizedState = simulatedAnnealingSolver.Solve(initialState, initialTemperature, coolingRate, numberOfIterations, 0);
                double energy = optimizedState.GetEnergy();

                Console.WriteLine($"Attempt {attempt + 1}: Current Energy = {energy}, Best Energy so far = {bestEnergy}");

                if (energy < bestEnergy)
                {
                    bestEnergy = energy;
                    bestState = optimizedState;

                    Console.WriteLine($"New best energy found: {bestEnergy}");
                }
            }


            Console.WriteLine("-----Result-----");

            TradingAction[] optimizedActions = ((OptimizationProblemState)bestState).GetActions();
            SimulationResultAnalyzer simulationResultAnalyzer = new SimulationResultAnalyzer(optimizedActions, rule);
            simulationResultAnalyzer.IsLoggingEnabled = true;
            simulationResultAnalyzer.Result();
        }
    }
}