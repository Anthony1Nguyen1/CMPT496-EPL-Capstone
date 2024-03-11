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
    public SubmitResult Submit(List<Sprite> sprites, List<GameObject> sources, int tryNumber, int[] indices)
    {
        AnimateActiveSprites(sprites, sources, tryNumber);
        var correctGuesses = GetCorrectNumberOfItems(indices);
        var result = new SubmitResult
        {
            CorrectGuesses = correctGuesses,
            GameWon = correctGuesses == 4
        };

        return result;
    }

    // Purpose: Animate the sprites to move to specific destination positions within the history panel.
    // Params: sprites: List of active sprites to animate
    //         sources: Initial GameObjects selected (e.g. initCauldron, etc.)
    //         tryNumber: The current move the user is on
    // Return: void
    // ReSharper disable Unity.PerformanceAnalysis
    public void AnimateActiveSprites(List<Sprite> sprites, List<GameObject> sources, int tryNumber)
    {
        InstantiateCopies(sprites, sources, tryNumber);
        // StartCoroutine(AnimateCopies(sprites, sources, tryNumber));
    }
    private void InstantiateCopies(List<Sprite> sprites, List<GameObject> sources, int tryNumber)
        {
            // Loop through each sprite
            for (var i = 0; i < sprites.Count; i++)
            {
                // Instantiate the GameObject and attach an Image component.
                var animatedSpriteObject = new GameObject($"Copy{i}");
                var imageComponent = animatedSpriteObject.AddComponent<Image>();
                imageComponent.sprite = sprites[i];

                // Make a copy of the ingredient.
                var copy = new GameObject($"Copy{i}");

                // Set the sprite and preserve its aspect.
                var imgComponent = copy.AddComponent<Image>();
                imgComponent.sprite = sprites[i];
                imgComponent.preserveAspect = true;

                // Set the initial parent to the parent of the source object.
                copy.transform.SetParent(sources[i].transform.parent);
                copy.transform.localPosition = sources[i].transform.localPosition;

                // Set the size of the copy to match the size of the source.
                var sourceRect = sources[i].GetComponent<RectTransform>();
                var copyRect   = copy.GetComponent<RectTransform>();
                copyRect.sizeDelta = sourceRect.sizeDelta;

                // Get destination location.
                var frameIndex = i % 4;
                var move = $"Move{tryNumber}";
                var destinationFrame = transform.Find(move).Find($"Frame{frameIndex}");
                Debug.Log($"Parent is: {move}, {destinationFrame}");

                // Start animation coroutine.
                // yield return StartCoroutine(MoveSpriteCoroutine(copy.transform, destinationFrame.transform, 1f));
                //

                // Set the copy's parent to the destination frame.
                // copy.transform.SetParent(destinationFrame);
            }
        }

    // Coroutine to move the sprite to the animation destination
    private IEnumerator MoveSpriteCoroutine(Transform objTransform, Transform destinationTransform, float duration)
    {
        var startPosition = objTransform.position;
        var startScale = objTransform.localScale;
        var destinationPosition = destinationTransform.position;
        var destinationScale = new Vector3(0.45f, 0.45f);
        var elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Interpolate position
            objTransform.position = Vector3.Lerp(startPosition, destinationPosition, elapsedTime / duration);

            // Interpolate scale
            objTransform.localScale = Vector3.Lerp(startScale, destinationScale, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final position and scale are exact
        objTransform.position = destinationPosition;
        objTransform.localScale = destinationScale;
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
