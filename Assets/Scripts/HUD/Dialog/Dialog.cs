using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog {
    public enum Face { happy, scared, sad };

    public CutScene cutScene;

    [Space]
    public Face face;

    [TextArea(3, 10)]
    public string sentence;
}
