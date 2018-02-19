using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceType
{
    none = -1,
    bigJumpGap = 0,
    bigJumpLong = 1,
    bigJumpPiece = 2,
    smallJumpGap = 3,
    smallJumpLong = 4,
    smallJumpPiece = 5,
    smallUnderPiece = 6,
    underOver = 7
}

public class Piece : MonoBehaviour {

    public PieceType type;
    public int visualIndex;
}
