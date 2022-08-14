using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsManager : PMonoBehaviour
{
    public static InputsManager instance;
    [SerializeField] protected Vector2 mousePos;
    [SerializeField] protected Vector3 inputMousePos;

    protected override void Start()
    {
        if (instance != this && instance != null) Debug.Log("Only allow one InputsManager");
        InputsManager.instance = this;
    }

    protected override void Update()
    {
        this.LoadMousePos();
        this.inputMousePos = Input.mousePosition;

        if (Input.GetKeyDown(KeyCode.Alpha1)) PlayerCtrl.instance.ChangeSkill(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) PlayerCtrl.instance.ChangeSkill(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) PlayerCtrl.instance.ChangeSkill(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) PlayerCtrl.instance.ChangeSkill(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) PlayerCtrl.instance.ChangeSkill(4);
        if (Input.GetKey(KeyCode.Mouse0)) PlayerCtrl.instance.Attack();
        if (Input.GetKeyDown(KeyCode.A)) PlayerCtrl.instance.playerModelCtrl.level.Down();
        if (Input.GetKeyDown(KeyCode.D)) PlayerCtrl.instance.playerModelCtrl.level.Up();
    }

    protected virtual void LoadMousePos()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.mousePos.x = worldPosition.x;
        this.mousePos.y = worldPosition.y;
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
