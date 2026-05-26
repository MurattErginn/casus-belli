using UnityEngine;

namespace CasusBelli.Cards
{
    [CreateAssetMenu(fileName = "New Support Card", menuName = "Casus Belli/Cards/Support Card")]
    public sealed class SupportCardData : CardData
    {
        [SerializeField] private SupportSpeed speed;
        [SerializeField] private CardAbilityData primaryAbility;
        [SerializeField] private CardAbilityData secondaryAbility;

        public override CardType CardType => CardType.Support;
        public SupportSpeed Speed => speed;
        public CardAbilityData PrimaryAbility => primaryAbility;
        public CardAbilityData SecondaryAbility => secondaryAbility;
    }
}
