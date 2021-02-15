using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class Program
    {
        private static readonly Board _board = new Board();
        private static Player _currentPlayer = Player.One;

        static void Main(string[] args)
        {
            bool isWinner = false;
            
            DrawBoard();
            while (!isWinner)
            {
                ReadMove();
                Console.WriteLine();
                DrawBoard();
                _currentPlayer = _currentPlayer == Player.One ? Player.Two : Player.One;
            }
        }

        private static void ReadMove()
        {
            var input = Console.ReadKey();

            ChangeField(input);
        }

        private static void ChangeField(ConsoleKeyInfo input)
        {
            if (int.TryParse(input.Key.ToString().Remove(0, 1), out int result))
            {
                switch (result)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        ProcessField(result);
                        break;
                    case 0:
                        HandleInvalidInput();
                        break;
                }
            }
            else
            {
                HandleInvalidInput();
            }
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
            { 1, Status.Empty},
            { 2, Status.Empty},
            { 3, Status.Empty},
            { 4, Status.Empty},
            { 5, Status.Empty},
            { 6, Status.Empty},
            { 7, Status.Empty},
            { 8, Status.Empty},
            { 9, Status.Empty}
        };
    }

    internal enum Status
    {
        Empty,
        PlayerOne,
        PlayerTwo
    }
}