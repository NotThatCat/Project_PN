using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] public Vector3 pos;

    private void Awake()
    {
        this.pos = this.transform.position;
    }

    private void Update()
    {
        Debug.Log("Position: " + this.transform.position + " And Pos: " + this.pos);
    }


    //protected override void Reset()
    //{
    //    base.Reset();
    //    Debug.Log(this.transform.localRotation);
    //    Debug.Log(this.transform.rotation);
    //}



}
