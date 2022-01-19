using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleServer : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        NetworkManagerWorld.singleton.StartServer();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
