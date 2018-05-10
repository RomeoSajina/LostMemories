﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Path", menuName = "Path")]
public class EnemyPath : ScriptableObject {
    public Transform[] pathPoints;
}
