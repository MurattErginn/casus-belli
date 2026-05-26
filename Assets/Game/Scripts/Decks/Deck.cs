using System;
using System.Collections.Generic;
using CasusBelli.Cards;

namespace CasusBelli.Decks
{
    public sealed class Deck<TCard> where TCard : CardData
    {
        private readonly Queue<TCard> cards;

        public Deck(IEnumerable<TCard> initialCards)
        {
            if (initialCards == null)
            {
                throw new ArgumentNullException(nameof(initialCards));
            }

            cards = new Queue<TCard>(initialCards);
        }

        public int Count => cards.Count;
        public bool IsEmpty => cards.Count == 0;

        public TCard Draw()
        {
            if (cards.Count == 0)
            {
                throw new InvalidOperationException("Cannot draw from an empty deck.");
            }

            return cards.Dequeue();
        }

        public bool TryDraw(out TCard card)
        {
            if (cards.Count == 0)
            {
                card = null;
                return false;
            }

            card = cards.Dequeue();
            return true;
        }

        public void ReturnToBottom(TCard card)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card));
            }

            cards.Enqueue(card);
        }
    }
}
