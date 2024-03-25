using UnityEngine;

namespace Aseprite.Scripts
{
    // InstructionsButtonPress.cs
    public class InstructionsButtonPress : ButtonPress
    {
        private enum ArrowType { Left, Right }
        [SerializeField] private ArrowType arrowType;
        [SerializeField] private PageManager pageManager;

        protected override void OnPointerUp(object sender, System.EventArgs e)
        {
            base.OnPointerUp(sender, e);

            if (arrowType == ArrowType.Left)
            {
                pageManager.FlipPage(-1);
            }
            else if (arrowType == ArrowType.Right)
            {
                pageManager.FlipPage(1);
            }
        }
    }
}