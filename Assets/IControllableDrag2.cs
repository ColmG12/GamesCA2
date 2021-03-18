using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IControllableDrag2
{
    void youveBeenTapped();
    void MoveTo(Touch touch, Vector3 destination);

    void selectObject(bool isSelected);
}