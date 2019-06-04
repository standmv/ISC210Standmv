using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlJugador : MonoBehaviour
{
    public float velocidad;
    private Rigidbody jugadorRb;
    private int contador;

    private void Start()
    {
        jugadorRb = GetComponent<Rigidbody>();
        contador = 0;
        ActualizarTextoContador();
    }
    private void Update()
    {

    }

    private void FixedUpdate()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movimientoHorizontal, 0, movimientoVertical);
        jugadorRb.AddForce(movimiento * velocidad);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);
        if (other.gameObject.CompareTag("Comida"))
        {
            other.gameObject.SetActive(false);
            contador += 1;
            ActualizarTextoContador();
        }
    }

    void ActualizarTextoContador()
    {
        TextMesh textoContador = GameObject.Find("TextoContador").GetComponent<TextMesh>();
        textoContador.text = "Puntuacion: " + contador.ToString();
        return;
    }
}
