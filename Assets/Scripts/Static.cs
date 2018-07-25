using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public static Vector2[] Card12Points =
    {
        new Vector2(-50, 75), new Vector2(0, 75), new Vector2(50, 75),
        new Vector2(-50, 25), new Vector2(0, 25), new Vector2(50, 25),
        new Vector2(-50, -25), new Vector2(0, -25), new Vector2(50, -25),
        new Vector2(-50, -75), new Vector2(0, -75), new Vector2(50, -75)
    };
    public static Vector2[] Card70Points =
    {
        new Vector2(-60, 90), new Vector2(-40, 90), new Vector2(-20, 90), new Vector2(0, 90),new Vector2(20, 90),new Vector2(40, 90),new Vector2(60, 90),
        new Vector2(-60, 70), new Vector2(-40, 70), new Vector2(-20, 70), new Vector2(0, 70),new Vector2(20, 70),new Vector2(40, 70),new Vector2(60, 70),
        new Vector2(-60, 50), new Vector2(-40, 50), new Vector2(-20, 50), new Vector2(0, 50),new Vector2(20, 50),new Vector2(40, 50),new Vector2(60, 50),
        new Vector2(-60, 30), new Vector2(-40, 30), new Vector2(-20, 30), new Vector2(0, 30),new Vector2(20, 30),new Vector2(40, 30),new Vector2(60, 30),
        new Vector2(-60, 10), new Vector2(-40, 10), new Vector2(-20, 10), new Vector2(0, 10),new Vector2(20, 10),new Vector2(40, 10),new Vector2(60, 10),
        new Vector2(-60, -10), new Vector2(-40, -10), new Vector2(-20, -10), new Vector2(0, -10),new Vector2(20, -10),new Vector2(40, -10),new Vector2(60, -10),
        new Vector2(-60, -30), new Vector2(-40, -30), new Vector2(-20, -30), new Vector2(0, -30),new Vector2(20, -30),new Vector2(40, -30),new Vector2(60, -30),
        new Vector2(-60, -50), new Vector2(-40, -50), new Vector2(-20, -50), new Vector2(0, -50),new Vector2(20, -50),new Vector2(40, -50),new Vector2(60, -50),
        new Vector2(-60, -70), new Vector2(-40, -70), new Vector2(-20, -70), new Vector2(0, -70),new Vector2(20, -70),new Vector2(40, -70),new Vector2(60, -70),
        new Vector2(-60, -90), new Vector2(-40, -90), new Vector2(-20, -90), new Vector2(0, -90),new Vector2(20, -90),new Vector2(40, -90),new Vector2(60, -90),
    };

    public static int LocateNearestPoint(Vector2 position, Vector2[] avaliblePoints)
    {
        var resultIndex = 0;
        var distance = ComputeDistance(position, avaliblePoints[0]);
        for (var i = 0; i < avaliblePoints.Length; i++)
        {
            var tempDistance = ComputeDistance(position, avaliblePoints[i]);
            if (tempDistance < distance)
            {
                distance = tempDistance;
                resultIndex = i;
            }
        }
        return resultIndex;
    }

    public static Color ShapeColorToUnityColor(Shape.Figures_Colours shapeColor)
    {
        Color result = Color.white;
        switch (shapeColor)
        {
            case Shape.Figures_Colours.Light_Blue:
                result = new Color(0, 0, 1);
                break;
            case Shape.Figures_Colours.Dark_Blue:
                result = new Color(0, 0, 0.627451f);
                break;
            case Shape.Figures_Colours.Light_Green:
                result = new Color(0, 1, 0);
                break;
            case Shape.Figures_Colours.Dark_Green:
                result = new Color(0.07058824f, 0.372549f, 0.1568628f);
                break;
            case Shape.Figures_Colours.Violet:
                result = new Color(0.6392157f, 0.2862745f, 0.6392157f);
                break;
            case Shape.Figures_Colours.Pink:
                result = new Color(1, 0.5019608f, 0.7529412f);
                break;
            case Shape.Figures_Colours.Red:
                result = new Color(1, 0, 0);
                break;
            case Shape.Figures_Colours.Yellow:
                result = new Color(1, 1, 0);
                break;
            case Shape.Figures_Colours.Orange:
                result = new Color(1, 0.5311697f, 0.05490196f);
                break;
            default:
                throw new ArgumentOutOfRangeException("shapeColor", shapeColor, null);
        }

        return result;
    }

    static float ComputeDistance(Vector2 point1, Vector2 point2)
    {
        var result = Mathf.Sqrt(Mathf.Pow((point2.x - point1.x), 2) + Mathf.Pow((point2.y - point1.y), 2));
        return result;
    }
}

public static class Static
{
    public static Difficulty_Modifiers DifficultyModifiers;
    public static List<Shape_With_Place> ShapeWithPlaces;
}
