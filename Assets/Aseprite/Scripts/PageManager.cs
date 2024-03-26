using System;
using UnityEngine;
using UnityEngine.UI;

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

        private void Start() { UpdatePage(); CheckArrowsState(); }

        public void FlipPage(int increment)
        {
            currentPage = Mathf.Clamp(currentPage + increment, 0, pages.Length - 1);
            UpdatePage();
            CheckArrowsState();
        }

        private void UpdatePage()
        {
            // Disable all pages
            foreach (var page in pages) { page.SetActive(false); }

            // Enable the current page
            pages[currentPage].SetActive(true);
        }

        private void CheckArrowsState()
        {
            if (leftArrow != null)  { leftArrow.SetActive(currentPage != 0); }
            if (rightArrow != null) { rightArrow.SetActive(currentPage != pages.Length - 1); }
        }

        public void ResetPage() { currentPage = 0; UpdatePage(); }
    }
}
