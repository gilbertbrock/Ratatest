///////////////////////////////////////////////////////////////////////////////////////////////
// GravityWell.cs
// Created By: Anthony Lowder
//
// Description:
// Responsible for collecting and affecting physics objects. 
// While active checks for collision with rigibodies, collects them, pulls them toward world
// location, and checks for input to push away collected rigidbodies.
///////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;

public class GravityWell : MonoBehaviour {

    //////////////////////////////////////////////////
    // INTERNAL VARIABLES
    //////////////////////////////////////////////////

    // Contains the rigidbody the GravWell will affect
    private struct PhysObj
    {
        public Rigidbody obj;
        public bool inside;
        public float timer;
        public float initDrag;
        public bool initGrav;
    }

    private List<PhysObj> objects = new List<PhysObj>(); // Holds the collection of affected objects
    private bool push; // If the player wishes to push affected objects away
    private bool OnlyOneObj = true; // Is the GravWell only allowed to affect one object
    private int layerIndex = 27; // The index of the Gravnull layer

    //////////////////////////////////////////////////
    // EXTERNAL VARIABLES
    //////////////////////////////////////////////////

    [Header("Affected Objs")]
    [Tooltip("Will only affect objects with this \"Tag\". Leave blank to affect all rigidbodies.")]
    [SerializeField] string objTag = "Phys";

    [Header("Carry Parameters")]
    [Tooltip("The magnitude of the force pulling affected objects.")]
    [SerializeField] private float pullForce;
    [Tooltip("The magnitude of the force pushing affected objects away.")]
    [SerializeField] private float pushForce;
    [Tooltip("How quickly the affected objects will stop rotating.")]
    [SerializeField] private float angDrag;
    [Tooltip("Is multi object carrying allowed?")]
    [SerializeField] private bool multiCarry;

    [Header("Drop Parameters")]
    [Tooltip("How far away affected objects need to be before unaffected.")]
    [SerializeField] private float dropDist;
    [Tooltip("How long affected objects need to be away before unaffected.")]
    [SerializeField] private float dropTime;
    [Tooltip("Mask for raycast below player's feet to drop carried objects.")]
    [SerializeField] private LayerMask dropMask;
    [SerializeField] private Transform player;

    [Header("Input Commands")]
    [Tooltip("The name of the button to toggle multi object carrying.")]
    [SerializeField] private KeyCode multiButton = KeyCode.Mouse2;
    [Tooltip("The name of the button to invoke push.")]
    [SerializeField] private KeyCode pushButton = KeyCode.Mouse1;

    [Header("Debugging")]
    [Tooltip("Enable debug prints.")]
    [SerializeField] private bool printDebug;

    //////////////////////////////////////////////////
    // MONOBEHAVIOUR FUNCTIONS
    //////////////////////////////////////////////////

    private void OnDisable() { DropAllObjects(); }

    private void Update() { NullChecks(); UpdateInput(); }
    private void FixedUpdate() { if (objects.Count > 0) { UpdateTimers(); UpdateForce(); DropUnderneath(); } }

    //////////////////////////////////////////////////
    // MAIN UPDATES
    //////////////////////////////////////////////////

    private void NullChecks() {
        int i = 0;
        while (i < objects.Count) {
            if (objects[i].obj == null) { RemoveFromList(i); }
            else { i++; }
        }
    } // Removes affected objects that are destroyed

    private void UpdateInput() {
        if (Input.GetKeyDown(pushButton) && objects.Count > 0) { DebugLog("GravityWell :: Push Activated"); push = true; }
        else if (Input.GetKeyDown(multiButton) && multiCarry) { ToggleMultiCarry(); DebugLog("GravityWell :: Multi-Carry Toggled - " + !OnlyOneObj); }
    } // Player input check
    private void UpdateForce() {
        for (int i = 0; i < objects.Count; i++) { if (push) { PushObject(objects[i].obj); } else { PullObject(objects[i].obj); } }
        if (push) {
            for (int i = 0; i < objects.Count; i++) {
                objects[i].obj.angularDrag = objects[i].initDrag;
                objects[i].obj.useGravity = objects[i].initGrav;
            }
            objects.Clear(); push = false;
        }
    } // Applies the pull or push effect to affected objects
    private void UpdateTimers() {
        for (int i = 0; i < objects.Count; i++) {
            if (!objects[i].inside) {
                RunObjTimer(i);
                float dist = Vector3.Distance(objects[i].obj.transform.position, transform.position);
                if (objects[i].timer >= dropTime && dist >= dropDist) {
                    DebugLog("GravityWell :: " + objects[i].obj.name + " Dropped - Out of Range");
                    RemoveFromList(i);
                }
            }
        }
    } // Drops affected objects that are out of range for too long

