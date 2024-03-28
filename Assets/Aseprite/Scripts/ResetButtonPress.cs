using UnityEngine;

namespace Aseprite.Scripts
{
    public class ResetButtonPress : ButtonPress
    {
        [SerializeField] private PageManager pageManager;

        // Extension function that calls the reset function on the PageManager script.
        protected override void OnPointerUp(object sender, System.EventArgs e)
        {
            base.OnPointerUp(sender, e);
            pageManager.ResetPage();
        }
    }
}