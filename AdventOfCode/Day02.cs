using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    public class Day02 : BaseDay {
        private string[] _input;

        private List<List<int>> _matrix = new List<List<int>>();

        public Day02() {
            _input = File.ReadAllLines(InputFilePath);
        }

        private void CreateMatrixFromInput(string[] input) {
            _matrix = new List<List<int>>();

            for (int i = 0; i < input.Length; i++) {
                string[] curr_split = input[i].Split(' ');

                _matrix.Add(new List<int>());
                for (int j = 0; j < curr_split.Length; j++) {
                    _matrix[i].Add(Convert.ToInt32(curr_split[j]));
                }
            }
        }

        private string SolveFirst(string[] input) {
            CreateMatrixFromInput(input);

            int safe = 0;

            for (int i = 0; i < _matrix.Count; i++) {
                bool currIsSafe = true;

                bool increments = true;

                for (int j = 0; j < _matrix[i].Count - 1; j++) {
                    int diff = _matrix[i][j + 1] - _matrix[i][j];

                    if (j == 0) {
                        if (diff < 0) {
                            increments = false;
                        }
                    }

                    if (diff < 0) {
                        if (increments == true) {
                            currIsSafe = false;
                            break;
                        }
                    } else if (diff > 0) {
                        if (increments == false) {
                            currIsSafe = false;
                            break;
                        }
                    } else if (diff == 0) {
                        currIsSafe = false;
                        break;
                    }

                    if (Math.Abs(diff) > 3) { 
                        currIsSafe = false; 
                        break; 
                    }
                }

                if (currIsSafe) {
                    safe += 1;
                }
            }

            return safe.ToString();
        }

        private string SolveSecond(string[] input) {
            int safe = 0;

            CreateMatrixFromInput(input);

            for (int i = 0; i < _matrix.Count; i++) {
                int increments = 0;
                int decrements = 0;
                int zeroDiff = 0;

                for (int j = 0; j < _matrix[i].Count - 1; j++) {
                    if (_matrix[i][j + 1] - _matrix[i][j] > 0) {
                        increments++;
                    } else if (_matrix[i][j + 1] - _matrix[i][j] < 0) {
                        decrements++;
                    } else {
                        zeroDiff++;
                    }
                }

                if (zeroDiff > 1) {
                    continue;
                }

                if (CheckList(_matrix[i], increments > decrements)) {
                    safe += 1;
                } else {
                    // Make copy of list
                    List<int> tmp = new List<int>(_matrix[i]);

                    bool isSafe = false;

                    for (int j = 0; j < tmp.Count - 2; j++) {
                        int nextDiff = tmp[j + 1] - tmp[j];

                        if (nextDiff == 0 || (nextDiff > 3 || nextDiff < -3) || (nextDiff > 0 && increments < decrements) || (nextDiff < 0 && increments > decrements)) {
                            List<int> remTmp = new List<int>(tmp);

                            // Try remove current
                            remTmp.RemoveAt(j);

                            if (CheckList(remTmp, increments > decrements)) {
                                safe++;
                                isSafe = true;
                                break;
                            } else {
                                remTmp = new List<int>(tmp);
                                remTmp.RemoveAt(j + 1);

                                if (CheckList(remTmp, increments > decrements)) {
                                    safe++;
                                    isSafe = true;
                                    break;
                                } else {
                                    break;
                                }
                            }
                        }
                    }

                    if (!isSafe) {
                        int indexA = _matrix[i].Count - 2;
                        int indexB = _matrix[i].Count - 1;

                        tmp = new List<int>(_matrix[i]);

                        tmp.RemoveAt(indexA);

                        if (CheckList(tmp, increments > decrements)) {
                            safe++;
                        } else {
                            tmp = new List<int>(_matrix[i]);

                            tmp.RemoveAt(indexB);

                            if (CheckList(tmp, increments > decrements)) {
                                safe++;
                            }
                        }
                    }
                }
            }

            return safe.ToString();
        }

        private bool CheckList(List<int> list, bool increments) {
            for (int j = 0; j < list.Count - 1; j++) {
                int diff = list[j + 1] - list[j];

                if (diff < 0) {
                    if (increments == true) {
                        return false;
                    }
                } else if (diff > 0) {
                    if (increments == false) {
                        return false;
                    }
                } else if (diff == 0) {
                    return false;
                }

                if (Math.Abs(diff) > 3) {
                    return false;
                }
            }

            return true;
        }

        public override ValueTask<string> Solve_1() => new(SolveFirst(_input));

        public override ValueTask<string> Solve_2() => new(SolveSecond(_input));
    }
}
