/* Description: Script that controls the history panel. */

using UnityEngine;
using UnityEngine.UI;

public class HistoryController : MonoBehaviour
{
    [SerializeField] private Sprite[] guessesBoxSprites;
    [SerializeField] private int[] pattern;
    [SerializeField] private bool useDotIndicators;
    [SerializeField] private Sprite greenFrame;

    // Subclass for the submit result (returns amount of correct guesses and whether the game has been won)
    public class SubmitResult
    {
        public int CorrectGuesses { get; set; }
        public bool GameWon { get; set; }
    }

    // Purpose: Generates the winning pattern (proper indices) of items for the game.
    // Params: none
    // Return: void
    private void Start()
    {
        // List declarations for winning pattern and chosen.
        pattern = new int[4];
        for (var i = 0; i < 4; i++)
        {
            pattern[i] = (Random.Range(0, 4));
        }
        print("Pattern is: " + string.Join(", ", pattern));
    }

    // Purpose: Fills a row/move in the history panel with whatever the user submitted.
    // Params: items: array of user-selected items
    //         tryNumber: the number of tries the user has attempted so far.
    // Return: the number of correct guesses.
    public SubmitResult GetResult(int[] indices, int tryNumber)
    {
        var correctGuesses = GetCorrectNumberOfItems(indices);
        var row = transform.GetChild(tryNumber);

        // Dot indicators used in harder difficulty.
        if (useDotIndicators)
        {
            row.GetChild(4).GetComponent<Image>().sprite = guessesBoxSprites[correctGuesses];
        }

        // Green frames used in easier difficulty.
        else
        {
            for (var i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] == indices[i])
                {
                    row.GetChild(i).GetComponent<Image>().sprite = greenFrame;
                }
            }
        }

        var result = new SubmitResult
        {
            CorrectGuesses = correctGuesses,
            GameWon = correctGuesses == 4
        };

        return result;
    }


    // Purpose: Calculates the number of correctly guessed items (provided indices vs. this object's pattern).
    // Params: indices: An array containing the indices of the user's guesses.
    // Return: the number of correct guesses.
    private int GetCorrectNumberOfItems(int[] indices)
    {
        var correctSoFar = 0;
        for (var i = 0; i < pattern.Length; i++)
        {
            if (pattern[i] == indices[i]) { correctSoFar++; }
        }

        return correctSoFar;
    }
}
