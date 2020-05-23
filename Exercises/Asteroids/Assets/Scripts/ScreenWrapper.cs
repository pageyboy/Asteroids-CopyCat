using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class ScreenWrapper
{
    
    public static void AdjustPosition(GameObject gameObject, float collCircleRadius)
    {
        Vector3 currentPosition = gameObject.transform.position;
        if (currentPosition.x < ScreenUtils.Left)
        {
            currentPosition.x = ScreenUtils.Right - collCircleRadius;
        }
        else if (currentPosition.x > ScreenUtils.Right)
        {
            currentPosition.x = ScreenUtils.Left + collCircleRadius;
        }
        if (currentPosition.y < ScreenUtils.Bottom)
        {
            currentPosition.y = ScreenUtils.Top - collCircleRadius;
        }
        else if (currentPosition.y > ScreenUtils.Top)
        {
            currentPosition.y = ScreenUtils.Bottom + collCircleRadius;
        }

        gameObject.transform.position = currentPosition;
    }

}
