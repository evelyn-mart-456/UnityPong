
using UnityEngine;
using Unity.Netcode;

public class RightPaddleController : PaddleController
{
    protected override string GetInputAxisName()
    {
        return "Vertical";
    }
}



