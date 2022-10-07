// Колода

using System.Runtime.CompilerServices;
using Deck = System.Collections.Generic.List<Card>;
// Набор карт у игрока
using Hand = System.Collections.Generic.List<Card>;
// Набор карт, выложенных на стол
using Table = System.Collections.Generic.List<Card>;

// Масть
internal enum Suit
{
    Diamonds,
    Clubs,
    Hearts,
    Spades
}

// Значение
internal enum Rank
{
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13,
    Ace = 14
}

// Карта
record Card(Rank Rank, Suit Suit);

// Тип для обозначения игрока (первый, второй)
internal enum Player
{
    Player1,
    Player2
}

namespace Task1
{
    public class Task1
    {

 /*
 * Реализуйте игру "Пьяница" (в простейшем варианте, на колоде в 36 карт)
 * https://ru.wikipedia.org/wiki/%D0%9F%D1%8C%D1%8F%D0%BD%D0%B8%D1%86%D0%B0_(%D0%BA%D0%B0%D1%80%D1%82%D0%BE%D1%87%D0%BD%D0%B0%D1%8F_%D0%B8%D0%B3%D1%80%D0%B0)
 * Рука — это набор карт игрока. Карты выкладываются на стол из начала "рук" и сравниваются
 * только по значениям (масть игнорируется). При равных значениях сравниваются следующие карты.
 * Набор карт со стола перекладывается в конец руки победителя. Шестерка туза не бьёт.
 *
 * Реализация должна сопровождаться тестами.
 */

        // Размер колоды
        internal const int DeckSize = 36;

