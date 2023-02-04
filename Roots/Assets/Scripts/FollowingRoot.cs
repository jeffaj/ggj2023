using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;
using System.Linq;

[RequireComponent(typeof(LineRenderer))]
public class FollowingRoot : MonoBehaviour
{
    private static List<Vector3> VerticalDown = new List<Vector3>{
        new Vector3(0.5f, 1),
        new Vector3(0.5f, 0),
    };

    private static List<Vector3> HorizontalLeftRight = new List<Vector3>{
        new Vector3(0f, 0.5f),
        new Vector3(1, 0.5f),
    };

    private static List<Vector3> HorizontalRightLeft = new List<Vector3>{
        new Vector3(1f, 0.5f),
        new Vector3(0, 0.5f),
    };

    private static List<Vector3> TopToLeftElbow = new List<Vector3>{
        new Vector3(0.5f, 1),
        new Vector3(0.5f, 0.5f),
        new Vector3(0, 0.5f),
    };

    private static List<Vector3> TopToRightElbow = new List<Vector3>{
        new Vector3(0.5f, 1),
        new Vector3(0.5f, 0.5f),
        new Vector3(1, 0.5f),
    };

    private static List<Vector3> RightToBottomElbow = new List<Vector3>{
        new Vector3(1f, 0.5f),
        new Vector3(0.5f, 0.5f),
        new Vector3(0.5f, 0f),
    };

    private static List<Vector3> LeftToBottomElbow = new List<Vector3>{
        new Vector3(0f, 0.5f),
        new Vector3(0.5f, 0.5f),
        new Vector3(0.5f, 0f),
    };

    private LineRenderer _lineRenderer;

    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInput.ReturnPressed)
        {
            Append();
        }
    }

    void Append()
    {
        var linePoints = ConcatSegments(
            new Vector3(0, 3, 0),
            new List<List<Vector3>> {
                VerticalDown,
                TopToRightElbow,
                HorizontalLeftRight,
                LeftToBottomElbow,
                TopToLeftElbow,
                HorizontalRightLeft,
                RightToBottomElbow
            });

        _lineRenderer.positionCount = linePoints.Count;
        _lineRenderer.SetPositions(linePoints.ToArray());
    }

    List<Vector3> ConcatSegments(Vector3 start, List<List<Vector3>> segments)
    {
        List<Vector3> result = new List<Vector3>();

        Vector3 currOffset = start;

        foreach (var segment in segments)
        {
            for (int i = 0; i < segment.Count; i++)
            {
                var delta = segment[i] - segment.First();
                var next = currOffset + delta;

                // avoid dupes
                if (result.Count == 0 || next != result.Last())
                {
                    result.Add(currOffset + delta);
                }
            }

            currOffset = currOffset + segment.Last() - segment.First();
        }

        return result;
    }
}
