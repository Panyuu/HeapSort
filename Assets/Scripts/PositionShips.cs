using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Visual.Base;

public class PositionShips : VisualActions
{
    public override IEnumerator Run() {
        yield return new WaitForSeconds(1f);
    }
}
