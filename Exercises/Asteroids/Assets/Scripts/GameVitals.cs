using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// this class is used to keep track of the vitals to be displayed on the screen
/// </summary>
public static class GameVitals
{
    static bool show = false;
    static float xVelocity;
    static float yVelocity;
    static int rotation;
    static float angularVelocity;
    static string vitalsString = "";

    public static string VitalsString
    {
        get
        { 
            return vitalsString;
        }
    }

    public static void UpdateVitals(float newXVelocity, float newYVelocity,
        float newRotation, float newAngularVelocity)
    {
        xVelocity = newXVelocity;
        yVelocity = newYVelocity;
        rotation = (int)newRotation;
        angularVelocity = newAngularVelocity;
        if (show)
        {
            vitalsString = "x Velocity: " + xVelocity.ToString("0.00") +
                            "\ny Velocity: " + yVelocity.ToString("0.00") +
                            "\nDirection: " + rotation + "°" +
                            "\nAngular Velocity: " + angularVelocity.ToString("0.0") + "°/s";
        }

    }

}
