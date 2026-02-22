
using UnityEngine;
using Unity.Netcode;

public class LeftPaddleController : PaddleController
{
    protected override string GetInputAxisName()
    {
        return "Vertical";
    }
}






 
