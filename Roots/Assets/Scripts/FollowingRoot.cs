using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;
using System.Linq;
using System;

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

    enum Cardinal
    {
        Left,
        Right,
        Top,
        Bottom,
    }

    static Dictionary<Cardinal, Dictionary<Cardinal, List<Vector3>>> EnterToExitSegments = new Dictionary<Cardinal, Dictionary<Cardinal, List<Vector3>>>();

    static FollowingRoot()
    {
        EnterToExitSegments[Cardinal.Left] = new Dictionary<Cardinal, List<Vector3>>();
        EnterToExitSegments[Cardinal.Left][Cardinal.Right] = HorizontalLeftRight;
        EnterToExitSegments[Cardinal.Left][Cardinal.Bottom] = LeftToBottomElbow;

        EnterToExitSegments[Cardinal.Right] = new Dictionary<Cardinal, List<Vector3>>();
        EnterToExitSegments[Cardinal.Right][Cardinal.Left] = HorizontalRightLeft;
        EnterToExitSegments[Cardinal.Right][Cardinal.Bottom] = RightToBottomElbow;

        EnterToExitSegments[Cardinal.Top] = new Dictionary<Cardinal, List<Vector3>>();
        EnterToExitSegments[Cardinal.Top][Cardinal.Left] = TopToLeftElbow;
        EnterToExitSegments[Cardinal.Top][Cardinal.Bottom] = VerticalDown;
        EnterToExitSegments[Cardinal.Top][Cardinal.Right] = TopToRightElbow;
    }

    private LineRenderer _lineRenderer;

    private GameSettings _gameSettings;

    private List<Vector2Int> _committedPath;

    private List<List<Vector3>> _cachedSegments;

    private float _gameStartTime;
    private float _lastPathCalculationTime = 0;
    private float _runRate = 0.2f;

    public Vector2Int MaxReachedGridPosition { get; private set; }

    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _gameSettings = FindObjectOfType<GameSettings>();
    }

    void Start()
    {
        _gameStartTime = Time.time;
    }

    void Update()
    {
        if (Time.time - _gameStartTime < _gameSettings.RootStartDelaySeconds)
        {
            return;
        }

        var startGridPosition = Game.LevelGrid.PlayerStartGridPosition + new Vector2Int(0, 1);

        float travelTime = Time.time - _gameStartTime - _gameSettings.RootStartDelaySeconds;
        float blocksToTravel = travelTime * _gameSettings.RootSpeedBlocksPerSecond;

        if (Time.time - _runRate > _lastPathCalculationTime || _cachedSegments == null)
        {
            _lastPathCalculationTime = Time.time;

            var path = FindPathToPlayer(startGridPosition);
            var committedBlocks = Math.Min(Mathf.CeilToInt(blocksToTravel) + 1, path.Count);
            _committedPath = path.GetRange(0, committedBlocks);

            var lastTouchingIdx = Mathf.FloorToInt(blocksToTravel) + 1;
            if (blocksToTravel - Mathf.Floor(blocksToTravel) > 0.8)
            {
                lastTouchingIdx++;
            }

            var lastMax = MaxReachedGridPosition;
            MaxReachedGridPosition = path[Math.Min(lastTouchingIdx, path.Count - 1)];

            if (lastMax != MaxReachedGridPosition)
            {
                Debug.Log($"updating max reached: {MaxReachedGridPosition}");
            }

            _cachedSegments = GenerateSegments(path);
        }

        var start = Game.LevelGrid.GetLocalPosition(startGridPosition) - new Vector3(0, 0.5f, 0);
        var linePoints = ConcatSegments(start, 1, _cachedSegments, blocksToTravel);

        _lineRenderer.positionCount = linePoints.Count;
        _lineRenderer.SetPositions(linePoints.ToArray());
    }

    void Append()
    {
    }

    public class Point : IComparable<Point>
    {
        public readonly float DistToPlayer;
        public readonly Vector2Int Position;

        public Point(float dist, Vector2Int pos)
        {
            DistToPlayer = dist;
            Position = pos;
        }

        public int CompareTo(Point obj)
        {
            return DistToPlayer.CompareTo(obj.DistToPlayer);
        }
    }

    private static List<Vector2Int> Deltas = new List<Vector2Int>{
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
    };

    // A*
    // TODO: bug with termination when going through an artifact square [maybe not anymore?]
    private List<Vector2Int> FindPathToPlayer(Vector2Int gridStartPos)
    {
        int steps = 0;

        List<Vector2Int> Path = new List<Vector2Int>();

        PriorityQueue<Point> ToExplore = new PriorityQueue<Point>();
        // HashSet<Vector2Int> Seen = new HashSet<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> ReachedFrom = new Dictionary<Vector2Int, Vector2Int>();

        // init with committed path
        if (_committedPath == null)
        {
            ToExplore.Enqueue(MakePoint(gridStartPos));
        }
        else
        {
            for (int i = 0; i < _committedPath.Count; i++)
            {
                // Seen.Add(_committedPath[i]);

                if (i == 0)
                {
                    continue;
                }

                ReachedFrom[_committedPath[i]] = _committedPath[i - 1];
            }

            ToExplore.Enqueue(MakePoint(_committedPath.Last()));
        }

        while (ToExplore.Count() > 0)
        {
            // just in case we are bugging out
            steps++;
            if (steps == 100)
            {
                Debug.Log("terminating too many steps");
                return Path;
            }

            Point next = ToExplore.Dequeue();

            // Debug.Log($"Exploring {next.Position}");

            if (next.Position == Game.Player.GridPosition)
            {
                // Debug.Log("Found End");
                break;
            }

            foreach (var delta in Deltas)
            {
                var newPoint = next.Position + delta;
                if (Game.LevelGrid.IsValidPosition(newPoint) && Game.LevelGrid.GetTile(newPoint) == null)
                {
                    if (!ReachedFrom.ContainsKey(newPoint))
                    {
                        ReachedFrom[newPoint] = next.Position;
                        ToExplore.Enqueue(MakePoint(newPoint));
                    }
                }
            }
        }

        // reconstruct
        Vector2Int curr = Game.Player.GridPosition;
        while (curr != gridStartPos)
        {
            Path.Add(curr);

            if (!ReachedFrom.ContainsKey(curr))
            {
                Debug.Log($"missing reached from for {curr}");
            }

            curr = ReachedFrom[curr];
        }

        Path.Add(gridStartPos);

        Path.Reverse();

        return Path;
    }

    float Distance(Vector2Int first, Vector2Int second)
    {
        var firstVec = new Vector2(first.x, first.y);
        var secondVec = new Vector2(second.x, second.y);

        return Vector2.Distance(firstVec, secondVec);
    }

    Point MakePoint(Vector2Int pos)
    {
        return new Point(Distance(pos, Game.Player.GridPosition), pos);
    }

    List<List<Vector3>> GenerateSegments(List<Vector2Int> Path)
    {
        var result = new List<List<Vector3>>();

        for (int i = 1; i < Path.Count - 1; i++)
        {
            Vector2Int prev = Path[i - 1];
            Vector2Int next = Path[i + 1];

            var enter = DetermineCardinal(prev, Path[i]);
            var exit = DetermineCardinal(next, Path[i]);

            // Debug.Log($"Going {enter} to {exit}");

            var piece = EnterToExitSegments[enter][exit];
            result.Add(piece);
        }

        return result;
    }

    Cardinal DetermineCardinal(Vector2Int from, Vector2Int to)
    {
        var delta = to - from;

        // Debug.Log(delta);

        if (delta.x > 0)
        {
            return Cardinal.Left;
        }
        else if (delta.x < 0)
        {
            return Cardinal.Right;
        }
        else if (delta.y > 0)
        {
            return Cardinal.Bottom;
        }
        else
        {
            return Cardinal.Top;
        }
    }

    List<Vector3> ConcatSegments(Vector3 start, float scale, List<List<Vector3>> segments, float blocksToTravel)
    {
        List<Vector3> result = new List<Vector3>();

        Vector3 currOffset = start;

        int segmentCount = 0;
        foreach (var segment in segments)
        {
            segmentCount++;

            bool isLastSegment = segmentCount >= blocksToTravel;

            var segmentToAdd = segment;

            if (isLastSegment)
            {
                // last one
                var lastBlockInterpolateAmt = blocksToTravel - Mathf.Floor(blocksToTravel);
                segmentToAdd = InterpolateSegment(segment, lastBlockInterpolateAmt);
            }

            for (int i = 0; i < segmentToAdd.Count; i++)
            {
                var delta = segmentToAdd[i] - segmentToAdd.First();
                var next = currOffset + delta * scale;

                // avoid dupes
                if (result.Count == 0 || next != result.Last())
                {
                    result.Add(next);
                }
            }

            currOffset = currOffset + (segment.Last() - segment.First()) * scale;

            if (isLastSegment)
            {
                break;
            }
        }

        return result;
    }

    List<Vector3> InterpolateSegment(List<Vector3> segment, float percent)
    {
        float totalDist = 0;
        for (int i = 1; i < segment.Count; i++)
        {
            totalDist += Vector2.Distance(segment[i], segment[i - 1]);
        }

        float distToTravel = totalDist * percent;

        List<Vector3> newSegs = new List<Vector3>
        {
            segment[0]
        };

        float distTraveled = 0;
        for (int i = 1; i < segment.Count; i++)
        {
            float thisDist = Vector2.Distance(segment[i], segment[i - 1]);
            if (distTraveled + thisDist > distToTravel)
            {
                // last one
                float remProg = (distToTravel - distTraveled) / thisDist;
                newSegs.Add(Vector2.Lerp(segment[i - 1], segment[i], remProg));

                break;
            }

            newSegs.Add(segment[i]);
            distTraveled += thisDist;
        }

        return newSegs;
    }
}
