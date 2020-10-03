using CardGame_DataAccess.Entities;
using CardGame_DataAccess.Entities.Enums;
using CardGame_DataAccess.Factories;
using CardGame_DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame_DataAccess.DataInitializations
{
    public static class Startup
    {
        private static readonly CardGameDbContext _dbContext;
        private static readonly SetRepository _setRepository;
        private static readonly CardTypeRepository _cardTypeRepository;
        private static readonly SubTypeRepository _subTypeRepository;
        private static readonly RuleRepository _ruleRepository;
        private static readonly CardRepository _cardRepository;
        private static readonly DeckRepository _deckRepository;

        static Startup()
        {
            var dbContextFactory = new CardGameDbContextFactory();
            _dbContext = dbContextFactory.CreateDbContext(new string[0]);
            _setRepository = new SetRepository(_dbContext);
            _cardTypeRepository = new CardTypeRepository(_dbContext);
            _subTypeRepository = new SubTypeRepository(_dbContext);
            _ruleRepository = new RuleRepository(_dbContext);
            _cardRepository = new CardRepository(_dbContext);
            _deckRepository = new DeckRepository(_dbContext);
        }

        public static async Task CreateData()
        {
            await CreateSet();
            await CreateCardTypes();
            await CreateSubTypes();
            await CreateCards();
            await CreateDecks();
        }

        private static async Task CreateDecks()
        {
            var allCards = await _cardRepository.GetCards();
            var allDecks = await _deckRepository.GetDecks();

            if (allDecks.All(d => d.Name != "Init1"))
                await CreateDeck1(allCards);
            if (allDecks.All(d => d.Name != "Init2"))
                await CreateDeck2(allCards);
        }

        private static async Task CreateDeck2(IEnumerable<Card> allCards)
        {
            var deck = new Deck { Name = "Init2" };
            var cards = new List<CardDeck>();
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Throne hall"),
                Amount = 3,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Kathedral city"),
                Amount = 6,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Watch tower"),
                Amount = 6,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Villager"),
                Amount = 20,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Spearman"),
                Amount = 10,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Haste blessing"),
                Amount = 10,
                Deck = deck
            });
            deck.Cards = cards;
            await _deckRepository.CreateDeck(deck);
        }
        private static async Task CreateDeck1(IEnumerable<Card> allCards)
        {
            var deck = new Deck { Name = "Init1" };
            var cards = new List<CardDeck>();
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Kathedral city"),
                Amount = 9,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Blacksmith guild"),
                Amount = 6,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Villager"),
                Amount = 20,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Priest of the dead sun"),
                Amount = 10,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Haste blessing"),
                Amount = 10,
                Deck = deck
            });
            deck.Cards = cards;
            await _deckRepository.CreateDeck(deck);
        }

        private static async Task CreateCards()
        {
            var cards = await _cardRepository.GetCards();
            if (cards.All(c => c.Name != "Kathedral city"))
                await CreateKathedralCity();
            if (cards.All(c => c.Name != "Throne hall"))
                await CreateThroneHall();
            if (cards.All(c => c.Name != "Villager"))
                await CreateVillager();
            if (cards.All(c => c.Name != "Priest of the dead sun"))
                await CreatePriestOfTheDeadSun();
            if (cards.All(c => c.Name != "Spearman"))
                await CreateSpearman();
            if (cards.All(c => c.Name != "Haste blessing"))
                await CreateHasteBlessing();
            if (cards.All(c => c.Name != "Watch tower"))
                await CreateWatchTower();
            if (cards.All(c => c.Name != "Blacksmith guild"))
                await CreateBlacksmithGuild();
            if (cards.All(c => c.Name != "Old knight"))
                await CreateOldKnight();
            if (cards.All(c => c.Name != "Strength blessing"))
                await CreateStrengthBlessing();
        }

        private static async Task CreateStrengthBlessing()
        {
            var card = new Card
            {
                Name = "Strength blessing",
                CostBlue = 1,
                Kind = Kind.Spell,
                InvocationTarget = InvocationTarget.OwnUnit | InvocationTarget.EnemyUnit,
                Rarity = Rarity.Brown,
                Description = "Increase creature attack by 3 till the end of this turn.",
                Color = CardColor.Blue,
                Number = 6,
                Flavour = "Człowiek tak rozgniewany, że może, nie zauważając tego nawet, podnieść trzystufuntową małpę, ma najwyraźniej wiele na głowie.",
            };

            card.Set = await _setRepository.GetSetWithName("The Big Bang");
            card.CardType = await _cardTypeRepository.GetCardTypeWithNameAsync("Spell");
            card.SubType = await _subTypeRepository.GetSubTypeWithNameAsync("Transformation");
            var rule1 = new Rule
            {
                When = "SpellCasting",
                Condition = "OnField('TARGET')",
                Effect = "AddAttack('TARGET',3,1)",
                Description = "Decrease creature cooldown by 1."
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule1 });

            await _cardRepository.CreateCard(card);
        }

        private static async Task CreateOldKnight()
        {
            var card = new Card
            {
                Name = "Old knight",
                CostBlue = 1,
                Cooldown = 1,
                Kind = Kind.Creature,
                InvocationTarget = InvocationTarget.OwnEmptyField,
                Rarity = Rarity.Silver,
                Description = "After each attack his base cooldown is increased by 1.",
                Color = CardColor.Blue,
                Number = 2,
                Flavour = "(…) nie przegrał żadnej bitwy w swoim życiu (…).  Stary juz był i ze starości umarł (…).",
                Health = 2,
                Attack = 2,
            };

            card.Set = await _setRepository.GetSetWithName("The Big Bang");
            card.CardType = await _cardTypeRepository.GetCardTypeWithNameAsync("Human");
            card.SubType = await _subTypeRepository.GetSubTypeWithNameAsync("Knight");
            var rule = new Rule
            {
                When = "UnitAttacked",
                Condition = "OnField('SELF')",
                Effect = "AddBaseCooldown('SELF',1)",
                Description = "After each attack his base cooldown is increased by 1."
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule });

            await _cardRepository.CreateCard(card);
        }

        private static async Task CreateBlacksmithGuild()
        {
            var card = new Card
            {
                Name = "Blacksmith guild",
                CostBlue = 1,
                Cooldown = 1,
                Kind = Kind.Land,
                InvocationTarget = InvocationTarget.None,
                Rarity = Rarity.Silver,
                Description = "Gives 1 basic energy. If you have 2 or 3 blacksmith guilds it give 1 additional basic energy, once per turn.",
                Color = CardColor.Blue,
                Number = 67,
                Flavour = "'-Dziadku, sami jesteśmy kowalami swojego losu.\n- Kup sobie najpierw kuźnię.",
            };

            card.Set = await _setRepository.GetSetWithName("The Big Bang");
            card.CardType = await _cardTypeRepository.GetCardTypeWithNameAsync("Land");
            var rule1 = new Rule
            {
                When = "TurnStarted",
                Condition = "Owner('SELF');Cooldown('SELF',0);OnField('SELF')",
                Effect = "AddEnergy('BLUE',1)",
                Description = "Gives 1 basic energy."
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule1 });
            var rule2 = new Rule
            {
                When = "TurnStarted",
                Condition = "Owner('SELF');OnField('SELF');Controls('SELF','Blacksmith guild',2);TimesPerTurn('Blacksmith guild',1)",
                Effect = "AddEnergy('BLUE',1)",
                Description = "The same turn you play it you can play another land card."
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule2 });

            await _cardRepository.CreateCard(card);
        }

        private static async Task CreateWatchTower()
        {
            var card = new Card
            {
                Name = "Watch tower",
                CostBlue = 1,
                Cooldown = 1,
                Kind = Kind.Land,
                InvocationTarget = InvocationTarget.None,
                Rarity = Rarity.Silver,
                Description = "Gives 1 basic energy. The same turn you play it you can play another land card.",
                Color = CardColor.Blue,
                Number = 68,
                Flavour = "Mocny jak wieża bądź, co się nie zegnie, Chociaż się wicher na jej szczyty wali.",
            };

            card.Set = await _setRepository.GetSetWithName("The Big Bang");
            card.CardType = await _cardTypeRepository.GetCardTypeWithNameAsync("Land");
            var rule1 = new Rule
            {
                When = "TurnStarted",
                Condition = "Owner('SELF');Cooldown('SELF',0);OnField('SELF')",
                Effect = "AddEnergy('BLUE',1)",
                Description = "Gives 1 basic energy."
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule1 });
            var rule2 = new Rule
            {
                When = "CardPlayed",
                Condition = "Owner('SELF');OnField('SELF')",
                Effect = "SetLandCardPlayedFlag('OWNHERO','FALSE')",
                Description = "The same turn you play it you can play another land card."
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule2 });

            await _cardRepository.CreateCard(card);
        }

        private static async Task CreateHasteBlessing()
        {
            var card = new Card
            {
                Name = "Haste blessing",
                CostBlue = 1,
                Kind = Kind.Spell,
                InvocationTarget = InvocationTarget.OwnUnit | InvocationTarget.EnemyUnit,
                Rarity = Rarity.Brown,
                Description = "Decrease creature cooldown by 1.",
                Color = CardColor.Blue,
                Number = 5,
                Flavour = "Nic nie jest bardziej niebezpieczne niż wróg, który nie ma nic do stracenia.",
            };

            card.Set = await _setRepository.GetSetWithName("The Big Bang");
            card.CardType = await _cardTypeRepository.GetCardTypeWithNameAsync("Spell");
            card.SubType = await _subTypeRepository.GetSubTypeWithNameAsync("Transformation");
            var rule1 = new Rule
            {
                When = "SpellCasting",
                Condition = "OnField('TARGET')",
                Effect = "AddCooldown('TARGET',-1)",
                Description = "Decrease creature cooldown by 1."
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule1 });

            await _cardRepository.CreateCard(card);
        }

        private static async Task CreateSpearman()
        {
            var card = new Card
            {
                Name = "Spearman",
                CostBlue = 2,
                Cooldown = 2,
                Kind = Kind.Creature,
                InvocationTarget = InvocationTarget.OwnEmptyField,
                Rarity = Rarity.Brown,
                Description = "Spiky 2. Morale 2+: +2 health",
                Color = CardColor.Blue,
                Number = 10,
                Flavour = "(…) rzuca się w wir walki i wypruwa wrogom flaki włócznią i mieczem, jak przystało na cywilizowanego człowieka!",
                Health = 2,
                Attack = 2,
            };

            card.Set = await _setRepository.GetSetWithName("The Big Bang");
            card.CardType = await _cardTypeRepository.GetCardTypeWithNameAsync("Human");
            card.SubType = await _subTypeRepository.GetSubTypeWithNameAsync("Soldier");
            var rule1 = new Rule
            {
                When = "PlayerInitialized",
                Condition = "Owner('SELF');Times(1)",
                Effect = "Morale('SELF',2)->AddHealth('SELF',2,'INFINITE')",
                Description = "Morale 2+: +2 health"
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule1 });
            var rule2 = new Rule
            {
                When = "UnitBeingAttacking",
                Condition = "OnField('SELF')",
                Effect = "AddHealth('TARGET',-2,'INFINITE')",
                Description = "Spiky 2"
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule2 });

            await _cardRepository.CreateCard(card);
        }

        private static async Task CreatePriestOfTheDeadSun()
        {
            var card = new Card
            {
                Name = "Priest of the dead sun",
                CostBlue = 1,
                Cooldown = 2,
                Kind = Kind.Creature,
                InvocationTarget = InvocationTarget.OwnEmptyField,
                Rarity = Rarity.Brown,
                Description = "When appear on battlefield increase neighbour creatures life by 1 and heals 2 hero hit points.",
                Color = CardColor.Blue,
                Number = 3,
                Flavour = "Ludzie potrzebują wiary w bogów, choćby dlatego, że tak trudno jest wierzyć w ludzi.",
                Health = 2,
                Attack = 1,
            };

            card.Set = await _setRepository.GetSetWithName("The Big Bang");
            card.CardType = await _cardTypeRepository.GetCardTypeWithNameAsync("Human");
            card.SubType = await _subTypeRepository.GetSubTypeWithNameAsync("Priest");
            var rule1 = new Rule
            {
                When = "CardPlayed",
                Condition = "Owner('SELF');OnField('SELF');Times(1)",
                Effect = "AddHealth('NEIGHBOURS',1,'INFINITE')",
                Description = "Increase neighbour creatures life by 1."
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule1 });
            var rule2 = new Rule
            {
                When = "CardPlayed",
                Condition = "Owner('SELF');OnField('SELF');Times(1)",
                Effect = "Heal('OWNHERO',2)",
                Description = "Heals 2 hero hit points."
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule2 });

            await _cardRepository.CreateCard(card);
        }

        private static async Task CreateVillager()
        {
            var card = new Card
            {
                Name = "Villager",
                CostBlue = 1,
                Cooldown = 2,
                Kind = Kind.Creature,
                InvocationTarget = InvocationTarget.OwnEmptyField,
                Rarity = Rarity.Brown,
                Description = "Morale 2+: +1 attack",
                Color = CardColor.Blue,
                Number = 1,
                Flavour = "Na wojnie wszystko jest możliwe. Wojna miesza, zrównuje, chłop czy filozof - wszyscy są do umierania.",
                Health = 2,
                Attack = 2,
            };

            card.Set = await _setRepository.GetSetWithName("The Big Bang");
            card.CardType = await _cardTypeRepository.GetCardTypeWithNameAsync("Land");
            var rule = new Rule
            {
                When = "PlayerInitialized",
                Condition = "Owner('SELF');Times(1)",
                Effect = "Morale('SELF',2)->AddAttack('SELF',1,'INFINITE')",
                Description = "Morale 2+: +1 attack"
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule });

            await _cardRepository.CreateCard(card);
        }
        private static async Task CreateThroneHall()
        {
            var card = new Card
            {
                Name = "Throne hall",
                CostBlue = 2,
                Cooldown = 2,
                Kind = Kind.Land,
                InvocationTarget = InvocationTarget.None,
                Rarity = Rarity.Gold,
                Description = "Gives 3 temporary energy.",
                Color = CardColor.Blue,
                Number = 69,
                Flavour = "W grze o tron zwycięża się albo umiera. Nie ma ziemi niczyjej.",
            };

            card.Set = await _setRepository.GetSetWithName("The Big Bang");
            card.CardType = await _cardTypeRepository.GetCardTypeWithNameAsync("Land");
            var rule = new Rule
            {
                When = "TurnStarted",
                Condition = "Owner('SELF');Cooldown('SELF',0);OnField('SELF')",
                Effect = "AddEnergy('BLUE',3)",
                Description = "Gives 3 temporary energy."
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule });

            await _cardRepository.CreateCard(card);
        }

        private static async Task CreateKathedralCity()
        {
            var card = new Card
            {
                Name = "Kathedral city",
                CostBlue = 0,
                Cooldown = 1,
                Kind = Kind.Land,
                InvocationTarget = InvocationTarget.None,
                Rarity = Rarity.Brown,
                Description = "Gives 1 basic energy.",
                Color = CardColor.Blue,
                Number = 61,
                Flavour = "Miasto z góry lepiej się prezentuje, ludzie wydają się ładniejsi, a najgorsi wyglądają niemalże na dobrych.",
            };

            card.Set = await _setRepository.GetSetWithName("The Big Bang");
            card.CardType = await _cardTypeRepository.GetCardTypeWithNameAsync("Land");
            var rule = new Rule
            {
                When = "TurnStarted",
                Condition = "Owner('SELF');Cooldown('SELF',0);OnField('SELF')",
                Effect = "AddEnergy('BLUE',1)",
                Description = "Gives 1 basic energy."
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule });

            await _cardRepository.CreateCard(card);
        }

        private static async Task CreateSubTypes()
        {
            if ((await _subTypeRepository.GetSubTypeWithNameAsync("Priest")) == null)
                await _subTypeRepository.CreateSubType(new Subtype { Name = "Priest" });
            if ((await _subTypeRepository.GetSubTypeWithNameAsync("Soldier")) == null)
                await _subTypeRepository.CreateSubType(new Subtype { Name = "Soldier" });
            if ((await _subTypeRepository.GetSubTypeWithNameAsync("Transformation")) == null)
                await _subTypeRepository.CreateSubType(new Subtype { Name = "Transformation" });
            if ((await _subTypeRepository.GetSubTypeWithNameAsync("Knight")) == null)
                await _subTypeRepository.CreateSubType(new Subtype { Name = "Knight" });
        }
        private static async Task CreateCardTypes()
        {
            if ((await _cardTypeRepository.GetCardTypeWithNameAsync("Transformation")) == null)
                await _cardTypeRepository.CreateCardTypeAsync(new CardType { Name = "Human" });
            if ((await _cardTypeRepository.GetCardTypeWithNameAsync("Land")) == null)
                await _cardTypeRepository.CreateCardTypeAsync(new CardType { Name = "Land" });
            if ((await _cardTypeRepository.GetCardTypeWithNameAsync("Spell")) == null)
                await _cardTypeRepository.CreateCardTypeAsync(new CardType { Name = "Spell" });
        }
        private static async Task CreateSet()
        {
            if ((await _setRepository.GetSetWithName("The Big Bang")) == null)
                await _setRepository.CreateSet(new Set { Name = "The Big Bang" });
        }
    }
}
