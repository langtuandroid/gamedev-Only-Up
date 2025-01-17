﻿using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class vSeparator : PropertyAttribute
{     
    public string label;
    public string tooltip;
    public string style;
    public int fontSize = 10;

    public vSeparator()
    {       
        fontSize = 15;
    }

    public vSeparator(string label, string tooltip = "")
    {        
        this.label = label;
        this.tooltip = tooltip;
        fontSize = 15;
    }

    public vSeparator(string label, int fontSize, string tooltip = "")
    {
        this.label = label;
        this.tooltip = tooltip;
        this.fontSize = fontSize;
    }
}
