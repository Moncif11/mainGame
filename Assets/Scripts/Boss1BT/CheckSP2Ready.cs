using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class CheckSP2Ready : Node
{
    Transform _transform; 
    public CheckSP2Ready(Transform transform){
        _transform = transform;
    }
}
