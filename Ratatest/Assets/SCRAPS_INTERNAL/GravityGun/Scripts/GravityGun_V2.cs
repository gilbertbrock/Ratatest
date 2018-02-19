using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun_V2 : MonoBehaviour {

    private enum GUN_STATE { INACTIVE, ACTIVE }
    private GUN_STATE currentState = GUN_STATE.INACTIVE;

    private enum INPUT_STYLE { HOLD, TOGGLE }
    [SerializeField] private INPUT_STYLE inputStyle;

    [SerializeField] private KeyCode shootButton = KeyCode.Mouse0;

    [SerializeField] private GameObject gravNode;
    private GravityWell gravWell;

    [SerializeField] private float gNodeSpeed;

    [SerializeField] private Transform player;

    [SerializeField] private LayerMask shootMask;
    [SerializeField] private LayerMask carryMask;
    [SerializeField] private Vector3 rayOffset;
    [SerializeField] private float rayDist;


    private void Start() { gravWell = gravNode.GetComponentInChildren<GravityWell>(); gravNode.SetActive(false); }
    private void Update() { UpdateInput(); Active(); }

    private void UpdateInput() {
        if (inputStyle == INPUT_STYLE.HOLD) {
            if (Input.GetKeyDown(shootButton)) { Activate(); }
            else if (Input.GetKeyUp(shootButton)) { Deactivate(); }
        }
        else {
            if (Input.GetKeyDown(shootButton)) {
                if (currentState == GUN_STATE.ACTIVE) { Deactivate(); }
                else { Activate(); }
            }
        }
    }

    private void Active() { if (currentState == GUN_STATE.ACTIVE) { MoveGravNode(ShootRay()); } }
    private void Activate() { MoveGravNode(ShootRay(), true); gravNode.SetActive(true); currentState = GUN_STATE.ACTIVE; }
    private void Deactivate() { gravNode.SetActive(false); currentState = GUN_STATE.INACTIVE; }

    private Vector3 ShootRay() {
        Vector3 origin = player.position + rayOffset;
        Vector3 direction = transform.position - origin;

        Debug.DrawRay(origin, direction, Color.green);

        RaycastHit hit;
        Ray ray = new Ray(origin, direction);

        if (gravWell.GetObjCount() > 0) { if (Physics.Raycast(ray, out hit, rayDist, carryMask)) { return hit.point; } }
        else { if (Physics.Raycast(ray, out hit, rayDist, shootMask)) { return hit.point; } }
        return transform.position;
    }

    private void MoveGravNode(Vector3 pos, bool snap = false) {
        gravNode.transform.rotation = transform.rotation;
        if (snap) { gravNode.transform.position = pos; }
        else { gravNode.transform.position = Vector3.Lerp(gravNode.transform.position, pos, gNodeSpeed * Time.deltaTime); }
    }

} // End of Class