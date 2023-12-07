using AdventOfCode2023.App.Services;

namespace AdventOfCode2023.App.Day7;

// Enum representing different types of hands
public enum HandType
{
    HighCard = 0,
    Pair = 1,
    TwoPair = 2,
    ThreeOfAKind = 3,
    FullHouse = 4,
    FourOfAKind = 5,
    FiveOfAKind = 6
}

public class CardManager
{
    private const string CARD_RANKS = "23456789TJQKA";
    private const int FiveOfAKindCount = 5;
    private string[] Lines { get; set; }

    public CardManager()
    {
        Lines ??= FileManger.Read("Day7\\Data.txt");
    }

    // ------ MAIN --------
    public int ParseGames(bool jokers = false)
    {
        // Parse each line into a Game object, choose parsing method based on the presence of jokers
        return Lines.Select(line =>
        {
            var parts = line.Split(" ");
            var bet = int.Parse(parts[1]);
            var hand = parts[0];

            // Choose parsing method based on the presence of jokers
            return jokers
                ? ParseGameWithJokers(hand, bet)
                : ParseGameWithoutJokers(hand, bet);
        }).ToList()
        .Order() // Order the list of games based on hand strength
        .Select((game, index) => (index + 1) * game.Bet) // Calculate and return the total winnings
        .Sum();
    }

    // ------ HELPER --------
    // Parse a game with jokers
    private Game ParseGameWithJokers(string hand, int bet)
    {
        // Group cards by occurrence and calculate the score
        var occurring = hand
            .GroupBy(c => c)
            .ToDictionary(group => group.Key, group => group.Count());

        int score = CalculateScore(hand);
        int jokerCount = ExtractJokerCount(ref occurring);

        if (occurring.Count == 0)
        {
            // All cards are jokers, indicating Five of a Kind
            return new Game(hand, score, bet, FiveOfAKindCount, 0);
        }
        
        // Find the card with the maximum occurrence
        var (maxCount, maxCountKey) = GetMaxOccurrence(occurring, jokerCount);
        var occuring1 = GetOccurrence1(occurring);

        return new Game(hand, score, bet, maxCount, occuring1);
    }
    
    // Parse a game without jokers
    private Game ParseGameWithoutJokers(string hand, int bet)
    {
        // Calculate the score and find the occurrence of the second most frequent card
        int score = hand.Aggregate(0, (acc, c) => (acc << 4) + CARD_RANKS.IndexOf(c));
        var occurring = hand.GroupBy(c => c).Select(group => group.Count()).OrderByDescending(count => count).ToList();
        var occuring1 = (occurring.Count > 1 ? occurring[1] : 0);

        return new Game(hand, score, bet, occurring[0], occuring1);
    }

    // Calculate the score of a hand based on card ranks
    private int CalculateScore(string hand)
        => hand.Aggregate(0, (acc, c) => (acc << 4) + (c == 'J' ? 0 : CARD_RANKS.IndexOf(c) + 1));

    // Extract joker count from the occurrences and remove 'J' from the dictionary
    private int ExtractJokerCount(ref Dictionary<char, int> occurring)
    {
        int jokerCount = 0;
        if (occurring.ContainsKey('J'))
        {
            jokerCount = occurring['J'];
            occurring.Remove('J');
        }
        return jokerCount;
    }

    // Find the card with the maximum occurrence
    private (int maxCount, char maxCountKey) GetMaxOccurrence(Dictionary<char, int> occurring, int jokerCount)
    {
        var maxCount = occurring.Values.Max();
        var maxCountKey = occurring.FirstOrDefault(x => x.Value == maxCount).Key;

        if (jokerCount > 0)
        {
            occurring[maxCountKey] += jokerCount;
            maxCount = occurring[maxCountKey];
        }

        return (maxCount, maxCountKey);
    }

    // Get the occurrence count of the second most frequent card
    private int GetOccurrence1(Dictionary<char, int> occurring)
        => occurring.Count > 1
            ? occurring
                .OrderByDescending(x => x.Value)
                .Skip(1)
                .FirstOrDefault()
                .Value
            : 0;

    public record Game(string hand, int Score, int Bet, int Occurring0, int Occurring1) : IComparable<Game>
    {
        // Property to get the type of hand based on occurrence counts
        public HandType Type =>
            Occurring0 switch
            {
                5 => HandType.FiveOfAKind,
                4 => HandType.FourOfAKind,
                3 => Occurring1 == 2 ? HandType.FullHouse : HandType.ThreeOfAKind,
                2 => Occurring1 == 2 ? HandType.TwoPair : HandType.Pair,
                _ => HandType.HighCard,
            };
        
        // Compare two games based on hand type and score
        public int CompareTo(Game other) => Type == other.Type 
            ? Score.CompareTo(other.Score) 
            : Type.CompareTo(other.Type);
    }
}
