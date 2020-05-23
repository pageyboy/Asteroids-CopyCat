using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides screen utilities
/// </summary>
public static class ScreenUtils
{
    #region Fields

    // saved to support resolution changes
    static int screenWidth;
    static int screenHeight;

    // cached for efficient boundary checking
    static float left;
    static float right;
    static float top;
    static float bottom;
    static float ratio;

    #endregion

    #region Properties

    /// <summary>
    /// Store the ratio of screen width to height
    /// </summary>
    public static float Ratio
    {
        get
        {
            CheckScreenSizeChanged();
            return ratio;
        }
    }

    /// <summary>
    /// Gets the left edge of the screen in world coordinates
    /// </summary>
    /// <value>left edge of the screen</value>
    public static float Left
    {
        get
        {
            CheckScreenSizeChanged();
            return left;
        }
    }

    /// <summary>
    /// Gets the right edge of the screen in world coordinates
    /// </summary>
    /// <value>right edge of the screen</value>
    public static float Right
    {
        get
        {
            CheckScreenSizeChanged();
            return right;
        }
    }

    /// <summary>
    /// Gets the top edge of the screen in world coordinates
    /// </summary>
    /// <value>top edge of the screen</value>
    public static float Top
    {
        get
        {
            CheckScreenSizeChanged();
            return top;
        }
    }

    /// <summary>
    /// Gets the bottom edge of the screen in world coordinates
    /// </summary>
    /// <value>bottom edge of the screen</value>
    public static float Bottom
    {
        get 
        {
            CheckScreenSizeChanged();
            return bottom; 
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Initializes the screen utilities
    /// </summary>
    public static void Initialize()
    {
        // save to support resolution changes
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        // save screen edges in world coordinates
        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
        Vector3 upperRightCornerScreen = new Vector3(
            screenWidth, screenHeight, screenZ);
        Vector3 lowerLeftCornerWorld =
            Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
        Vector3 upperRightCornerWorld =
            Camera.main.ScreenToWorldPoint(upperRightCornerScreen);
        left = lowerLeftCornerWorld.x;
        right = upperRightCornerWorld.x;
        top = upperRightCornerWorld.y;
        bottom = lowerLeftCornerWorld.y;
        CalculateScreenRatio();
    }

    static void CalculateScreenRatio()
    {
        float screenWidth = right - left;
        float screenHeight = top - Bottom;
        ratio = screenWidth / screenHeight;
    }

    /// <summary>
    /// Checks for screen size change
    /// </summary>
    static void CheckScreenSizeChanged()
    {
        if (screenWidth != Screen.width ||
            screenHeight != Screen.height)
        {
            Initialize();
        }
    }

    #endregion
}
