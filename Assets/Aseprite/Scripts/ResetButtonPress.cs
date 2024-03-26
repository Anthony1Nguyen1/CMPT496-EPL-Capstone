using UnityEngine;

namespace Aseprite.Scripts
{
    // ResetButtonPress.cs
    public class ResetButtonPress : ButtonPress
    {
        [SerializeField] private PageManager pageManager;

        protected override void OnPointerUp(object sender, System.EventArgs e)
        {
            base.OnPointerUp(sender, e);
            pageManager.ResetPage();
        }
    }
}