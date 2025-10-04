using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Card
{
    //Fields, example: Rank rank;
    //check the help documentation for the fields
    private Suit suit;
    private Rank rank;
    private bool isFaceUp;

    //Card Constructor
    public Card(Suit suit, Rank rank)
    {
        this.suit = suit;
        this.rank = rank;
        this.isFaceUp = false;
    }

    //Define properties for all above fields
    //code example: public Suit Suit { get { return suit; } }

    public Suit Suit
    {
        get { return suit; }
    }

    public Rank Rank
    {
        get { return rank; }
    }

    public bool IsFaceUp
    {
        get { return isFaceUp; }
    }


    public void FlipOver()
    {
        //implementation 
        isFaceUp = !isFaceUp;
    }

    public override string ToString()
    {
        if (isFaceUp)
        {
            return $"{rank} of {suit}";
        }
        else
        {
            return "Card is Face Down";
        }
    }
        
}
