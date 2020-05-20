using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class ScreenWrapper
{
    
    public static void AdjustPosition(GameObject gameObject, float collCircleRadius)
    {
        Vector3 currentPosition = gameObject.transform.position;
        if (currentPosition.x < ScreenUtils.ScreenLeft)
        {
            currentPosition.x = ScreenUtils.ScreenRight - collCircleRadius;
        }
        else if (currentPosition.x > ScreenUtils.ScreenRight)
        {
            currentPosition.x = ScreenUtils.ScreenLeft + collCircleRadius;
        }
        if (currentPosition.y < ScreenUtils.ScreenBottom)
        {
            currentPosition.y = ScreenUtils.ScreenTop - collCircleRadius;
        }
        else if (currentPosition.y > ScreenUtils.ScreenTop)
        {
            currentPosition.y = ScreenUtils.ScreenBottom + collCircleRadius;
        }

        gameObject.transform.position = currentPosition;
    }

}
