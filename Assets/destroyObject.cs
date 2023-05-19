using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyObject : MonoBehaviour
{
    public GameObject destroyed_version;
    public bool breakIt;
    // Start is called before the first frame update
    void Start()
    {
        breakIt = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (breakIt)
        {
            GameObject newObject = Instantiate(destroyed_version, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }


}