        // Возвращается null, если значения карт совпадают
        internal static Player? RoundWinner(Card card1, Card card2)
        {
            if (card1.Rank == card2.Rank) return null;
            return card1.Rank > card2.Rank ? Player.Player1 : Player.Player2;
        }
        
// Возвращает полную колоду (36 карт) в фиксированном порядке
        internal static Deck FullDeck()
        {
            Deck deck = new Deck();
            deck.Add(new Card(Rank.Six, Suit.Clubs));
            deck.Add(new Card(Rank.Seven, Suit.Clubs));
            deck.Add(new Card(Rank.Eight, Suit.Clubs));
            deck.Add(new Card(Rank.Nine, Suit.Clubs));
            deck.Add(new Card(Rank.Ten, Suit.Clubs));
            deck.Add(new Card(Rank.Jack, Suit.Clubs));
            deck.Add(new Card(Rank.Queen, Suit.Clubs));
            deck.Add(new Card(Rank.King, Suit.Clubs));
            deck.Add(new Card(Rank.Ace, Suit.Clubs));
            deck.Add(new Card(Rank.Six, Suit.Diamonds));
            deck.Add(new Card(Rank.Seven, Suit.Diamonds));
            deck.Add(new Card(Rank.Eight, Suit.Diamonds));
            deck.Add(new Card(Rank.Nine, Suit.Diamonds));
            deck.Add(new Card(Rank.Ten, Suit.Diamonds));
            deck.Add(new Card(Rank.Jack, Suit.Diamonds));
            deck.Add(new Card(Rank.Queen, Suit.Diamonds));
            deck.Add(new Card(Rank.King, Suit.Diamonds));
            deck.Add(new Card(Rank.Ace, Suit.Diamonds));
            deck.Add(new Card(Rank.Six, Suit.Hearts));
            deck.Add(new Card(Rank.Seven, Suit.Hearts));
            deck.Add(new Card(Rank.Eight, Suit.Hearts));
            deck.Add(new Card(Rank.Nine, Suit.Hearts));
            deck.Add(new Card(Rank.Ten, Suit.Hearts));
            deck.Add(new Card(Rank.Jack, Suit.Hearts));
            deck.Add(new Card(Rank.Queen, Suit.Hearts));
            deck.Add(new Card(Rank.King, Suit.Hearts));
            deck.Add(new Card(Rank.Ace, Suit.Hearts));
            deck.Add(new Card(Rank.Six, Suit.Spades));
            deck.Add(new Card(Rank.Seven, Suit.Spades));
            deck.Add(new Card(Rank.Eight, Suit.Spades));
            deck.Add(new Card(Rank.Nine, Suit.Spades));
            deck.Add(new Card(Rank.Ten, Suit.Spades));
            deck.Add(new Card(Rank.Jack, Suit.Spades));
            deck.Add(new Card(Rank.Queen, Suit.Spades));
            deck.Add(new Card(Rank.King, Suit.Spades));
            deck.Add(new Card(Rank.Ace, Suit.Spades));
            return deck;
        }

// Раздача карт: случайное перемешивание (shuffle) и деление колоды пополам
        internal static Dictionary<Player, Hand> Deal(Deck deck) 
        {
            static void Shuffle (Deck deck)
            {
                Random rng = new Random();
                int n = deck.Count;
                while (n > 1)
                {
                    n--;
                    int k = rng.Next(n + 1);
                    (deck[k], deck[n]) = (deck[n], deck[k]);
                }
            }

            Shuffle(deck);
            Hand hand1 = deck.GetRange(0, 18);
            Hand hand2 = deck.GetRange(18, 18);
            Dictionary<Player, Hand> dictionary = new Dictionary<Player, Deck>();
            dictionary.Add(Player.Player1, hand1);
            dictionary.Add(Player.Player2, hand2);
            return dictionary;
        }

// Один раунд игры (в том числе спор при равных картах).
// Возвращается победитель раунда и набор карт, выложенных на стол.
        internal static Tuple<Player, Table> Round(Dictionary<Player, Hand> hands)
        {
            Player? winner;
            Table table = new Table();
            var c = 0;
            do
            {
                if (c == 0)
                {
                    Card card1 = hands[Player.Player1][0];
                    Card card2 = hands[Player.Player2][0];
                    table.Add(card1);
                    table.Add(card2);
                    winner = RoundWinner(card1, card2);
                    hands[Player.Player1].Remove(card1);
                    hands[Player.Player2].Remove(card2);
                }
                else
                {
                    Card card1 = hands[Player.Player2][0];
                    Card card2 = hands[Player.Player1][0];
                    table.Add(card1);
                    table.Add(card2);
                    winner = RoundWinner(card2, card1);
                    hands[Player.Player1].Remove(card2);
                    hands[Player.Player2].Remove(card1);
                }
                c = (c + 1) % 2; 
            } while (winner == null && 0 < hands[Player.Player1].Count && 0 < hands[Player.Player2].Count);

            if (winner == null)
            {
                return 0 >= hands[Player.Player1].Count
                    ? new Tuple<Player, Table>(Player.Player1, table)
                    : new Tuple<Player, Table>(Player.Player2, table);
            }

            return new Tuple<Player, Table>((Player)winner, table);
        }

// Полный цикл игры (возвращается победивший игрок)
// в процессе игры печатаются ходы
        internal static Player Game(Dictionary<Player, Hand> hands) {
            var t = 1;
            while (hands[Player.Player1].Count > 0 && hands[Player.Player2].Count > 0)
            {
                Tuple<Player, Table> afterRound = Round(hands);
                Console.Write(t + " ");
                foreach (var card in afterRound.Item2)
                {
                    hands[afterRound.Item1].Add(card);
                    Console.Write("[" + card.Rank + " " + card.Suit + "]; ");
                }

                Console.WriteLine("Won " + $"{afterRound.Item1};");
                t += 1;
            }

            return hands[Player.Player1].Count > 0 ? Player.Player1 : Player.Player2;
        }

        public static void Main(string[] args)
        { 
            var deck = FullDeck();
        var hands = Deal(deck);
        var winner = Game(hands);
            Console.WriteLine($"Победитель: {winner}");
        }
    }
}