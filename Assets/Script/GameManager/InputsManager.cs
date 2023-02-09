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
        if (Input.GetKeyDown(KeyCode.Alpha1)) PlayerCtrl.instance.ChangeSkill(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) PlayerCtrl.instance.ChangeSkill(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) PlayerCtrl.instance.ChangeSkill(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) PlayerCtrl.instance.ChangeSkill(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) PlayerCtrl.instance.ChangeSkill(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) PlayerCtrl.instance.ChangeSkill(5);
        if (Input.GetKeyDown(KeyCode.Alpha7)) PlayerCtrl.instance.ChangeSkill(6);
        if (Input.GetKeyDown(KeyCode.Alpha8)) PlayerCtrl.instance.ChangeSkill(7);
        if (Input.GetKeyDown(KeyCode.Alpha9)) PlayerCtrl.instance.ChangeSkill(8);
        if (Input.GetKeyDown(KeyCode.Alpha0)) PlayerCtrl.instance.ChangeSkill(9);
        if (Input.GetKeyDown(KeyCode.Q)) PlayerCtrl.instance.NextSkill(-1);
        if (Input.GetKeyDown(KeyCode.E)) PlayerCtrl.instance.NextSkill(1);
        if (Input.GetKeyDown(KeyCode.A)) PlayerCtrl.instance.level.Down();
        if (Input.GetKeyDown(KeyCode.D)) PlayerCtrl.instance.level.Up();
        if (Input.GetKey(KeyCode.Mouse0)) PlayerCtrl.instance.Attack();
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
