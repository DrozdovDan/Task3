using System.ComponentModel.DataAnnotations.Schema;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Task1.Task1;

namespace Task1;

using Table = System.Collections.Generic.List<Card>;

public class Tests
{
    [Test]
    public void RoundWinnerTest()
    {
        Card card1 = new Card(Rank.Six, Suit.Clubs);
        Card card2 = new Card(Rank.Ace, Suit.Hearts);
        That(RoundWinner(card1, card2), Is.EqualTo(Player.Player2));
        card1 = new Card(Rank.Ace, Suit.Clubs);
        That(RoundWinner(card1, card2), Is.EqualTo(null));
        card2 = new Card(Rank.King, Suit.Spades);
        That(RoundWinner(card1, card2), Is.EqualTo(Player.Player1));
    }

    [Test]
    public void FullDeckTest()
    {
        var deck = FullDeck();
        That(deck, Has.Count.EqualTo(DeckSize));
    }

    [Test]
    public void RoundTest()
    {
        var deck = FullDeck();
        var hands = Deal(deck);
        hands[Player.Player1].Sort(delegate(Card x, Card y)
        {
            if (x.Rank < y.Rank)
            {
                return -1;
            }

            if (x.Rank > y.Rank)
            {
                return 1;
            }

            return 0;
        });
        hands[Player.Player2].Sort(delegate(Card x, Card y)
        {
            if (x.Rank > y.Rank)
            {
                return -1;
            }

            if (x.Rank < y.Rank)
            {
                return 1;
            }

            return 0;
        });
        Table table = new Table();
        table.Add(hands[Player.Player1][0]);
        table.Add(hands[Player.Player2][0]);
        That(Round(hands), Is.EqualTo(new Tuple<Player, Table>(Player.Player2, table)));
    }

    [Test]
    public void Game2CardsTest()
    {
        List<Card> deck = FullDeck();
        deck.Sort(delegate(Card x, Card y)
        {
            if (x.Rank > y.Rank)
            {
                return 1;
            }

            if (x.Rank < y.Rank)
            {
                return -1;
            }

            return 0;
        });
        
        Dictionary<Player, List<Card>> hands = new Dictionary<Player, List<Card>>
        {
            { Player.Player1, deck.GetRange(0, 18)},
            { Player.Player2, deck.GetRange(18, 18)}
        };
        var gameWinner = Game(hands);
        That(gameWinner, Is.EqualTo(Player.Player2));
    }
}