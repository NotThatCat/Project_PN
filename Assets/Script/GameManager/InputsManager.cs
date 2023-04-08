using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsManager : PMonoBehaviour
{
    public static InputsManager instance;
    [SerializeField] protected Vector2 mousePos;
    [SerializeField] protected Vector3 inputMousePos;
    [SerializeField] protected bool allowInput = true;
    [SerializeField] protected bool inGameInput = true;

    protected override void Start()
    {
        if (instance != this && instance != null) Debug.Log("Only allow one InputsManager");
        InputsManager.instance = this;
    }

    protected override void Update()
    {
        if (!inGameInput) return;
        if (this.inGameInput)
        {
            this.InputPlayerMoving();
            this.InputPlayerSkill();
        }
    }

    protected virtual void InputPlayerMoving()
    {
        this.LoadMousePos();
        this.inputMousePos = Input.mousePosition;
    }

    protected virtual void InputPlayerSkill()
    {
        if (Input.GetKeyDown(KeyCode.Space)) PlayerCtrl.instance.SpecialAttack("TestSkill");
        if (Input.GetKeyDown(KeyCode.A)) PlayerCtrl.instance.level.Down();
        if (Input.GetKeyDown(KeyCode.D)) PlayerCtrl.instance.level.Up();
    }

    public virtual void DisableInGameInput()
    {
        this.inGameInput = false;
    }

    protected virtual void LoadMousePos()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.mousePos.x = worldPosition.x;
        this.mousePos.y = worldPosition.y + 0.4f;
    }

    public Vector2 GetMousePos()
    {
        return this.mousePos;
    }
    public Vector2 GetInputMousePos()
    {
        return this.inputMousePos;
    }
}
