namespace Chejiaha_Midterm
{
    // Parent/abstract class of search methods
    abstract class Search
    {
        public double counter = 0;
        // search method is used in every child class. Does not get overriden.
        // This method checks if the word is on the board and ensures that the program
        // does not check an outofbounds index before continuing to other search methods.
        public double search(char[] arr, char[] arrRev, char[,] board, int len)
        {
            counter = 0; // initialize the counter for matches found
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    // checks if first letter is on the board
                    // checks if the word entered by user exceeds the legnth of board
                    if ((board[i, j] == arr[0] || board[i, j] == arrRev[0]) && arr.Length <= len)
                    {
                        // if word is found in normal or reverse order then increment the counter
                        // uses the virtual method searchWord in child class
                        if (searchWord(i, j, arr, board) || searchWord(i, j, arrRev, board))
                        {
                            counter++;
                        }
                    }
                }
            }
            return counter; // always return the count for each orientation of search
        }

        // similar to the previous search method. This one sets the boolean values in boolean board to be true if found.
        // this method does not get overriden and is called by every child class.
        public bool[,] searchHighlight(char[] arr, char[] arrRev, char[,] board, bool[,] hboard, int len)
        {
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    // checks if first letter is on the board
                    // checks if the word entered by user exceeds the legnth of board
                    if ((board[i, j] == arr[0] || board[i, j] == arrRev[0]) && arr.Length <= len)
                    {
                        // if word is found in normal or reverse order then increment set value to true
                        if (searchWord(i, j, arr, board) || searchWord(i, j, arrRev, board))
                        {
                            // pass and set the same hboard, do not want to instantiate as we want to retrieve all
                            // valuess in every direction of search.
                            // uses virtual method highLightedBoard in child class
                            hboard = highlightedBoard(i, j, arr, hboard);
                        }
                    }
                }
            }
            return hboard; // always return the boolean board
        }

        // Declaring virtual mathods. These will be overriden in child classes
        // These methods will be overridden in the child classes. Virtual methods are used because in the main class, 
        // the base method Search from the abstract class is called. 
        // In the Search method, the child class is called to search in each direction and highlight. 
        // Therefore a virtual implementation is used.
        public virtual bool searchWord(int i, int j, char[] arr, char[,] board)
        {
            return false;
        }

        public virtual bool[,] highlightedBoard(int i, int j, char[] arr, bool[,] hboard)
        {
            return hboard;
        }
    }
}
