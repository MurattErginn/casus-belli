using UnityEngine;

namespace CasusBelli.Cards
{
    [CreateAssetMenu(fileName = "New Card Ability", menuName = "Casus Belli/Cards/Card Ability")]
    public sealed class CardAbilityData : ScriptableObject
    {
        [SerializeField] private string abilityName;
        [SerializeField, TextArea] private string description;
        [SerializeField] private AbilityTrigger trigger;

        public string AbilityName => abilityName;
        public string Description => description;
        public AbilityTrigger Trigger => trigger;
    }
}
