using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : PMonoBehaviour
{
    public virtual void NextSkill()
    {
        Debug.Log("PlayerCtrl.instance.NextSkill(1);");
        // PlayerCtrl.instance.NextSkill(1);
    }

    public virtual void PreviousSkill()
    {
        Debug.Log("PlayerCtrl.instance.NextSkill(-1);");
        // PlayerCtrl.instance.NextSkill(-1);
    }
}
