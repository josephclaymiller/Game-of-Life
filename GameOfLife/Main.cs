using System;

namespace GameOfLife
{
//	Conway's Game of Life Rules
//	1. Any live cell with fewer than two live neighbours dies, as if caused by under-population.
//	2. Any live cell with two or three live neighbours lives on to the next generation.
//	3. Any live cell with more than three live neighbours dies, as if by overcrowding.
//	4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
	class MainClass
	{
		private static int boardSize = 10;
		private static bool[,] board;
		private static bool[,] nextBoard;
		private static string aliveSymbol = "*";
		private static string deadSymbol = "-";
		private static int turns = 0;
		private const int maxTurns = 10;

		public static void Main(string[] args) {
			Console.WriteLine("Game of Life");
			SetGliderPattern();
			ShowBoard();
			TakeTurns(maxTurns);
		}

		private static void TakeTurns(int turns) {
			for (int i = 0; i < turns; i++) {
				TakeTurn();
				ShowBoard();
			}
		}

		private static void ShowBoard() {
			Console.WriteLine("Turn {0}", turns);
			for (int row = 0; row < boardSize; row++) {
				for (int col = 0; col < boardSize; col++) {
					if (board[row, col]) {
						Console.Write(aliveSymbol);
					} else {
						Console.Write(deadSymbol);
					}
				}
				Console.WriteLine(); // Return after each row
			}
			Console.WriteLine(); // Extra space after board
			Console.ReadLine(); // Wait for user to hit enter
		}

		private static void TakeTurn() {
			nextBoard = new bool[boardSize,boardSize]; // Clear out next board
			for (int row = 0; row < boardSize; row++) {
				for (int col = 0; col < boardSize; col++) {
					// Check neighbours of board[row, col]
					bool living = IsAlive(row, col, board[row, col]);
					// Set if alive or dead on next board
					nextBoard[row, col] = living;
				}
			}
			board = nextBoard; // Set current board to next board
			turns ++;
		}

		private static int LivingNeighbours(int row, int col) {
			int neighbours = 0;
			for (int r = row-1; r <= row+1; r++) {
				for (int c = col-1; c <= col+1; c++) {
					// Skip the current cell
					if (r == row && c == col) continue; 
					// Do not check if neighbor is off the board
					if (r < 0 || r >= boardSize || c < 0 || c >= boardSize) continue;
					// Add neighbours
					if (board[r, c]) neighbours++;
				}
			}
			return neighbours;
		}

		private static bool IsAlive(int row, int col, bool alive) {
			// Get number of living neighbours
			int neighbours = LivingNeighbours (row, col);
			// Return true for alive if 3 live neighbours
			if (neighbours == 3)
				return true;
			// Return true for alive if alive and 2 neighbours
			if (alive && neighbours == 2)
				return true;
			// Otherwise return false for dead
			return false;
		}

		private static void SetGliderPattern() {
			board = new bool[boardSize,boardSize];
			board [3, 5] = true;
			board [4, 5] = true;
			board [5, 5] = true;
			board [5, 4] = true;
			board [4, 3] = true;
		}

	}
}
