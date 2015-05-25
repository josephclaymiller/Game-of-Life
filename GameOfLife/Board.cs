using System;

namespace GameOfLife
{
	public class Board
	{
		public string pattern {
			set { 
				_pattern = value;
				SetPattern(_pattern);
			}
		}
		private string _pattern;
		private int boardSize;
		private bool[,] board;
		private bool[,] nextBoard;
		private string aliveSymbol = "*";
		private string deadSymbol = "-";
		private int turns;

		public Board(int boardSize = 10) {
			this.boardSize = boardSize;
			board = new bool[boardSize,boardSize];
			turns = 0;
		}
		
		public void TakeTurns(int turns = 10) {
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

		public void SetPattern (string pattern) {
			board = new bool[boardSize,boardSize];
			switch (pattern) {
			case "glider":
				SetGliderPattern();
				break;
			}
		}

		private void SetGliderPattern() {
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
			int neighbours = LivingNeighbours(row, col);
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

