using System;

namespace GameOfLife
{
	public class Board
	{
		private static int boardSize = 10;
		private static bool[,] board;
		private static bool[,] nextBoard;
		private static string aliveSymbol = "*";
		private static string deadSymbol = "-";
		private static int turns = 0;
		private const int maxTurns = 10;
		
		public void TakeTurns(int turns) {
			for (int i = 0; i < turns; i++) {
				TakeTurn();
				ShowBoard();
			}
		}
		
		public void ShowBoard() {
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

		public void SetGliderPattern() {
			board = new bool[boardSize,boardSize];
			board [3, 5] = true;
			board [4, 5] = true;
			board [5, 5] = true;
			board [5, 4] = true;
			board [4, 3] = true;
		}

		public void SetBoardSize(int size){
			boardSize = size;
		}
		
		private void TakeTurn() {
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
		
		private int LivingNeighbours(int row, int col) {
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
		
		private bool IsAlive(int row, int col, bool alive) {
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

	}
}

