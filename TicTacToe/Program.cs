using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    internal static class Program
    {
        private static readonly Board _board = new Board();
        private static Player _currentPlayer = Player.One;

        static void Main()
        {
            bool isWinner = false;

            do
            {
                DrawBoard();
                ReadMove();
                Console.WriteLine();
                _currentPlayer = _currentPlayer == Player.One ? Player.Two : Player.One;
            } while (!isWinner);
        }

        private static bool GetWinner()
        {
            // TODO: implement win logic
            return false;
        }

        private static void ReadMove()
        {
            var input = Console.ReadKey();

            ChangeField(input);
        }

        private static void ChangeField(ConsoleKeyInfo input)
        {
            var digit = int.TryParse(input.Key.ToString().Remove(0, 1), out int result);
            if (result >= 1 && result <= 9)
                ProcessField(result);
            else
                HandleInvalidInput();
        }

        private static void HandleInvalidInput(string message = "Invalid input. Try again.")
        {
            Console.WriteLine(message);
            ReadMove();
        }

        private static void ProcessField(int inputKey)
        {
            if (_board.squareStatus[key: inputKey] == Status.Empty)
            {
                _board.squareStatus[key: inputKey] =
                    _currentPlayer == Player.One
                        ? Status.PlayerOne
                        : Status.PlayerTwo;
            }
            else
            {
                HandleInvalidInput("Field already occupied!");
            }
        }

        private static void DrawBoard()
        {
            var builder = new StringBuilder();

            for (int i = 1; i <= 3; i++)
                builder.Append(GetFieldValue(i));
            builder.AppendLine();
            for (int i = 4; i <= 6; i++)
                builder.Append(GetFieldValue(i));
            builder.AppendLine();
            for (int i = 7; i <= 9; i++)
                builder.Append(GetFieldValue(i));

            Console.WriteLine(builder);
        }

        private static string GetFieldValue(int field)
        {
            return _board.squareStatus[key: field] switch
            {
                Status.Empty => field.ToString(),
                Status.PlayerOne => "X",
                Status.PlayerTwo => "O",
                _ => null
            };
        }
    }

    internal enum Player
    {
        One,
        Two
    }

    internal class Board
    {
        public readonly Dictionary<int, Status> squareStatus = new Dictionary<int, Status>()
        {
            {1, Status.Empty},
            {2, Status.Empty},
            {3, Status.Empty},
            {4, Status.Empty},
            {5, Status.Empty},
            {6, Status.Empty},
            {7, Status.Empty},
            {8, Status.Empty},
            {9, Status.Empty}
        };
    }

    internal enum Status
    {
        Empty,
        PlayerOne,
        PlayerTwo
    }
}