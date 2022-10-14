using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuaternionExtensions
{
    public static Quaternion Diff(this Quaternion to, Quaternion from)
    {
        return to * Quaternion.Inverse(from);
    }
    public static Quaternion Add(this Quaternion start, Quaternion diff)
    {
        return diff * start;
    }
}
