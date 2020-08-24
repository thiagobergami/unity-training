using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    private void Update()
    {
        //metodo unity para olhar
        transform.LookAt(transform.position + Camera.main.transform.forward);
        //camera no eixo z
        
    }
}
