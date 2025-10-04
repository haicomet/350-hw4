
namespace TestProject1;


[TestClass]
public sealed class Test1
{


    // CARD TEST 
    [TestMethod]
    public void CardConstructorTest()
    {
        Suit suit = Suit.Diamonds;
        Rank rank = Rank.Ace;

        Card card = new Card(suit, rank);

        Assert.AreEqual(suit, card.Suit);
        Assert.AreEqual(rank, card.Rank);
        Assert.IsFalse(card.IsFaceUp, "Card should start face down.");
    }

    [TestMethod]
    public void CardFlipOverTest()
    {
        Card card = new Card(Suit.Hearts, Rank.Queen);
        bool initialFaceUp = card.IsFaceUp;

        card.FlipOver();
        bool flip1 = card.IsFaceUp;

        card.FlipOver();
        bool flip2 = card.IsFaceUp;

        Assert.IsTrue(initialFaceUp != flip1, "First flip should change state to true.");
        Assert.IsTrue(flip1 != flip2, "Second flip should change state back to false.");
    }

    [TestMethod]
    public void CardToStringFaceDownTest()
    {
        Card card = new Card(Suit.Spades, Rank.Three);

        string cardString = card.ToString();

        Assert.AreEqual("Card is Face Down", cardString, "ToString should indicate face down.");
    }

    [TestMethod]
    public void CardToStringFaceUpTest()
    {
        Card card = new Card(Suit.Clubs, Rank.Ten);
        card.FlipOver();

        string cardString = card.ToString();

        Assert.AreEqual("Ten of Clubs", cardString, "ToString should show rank and suit when face up.");
    }

    // PLAYER TEST

    private Card twoSpades = new Card(Suit.Spades, Rank.Two);
    private Card threeHearts = new Card(Suit.Hearts, Rank.Three);
    private Card fourDiamonds = new Card(Suit.Diamonds, Rank.Four);

    [TestMethod]
    public void PlayerConstructorTest()
    {
        Player player = new Player();

        Assert.AreEqual(0, player.CardCount, "New player should have 0 cards.");
        Assert.IsTrue(player.isHandEmpty(), "New player's hand should be empty.");
    }

    [TestMethod]
    public void PlayerGetCardTest()
    {
        Player player = new Player();
        player.getCard(twoSpades);

        Assert.AreEqual(1, player.CardCount, "Player should have 1 card after getting a card.");
        Assert.IsFalse(player.isHandEmpty(), "Player's hand should not be empty after getting a card.");
    }

    [TestMethod]
    public void PlayerPlayCardTest()
    {
        Player player = new Player();
        player.getCard(twoSpades);
        player.getCard(threeHearts);

        Card playedCard = player.playCard(0);

        Assert.AreEqual(twoSpades, playedCard, "Played card should be the first card added.");
        Assert.AreEqual(1, player.CardCount, "Player should have 1 card left after playing one.");
    }

    [TestMethod]
    public void PlayerSortHandTest()
    {
        Player player = new Player();
        player.getCard(threeHearts);
        player.getCard(twoSpades);
        player.getCard(fourDiamonds);

        player.sortHand();

        Assert.AreEqual(Rank.Two, player.playCard(0).Rank, "First card should be Two after sorting.");
        Assert.AreEqual(Rank.Three, player.playCard(0).Rank, "Second card should be Three after sorting.");
        Assert.AreEqual(Rank.Four, player.playCard(0).Rank, "Third card should be Four after sorting.");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void PlayerPlayCardInvalidIndexTest()
    {
        Player player = new Player();
        player.getCard(twoSpades);

        player.playCard(1); // This should throw an exception
    }

    // DECK TEST
    [TestMethod]
    public void DeckConstructorTest()
    {
        Deck deck = new Deck();

        Assert.AreEqual(52, deck.Cards.Count, "New deck should have 52 cards.");
    }

    [TestMethod]
    public void DeckTakeTopCardTest()
    {
        Deck deck = new Deck();
        Card topCard = deck.TakeTopCard();

        Assert.IsNotNull(topCard, "Top card should not be null.");
        Assert.AreEqual(51, deck.Cards.Count, "Deck should have 51 cards after taking one.");
    }

    [TestMethod]
    public void DeckShuffleTest()
    {
        Deck deck = new Deck();
        var originalOrder = deck.Cards.Select(card => card).ToList();
        deck.Shuffle();
        var shuffledOrder = deck.Cards.Select(card => card).ToList();

        // Check that the order is different 
        bool orderChanged = !originalOrder.SequenceEqual(shuffledOrder);
        Assert.IsTrue(orderChanged || originalOrder.Count == 1); // If only one card, order can't change

        // Check that all cards are still present
        var originalSorted = originalOrder.OrderBy(c => c.Suit).ThenBy(c => c.Rank).ToList();
        var shuffledSorted = shuffledOrder.OrderBy(c => c.Suit).ThenBy(c => c.Rank).ToList();
        Assert.IsTrue(originalSorted.SequenceEqual(shuffledSorted));

        // Check deck size unchanged
        Assert.AreEqual(originalOrder.Count, shuffledOrder.Count);
    }

    [TestMethod]
    public void DeckCutTest()
    {
        Deck deck = new Deck();
        var originalOrder = deck.Cards.Select(card => card).ToList();

        int cutIndex = 10;
        deck.Cut(cutIndex);
        var cutOrder = deck.Cards;

        var expectedOrder = originalOrder.Skip(cutIndex).Concat(originalOrder.Take(cutIndex)).ToList();
        Assert.IsTrue(expectedOrder.SequenceEqual(cutOrder));

        Assert.AreEqual(originalOrder.Count, cutOrder.Count);
    }
    [TestMethod]
    public void DeckCutInvalidTest()
    {
        Deck deck = new Deck();
        Card originalTopCard = deck.Cards[0];

        deck.Cut(0);

        Assert.AreEqual(originalTopCard, deck.Cards[0], "Cut with index 0 should not change card order.");

        deck.Cut(52);

        Assert.AreEqual(originalTopCard, deck.Cards[0], "Cut with index equal to card count should not change card order.");
    }

    [TestMethod]
    public void DeckEmptyTest()
    {
        Deck deck = new Deck();

        Assert.IsFalse(deck.Empty, "Deck should not be empty initially.");

        for (int i = 0; i < 52; i++)
        {
            deck.TakeTopCard();
        }

        Assert.IsTrue(deck.Empty, "Deck should be empty after drawing all cards.");
    }
}