using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistolAnim : MonoBehaviour
{
    public Transform hand;
    public bool updateRotation;
    private void Update() {
        transform.position=hand.position;
        if(updateRotation)
        {
        transform.localRotation=hand.localRotation;
        }
    }
}
