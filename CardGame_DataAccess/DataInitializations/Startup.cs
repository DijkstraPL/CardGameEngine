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

            if (allDecks.Any(d => d.Name == "Init1"))
                await _deckRepository.RemoveDeck("Init1");
            if (allDecks.Any(d => d.Name == "Init2"))
                await _deckRepository.RemoveDeck("Init2");

            await CreateDeck1(allCards);
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
                Amount = 9,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Watch tower"),
                Amount = 3,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Catapult"),
                Amount = 3,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Spearman"),
                Amount = 3,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Haste blessing"),
                Amount = 3,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Wall"),
                Amount = 3,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Chapel"),
                Amount = 3,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Battle eagle"),
                Amount = 20,
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
                Amount = 12,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Blacksmith guild"),
                Amount = 3,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Villager"),
                Amount = 3,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Crossbowman"),
                Amount = 3,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Priest of the dead sun"),
                Amount = 3,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Hound"),
                Amount = 3,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "High priest of the dead sun"),
                Amount = 3,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Strength blessing"),
                Amount = 3,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Defender of comrades"),
                Amount = 6,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Highest priest of the dead sun"),
                Amount = 5,
                Deck = deck
            });
            cards.Add(new CardDeck
            {
                Card = allCards.FirstOrDefault(c => c.Name == "Morale boost"),
                Amount = 6,
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
            if (cards.All(c => c.Name != "Crossbowman"))
                await CreateCrossbowman();
            if (cards.All(c => c.Name != "Wall"))
                await CreateWall();
            if (cards.All(c => c.Name != "Hound"))
                await CreateHound();
            if (cards.All(c => c.Name != "Chapel"))
                await CreateChapel();
            if (cards.All(c => c.Name != "High priest of the dead sun"))
                await CreateHighPriestOfTheDeadSun();
            if (cards.All(c => c.Name != "Catapult"))
                await CreateCatapult();
            if (cards.All(c => c.Name != "Defender of comrades"))
                await CreateDefenderOfComrades();
            if (cards.All(c => c.Name != "Battle eagle"))
                await CreateBattleEagle();
            if (cards.All(c => c.Name != "Highest priest of the dead sun"))
                await CreateHighestPriestOfTheDeadSun();
            if (cards.All(c => c.Name != "Morale boost"))
                await CreateMoraleBoost();
        }

        private static async Task CreateMoraleBoost()
        {
            var card = new Card
            {
                Name = "Morale boost",
                CostBlue = 3,
                Kind = Kind.Spell,
                InvocationTarget = InvocationTarget.None,
                Rarity = Rarity.Brown,
                Description = "Add 3 morale.",
                Color = CardColor.Blue,
                Number = 36,
                Flavour = "Człowieka można zniszczyć, ale nie pokonać.",
            };

            card.Set = await _setRepository.GetSetWithName("The Big Bang");
            card.CardType = await _cardTypeRepository.GetCardTypeWithNameAsync("Spell");
            card.SubType = await _subTypeRepository.GetSubTypeWithNameAsync("Transformation");
            var rule1 = new Rule
            {
                Effect = "MoraleBoost(3)",
                Description = "Add 3 morale."
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule1 });

            await _cardRepository.CreateCard(card);
        }

        private static async Task CreateHighestPriestOfTheDeadSun()
        {
            var card = new Card
            {
                Name = "Highest priest of the dead sun",
                CostBlue = 5,
                Cooldown = 3,
                Kind = Kind.Creature,
                InvocationTarget = InvocationTarget.OwnEmptyField,
                Rarity = Rarity.Gold,
                Description = "When his cooldown becomes 0 he decrease cooldown of neighbour creatures by 2.",
                Color = CardColor.Blue,
                Number = 52,
                Flavour = "Jednak w porównaniu z innymi Najwyższy Kapłan sprawia wrażenie, zdrowego, psychotycznego pojeba.",
                Health = 6,
                Attack = 4,
            };

            card.Set = await _setRepository.GetSetWithName("The Big Bang");
            card.CardType = await _cardTypeRepository.GetCardTypeWithNameAsync("Human");
            card.SubType = await _subTypeRepository.GetSubTypeWithNameAsync("Priest");
            var rule = new Rule
            {
                Effect = "HighestPriestOfTheDeadSun(2)",
                Description = "When his cooldown becomes 0 he decrease cooldown of neighbour creatures by 2."
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule });

            await _cardRepository.CreateCard(card);
        }

        private static async Task CreateBattleEagle()
        {
            var card = new Card
            {
                Name = "Battle eagle",
                CostBlue = 4,
                Cooldown = 2,
                Kind = Kind.Creature,
                InvocationTarget = InvocationTarget.OwnEmptyField,
                Rarity = Rarity.Brown,
                Description = "",
                Trait = Trait.Flying,
                Color = CardColor.Blue,
                Number = 43,
                Flavour = "Za dużo orłów, za mało drobiu.",
                Health = 4,
                Attack = 5,
            };

            card.Set = await _setRepository.GetSetWithName("The Big Bang");
            card.CardType = await _cardTypeRepository.GetCardTypeWithNameAsync("Beast");
           
            await _cardRepository.CreateCard(card);
        }

        private static async Task CreateDefenderOfComrades()
        {
            var card = new Card
            {
                Name = "Defender of comrades",
                CostBlue = 2,
                Cooldown = 2,
                Kind = Kind.Creature,
                InvocationTarget = InvocationTarget.OwnEmptyField,
                Rarity = Rarity.Brown,
                Description = "For each morale it gets +1 attack.",
                Trait= Trait.Defender,
                Color = CardColor.Blue,
                Number = 14,
                Flavour = "Żeby chronić siebie i to, co kochamy, walczymy jak bestie.",
                Health = 3,
                Attack = 0,
            };

            card.Set = await _setRepository.GetSetWithName("The Big Bang");
            card.CardType = await _cardTypeRepository.GetCardTypeWithNameAsync("Human");
            card.SubType = await _subTypeRepository.GetSubTypeWithNameAsync("Soldier");
            var rule = new Rule
            {
                Effect = "DefenderOfComrades",
                Description = "For each morale it gets +1 attack."
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule });

            await _cardRepository.CreateCard(card);
        }

        private static async Task CreateCatapult()
        {
            var card = new Card
            {
                Name = "Catapult",
                CostBlue = 3,
                Cooldown = 3,
                Kind = Kind.Structure,
                InvocationTarget = InvocationTarget.OwnEmptyField,
                Rarity = Rarity.Brown,
                Description = "When come to play it cooldown is the same as the smalest cooldown of your catapults.",
                Color = CardColor.Blue,
                Trait = Trait.DistanceAttack,
                Number = 25,
                Flavour = "Dlaczego nie są na tyle cywilizowani, żeby atakować mnie trochę wolniej?",
                Health = 4,
                Attack = 4,
            };

            card.Set = await _setRepository.GetSetWithName("The Big Bang");
            card.CardType = await _cardTypeRepository.GetCardTypeWithNameAsync("Structure");
            card.SubType = await _subTypeRepository.GetSubTypeWithNameAsync("Artillery");
            var rule = new Rule
            {
                Effect = "Catapult",
                Description = "When come to play it cooldown is the same as the smalest cooldown of your catapults."
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule });

            await _cardRepository.CreateCard(card);
        }


        private static async Task CreateHighPriestOfTheDeadSun()
        {
            var card = new Card
            {
                Name = "High priest of the dead sun",
                CostBlue = 2,
                Cooldown = 2,
                Kind = Kind.Creature,
                InvocationTarget = InvocationTarget.OwnEmptyField,
                Rarity = Rarity.Silver,
                Description = "Increase neighbour creatures attack by 1.",
                Color = CardColor.Blue,
                Number = 11,
                Flavour = "Kapłani zakryli przed ludźmi słońce, a dali im świeczkę.",
                Health = 3,
                Attack = 1,
            };

            card.Set = await _setRepository.GetSetWithName("The Big Bang");
            card.CardType = await _cardTypeRepository.GetCardTypeWithNameAsync("Human");
            card.SubType = await _subTypeRepository.GetSubTypeWithNameAsync("Priest");
            var rule = new Rule
            {
                Effect = "HighPriestOfTheDeadSun(1)",
                Description = "Increase neighbour creatures attack by 1."
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule });

            await _cardRepository.CreateCard(card);
        }

        private static async Task CreateHound()
        {
            var card = new Card
            {
                Name = "Hound",
                CostBlue = 2,
                Cooldown = 2,
                Kind = Kind.Creature,
                InvocationTarget = InvocationTarget.OwnEmptyField,
                Rarity = Rarity.Brown,
                Description = "Suppressor 2",
                Color = CardColor.Blue,
                Number = 15,
                Flavour = "Psu jest wszystko jedno, czy jesteś biedny, czy bogaty, wykształcony czy analfabeta, mądry czy głupi. Daj mu serce, a on odda ci swoje.",
                Health = 2,
                Attack = 2,
            };

            card.Set = await _setRepository.GetSetWithName("The Big Bang");
            card.CardType = await _cardTypeRepository.GetCardTypeWithNameAsync("Beast");
            var rule = new Rule
            {
                Effect = "Hound(2)",
                Description = "Suppressor 2"
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule });

            await _cardRepository.CreateCard(card);
        }

        private static async Task CreateWall()
        {
            var card = new Card
            {
                Name = "Wall",
                CostBlue = 1,
                Cooldown = null,
                Kind = Kind.Structure,
                InvocationTarget = InvocationTarget.OwnEmptyField,
                Rarity = Rarity.Brown,
                Trait = Trait.Defender,
                Description = "",
                Color = CardColor.Blue,
                Number = 7,
                Flavour = "Jak to się dzieje, że gdy tylko ktoś zbuduje mur, ktoś inny zaraz chce wiedzieć, co jest po jego drugiej stronie?",
                Health = 4,
                Attack = null,
            };

            card.Set = await _setRepository.GetSetWithName("The Big Bang");
            card.CardType = await _cardTypeRepository.GetCardTypeWithNameAsync("Structure");
            card.SubType = await _subTypeRepository.GetSubTypeWithNameAsync("Construction");

            await _cardRepository.CreateCard(card);
        }
        private static async Task CreateChapel()
        {
            var card = new Card
            {
                Name = "Chapel",
                CostBlue = 2,
                Cooldown = null,
                Kind = Kind.Structure,
                InvocationTarget = InvocationTarget.OwnEmptyField,
                Rarity = Rarity.Brown,
                Description = "Increase health of all creatures by 1.",
                Color = CardColor.Blue,
                Number = 20,
                Flavour = "Byli też misjonarze. Wykarczowali pole za wioską, postawili kaplicę, a potem zaprosili Carapana na pogadankę. Okazało się, że ten ich nowy Bóg jest taki sam jak nasz (…)",
                Health = 3,
                Attack = null,
            };

            card.Set = await _setRepository.GetSetWithName("The Big Bang");
            card.CardType = await _cardTypeRepository.GetCardTypeWithNameAsync("Structure");
            card.SubType = await _subTypeRepository.GetSubTypeWithNameAsync("Totem");

            var rule1 = new Rule
            {
                Effect = "Chapel(1)",
                Description = "Increase health of all creatures by 1."
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule1 });
            await _cardRepository.CreateCard(card);
        }

        private static async Task CreateCrossbowman()
        {
            var card = new Card
            {
                Name = "Crossbowman",
                CostBlue = 1,
                Cooldown = 3,
                Kind = Kind.Creature,
                InvocationTarget = InvocationTarget.OwnEmptyField,
                Rarity = Rarity.Brown,
                Trait= Trait.DistanceAttack,
                Description = "",
                Color = CardColor.Blue,
                Number = 4,
                Flavour = "Coś brzęknęło - to kusza Cuddy’ego wystrzeliła mu w ręku. Bełt świsnął koło ucha kaprala Nobbsa i wylądował w rzece, gdzie utknął.",
                Health = 2,
                Attack = 4,
            };

            card.Set = await _setRepository.GetSetWithName("The Big Bang");
            card.CardType = await _cardTypeRepository.GetCardTypeWithNameAsync("Human");
            card.SubType = await _subTypeRepository.GetSubTypeWithNameAsync("Soldier");
          
            await _cardRepository.CreateCard(card);
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
                Effect = "StrengthBlessing(3)",
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
                Effect = "OldKnight(1)",
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
                Effect = "BlacksmithGuild(1)",
                Description = "Gives 1 basic energy."
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule1 });
       
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
                Effect = "WatchTower(1)",
                Description = "Gives 1 basic energy."
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule1 });
          
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
                Effect = "HasteBlessing",
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
                Effect = "Spearman(2,2,2)",
                Description = "Morale 2+: +2 health"
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule1 });
           
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
                Effect = "PriestOfTheDeadSun",
                Description = "Increase neighbour creatures life by 1."
            };
            card.Rules.Add(new CardRule { Card = card, Rule = rule1 });
            
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
                Effect = "Villager(2,1)",
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
                Effect = "BasicBlueLand(3)",
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
                Effect = "BasicBlueLand(1)",
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
            if ((await _subTypeRepository.GetSubTypeWithNameAsync("Construction")) == null)
                await _subTypeRepository.CreateSubType(new Subtype { Name = "Construction" });
            if ((await _subTypeRepository.GetSubTypeWithNameAsync("Totem")) == null)
                await _subTypeRepository.CreateSubType(new Subtype { Name = "Totem" });
            if ((await _subTypeRepository.GetSubTypeWithNameAsync("Artillery")) == null)
                await _subTypeRepository.CreateSubType(new Subtype { Name = "Artillery" });
        }
        private static async Task CreateCardTypes()
        {
            if ((await _cardTypeRepository.GetCardTypeWithNameAsync("Transformation")) == null)
                await _cardTypeRepository.CreateCardTypeAsync(new CardType { Name = "Human" });
            if ((await _cardTypeRepository.GetCardTypeWithNameAsync("Land")) == null)
                await _cardTypeRepository.CreateCardTypeAsync(new CardType { Name = "Land" });
            if ((await _cardTypeRepository.GetCardTypeWithNameAsync("Spell")) == null)
                await _cardTypeRepository.CreateCardTypeAsync(new CardType { Name = "Spell" });
            if ((await _cardTypeRepository.GetCardTypeWithNameAsync("Structure")) == null)
                await _cardTypeRepository.CreateCardTypeAsync(new CardType { Name = "Structure" });
            if ((await _cardTypeRepository.GetCardTypeWithNameAsync("Beast")) == null)
                await _cardTypeRepository.CreateCardTypeAsync(new CardType { Name = "Beast" });
        }
        private static async Task CreateSet()
        {
            if ((await _setRepository.GetSetWithName("The Big Bang")) == null)
                await _setRepository.CreateSet(new Set { Name = "The Big Bang" });
        }
    }
}
