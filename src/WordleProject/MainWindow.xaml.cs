using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WordleProject
{
    public partial class MainWindow : Window
    {

        // ADD WORD LIST HERE

        string targetWord = "APPLE";
        int currentRow = 0;
        int currentCol = 0;
        TextBlock[,] tiles = new TextBlock[6, 5];


        public MainWindow()
        {
            InitializeComponent();

            //Game Grid
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    var border = new Border
                    {
                        BorderBrush = new SolidColorBrush(Color.FromRgb(58, 58, 58)),
                        BorderThickness = new Thickness(2),
                        Width = 60,
                        Height = 60,
                        CornerRadius = new CornerRadius(4),
                        Background = new SolidColorBrush(Color.FromRgb(18, 18, 18)),
                        Margin = new Thickness(4)
                    };

                    var textBlock = new TextBlock
                    {
                        Text = "",
                        Foreground = Brushes.White,
                        FontSize = 24,
                        FontWeight = FontWeights.Bold,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        TextAlignment = TextAlignment.Center
                    };

                    border.Child = textBlock;
                    tiles[row, col] = textBlock;
                    TileGrid.Children.Add(border);
                }
            }
            this.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {   
            // End Game
            if (currentRow >= 6) return;

            // A to Z and Column Number
            if (e.Key >= Key.A && e.Key <= Key.Z && currentCol < 5)
            {
                char letter = e.Key.ToString()[0];
                tiles[currentRow, currentCol].Text = letter.ToString();
                currentCol++;
            }
            // Backspace
            else if (e.Key == Key.Back && currentCol > 0)
            {
                currentCol--;
                tiles[currentRow, currentCol].Text = "";
            }
            //Checking The Guess
            else if (e.Key == Key.Enter && currentCol == 5)
            {
                CheckGuess();
            }
        }

        private void CheckGuess()
        {
            string guess = "";
            for (int i = 0; i < 5; i++)
                guess += tiles[currentRow, i].Text.ToUpper();

            char[] result = new char[5];
            bool[] used = new bool[5];

            // Green Letters
            for (int i = 0; i < 5; i++)
            {
                if (guess[i] == targetWord[i])
                {
                    result[i] = 'G';
                    used[i] = true;
                }
            }

            // Yellow Letters
            for (int i = 0; i < 5; i++)
            {
                if (result[i] == 'G') continue;
                for (int j = 0; j < 5; j++)
                {
                    if (!used[j] && guess[i] == targetWord[j])
                    {
                        result[i] = 'Y';
                        used[j] = true;
                        break;
                    }
                }
            }

            // Setting colors
            for (int i = 0; i < 5; i++)
            {
                if (result[i] == 'G')
                    tiles[currentRow, i].Background = new SolidColorBrush(Color.FromRgb(83, 141, 78));
                else if (result[i] == 'Y')
                    tiles[currentRow, i].Background = new SolidColorBrush(Color.FromRgb(181, 159, 59));
                else
                    tiles[currentRow, i].Background = new SolidColorBrush(Color.FromRgb(58, 58, 58));
            }

            if (guess == targetWord)
            {
                displayButton();
            }
            else if (currentRow == 5)
            {
                // Restart Button
                displayButton();

                // Display The Word
                TextBox wordBox = new TextBox
                {
                    Text = $"The word was: {targetWord}",
                    Width = 200,
                    Height = 40,
                    FontSize = 15,
                    BorderThickness = new Thickness(0),
                    Background = Brushes.Transparent,
                    Foreground = new SolidColorBrush(Color.FromRgb(37, 130, 0)),
                    IsReadOnly = true,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextAlignment = TextAlignment.Center
                };

                ButtonGrid.Children.Add(wordBox);
            }

            currentRow++;
            currentCol = 0;
        }

        private void displayButton()
        {
            Button restartButton = new Button
            {
                Content = "RESTART",
                Width = 150,
                Height = 50,
                FontSize = 25,
                BorderThickness = new Thickness(0),
                FontWeight = FontWeights.Black,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = Brushes.Transparent,
                Foreground = new SolidColorBrush(Color.FromRgb(37, 130, 0)),
                VerticalAlignment = VerticalAlignment.Center
            };

            ButtonGrid1.Children.Add(restartButton);
        }
    }

    // CUSTOM WORD LIST
    // RESTART
    // FIX LETTER BG

}