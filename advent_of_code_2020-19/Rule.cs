using System.Collections.Generic;
using System.Linq;

public record Rule {

    public int RuleNumber { get; init; }
    public List<List<int>> RuleContents { get; init; }
    public List<int> FlattenedContents {
        get {
            return RuleContents.SelectMany(x => x).ToList();
        }
    }

    public static Rule FromString(string ruleAsString) {

        int ruleNumber = int.Parse(ruleAsString.Split(": ")[0]);
        var ruleContents = ruleAsString.Split(": ")[1]
            .Split(" | ")
            .Select(x => x.Split(" ").Select(int.Parse).ToList())
            .ToList();

        return new Rule {
            RuleNumber = ruleNumber,
            RuleContents = ruleContents
        };
    }
}