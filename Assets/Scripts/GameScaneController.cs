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

    public static Vector2 LocateNearestPoint(Vector2 position, Vector2[] avaliblePoints)
    {
        Vector2 result = avaliblePoints[0];
        float distance = ComputeDistance(position, avaliblePoints[0]);
        foreach (var point in avaliblePoints)
        {
            var tempDistance = ComputeDistance(position, point);
            if (tempDistance < distance)
            {
                distance = tempDistance;
                result = point;
            }
        }
        return result;
    }

    static float ComputeDistance(Vector2 point1, Vector2 point2)
    {
        var result = Mathf.Sqrt(Mathf.Pow((point2.x - point1.x), 2) + Mathf.Pow((point2.y - point1.y), 2));
        return result;
    }
}

public class GameScaneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
