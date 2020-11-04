using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    public int playderID;
    public float speed = 1;
    public float turnSpeed = 2;
    public Transform rotationPivot;

    Vector3 move;
    float turn;

    // Use this for initialization
    void Start () {
        move = Vector3.zero;
        turn = 0;
    }
	
	// Update is called once per frame
	void Update () {

        turn = Mathf.Lerp(turn, turnSpeed * Input.GetAxis("Horizontal_P" + playderID), 0.1f);
        transform.RotateAround(rotationPivot.position, Vector3.up, turn);
        move = Vector3.Lerp(move, Vector3.forward * speed * Input.GetAxis("Vertical_P" + playderID), Time.deltaTime * 1f);
        move.y -= 5;
        move.z = Mathf.Clamp01(move.z);
        GetComponent<CharacterController>().Move(transform.TransformVector(move));
    }
}
