namespace SimulatedAnnealing
{
    public interface IProblemState
    {
        double GetEnergy();
        IProblemState GetNextState();
    }

    public class SimulatedAnnealingSolver
    {
        private readonly Random Random = new Random();

        public IProblemState Solve(IProblemState initialState, double initialTemperature,
                                   double coolingRate, int numberOfIterations, double logOutputPercent = 0)
        {
            IProblemState currentState = initialState;
            double temperature = initialTemperature;
            int logInterval = (int)(numberOfIterations * logOutputPercent);

            for (int i = 0; i < numberOfIterations; i++)
            {
                IProblemState nextState = currentState.GetNextState();
                double energyDelta = nextState.GetEnergy() - currentState.GetEnergy();

                if (energyDelta < 0 || Random.NextDouble() < Math.Exp(-energyDelta / temperature))
                {
                    currentState = nextState;
                }

                temperature *= coolingRate;

                if (logOutputPercent != 0 && i % logInterval == 0)
                {
                    Console.WriteLine($"Iteration {i}: Energy = {currentState.GetEnergy()}, Temperature = {temperature}");
                }
            }

            return currentState;
        }
    }
}
