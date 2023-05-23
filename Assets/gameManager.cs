using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;


public class gameManager : MonoBehaviour
{
    public GameObject myo = null, myo2 = null ;
    public float explosionForce ;

    private TextMeshProUGUI fps;
    int fpsVal;
    bool isRThunderActive, isLThunderActive;
    float lastUpdatedFPS;
    public GameObject cube;
    GameObject lightningRStart, lightningREnd;
    GameObject lightningLStart, lightningLEnd;
    private Pose _lastPose;
    public GameObject thunderR, thunderL;

    // Start is called before the first frame update
    void Start()
    {

        fps = GameObject.Find("fps").GetComponent<TextMeshProUGUI>();
        lastUpdatedFPS = 0;
        //right thunder initialization
        lightningRStart = GameObject.Find("RLightningStart");
        lightningREnd = GameObject.Find("RLightningEnd");
        thunderR.SetActive(false);
        //left thunde rinitialization
        lightningLStart = GameObject.Find("LLightningStart");
        lightningLEnd = GameObject.Find("LLightningEnd");
        thunderL.SetActive(false);
        Pose _lastPose = Pose.Unknown;

}

// Update is called once per frame
void Update()
    {
        

            if (Time.realtimeSinceStartup - lastUpdatedFPS > 1)
        {
            fpsVal = (int)(1.0f / Time.deltaTime);
            fps.text = fpsVal.ToString();
            lastUpdatedFPS = Time.realtimeSinceStartup;
        }
        if (isRThunderActive) updateRThunderPosition() ;
        if (isLThunderActive) updateLThunderPosition() ;
    }
    public void firstSpellR()
    {
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();
        thalmicMyo.Vibrate(VibrationType.Long);
        ExtendUnlockAndNotifyUserAction(thalmicMyo);
        
        isRThunderActive = true;
        thunderR.SetActive(true);
    }
    public void firstSpellL()
    {
        ThalmicMyo thalmicMyo2 = myo2.GetComponent<ThalmicMyo>();
        thalmicMyo2.Vibrate(VibrationType.Long);
        ExtendUnlockAndNotifyUserAction(thalmicMyo2);

        isLThunderActive = true;
        thunderL.SetActive(true);
    }
    public void stopFirstSpellR()
    {
        isRThunderActive = false;
        thunderR.SetActive(false);
    }
    public void stopFirstSpellL()
    {
        isLThunderActive = false;
        thunderL.SetActive(false);
    }
    void updateRThunderPosition()
    {

            GameObject thehandR = GameObject.Find("OculusHand_R");
            Debug.Log("oui");
            Vector3 offset = -thehandR.transform.right*.1f; //make an offset for the starting point and the raycast so it doesn't hit the hand collider
            lightningRStart.transform.position = thehandR.transform.position+ offset;
            Ray ray = new Ray(thehandR.transform.position+offset, -thehandR.transform.right);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                // get the coords where the ray hit
                Vector3 hitPoint = hit.point;
            //checks if the collided object is destroyable, if so, activate the script destroyobject
                if(hit.collider.gameObject.tag == "Destroyable")
            {
                hit.collider.gameObject.GetComponent<destroyObject>().breakIt = true;
            }
                // displace the lightning end at the place of the collision
                lightningREnd.transform.position = hitPoint;
                ExplodeAtLightningsEnd(hit);

        }
        else
            {
                lightningREnd.transform.position = thehandR.transform.position + -thehandR.transform.right*50;
            }
    }

    void updateLThunderPosition()
    {

        GameObject thehandL = GameObject.Find("OculusHand_L");
        Vector3 offset = thehandL.transform.right * .1f; //make an offset for the starting point and the raycast so it doesn't hit the hand collider
        lightningLStart.transform.position = thehandL.transform.position + offset;
        Ray ray = new Ray(thehandL.transform.position + offset, thehandL.transform.right);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            // get the coords where the ray hit
            Vector3 hitPoint = hit.point;
            //checks if the collided object is destroyable, if so, activate the script destroyobject
            if (hit.collider.gameObject.tag == "Destroyable")
            {
                hit.collider.gameObject.GetComponent<destroyObject>().breakIt = true;
            }
            // displace the lightning end at the place of the collision
            lightningLEnd.transform.position = hitPoint;
            ExplodeAtLightningsEnd(hit);


        }
        else
        {
            lightningLEnd.transform.position = thehandL.transform.position + thehandL.transform.right * 50;
        }
    }
        void ExtendUnlockAndNotifyUserAction (ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard) {
            myo.Unlock (UnlockType.Timed);
        }

        myo.NotifyUserAction ();
    }
    void ExplodeAtLightningsEnd(RaycastHit hit)
    {
        Vector3 hitPoint = hit.point;
        Collider[] colliders = Physics.OverlapSphere(hit.transform.position, 1f);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, hit.transform.position, 8f);
            }
        }
    }
}
