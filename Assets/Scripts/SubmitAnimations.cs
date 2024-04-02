using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitAnimations : MonoBehaviour
{
    // Purpose: Animate the sprites to move to specific destination positions within the history panel.
    // Params: sprites: List of active sprites to animate
    //         sources: Initial GameObjects selected (e.g. initCauldron, etc.)
    //         tryNumber: The current move the user is on
    //         panel: The history panel's transform
    // Return: void
    public void AnimateActiveSprites(List<Sprite> sprites, List<GameObject> sources, int tryNumber, Transform panel)
    {
        var copies = InstantiateCopies(sprites, sources);
        AnimateCopies(copies, tryNumber, panel);
    }

    // Purpose: Instantiates the copied item.
    // Params: sprites: List of active sprites to be copied.
    //         sources: Initial GameObjects selected (e.g. initCauldron, etc.)
    // Return: void
    private static List<GameObject> InstantiateCopies(List<Sprite> sprites, List<GameObject> sources)
    {

        var copies = new List<GameObject>();

        for (var i = 0; i < sprites.Count; i++)
        {
            // Make a copy of the ingredient.
            var copy = new GameObject($"Copy{i}");

            // Set its sprite.
            var imgComponent = copy.AddComponent<Image>();
            imgComponent.sprite = sprites[i];
            imgComponent.preserveAspect = true;

            // Set the parent to be the source frame (in the selection area).
            copy.transform.SetParent(sources[i].transform.parent);
            copy.transform.localPosition = sources[i].transform.localPosition;

            // Match the copy size to the source frame size.
            var sourceRect = sources[i].GetComponent<RectTransform>();
            var copyRect   = copy.GetComponent<RectTransform>();
            copyRect.sizeDelta = sourceRect.sizeDelta;

            copies.Add(copy);
        }
        return copies;
    }

    // Purpose: Animates the copied items.
    // Params: copies: The list of copied items.
    //         tryNumber: The current move the user is on
    //         panel: The history panel's transform
    // Return: void
    private void AnimateCopies(List<GameObject> copies, int tryNumber, Transform panel)
    {
        for (var i = 0; i < copies.Count; i++)
        {
            // Get destination location.
            var frameIndex = i % 4;
            var move = $"Move{tryNumber}";
            var destinationFrame = panel.Find(move).Find($"Frame{frameIndex}");
            StartCoroutine(MoveSpriteCoroutine(copies[i].transform, destinationFrame.transform, 1f, i * 0.15f));
        }
    }

    // Purpose: Coroutine for moving the copied items.
    // Params: source: Transform of the source object to move.
    //         destination: Transform of the destination object.
    //         duration: Duration of the movement animation.
    //         delay: Delay before starting the animation.
    // Return: IEnumerator
    private static IEnumerator MoveSpriteCoroutine(Transform source, Transform destination, float duration, float delay)
    {
        yield return new WaitForSeconds(delay);

        var startPosition = source.position;
        var startScale = source.localScale;
        var destinationPosition = destination.position;
        var destinationScale = new Vector3(0.45f, 0.45f);
        var elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the progress of the animation.
            var progress      = elapsedTime / duration;
            var easedProgress = 1 - Mathf.Pow(1 - progress, 2);

            // Interpolate position and scale.
            source.position   = Vector3.Lerp(startPosition, destinationPosition, easedProgress);
            source.localScale = Vector3.Lerp(startScale, destinationScale, easedProgress);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        source.position   = destinationPosition;
        source.localScale = destinationScale;
        source.SetParent(destination);
    }
}
