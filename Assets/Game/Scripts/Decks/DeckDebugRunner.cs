using System.Collections.Generic;
using CasusBelli.Cards;
using UnityEngine;

namespace CasusBelli.Decks
{
    [DisallowMultipleComponent]
    public sealed class DeckDebugRunner : MonoBehaviour
    {
        [SerializeField] private DeckDefinition deckDefinition;
        [SerializeField, Min(0)] private int armyCardsToDraw = 2;
        [SerializeField, Min(0)] private int supportCardsToDraw = 2;
        [SerializeField] private bool runOnStart = true;
        [SerializeField] private bool returnDrawnCardsToBottom = true;

        private void Start()
        {
            if (runOnStart)
            {
                RunDebugDraw();
            }
        }

        [ContextMenu("Run Debug Draw")]
        public void RunDebugDraw()
        {
            if (deckDefinition == null)
            {
                Debug.LogError("Deck debug runner needs a DeckDefinition reference.", this);
                return;
            }

            LogDeckSummary();

            Deck<ArmyCardData> armyDeck = deckDefinition.CreateArmyDeck();
            Deck<SupportCardData> supportDeck = deckDefinition.CreateSupportDeck();

            List<ArmyCardData> drawnArmyCards = DrawCards(armyDeck, armyCardsToDraw, "Army");
            List<SupportCardData> drawnSupportCards = DrawCards(supportDeck, supportCardsToDraw, "Support");

            if (returnDrawnCardsToBottom)
            {
                ReturnCardsToBottom(armyDeck, drawnArmyCards, "Army");
                ReturnCardsToBottom(supportDeck, drawnSupportCards, "Support");
            }

            Debug.Log(
                $"Deck debug finished. Army deck count: {armyDeck.Count}, Support deck count: {supportDeck.Count}.",
                this);
        }

        private void LogDeckSummary()
        {
            string validationStatus = deckDefinition.HasRequiredCardCounts
                ? "valid starter counts"
                : "non-starter test counts";

            Debug.Log(
                $"Testing deck '{deckDefinition.name}' with {deckDefinition.ArmyCards.Count} army cards and " +
                $"{deckDefinition.SupportCards.Count} support cards ({validationStatus}).",
                this);
        }

        private List<TCard> DrawCards<TCard>(Deck<TCard> deck, int drawCount, string deckLabel)
            where TCard : CardData
        {
            List<TCard> drawnCards = new List<TCard>();

            for (int i = 0; i < drawCount; i++)
            {
                if (!deck.TryDraw(out TCard card))
                {
                    Debug.LogWarning($"{deckLabel} deck is empty after drawing {drawnCards.Count} card(s).", this);
                    break;
                }

                drawnCards.Add(card);
                Debug.Log($"Drew {deckLabel} card {i + 1}: {FormatCard(card)}", this);
            }

            Debug.Log($"{deckLabel} deck count after draw: {deck.Count}.", this);
            return drawnCards;
        }

        private void ReturnCardsToBottom<TCard>(Deck<TCard> deck, IReadOnlyList<TCard> cards, string deckLabel)
            where TCard : CardData
        {
            for (int i = 0; i < cards.Count; i++)
            {
                TCard card = cards[i];

                if (card == null)
                {
                    Debug.LogWarning($"Skipped a null {deckLabel} card while returning drawn cards.", this);
                    continue;
                }

                deck.ReturnToBottom(card);
                Debug.Log($"Returned {deckLabel} card to bottom: {FormatCard(card)}", this);
            }

            Debug.Log($"{deckLabel} deck count after return: {deck.Count}.", this);
        }

        private static string FormatCard(CardData card)
        {
            if (card == null)
            {
                return "<null card>";
            }

            string displayName = string.IsNullOrWhiteSpace(card.CardName) ? card.name : card.CardName;

            if (card is ArmyCardData armyCard)
            {
                return $"{displayName} [{armyCard.CardType}, Power {armyCard.Power}]";
            }

            if (card is SupportCardData supportCard)
            {
                return $"{displayName} [{supportCard.CardType}, Speed {supportCard.Speed}]";
            }

            return $"{displayName} [{card.CardType}]";
        }
    }
}
