using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public ItemController itemController;
    public void OnDownArrowClick() { itemController.CycleDown(); }
    public void OnUpArrowClick() { itemController.CycleUp(); }
}
