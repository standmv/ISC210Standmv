using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneBehaviour : MonoBehaviour
{
    public GameObject Ball;
    public GameObject LeftPlayer;
    public GameObject RightPlayer;
    public GlobalScript GlobalScript;
    bool _isLeftDeadZone;

    // Start is called before the first frame update
    void Start()
    {
        _isLeftDeadZone = gameObject.name == "LeftDeadZone";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name != "Ball")
            return;
        GlobalScript.IncrementScore(_isLeftDeadZone);
        Ball.transform.SetParent(_isLeftDeadZone ? RightPlayer.transform : LeftPlayer.transform);
        Ball.transform.localPosition = new Vector3(_isLeftDeadZone ? -2.5f : 2.5f, 0);
        Ball.SendMessage("UpdateBallState");
       
    }
}
