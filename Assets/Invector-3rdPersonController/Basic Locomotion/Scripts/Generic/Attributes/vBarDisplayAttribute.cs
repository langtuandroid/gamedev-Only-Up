using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
public class vBarDisplayAttribute : PropertyAttribute
{
    public readonly string maxValueProperty;
    public readonly bool showJuntInPlayMode;
    public vBarDisplayAttribute(string maxValueProperty, bool showJuntInPlayMode = false)
    {
        this.maxValueProperty = maxValueProperty;
        this.showJuntInPlayMode = showJuntInPlayMode;
    }
}
