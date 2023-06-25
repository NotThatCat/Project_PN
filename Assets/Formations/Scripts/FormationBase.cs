using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public abstract class FormationBase : MonoBehaviour
{
    [SerializeField] protected FORMATION_DIRECTION _direction = FORMATION_DIRECTION.Y;
    [SerializeField][Range(0, 1)] protected float _noise = 0;
    [SerializeField] protected float Spread = 1;
    public abstract IEnumerable<Vector3> EvaluatePoints();

    public Vector2 GetNoise(Vector2 pos)
    {
        float noise = Mathf.PerlinNoise(pos.x * _noise, pos.y * _noise);
        return new Vector2(noise, noise);
    }

    public Vector3 ToPosition(Vector2 pos)
    {
        switch (_direction)
        {
            case FORMATION_DIRECTION.Y:
                return new Vector3(pos.x, 0, pos.y);
            case FORMATION_DIRECTION.X:
                return new Vector3(0, pos.x, pos.y);
            case FORMATION_DIRECTION.Z:
                return new Vector3(pos.x, pos.y, 0);
            default:
                return new Vector3(pos.x, 0, pos.y);
        }
    }
}