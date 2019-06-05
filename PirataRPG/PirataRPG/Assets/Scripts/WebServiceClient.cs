using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class WebServiceClient : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(SendRequest("https://my.api.mockaroo.com/my_saved_schema.json?key=6b179840"));
        }
    }

    IEnumerator SendRequest(string url)
    {
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
        yield return unityWebRequest.SendWebRequest();

        Debug.Log(unityWebRequest.downloadHandler.text);
    }
}
