using System;

public class Player
{
    // Fields
    private int cardCount;
    private List<Card> hand;

    // Constructor
    public Player()
    {
        cardCount = 0;
        hand = new List<Card>();
    }

    // Method to receive a card
    public void getCard(Card card)
    {
        hand.Add(card);
        cardCount++;
    }

    // Method to play a card from hand
    public Card playCard(int index)
    {
        if (index < 0 || index >= cardCount)
        {
            throw new ArgumentOutOfRangeException("Index out of range");
        }
        Card playedCard = hand[index];
        hand.RemoveAt(index);
        cardCount--;
        return playedCard;
    }

    // Method to get the count of cards in hand
    public int CardCount
    {
        get { return cardCount; }
    }

    // Method to check if hand is empty
    public bool isHandEmpty()
    {
        return cardCount == 0;
    }

    // Method to show the hand
    public void showHand()
    {
        foreach (var card in hand)
        {
            Console.WriteLine($"{card.Rank} of {card.Suit} {(card.IsFaceUp ? "(Face Up)" : "(Face Down)")}");
        }
    }

    // Method to sort the hand 
    public void sortHand()
    {
        hand = hand.OrderBy(card => card.Rank).ThenBy(card => card.Suit).ToList();
    }
}