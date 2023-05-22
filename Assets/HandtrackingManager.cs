using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandtrackingManager : MonoBehaviour
{
    public GameObject[] occulusHand;
    public GameObject[] avatarHand;
    private Transform[] transformsOcculus = null;
    private Transform[] transformsAvatar = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < occulusHand.Length; i++)
        {
            avatarHand[i].transform.position = occulusHand[i].transform.position;
            avatarHand[i].transform.rotation = occulusHand[i].transform.rotation;
            avatarHand[i].transform.Rotate(new Vector3(-90f, 0f, +90));
        }
    }
}