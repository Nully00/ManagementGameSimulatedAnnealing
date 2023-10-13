namespace ManagementGame
{
    public readonly struct TradingAction
    {
        public TradingAction(Action action, int amount)
        {
            this.action = action;
            this.amount = amount;
        }

        public readonly Action action { get; }
        public readonly int amount { get; }
    }
}
