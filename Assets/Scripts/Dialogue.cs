using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

    [TextArea(1, 3)]
    public string[] sentences;

    [TextArea(1, 3)]
    public string[] level2;

    [TextArea(1, 3)]
    public string[] level3;

    [TextArea(1, 3)]
    public string[] level4;
}
