using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikePointManager : PMonoBehaviour
{
    public static StrikePointManager instance;
    [SerializeField] public List<Transform> strikePointTemplate;

    protected override void Start()
    {
        LoadComponents();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTemplate();
        // this.HideAll();
    }

    protected override void Awake()
    {
        if (StrikePointManager.instance != this && StrikePointManager.instance != null) Debug.Log("Only allow one StrikePointManager");
        StrikePointManager.instance = this;
    }


    protected virtual void LoadTemplate()
    {
        foreach (Transform template in transform)
        {
            this.strikePointTemplate.Add(template);
        }
    }

    protected virtual void HideAll()
    {
        foreach (Transform template in this.strikePointTemplate)
        {
            template.gameObject.SetActive(false);
        }
    }

    public virtual Transform GetStrikePointTemplate(string templateName, Transform templateParent, string returnName = "StrikePoints")
    {
        Transform bulletPrefab = this.GetStrikePointTemplateByName(templateName);
        Transform newStrikePoint = Instantiate(bulletPrefab, templateParent.position, templateParent.transform.rotation, templateParent);
        newStrikePoint.name = returnName;
        return newStrikePoint;
    }

    public virtual Transform GetStrikePointTemplateByName(string templateName)
    {
        foreach (Transform template in this.strikePointTemplate)
        {
            if (template.name == templateName) return template;
        }
        return null;
    }

}
