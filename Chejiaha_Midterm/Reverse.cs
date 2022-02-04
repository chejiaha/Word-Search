using System;
using System.Collections.Generic;
using System.Text;

// reverse the string that user enters.
// to be used in search classes, find the word backwards in board
namespace Chejiaha_Midterm
{
    class Reverse
    {

        // reverses the word that user entered into a new array
        public char[] reverse(char[] arr)
        {
            // declare new array
            char[] arrReversed = new char[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                // new array assigned original array backwards
                arrReversed[arr.Length - i - 1] = arr[i];
            }
            return arrReversed;
        }
    }   
}
