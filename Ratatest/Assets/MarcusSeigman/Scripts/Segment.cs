using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour {

	public int SegID { set; get; }
    public bool transition;

    public int length;

    public int beginy1, beginy2, beginy3;
    public int endy1, endy2, endy3;

    private Piece[] pieces;

    private void Awake()
    {
        pieces = gameObject.GetComponentsInChildren<Piece>();
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
    }

    public void DeSpawn()
    {
        gameObject.SetActive(false);
    }
}
