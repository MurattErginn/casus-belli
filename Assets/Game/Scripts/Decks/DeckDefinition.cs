using System.Collections.Generic;
using CasusBelli.Cards;
using UnityEngine;

namespace CasusBelli.Decks
{
    [CreateAssetMenu(fileName = "New Deck Definition", menuName = "Casus Belli/Decks/Deck Definition")]
    public sealed class DeckDefinition : ScriptableObject
    {
        public const int RequiredArmyCardCount = 15;
        public const int RequiredSupportCardCount = 10;

        [SerializeField] private List<ArmyCardData> armyCards = new List<ArmyCardData>();
        [SerializeField] private List<SupportCardData> supportCards = new List<SupportCardData>();

        public IReadOnlyList<ArmyCardData> ArmyCards => armyCards;
        public IReadOnlyList<SupportCardData> SupportCards => supportCards;
        public bool HasRequiredCardCounts =>
            armyCards.Count == RequiredArmyCardCount &&
            supportCards.Count == RequiredSupportCardCount;

        public Deck<ArmyCardData> CreateArmyDeck()
        {
            return new Deck<ArmyCardData>(armyCards);
        }

        public Deck<SupportCardData> CreateSupportDeck()
        {
            return new Deck<SupportCardData>(supportCards);
        }
    }
}
