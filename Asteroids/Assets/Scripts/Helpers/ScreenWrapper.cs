using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Static class that provides the functionality along with the ScreenUtils class for wrapping objects that fall off
/// of the world screen.
/// </summary>
static public class ScreenWrapper
{ 
    public static void AdjustPosition(GameObject gameObject, float collCircleRadius)
    {
        Vector3 currentPosition = gameObject.transform.position;
        float offset = collCircleRadius;
        if (currentPosition.x < ScreenUtils.Left)
        {
            currentPosition.x = ScreenUtils.Right;
        }
        else if (currentPosition.x > ScreenUtils.Right)
        {
            currentPosition.x = ScreenUtils.Left;
        }
        if (currentPosition.y < ScreenUtils.Bottom)
        {
            currentPosition.y = ScreenUtils.Top;
        }
        else if (currentPosition.y > ScreenUtils.Top)
        {
            currentPosition.y = ScreenUtils.Bottom;
        }

        gameObject.transform.position = currentPosition;
    }

}