    private void DropUnderneath() {
        RaycastHit hit;
        Vector3 direction = (player.position + new Vector3(0, -0.25f, 0)) - player.position;
        Ray ray = new Ray(player.position, direction);
        Debug.DrawRay(player.position, direction, Color.blue);

        if (Physics.Raycast(ray, out hit, 1f, dropMask)) {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null) {
                int index = CheckListFor(rb);
                if (index >= 0) { RemoveFromList(index); }
            }
        }
    } // Drops affected objects that are directly below the player

    //////////////////////////////////////////////////
    // AFFECTING COLLECTED OBJS
    //////////////////////////////////////////////////

    private void PullObject(Rigidbody rb) {
        Vector3 direction = transform.position - rb.transform.position;
        rb.velocity = (direction * pullForce);
        DebugLog("GravityWell :: " + rb.name + " Pulled");
    }
    private void PushObject(Rigidbody rb) {
        rb.velocity = Vector3.zero;
        Vector3 dirNorm = transform.parent.forward;
        Vector3 force = dirNorm * (pushForce * rb.mass);
        rb.AddForce(force, ForceMode.Impulse);
        DebugLog("GravityWell :: " + rb.name + " Pushed");
    }
    private void DropAllObjects() {
        for (int i = 0; i < objects.Count; i++) { RemoveFromList(i); }
        DebugLog("GravityWell :: All Objs Dropped");
    }

    private void ToggleMultiCarry() { if (OnlyOneObj) { OnlyOneObj = false; } else { OnlyOneObj = true; DropAllObjects(); } }

    //////////////////////////////////////////////////
    // AFFECTED OBJ TRACKING
    //////////////////////////////////////////////////

    private int CheckListFor(Rigidbody rb) {
        for (int i = 0; i < objects.Count; i++) { if (rb == objects[i].obj) { return i; } }
        return -1;
    }
    private void AddToList(Rigidbody rb) {
        PhysObj newObj = new PhysObj();
        newObj.obj = rb;
        newObj.inside = true;
        newObj.timer = 0;
        newObj.initDrag = rb.angularDrag;
        newObj.initGrav = rb.useGravity;
        rb.useGravity = false;
        rb.angularDrag = angDrag;
        SetCarryLayer(rb);
        objects.Add(newObj);
    }
    private void RemoveFromList(int index) {
        if (index >= 0 && index < objects.Count) {
            if (objects[index].obj != null) {
                objects[index].obj.useGravity = objects[index].initGrav;
                objects[index].obj.angularDrag = objects[index].initDrag;
            }
            objects.RemoveAt(index);
        }
    }

    private void StartObjTimer(int index) {
        PhysObj newObj = objects[index];
        newObj.inside = false;
        objects[index] = newObj;
    }
    private void RunObjTimer(int index) {
        PhysObj newObj = objects[index];
        newObj.timer += Time.deltaTime;
        objects[index] = newObj;
    }
    private void ResetObjTimer(int index) {
        PhysObj newObj = objects[index];
        newObj.inside = true;
        newObj.timer = 0;
        objects[index] = newObj;
    }

    private void SetCarryLayer(Rigidbody obj) {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        foreach (Transform child in children) { child.gameObject.layer = layerIndex; }
    }
    public int GetObjCount() { return objects.Count; }

    //////////////////////////////////////////////////
    // COLLISION EVENTS
    //////////////////////////////////////////////////

    private void OnTriggerEnter(Collider other) {
        if (other.tag == objTag || objTag == "") {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null) {
                int index = CheckListFor(rb);
                if (index >= 0) { ResetObjTimer(index); }
                else { if (OnlyOneObj && objects.Count <= 0) { AddToList(rb); } else if (!OnlyOneObj) { AddToList(rb); } }
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == objTag || objTag == "") {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null) { int index = CheckListFor(rb); if (index >= 0) { StartObjTimer(index); } }
        }
    }

    //////////////////////////////////////////////////
    // DEBUGGING
    //////////////////////////////////////////////////

    private void DebugLog(string message) { if (printDebug) { Debug.Log(message); } }

} // End of Class