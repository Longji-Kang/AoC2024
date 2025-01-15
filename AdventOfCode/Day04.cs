using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode {
    public class Day04 : BaseDay {
        private string[] _input;

        private List<List<char>> _matrix;

        private int _rightBounds;
        private int _downBounds;

        public Day04() {
            _input = File.ReadAllLines(InputFilePath);
        }

        private void GenerateMatrix(string[] input) {
            _matrix = new List<List<char>>();

            for (int i = 0; i < input.Length; i++) {
                _matrix.Add(new List<char>());

                for (int j = 0; j < input[i].Length; j++) {
                    _matrix[i].Add(input[i][j]);
                }
            }

            _rightBounds = _matrix[0].Count - 1;
            _downBounds  = _matrix.Count - 1;
        }

        private string SolveProblem1(string[] input) {
            GenerateMatrix(input);

            int sum = 0;

            for (int i = 0; i < _matrix.Count; i++) {
                for (int j = 0; j < _matrix[i].Count; j++) {
                    char curr = _matrix[i][j];

                    if (curr == 'X') {
                        sum += CheckForWords(i, j);
                    }
                }
            }

            return sum.ToString();
        }

        private string SolveProblem2(string[] input) {
            GenerateMatrix(input);

            int sum = 0;

            for (int i = 0; i < _matrix.Count; i++) {
                for (int j = 0; j < _matrix[i].Count; j++) {
                    char curr = _matrix[i][j];

                    if (curr == 'A') {
                        if (CheckForCrossMas(i, j)) {
                            sum++;
                        }
                    }
                }
            }

            return sum.ToString();
        }

        private int CheckForWords(int i, int j) {
            int count = 0;

            //Check for possibility of upward search
            bool upwardsSearchPossible = i - 3 >= 0;
            bool rightwardsSearchPossible = j + 3 <= _rightBounds;
            bool downwardsSearchPossible = i + 3 <= _downBounds;
            bool leftwardsSearchPossible = j - 3 >= 0;

            //Determine diagonal searches
            bool upRightSearchPossible = upwardsSearchPossible && rightwardsSearchPossible;
            bool downRightSearchPossible = downwardsSearchPossible && rightwardsSearchPossible;
            bool downLeftSearchPossible = downwardsSearchPossible && leftwardsSearchPossible;
            bool upLeftSearchPossible = upwardsSearchPossible && leftwardsSearchPossible;

            // Check Upwards
            if (upwardsSearchPossible) {
                if (_matrix[i - 1][j] == 'M') {
                    if (_matrix[i - 2][j] == 'A') {
                        if (_matrix[i - 3][j] == 'S') {
                            count++;
                        }
                    }
                }
            }

            //Check Rightwards
            if (rightwardsSearchPossible) {
                if (_matrix[i][j + 1] == 'M') {
                    if (_matrix[i][j + 2] == 'A') {
                        if (_matrix[i][j + 3] == 'S') {
                            count++;
                        }
                    }
                }
            }

            //Check Downwards
            if (downwardsSearchPossible) {
                if (_matrix[i + 1][j] == 'M') {
                    if (_matrix[i + 2][j] == 'A') {
                        if (_matrix[i + 3][j] == 'S') {
                            count++;
                        }
                    }
                }
            }

            //Check Leftwards
            if (leftwardsSearchPossible) {
                if (_matrix[i][j - 1] == 'M') {
                    if (_matrix[i][j - 2] == 'A') {
                        if (_matrix[i][j - 3] == 'S') {
                            count++;
                        }
                    }
                }
            }

            //Check Up Right
            if (upRightSearchPossible) {
                if (_matrix[i - 1][j + 1] == 'M') {
                    if (_matrix[i - 2][j + 2] == 'A') {
                        if (_matrix[i - 3][j + 3] == 'S') {
                            count++;
                        }
                    
                    }
                }
            }

            //Check Down Right
            if (downRightSearchPossible) {
                if (_matrix[i + 1][j + 1] == 'M') {
                    if (_matrix[i + 2][j + 2] == 'A') {
                        if (_matrix[i + 3][j + 3] == 'S') {
                            count++;
                        }
                    }
                }
            }

            //Check Down Left
            if (downLeftSearchPossible) {
                if (_matrix[i + 1][j - 1] == 'M') {
                    if (_matrix[i + 2][j - 2] == 'A') {
                        if (_matrix[i + 3][j - 3] == 'S') {
                            count++;
                        }
                    }
                }
            }

            //Check Up Left
            if (upLeftSearchPossible) {
                if (_matrix[i - 1][j - 1] == 'M') {
                    if (_matrix[i - 2][j - 2] == 'A') {
                        if (_matrix[i - 3][j - 3] == 'S') {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        private bool CheckForCrossMas(int i, int j) {
            bool valid = false;

            bool upRightPossible = (i - 1 >= 0) && (j + 1 <= _rightBounds);
            bool downRightPossible = (i + 1 <= _downBounds) && (j + 1 <= _rightBounds);
            bool downLeftPossible = (i + 1 <= _downBounds) && (j - 1 >= 0);
            bool upLeftPossible = (i - 1 >= 0) && (j - 1 >= 0);

            if (upRightPossible && downRightPossible && downLeftPossible && upLeftPossible) {
                //Check top right for S
                bool canContinue = true;
                if (upRightPossible && downLeftPossible) {
                    if (_matrix[i - 1][j + 1] == 'S') {
                        //Check bottom left for M
                        if (_matrix[i + 1][j - 1] != 'M') {
                            canContinue = false;
                        }
                    } else if (_matrix[i - 1][j + 1] == 'M') {
                        //Check bottom left for S
                        if (_matrix[i + 1][j - 1] != 'S') {
                            canContinue = false;
                        }
                    } else {
                        canContinue = false;
                    }
                } else {
                    canContinue = false;
                }

                //Only check left to right diagonal if previous was valid
                if (canContinue && upLeftPossible && downRightPossible) {
                    //Check top left for S
                    if (_matrix[i - 1][j - 1] == 'S') {
                        //Check bottom right for M
                        if (_matrix[i + 1][j + 1] == 'M') {
                            valid = true;
                        }
                    } else if (_matrix[i - 1][j - 1] == 'M') {
                        if (_matrix[i + 1][j + 1] == 'S') {
                            valid = true;
                        }
                    }
                }
            }

            return valid;
        }

        public override ValueTask<string> Solve_1() => new(SolveProblem1(_input));

        public override ValueTask<string> Solve_2() => new(SolveProblem2(_input));
    }
}
