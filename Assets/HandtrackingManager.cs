using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandtrackingManager : MonoBehaviour
{
    public GameObject[] occulusHand;
    public GameObject[] avatarHand;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < occulusHand.Length/2; i++)
        {
            avatarHand[i].transform.rotation = occulusHand[i].transform.rotation;
            avatarHand[i].transform.Rotate(new Vector3(-90f, 0f, +90));
          //  Debug.Log(i);
        }
        for (int i = occulusHand.Length/2 ; i < occulusHand.Length; i++)
        {
            avatarHand[i].transform.rotation = occulusHand[i].transform.rotation;
            avatarHand[i].transform.Rotate(new Vector3(0f, 0f, -90));
            //Debug.Log(avatarHand[i].name);
        }
    }
}