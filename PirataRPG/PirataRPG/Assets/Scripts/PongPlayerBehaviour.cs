using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPlayerBehaviour : MonoBehaviour
{
    bool _isLeftPlayer;
    private float _speed = 10f;
    Vector3 _deltaPos;
    const float UPPERBOUND = 3.3f, LOWERBOUND = -3.3f;
    static bool _onePlayer;
    GameObject _ball;

    private void Awake()
    {
        _isLeftPlayer = gameObject.name == "LeftPlayer";
        _ball = GameObject.Find("Ball");
    }
    // Start is called before the first frame update
    void Start()
    {
        //funcionalidad de prueba para jugar con la computadora
        _onePlayer = true;   
    }

    // Update is called once per frame
    void Update()
    {
        float desde, hasta;
        //BUSCANDO LOS LIMITES PARA PASARLOS AL LERP
        desde = gameObject.transform.position.y < _ball.transform.position.y ? gameObject.transform.position.y : _ball.transform.position.y;

        hasta = gameObject.transform.position.y > _ball.transform.position.y ? gameObject.transform.position.y : _ball.transform.position.y;

        if (_onePlayer && !_isLeftPlayer)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(Mathf.Lerp(desde, hasta, 0.95f),LOWERBOUND, UPPERBOUND), transform.position.z);
            return;
        }
        _deltaPos = new Vector3(0f, (_isLeftPlayer ? Input.GetAxis("LeftPlayer") : Input.GetAxis("RightPlayer")) * _speed * Time.deltaTime);
        transform.Translate(_deltaPos);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, LOWERBOUND, UPPERBOUND), transform.position.z);   
    }
}
