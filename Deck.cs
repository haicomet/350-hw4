using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Deck
{
    List<Card> cards = new List<Card>();

    //Deck Constructor
    public Deck()
    {
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                cards.Add(new Card(suit, rank));
            }
        }
    }

    //Implement a property to get Cards
    public List<Card> Cards
    {
        get { return cards; }
    }


    //Takes top card from deck (if deck is not empty, otherwise return null)
    public Card TakeTopCard()
    {
        //implementation
        if (cards.Count == 0) throw new InvalidOperationException("Deck is empty!");
        Card topCard = cards[0];
        cards.RemoveAt(0);
        return topCard;
    }

    //Shuffle Method
    public void Shuffle()
    {
        //implementation
        Random rand = new Random();
        int n = cards.Count;
        for (int i = 0; i < n; i++)
        {
            int j = rand.Next(i, n);
            Card temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }
    }

    //Cut method
    public void Cut(int index)
    {
        //implementation
        if (index < 0 || index >= cards.Count) return;
        List<Card> top = cards.Take(index).ToList();
        List<Card> bottom = cards.Skip(index).ToList();
        cards = bottom.Concat(top).ToList();
    }


    public void Print()
    {
        foreach (var card in cards)
        {
            Console.WriteLine($"{card.Rank} of {card.Suit}");
        }
    }

    public bool IsEmpty
    {
        get { return cards.Count == 0; }
    }
}