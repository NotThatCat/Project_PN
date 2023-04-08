using System.Collections;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;

public class ColoredFlash : PMonoBehaviour
{
    [SerializeField] protected Material flashMaterial;
    [SerializeField] protected float duration = 0.15f;
    [SerializeField] protected Color flashColor = Color.white;

    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Material originalMaterial;

    [SerializeField] protected bool isFlashing = false;

    protected Coroutine flashCoroutine;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadFlashMaterial();
    }

    protected virtual void LoadFlashMaterial()
    {
        if (this.flashMaterial != null) return;
        Material[] materials = Resources.LoadAll<Material>("Material/");

        foreach(Material mal in materials)
        {
            if(mal.name != "FlashMaterial") continue;
            this.flashMaterial = new Material(mal);
        }

        if (this.flashMaterial == null) Debug.Log("FlashMaterial not found");
    }

    public virtual void Flash()
    {
        this.Flash(flashColor);
    }

    public virtual void Flash(Color color)
    {
        if (this.isFlashing) return;
        this.isFlashing = true;
        flashCoroutine = StartCoroutine(FlashRoutine(color));
    }

    protected virtual IEnumerator FlashRoutine(Color color)
    {
        this.spriteRenderer.material = this.flashMaterial;
        this.flashMaterial.color = color;

        yield return new WaitForSeconds(this.duration);

        this.spriteRenderer.material = this.originalMaterial;
        this.isFlashing = false;
    }

    public override void ResetValue()
    {
        this.spriteRenderer.material = this.originalMaterial;
        this.isFlashing = false;
    }
}