using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : PMonoBehaviour
{
    protected override void Reset()
    {
        base.Reset();
        Debug.Log(this.transform.localRotation);
        Debug.Log(this.transform.rotation);
    }
}
