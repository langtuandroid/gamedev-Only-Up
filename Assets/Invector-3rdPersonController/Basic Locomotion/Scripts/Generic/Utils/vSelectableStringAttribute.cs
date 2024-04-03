using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field,AllowMultiple =true)]
public class vSelectableStringAttribute : PropertyAttribute
{
    public string tittle;
    public string selectableText;
    public vSelectableStringAttribute(string tittle, string selectableText)
    {
        this.tittle = tittle;
        this.selectableText = selectableText;
    }
}
