using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUI : PMonoBehaviour
{
    public virtual void NextSkill()
    {
        PlayerCtrl.instance.NextSkill(1);
    }

    public virtual void PreviousSkill()
    {
        PlayerCtrl.instance.NextSkill(-1);
    }
}
