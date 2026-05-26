using System.Collections.Generic;
using UnityEngine;

namespace CasusBelli.Cards
{
    [CreateAssetMenu(fileName = "New Army Card", menuName = "Casus Belli/Cards/Army Card")]
    public sealed class ArmyCardData : CardData
    {
        [SerializeField, Min(0)] private int power;
        [SerializeField] private CardAbilityData[] abilities;

        public override CardType CardType => CardType.Army;
        public int Power => power;
        public IReadOnlyList<CardAbilityData> Abilities => abilities;
    }
}
