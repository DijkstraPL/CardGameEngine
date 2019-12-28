namespace CardGame_Test.Cards.Order
{
    internal interface IMoraleEffect
    {
        int RequiredMorale { get; }
        void IncludeMoraleEffect();
    }
}