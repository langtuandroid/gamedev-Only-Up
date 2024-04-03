using System;
using UnityEngine;

namespace Invector
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class vHideInInspectorAttribute : PropertyAttribute
    {       
        public bool hideProperty { get; set; }
        public string refbooleanProperty;
        public bool invertValue;
        public vHideInInspectorAttribute(string refbooleanProperty, bool invertValue = false)
        {
            this.refbooleanProperty = refbooleanProperty;
            this.invertValue = invertValue;
        }

    }
}
