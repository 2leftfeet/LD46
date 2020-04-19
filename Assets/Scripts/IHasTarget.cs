using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasTarget
{
    Vector3 GetPoint();
    Transform GetTarget();
}
