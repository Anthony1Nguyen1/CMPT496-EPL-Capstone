using UnityEngine;

namespace Aseprite.Scripts
{
    public class PageManager : MonoBehaviour
    {
        // Pages
        [SerializeField] private GameObject[] pages;
        [SerializeField] private int currentPage;

        // Arrows
        [SerializeField] private GameObject leftArrow;
        [SerializeField] private GameObject rightArrow;

        // Purpose: Flips the page by the specified increment.
        // Params: increment: (either -1 or 1)
        // Return: void
        public void FlipPage(int increment)
        {
            currentPage = Mathf.Clamp(currentPage + increment, 0, pages.Length - 1);
            UpdatePage();
            CheckArrowsState();
        }

        // Purpose: Updates the visibility of the pages based on the current page index.
        // Params: none
        // Return: void
        private void UpdatePage()
        {
            // Disable all pages
            foreach (var page in pages) { page.SetActive(false); }

            // Enable the current page
            pages[currentPage].SetActive(true);
        }

        // Purpose: Disables arrows depending on the page (leftmost or rightmost).
        // Params: None
        // Return: void
        private void CheckArrowsState()
        {
            if (leftArrow != null) { leftArrow.SetActive(currentPage != 0); }
            if (rightArrow != null) { rightArrow.SetActive(currentPage != pages.Length - 1); }
        }

        // Purpose: Resets the instructions/arrow state to the first page.
        // Params: None
        // Return: void
        public void ResetPage()
        {
            currentPage = 0;
            UpdatePage();
            CheckArrowsState();
        }
    }
}
