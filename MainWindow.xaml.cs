/*
 * Quinn Parker-Joyes
 * April 23 2019
 * ICS4U
 * Scrabble program, shows word that uses most tiles and has most points
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.IO;
using System.Collections;

namespace _316968Unit3Summative
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //global variables
        string currentW, tempW, lastW, numbOfTiles;

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            numbOfTiles = txtInput.Text;
            MessageBox.Show(numbOfTiles, "Your Tiles:");
            //removes blank tiles
            while (numbOfTiles.IndexOf(" ") != -1)
            {
                numbOfTiles = numbOfTiles.Remove(numbOfTiles.IndexOf(" "), 1);
            }
            int blankTiles = 7 - numbOfTiles.Length;
            System.Net.WebClient wc = new System.Net.WebClient();
            Stream download = wc.OpenRead("http://darcy.rsgc.on.ca/ACES/ICS4U/SourceCode/Words.txt");
            StreamReader sr = new StreamReader(download);
            StreamWriter sw = new StreamWriter("scoringWords.txt");
            for (int i = 0; i < numbOfTiles.Length; i++)
            {
                int removeLetter = badLetters.IndexOf(numbOfTiles.Substring(i, 1));
                if (removeLetter != -1)
                {
                    badLetters = badLetters.Remove(removeLetter, 1);
                }
            }
            try
            {
                while (!sr.EndOfStream)
                {
                    int counter = 0;
                    currentW = sr.ReadLine().ToUpper();
                    tempW = currentW;
                    if (currentW.Length < 8 && currentW != lastW)
                    {
                        for (int i = 0; i < badLetters.Length; i++)
                        {
                            if (currentW.Contains(badLetters.Substring(i, 1)))
                            {
                                counter++;
                            }
                            if (counter > blankTiles)
                            {
                                i = 26;
                            }
                        }
                        if (counter <= blankTiles)
                        {
                            for (int i = 0; i < numbOfTiles.Length; i++)
                            {
                                string currentLetter = numbOfTiles.Substring(i, 1);
                                int removeLetter = tempW.IndexOf(currentLetter);
                                if (removeLetter > -1)
                                {
                                    tempW = tempW.Remove(removeLetter, 1);
                                }
                            }
                            if (tempW.Length == blankTiles || tempW.Length == 0)
                            {
                                sw.WriteLine(currentW);
                                lastW = currentW;
                            }
                        }
                    }
                }
                sr.Close();
                sw.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            StreamReader sr2 = new StreamReader("scoringWords.txt");
            string tempString = "";
            int maxPoints = 0;
            string topWord = "";
            try
            {
                while (!sr2.EndOfStream)
                {
                    currentW = sr2.ReadLine();
                    int totalPoints = 0;
                    for (int i = 0; i < currentW.Length; i++)
                    {
                        if (!badLetters.Contains(currentW.ToCharArray()[i]))
                        {
                            ScrabbleLetter sl = new ScrabbleLetter(currentW.ToCharArray()[i]);
                            totalPoints += sl.Points;
                        }
                    }
                    if (totalPoints > maxPoints)
                    {
                        maxPoints = totalPoints;
                        topWord = currentW;
                    }
                    tempString += currentW + " ";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txtOutput.Text = "Scoring Words:\n\n" + tempString + "\n\nHigh Scoring Word is " + topWord.Substring(0, 1) + topWord.Substring(1).ToLower() + " with a score of " + maxPoints + "\n\n" + "Your Tiles:\n\n" + numbOfTiles;
        }

        //sorts most to least common
        string badLetters = "ETAOINSHRDLCUMWFGYPBVKJXQZ";
        public MainWindow()
        {
            InitializeComponent();
            ScrabbleGame sg = new ScrabbleGame();
            numbOfTiles = sg.drawInitialTiles();
            MessageBox.Show(numbOfTiles, "Your Tiles:");
            while (numbOfTiles.IndexOf(" ") != -1)
            {
                numbOfTiles = numbOfTiles.Remove(numbOfTiles.IndexOf(" "), 1);
            }
            int blankTiles = 7 - numbOfTiles.Length;
            System.Net.WebClient wc = new System.Net.WebClient();
            Stream download = wc.OpenRead("http://darcy.rsgc.on.ca/ACES/ICS4U/SourceCode/Words.txt");
            StreamReader sr = new StreamReader(download);
            StreamWriter sw = new StreamWriter("scoringWords.txt");
            for (int i = 0; i < numbOfTiles.Length; i++)
            {
                int removeLetter = badLetters.IndexOf(numbOfTiles.Substring(i, 1));
                if (removeLetter != -1)
                {
                    badLetters = badLetters.Remove(removeLetter, 1);
                }
            }
            while (!sr.EndOfStream)
            {
                int counter = 0;
                currentW = sr.ReadLine().ToUpper();
                tempW = currentW;
                if (currentW.Length < 8 && currentW != lastW)
                {
                    for (int i = 0; i < badLetters.Length; i++)
                    {
                        if (currentW.Contains(badLetters.Substring(i, 1)))
                        {
                            counter++;
                        }
                        if (counter > blankTiles)
                        {
                            i = 26;
                        }
                    }
                    if (counter <= blankTiles)
                    {
                        for (int i = 0; i < numbOfTiles.Length; i++)
                        {
                            string currentLetter = numbOfTiles.Substring(i, 1);
                            int removeLetter = tempW.IndexOf(currentLetter);
                            if (removeLetter > -1)
                            {
                                tempW = tempW.Remove(removeLetter, 1);
                            }
                        }
                        if (tempW.Length == blankTiles || tempW.Length == 0)
                        {
                            sw.WriteLine(currentW);
                            lastW = currentW;
                        }
                    }
                }
            }
            sr.Close();
            sw.Close();
            StreamReader sr2 = new StreamReader("scoringWords.txt");
            string tempString = "";
            int maxPoints = 0;
            string topWord = "";
            while (!sr2.EndOfStream)
            {
                currentW = sr2.ReadLine();
                int totalPoints = 0;
                for (int i = 0; i < currentW.Length; i++)
                {
                    if (!badLetters.Contains(currentW.ToCharArray()[i]))
                    {
                        ScrabbleLetter sl = new ScrabbleLetter(currentW.ToCharArray()[i]);
                        totalPoints += sl.Points;
                    }
                }
                if (totalPoints > maxPoints)
                {
                    maxPoints = totalPoints;
                    topWord = currentW.Substring(0, 1) + currentW.Substring(1).ToLower();
                }
                else if (totalPoints == maxPoints)
                {
                    topWord += " or " + currentW.Substring(0, 1) + currentW.Substring(1).ToLower();
                }
                tempString += currentW + " ";
            }
            MessageBox.Show(tempString + "\n\nHigh Scoring Word is " + topWord + " with a score of " + maxPoints + "\n\n" + "Your Tiles:\n\n" + numbOfTiles, "Scoring Words");
            sr2.Close();
        }
    }
    
}
