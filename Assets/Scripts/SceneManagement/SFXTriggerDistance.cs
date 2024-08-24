using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXTriggerDistance : MonoBehaviour
{
    [HideInInspector] public Transform target;
    [SerializeField] float inRange;
    [SerializeField] float setVolume;
    [SerializeField] AudioSource SFX;

    void Start(){
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate(){
        CheckDistance();
    }

    void CheckDistance(){
        if ((Vector3.Distance(target.position, transform.position) <= inRange)){
            SFX.volume = setVolume;
        }
        else {
            SFX.volume = 0;
        }
    }
}
