using UnityEngine;

namespace CasusBelli.Cards
{
    public abstract class CardData : ScriptableObject
    {
        [SerializeField] private string cardName;
        [SerializeField, TextArea] private string description;
        [SerializeField] private Sprite artwork;

        public string CardName => cardName;
        public string Description => description;
        public Sprite Artwork => artwork;
        public abstract CardType CardType { get; }
    }
}
