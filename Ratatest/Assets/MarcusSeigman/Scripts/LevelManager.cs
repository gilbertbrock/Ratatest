using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance { set; get; }
    private const bool SHOW_COLLIDER = true;

    private const float DISTANCE_BEFORE_SPAWN = 100.0f;
    private const int INITIAL_SEGMENTS = 10;
    private const int MAX_SEGMENTS = 10;
    private Transform cameraContainer;
    private int amountOfActiveSegments;
    private int continiousSegments;
    private int currentSpawnZ;
    private int currentLevel;
    private int y1, y2, y3;

    //list of pieces
    public List<Piece> bigJumpGaps = new List<Piece>();
    public List<Piece> bigJumpLongs = new List<Piece>();
    public List<Piece> bigJumpPieces = new List<Piece>();
    public List<Piece> smallJumpGaps = new List<Piece>();
    public List<Piece> smallJumpLongs = new List<Piece>();
    public List<Piece> smallJumpPieces = new List<Piece>();
    public List<Piece> smallUnderPieces = new List<Piece>();
    public List<Piece> underOvers = new List<Piece>();
    [HideInInspector]
    public List<Piece> pieces = new List<Piece>(); // all the pieces.

    //list of segments
    public List<Segment> availableSegments = new List<Segment>();
    public List<Segment> availableTransitions = new List<Segment>();
    [HideInInspector]
    public List<Segment> segments = new List<Segment>();

    //Gameplay
    private bool isMoving = false;

    public void Awake()
    {
        Instance = this;
        cameraContainer = Camera.main.transform;
        currentSpawnZ = 0;
        currentLevel = 0;


    }

    private void Start()
    {
        for (int i = 0; i < INITIAL_SEGMENTS; i++)
        {
            GenerateSegment();
        }
    }

    private void Update()
    {
        if(currentSpawnZ - cameraContainer.position.z < DISTANCE_BEFORE_SPAWN)
        {
            GenerateSegment();

            if(amountOfActiveSegments >= MAX_SEGMENTS)
            {
                segments[amountOfActiveSegments - 1].DeSpawn();
                amountOfActiveSegments--;
            }
        }
    }
    private void GenerateSegment()
    {
        SpawnSegment();

        if (Random.Range(0f, 1f) < (continiousSegments * .25f))
        {
            continiousSegments = 0;
            SpawnTransition();

        }
        else
        {
            continiousSegments++;
        }
    }

    private void SpawnSegment()
    {
        List<Segment> possibleSeg = availableSegments.FindAll(x => x.beginy1 == y1 || x.beginy2 == y2 || x.beginy3 == y3);
        int id = Random.Range(0, possibleSeg.Count);

        Segment s = GetSegment(id, false);

        y1 = s.endy1;
        y2 = s.endy2;
        y3 = s.endy3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s.length;
        amountOfActiveSegments++;
        s.Spawn();

    }

    private void SpawnTransition()
    {
        List<Segment> possibleTransition = availableTransitions.FindAll(x => x.beginy1 == y1 || x.beginy2 == y2 || x.beginy3 == y3);
        int id = Random.Range(0, possibleTransition.Count);

        Segment s = GetSegment(id, true);

        y1 = s.endy1;
        y2 = s.endy2;
        y3 = s.endy3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s.length;
        amountOfActiveSegments++;
        s.Spawn();
    }

    public Segment GetSegment(int id, bool transition )
    {
        Segment s = null;
        s = segments.Find(x => x.SegID == id && x.transition == transition && !x.gameObject.activeSelf);

        if(s == null)
        {
            GameObject go = Instantiate((transition) ? availableTransitions[id].gameObject : availableSegments[id].gameObject) as GameObject;
            s = go.GetComponent<Segment>();

            s.SegID = id;
            s.transition = transition;

            segments.Insert(0, s);
        }
        else
        {
            segments.Remove(s);
            segments.Insert(0, s);
        }
        return s;
    }

    public Piece GetPiece(PieceType pt)
    {
        Piece p = pieces.Find(x => x.type == pt && !x.gameObject.activeSelf);

        if(p == null)
        {
            GameObject go = null;
            if (pt == PieceType.bigJumpGap)
                go = bigJumpGaps[0].gameObject;
            else if (pt == PieceType.bigJumpLong)
                go = bigJumpLongs[0].gameObject;
            else if (pt == PieceType.bigJumpPiece)
                go = bigJumpPieces[0].gameObject;
            else if (pt == PieceType.smallJumpGap)
                go = smallJumpGaps[0].gameObject;
            else if (pt == PieceType.smallJumpLong)
                go = smallJumpLongs[0].gameObject;
            else if (pt == PieceType.smallJumpPiece)
                go = smallJumpPieces[0].gameObject;
            else if (pt == PieceType.smallUnderPiece)
                go = smallUnderPieces[0].gameObject;
            else if (pt == PieceType.underOver)
                go = underOvers[0].gameObject;

            go = Instantiate(go);
            p = go.GetComponent<Piece>();
            pieces.Add(p);

        }
        return p;
    }
}
