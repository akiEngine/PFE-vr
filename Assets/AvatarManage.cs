#region Description

// The script performs a direct translation of the skeleton using only the position data of the joints.
// Objects in the skeleton will be created when the scene starts.

#endregion


using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace NuitrackSDK.Tutorials.FirstProject
{
    public class AvatarManage : MonoBehaviour
    {
        string message = "";

        public nuitrack.JointType[] typeJoint;
        public GameObject[] CreatedJoint;
        public GameObject PrefabJoint;
        List<UnityEngine.Vector3> averageArray;
        int iterator; // to count how many vectors are added then make the average
        void Start()
        {
            // CreatedJoint = new GameObject[typeJoint.Length];
            for (int q = 0; q < typeJoint.Length; q++)
            {
                //   CreatedJoint[q] = Instantiate(PrefabJoint);
                // CreatedJoint[q].transform.SetParent(transform);
                averageArray.Add(new UnityEngine.Vector3(0f,0f,0f));
            }
            //message = "Skeleton created";*/
        }
        void Update()
        {
            if (NuitrackManager.Users.Current != null && NuitrackManager.Users.Current.Skeleton != null)
            {
                message = "Skeleton found";

                for (int q = 0; q < typeJoint.Length; q++)
                {
                    UserData.SkeletonData.Joint joint = NuitrackManager.Users.Current.Skeleton.GetJoint(typeJoint[q]);
                    
                    averageArray[q]+=(joint.Position);
                    iterator++;
                    if (iterator == 3)
                    {
                        CreatedJoint[q].transform.localPosition = averageArray[q]/iterator;
                        iterator=0;


                    }
                }
            }
            else
            {
                message = "Skeleton not found";
            }
        }

        // Display the message on the screen
        void OnGUI()
        {
            GUI.color = Color.red;
            GUI.skin.label.fontSize = 50;
            GUILayout.Label(message);
        }
    }
}