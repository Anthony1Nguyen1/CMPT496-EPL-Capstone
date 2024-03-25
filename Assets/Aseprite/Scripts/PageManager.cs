using System;
using UnityEngine;

namespace Aseprite.Scripts
{
    public class PageManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] pages;
        [SerializeField] private int currentPage;

        private void Start()
        {
            UpdatePage();
        }

        public void FlipPage(int increment)
        {
            currentPage = Mathf.Clamp(currentPage + increment, 0, pages.Length - 1);
            UpdatePage();
        }

        private void UpdatePage()
        {
            // Disable all pages
            foreach (var page in pages) { page.SetActive(false); }

            // Enable the current page
            pages[currentPage].SetActive(true);
        }

        public void ResetPage() { currentPage = 0; UpdatePage(); }
    }
}