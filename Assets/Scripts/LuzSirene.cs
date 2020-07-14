using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzSirene : MonoBehaviour {
    private float startValue = 50;
    public bool blink = false;
    public float speed = 1; //Velocidade de ligar e desligar luz    
    private Light myLight;
    public int totalSeconds; //total de Segundos que a luz vai piscar    
    public int maxIntensity = 50;
    // Use this for initialization

    void Start () {
       
        myLight = GetComponent<Light>();
        myLight.intensity = maxIntensity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float waitTime = totalSeconds / 2;

        if (blink == true)
        {
            int value = Random.Range(0, totalSeconds);
            if (value == 0) {
                myLight.intensity = 0;
            }
            else
            {
                myLight.intensity = maxIntensity;
            } 
        }
        else
        {
            myLight.intensity = Mathf.PingPong(Time.time * speed, maxIntensity);
        }
    }    

    /*public IEnumerator flashNow() {
        float waitTime = totalSeconds / 2;
        // Get half of the seconds (One half to get brighter and one to get darker)
        while (myLight.intensity < maxIntensity)
        {
            myLight.intensity = 0;        // Increase intensity
            yield return null;
        }
        while (myLight.intensity > 0)
        {
            myLight.intensity -= Time.deltaTime / waitTime;        //Decrease intensity
            yield return null;
        }
        yield return null;

    }*/
}
