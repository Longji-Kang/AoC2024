using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode {
    internal class Day03 : BaseDay {
        private string[] _input;

        public Day03() {
            _input = File.ReadAllLines(InputFilePath);
        }

        private string SolveProblem1(string[] input) {
            Regex regex = new Regex(@"mul\(\d{1,3},\d{1,3}\)");

            int sum = 0;

            for (int i = 0; i < input.Length; i++) {
                MatchCollection matches = regex.Matches(input[i]);

                for (int j = 0; j < matches.Count; j++) {
                    string match = matches[j].Value;

                    int firstBracket = 3;
                    int secondBracket = match.IndexOf(")");
                    int comma = match.IndexOf(",");

                    int firstLength = comma - firstBracket - 1;
                    int secondLength = secondBracket - comma;

                    int firstNum = Convert.ToInt32(match.Substring(firstBracket + 1, firstLength));
                    int secondNum = Convert.ToInt32(match.Substring(comma + 1, secondLength - 1));

                    sum += firstNum * secondNum;
                }
            }

            return sum.ToString();
        }

        private string SolveProblem2(string[] input) {
            Regex regex = new Regex(@"(mul\(\d{1,3},\d{1,3}\))|(do\(\))|(don't\(\))");

            int sum = 0;

            bool enabled = true;

            for (int i = 0; i < input.Length; i++) {
                MatchCollection matches = regex.Matches(input[i]);

                for (int j = 0; j < matches.Count; j++) {
                    string match = matches[j].Value;

                    if (match == "do()") {
                        enabled = true;
                    } else if (match == "don't()") {
                        enabled = false;
                    } else {
                        if (enabled) {
                            int firstBracket = 3;
                            int secondBracket = match.IndexOf(")");
                            int comma = match.IndexOf(",");

                            int firstLength = comma - firstBracket - 1;
                            int secondLength = secondBracket - comma;

                            int firstNum = Convert.ToInt32(match.Substring(firstBracket + 1, firstLength));
                            int secondNum = Convert.ToInt32(match.Substring(comma + 1, secondLength - 1));

                            sum += firstNum * secondNum;
                        }
                    }
                }
            }

            return sum.ToString();
        }

        public override ValueTask<string> Solve_1() => new(SolveProblem1(_input));

        public override ValueTask<string> Solve_2() => new(SolveProblem2(_input));
    }
}
