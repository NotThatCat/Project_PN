using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    [SerializeField] public ITEM_TYPE type;
    [SerializeField] public SkillSO skillData;

#if UNITY_EDITOR
    protected virtual void Reset()
    {
        string assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
        string displayName = Path.GetFileNameWithoutExtension(assetPath);
        string resPath = "Skill/Player/" + displayName;
        this.skillData = Resources.Load<SkillSO>(resPath);

    }
#endif

}