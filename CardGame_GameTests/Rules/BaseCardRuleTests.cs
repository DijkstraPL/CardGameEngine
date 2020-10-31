using CardGame_Data.Data;
using CardGame_Data.Data.Enums;
using CardGame_Game.Cards;
using CardGame_Game.Game;
using CardGame_Game.Game.Interfaces;
using CardGame_Game.GameEvents;
using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Players.Interfaces;
using Moq;

namespace CardGame_GameTests.Rules
{
    public abstract class BaseCardRuleTests
    {
        protected Mock<IPlayer> _player;
        protected Mock<GameCard> _sourceCard;
        protected Mock<IGameEventsContainer> _gameEventsContainer;
        protected Mock<IGame> _game;

        protected UnitAttackedEvent _unitAttackedEvent;
        protected CardPlayedEvent _cardPlayedEvent;
        protected TurnStartedEvent _turnStartedEvent;
        protected PlayerInitializedEvent _playerInitializedEvent;
        protected SpellCastingEvent _spellCastingEvent;
        protected UnitBeingAttackingEvent _unitBeingAttackingEvent;

        protected GameEventArgs _gameEventArgs;

        protected void InitEvents()
        {
            _gameEventsContainer = new Mock<IGameEventsContainer>();
            _game = new Mock<IGame>();

            _unitAttackedEvent = new UnitAttackedEvent();
            _cardPlayedEvent = new CardPlayedEvent();
            _turnStartedEvent = new TurnStartedEvent();
            _playerInitializedEvent = new PlayerInitializedEvent();
            _spellCastingEvent = new SpellCastingEvent();
            _unitBeingAttackingEvent = new UnitBeingAttackingEvent();

            _gameEventsContainer.Setup(ge => ge.UnitAttackedEvent).Returns(_unitAttackedEvent);
            _gameEventsContainer.Setup(ge => ge.CardPlayedEvent).Returns(_cardPlayedEvent);
            _gameEventsContainer.Setup(ge => ge.TurnStartedEvent).Returns(_turnStartedEvent);
            _gameEventsContainer.Setup(ge => ge.PlayerInitializedEvent).Returns(_playerInitializedEvent);
            _gameEventsContainer.Setup(ge => ge.SpellCastingEvent).Returns(_spellCastingEvent);
            _gameEventsContainer.Setup(ge => ge.UnitBeingAttackingEvent).Returns(_unitBeingAttackingEvent);
        }
        protected void InitPlayer()
        {
            _player = new Mock<IPlayer>();
        }
        protected void InitGame()
        {
            _game = new Mock<IGame>();
        }
        protected void InitSourceCard()
        {
            var card = new Card();
            _sourceCard = new Mock<GameCard>(_player.Object, card, "a", "b", 1, InvocationTarget.None);

        }
        protected void InitGameEventArgs()
        {
            _gameEventArgs = new GameEventArgs
            {
                SourceCard = _sourceCard.Object,
                Player = _player.Object,
                Game = _game.Object
            };
        }
    }
}
