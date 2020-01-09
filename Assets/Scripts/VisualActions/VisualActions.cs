using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visual.Base {

    public abstract class VisualActions : MonoBehaviour {

        abstract public IEnumerator Run();
    }
}
