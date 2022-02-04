using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chejiaha_Midterm
{
    /// <summary>
    /// Jiahao Chen
    /// 991530024
    /// PROG32356
    /// 
    /// Simple word search application utilizing concepts from OOP.
    /// Displays knowledge of 2D arrays, abstract classes, inheritence,and WPF form user interface design.
    /// User will be asked to enter size of grid. Then click on draw button. Once the board is shown, user can
    /// enter a search string. If the string is found in the board, then user will be notified. User has options
    /// to highlight the words found, indicating their position on the board. User can also adjust text size via
    /// radio buttons.
    ///
    /// </summary>
    public partial class MainWindow : Window
    {
        // declaring global variables
        private static int len = 100;
        // initialize char and bool 2D array of length
        private char[,] board = new char[len, len];
        private bool[,] highlightedBoard = new bool[len, len];
        // all possible letters to be displayed
        private char[] alphabet = new char[26]
        { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
            'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private char[] word;
        // regex checks if user has entered only numbers
        Regex regex = new Regex("^[0-9]+$");
        // Object calls
        Reverse rev = new Reverse();
        Search sa = new SearchAcross();
        Search sd = new SearchDown();
        Search sdd = new SearchDiagonalDown();
        Search sdu = new SearchDiagonalUp();
        public MainWindow()
        {
            InitializeComponent();
        }

        public char[,] initialize()
        {
            // creates new random object
            Random rand = new Random();
            // initializes the boolean board everytime a new char board is created
            highlightedBoard = new bool[len, len];
            // iterates through the board
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    // generate an index from 0 - 25
                    int nextRand = rand.Next(0, alphabet.Length);
                    // assigns the letter at that random index for the alphabet array
                    char randomLetter = alphabet[nextRand];
                    // places the random letter in the 2D board array
                    board[i, j] = randomLetter;
                    // sets all values on boolean board to false
                    highlightedBoard[i, j] = false;
                }
            }
            return board;
        }

        // draws out the board in grid 
        public void drawBoard(char[,] board)
        {
            // clears the grid after before drawing anything
            wordGrid.Children.Clear();
            wordGrid.ColumnDefinitions.Clear();
            wordGrid.RowDefinitions.Clear();

            for (int i = 0; i < len; i++)
            {
                // initialize column and rows of grid
                wordGrid.ColumnDefinitions.Add(new ColumnDefinition());
                wordGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < wordGrid.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < wordGrid.ColumnDefinitions.Count; j++)
                {
                    // create a label for each empty cell and populate with 2D array
                    Label lbl = new Label();
                    lbl.Content = board[i, j]; // set label content to letter in board array
                    lbl.SetValue(Grid.RowProperty, i); // set letter in grid row
                    lbl.SetValue(Grid.ColumnProperty, j); // set letter in grid column
                    wordGrid.Children.Add(lbl); // dynamically add the label into each cell 
                }
            }
        }

        private void displayBoardBtn(object sender, RoutedEventArgs e)
        {
            // check is regex condition is met (only numbers in textbox)
            if (regex.IsMatch(length.Text))
            {
                // set the length of boards equal to value user entered
                len = Convert.ToInt32(length.Text);
                // initialize and draw
                // resets both char board and bool board
                initialize();
                drawBoard(board);
            }
            else
            {
                // if user did not enter a number, alert user
                MessageBox.Show("Please enter a length");
            }
        }

        private void SearchBtn(object sender, RoutedEventArgs e)
        {
            // checks if string is empty
            if (str.Text == "")
            {
                // alert user to enter a value to search
                MessageBox.Show("cannot be empty");
            }
            else
            {
                // sets the string entered by user to a char array
                word = str.Text.ToUpper().ToCharArray();
                // changes labels in stackpanel to display the number of matches found in puzzle
                // using global object calls from earlier
                lblAcross.Content = string.Format("Across: {0}", sa.search(word, rev.reverse(word), board, len));
                lblDown.Content = string.Format("Down: {0}", sd.search(word, rev.reverse(word), board, len));
                lblDiagonalDown.Content = string.Format("Diagonal Down: {0}", sdd.search(word, rev.reverse(word), board, len));
                lblDiagonalUp.Content = string.Format("Diagonal Up: {0}", sdu.search(word, rev.reverse(word), board, len));
                /*if ((bool)(chkHighlight.IsChecked))
                {
                    checkBox(object sender, RoutedEventArgs e);
                }*/
            }
        }

        // redraws the board when highlight is unchecked
        private void unChkHighlight(object sender, RoutedEventArgs e)
        {
            drawBoard(board);
        }

        private void checkBox(object sender, RoutedEventArgs e)
        {
            // check if user has entered a value into length before attempting to highlight
            if (str.Text != "" && length.Text !="")
            {
                // clears the grid after before drawing anything
                wordGrid.Children.Clear();
                wordGrid.ColumnDefinitions.Clear();
                wordGrid.RowDefinitions.Clear();
                highlightedBoard = new bool[len, len];

                // sets the boolean values on the boolean board where the word(s) were found
                // does not override the existing board each time an object is called, to show all 4 directions.
                // Only overwrites the board if new word is requested by user
                highlightedBoard = sa.searchHighlight(word, rev.reverse(word), board, highlightedBoard, len);
                highlightedBoard = sd.searchHighlight(word, rev.reverse(word), board, highlightedBoard, len);
                highlightedBoard = sdu.searchHighlight(word, rev.reverse(word), board, highlightedBoard, len);
                highlightedBoard = sdd.searchHighlight(word, rev.reverse(word), board, highlightedBoard, len);

                for (int i = 0; i < len; i++)
                {
                    // initialize column and rows of grid
                    wordGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    wordGrid.RowDefinitions.Add(new RowDefinition());
                }
                for (int i = 0; i < wordGrid.RowDefinitions.Count; i++)
                {
                    for (int j = 0; j < wordGrid.ColumnDefinitions.Count; j++)
                    {
                        // create a label for each empty cell and populate with 2D array
                        Label lbl = new Label();
                        lbl.Content = board[i, j]; // set label content to letter in board array
                        lbl.SetValue(Grid.RowProperty, i); // set letter in grid row
                        lbl.SetValue(Grid.ColumnProperty, j); // set letter in grid column
                        // if element in highlightedBoard array is true
                        if (highlightedBoard[i, j])
                        {
                            lbl.Foreground = Brushes.Red; // change label colour
                        }
                        wordGrid.Children.Add(lbl); // dynamically add the label into each cell 
                    }
                }
            }
            else
            {
                MessageBox.Show("Please draw the board and search for word first");
            }
        }

        // skin handlers
        // will change the size and padding of labels in word grid

        // smaller label
        private void rbLayout1_Clicked(object sender, RoutedEventArgs e)
        {
            ResourceDictionary skin =
             Application.LoadComponent(new Uri("Layout1.xaml", UriKind.Relative)) as ResourceDictionary;
            Resources.MergedDictionaries.Add(skin);
        }
        // bigger label
        private void rbLayout12_Clicked(object sender, RoutedEventArgs e)
        {
            ResourceDictionary skin =
             Application.LoadComponent(new Uri("Layout2.xaml", UriKind.Relative)) as ResourceDictionary;
            Resources.MergedDictionaries.Add(skin);
        }
        // reset label
        private void rbLayout3_Clicked(object sender, RoutedEventArgs e)
        {
            ResourceDictionary skin =
             Application.LoadComponent(new Uri("Layout3.xaml", UriKind.Relative)) as ResourceDictionary;
            Resources.MergedDictionaries.Add(skin);
        }
    }
}
