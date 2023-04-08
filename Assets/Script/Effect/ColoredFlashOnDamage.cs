using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ColoredFlashOnDamage : ColoredFlash
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMaterial();
    }

    protected virtual void LoadMaterial()
    {
        if (this.spriteRenderer != null) return;
        if (this.originalMaterial != null) return;
        this.spriteRenderer = transform.parent.parent.Find("Model")?.GetComponentInChildren<SpriteRenderer>();
        if (this.spriteRenderer == null) return;
        this.originalMaterial = this.spriteRenderer.sharedMaterial;
    }

}