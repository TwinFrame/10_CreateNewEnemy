using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelMoveState :State
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _wheel;

    private void Update()
    {
        if (Target == null)
            return;

        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, _speed * Time.deltaTime);

        _wheel.Rotate(Vector3.back, _speed);
    }
}
