namespace AdventOfCode;

public class Day01 : BaseDay
{
    private readonly string[] _input;
    private List<string> _firstSet;
    private List<string> _secondSet;

    public Day01()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    private void GenerateSets() {
        _firstSet = new List<string>();
        _secondSet = new List<string>();

        const string delimiter = "   ";

        foreach (var input in _input) {
            string[] sets = input.Split(delimiter);

            _firstSet.Add(sets[0]);
            _secondSet.Add(sets[1]);
        }
    }

    private string SolveProblem1(string[] input) {
        GenerateSets();

        int sumTotal = 0;

        int iterations = _firstSet.Count;

        for (int i = 0; i < iterations; i++) {
            string firstMin = _firstSet.Min();
            string secondMin = _secondSet.Min();

            int firstMinIndex = _firstSet.IndexOf(firstMin);
            int secondMinIndex = _secondSet.IndexOf(secondMin);

            _firstSet.RemoveAt(firstMinIndex);
            _secondSet.RemoveAt(secondMinIndex);

            int distance = Math.Abs(Convert.ToInt32(firstMin) - Convert.ToInt32(secondMin));

            sumTotal += distance;
        }

        return sumTotal.ToString();
    }

    private string SolveProblem2(string[] input) {
        GenerateSets();

        int sumTotal = 0;

        int iterations = _firstSet.Count;

        foreach (string curr in _firstSet) {
            int count = _secondSet.Count(s => s == curr);

            int weight = Convert.ToInt32(curr) * count;

            sumTotal += weight;
        }

        return sumTotal.ToString();
    }

    public override ValueTask<string> Solve_1() => new(SolveProblem1(_input));

    public override ValueTask<string> Solve_2() => new(SolveProblem2(_input));
}
