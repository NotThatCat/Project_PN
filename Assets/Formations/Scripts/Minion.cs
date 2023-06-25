using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Minion
{
    public GameObject posHolder;
    public GameObject minion;

    public Minion (GameObject posHolder, GameObject minion)
    {
        this.posHolder = posHolder;
        this.minion = minion;
    }

    public Minion()
    {

    }
}