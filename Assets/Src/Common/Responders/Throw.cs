using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

public class Throw : MonoBehaviour
{
    public Transform Ta, Tb; // transforms that mark the start and end (for debug)
    public float h = 2.5f; // desired parabola height
    public float distanceToTravel = 5;
    public float speedOfTravel = 3;
    public GameObject TargetingPrefab;
    public Transform DirectionalPrefab;
    private float flightDuration = 0f;
    private float elapsed_time = 0f;

    private IDirection TargetDirectionData;
    private Transform ObjectToThrow;
    private Vector3 mouse;
    private Vector2 a, b; // Vector positions for start and end
    private Vector2 destination, mouse2d, center;
    private Transform SpawnedRecticule;

    private float targetDistance, Vx, Vy = 0;

    private void Awake()
    {
        SpawnedRecticule = Instantiate(TargetingPrefab.transform, transform.position, Quaternion.identity);
        SpawnedRecticule.gameObject.SetActive(false);

        TargetDirectionData = DirectionalPrefab.GetComponent<IDirection>();
    }

    public void Update()
    {
        Vector2 targetDirection = TargetDirectionData.FacingDirection;
        center = new Vector2(transform.position.x, transform.position.y);
        destination = center + (targetDirection * distanceToTravel);

        Ray ray = Camera.main.ScreenPointToRay(destination);
        targetDistance = Vector2.Distance(ray.origin, ray.direction);

        Vx = destination.x;
        Vy = destination.y;

        a = transform.position;
        b = destination;

        SpawnedRecticule.gameObject.SetActive(true);
        SpawnedRecticule.transform.position = destination;
    }

    public void StartThrow(Component _objectToThrow)
    {
        ObjectToThrow = _objectToThrow.transform.root;
        flightDuration = targetDistance / destination.magnitude;
        elapsed_time = 0;

        SpawnedRecticule.gameObject.SetActive(false);
        StartCoroutine(SimulateThrow(elapsed_time, flightDuration, Vx, Vy, 0, a, b));
    }

    private IEnumerator SimulateThrow(float elapsedTime, float flightDuration, float Vx, float Vy, float gravity, Vector2 a, Vector2 b)
    {
        // Using this will hilariously throw the actual player if it's attached.
        //var prefab = transform.root;
        var dist = Vector2.Distance(a, b);
        var mag = (a - b).magnitude;
        var middle = (a + b) / 2;

        // Flight time is normalized to '1', so everything happens in between 0 and 1 in terms of travel time basically.
        var b_flightDuration = 1;

        while (elapsedTime < b_flightDuration)
        {
            ObjectToThrow.transform.position = SampleParabola(a, b, h, elapsedTime);

            // This allows for scaling, but it's hard to understand:
            // https://stackoverflow.com/questions/20309661/scale-sprite-up-and-down-to-give-illusion-of-a-jump
            // L = (cos(FOV/2) * Ws)/2
            // x'/x = y'/y = S

            elapsedTime += speedOfTravel * Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// Get position from a parabola defined by start and end, height, and time
    /// </summary>
    /// <param name='start'>
    /// The start point of the parabola
    /// </param>
    /// <param name='end'>
    /// The end point of the parabola
    /// </param>
    /// <param name='height'>
    /// The height of the parabola at its maximum
    /// </param>
    /// <param name='t'>
    /// Normalized time (0->1)
    /// </param>S
    private Vector2 SampleParabola(Vector2 start, Vector2 end, float height, float t)
    {
        // https://forum.unity.com/threads/generating-dynamic-parabola.211681/
        Vector2 travelDirection = end - start;
        Vector2 result = start + t * travelDirection;
        result.y += Mathf.Sin(t * Mathf.PI) * height;

        return result;
    }

    /*
    private void OnDrawGizmos()
    {
        //a = Ta.position; //Get vectors from the transforms
        //b = Tb.position;

        //Draw the height in the viewport, so i can make a better gif :]
        Handles.BeginGUI();
        GUI.skin.box.fontSize = 16;
        GUI.Box(new Rect(10, 10, 100, 25), h + "");
        Handles.EndGUI();

        //Draw the parabola by sample a few times
        Gizmos.color = Color.red;
        Gizmos.DrawLine(a, b);
        float count = 20;
        Vector2 lastP = a;
        for (float i = 0; i < count + 1; i++)
        {
            Vector2 p = SampleParabola(a, b, h, i / count);
            Gizmos.color = i % 2 == 0 ? Color.blue : Color.green;
            Gizmos.DrawLine(lastP, p);
            lastP = p;
        }
    }
    */
}