using CardGame_Game.GameEvents.Interfaces;
using CardGame_Game.Helpers;
using CardGame_Game.Rules.Interfaces;
using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CardGame_Game.Cards.Triggers
{
   public  class RuleMapper
    {
        private IList<(IRule rule, string[] args)> _rules = new List<(IRule rule, string[] args)>();
        public IEnumerable<(IRule rule, string[] args)> Rules => _rules;

        private readonly IGameEventsContainer _gameEventsContainer;
        private readonly GameCard _gameCard;

        public RuleMapper(IGameEventsContainer gameEventsContainer, GameCard gameCard, string rule)
        {
            _gameEventsContainer = gameEventsContainer ?? throw new ArgumentNullException(nameof(gameEventsContainer));
            _gameCard = gameCard ?? throw new ArgumentNullException(nameof(gameCard));

            SetRules(rule);
            foreach (var ruleData in Rules)
                ruleData.rule.Init(_gameCard, _gameEventsContainer, ruleData.args);
        }

        private void SetRules(string rules)
        {
            var splittedRuleData = rules?.Split(';') ?? new string[0];
            foreach (var ruleString in splittedRuleData)
            {
                var ruleName = ruleString.IndexOf('(') == -1 ? ruleString : ruleString.Substring(0, ruleString.IndexOf('('));
                var rule = GetRule(ruleName);
                var ruleArgs = ruleString.Contains('(') ? ruleString.EverythingBetween("(", ")").First().Split(',') : new string[0];
                var finalArgs = ruleArgs.ToList().Select(c => c.Replace("\'", string.Empty)).ToArray();
                _rules.Add((rule, finalArgs));
            }
        }

        private IRule GetRule(string ruleName)
        {
            IRule rule;
            var configuration = new ContainerConfiguration()
               .WithAssembly(typeof(GameCard).GetTypeInfo().Assembly);

            using (var container = configuration.CreateContainer())
            {
                if (!container.TryGetExport(ruleName, out rule))
                    throw new InvalidOperationException(nameof(ruleName));
            }

            return rule;
        }
    }
}
