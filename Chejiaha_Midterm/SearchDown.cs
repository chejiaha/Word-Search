using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chejiaha_Midterm
{
    class SearchDown : Search // searchDown inherits Search
    {

        // overrides virtual method from abstract class 
        public override bool searchWord(int startRow, int startCol, char[] arr, char[,] board)
        {
            char[] match = new char[arr.Length];
            // iterate through the array to find match
            for (int a = 0; a < arr.Length; a++)
            {
                // starts at position where first letter was found, then iterates across (static row coordinate)
                // to check if the rest of word matches the characters on the board
                if ((arr[a] == board[startRow + a, startCol]))
                {
                    // assign temp array values
                    match[a] = arr[a];
                }
            }
            // check if the array stored is equal to the word user entered
            if (match.SequenceEqual(arr))
            {
                // if equal then return true;
                return true;
            }
            return false;
        }

        // overrides the virtual method 
        // this method is only ran if the word has already been found in the board.
        // therefore, not if statements were required.
        public override bool[,] highlightedBoard(int startRow, int startCol, char[] arr, bool[,] hboard)
        {
            // loops for the length of array
            for (int a = 0; a < arr.Length; a++)
            {
                // sets the boolean values on bool board to be true where the word was found
                hboard[startRow + a, startCol] = true;
            }
            return hboard;
        }
    }
}
