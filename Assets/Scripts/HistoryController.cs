/* Description: Script that controls the history panel. */

/* Description: Script that controls the history panel. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryController : MonoBehaviour
{
    [SerializeField] private Sprite[] guessesBoxSprites;
    [SerializeField] private int[] pattern;

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
    public SubmitResult Submit(List<Sprite> items, List<Vector3> positions, int tryNumber, int[] indices)
    {
        AnimateActiveSprites(items, positions);
        var correctGuesses = GetCorrectNumberOfItems(indices);
        var result = new SubmitResult
        {
            CorrectGuesses = correctGuesses,
            GameWon = correctGuesses == 4
        };

        return result;
    }

    // Purpose: Animate the sprites to move to specific destination positions within the history panel.
    // Params: activeSprites: List of active sprites to animate
    //         destinations: List of destination positions for the active sprites
    // Return: void
    // ReSharper disable Unity.PerformanceAnalysis
    public void AnimateActiveSprites(List<Sprite> sprites, List<Vector3> positions)
    {

        // TODO: Get the AnimatedSprite into the specific move in the history panel
        // TODO: Get the sprite to show up as verification
        // TODO: Once verified, figure out how to handle destinations

        // Loop through each sprite
        for (var i = 0; i < sprites.Count; i++)
        {
            // Instantiate the GameObject and attach an Image component
            var animatedSpriteObject = new GameObject("AnimatedSprite");
            var imageComponent       = animatedSpriteObject.AddComponent<Image>();
            imageComponent.sprite    = sprites[i];

            // Set the position and parent of the new copy
            animatedSpriteObject.transform.position = positions[i];
            animatedSpriteObject.transform.SetParent(transform);

            // Start coroutine to move the animated sprite to its destination
            // StartCoroutine(MoveSpriteCoroutine(animatedSpriteObject, destinations[i]));
        }
    }

    // Coroutine to move the sprite to the animation destination
    private IEnumerator MoveSpriteCoroutine(GameObject animatedSpriteObject, Vector3 destination)
    {
        const float duration = 2f;
        var startPosition = animatedSpriteObject.transform.position;

        var elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            animatedSpriteObject.transform.position = Vector3.Lerp(startPosition, destination, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Destroy(animatedSpriteObject); // Destroy the animated sprite object after animation is complete
    }

    // Helper method to calculate the correct number of items
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
