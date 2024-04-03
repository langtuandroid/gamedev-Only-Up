using System;
using System.Collections.Generic;
using Invector;
using UnityEngine;
using UnityEngine.Events;

[vClassHeader("Mult-Toggle Event", helpBoxText = "Use the method SetToggleOn/Off via Events", openClose = false)]
public class vMultToggleEvent : vMonoBehaviour
{
    [Serializable]
    public class Toggle
    {
        public string name;
        [Header("Current Value of the toogle")]
        public bool value;
        [Header("Validation to compare with value")]
        public bool validation;

        public void ToggleOn()
        {
            value = true;
            onTurnOn.Invoke();
        }
        public void ToggleOff()
        {
            value = false;
            onTurnOff.Invoke();
        }
        public bool isValid => value.Equals(validation);
        public UnityEvent onTurnOn, onTurnOff;
    }
    public List<Toggle> toggles;
    public bool isValid;

    public UnityEvent onValidate, onInvalidate;

    public void Start()
    {
        CheckValidation();
    }
    public void ToggleOn(int index)
    {
        if (toggles.Count > 0 && index < toggles.Count)
        {
            toggles[index].ToggleOn();
            CheckValidation();
        }
    }
    public void ToggleOff(int index)
    {
        if (toggles.Count > 0 && index < toggles.Count)
        {
            toggles[index].ToggleOff();
            CheckValidation();
        }
    }

    public void ToggleOn(string name)
    {
        var toogle = toggles.Find(t => t.name.Equals(name));
        if (toogle != null)
        {
            toogle.ToggleOn();
            CheckValidation();
        }
    }

    public void ToggleOff(string name)
    {
        var toogle = toggles.Find(t => t.name.Equals(name));
        if (toogle != null)
        {
            toogle.ToggleOff();
            CheckValidation();
        }
    }

    void CheckValidation()
    {
        var _isValid = isValid;
        var validToggles = toggles.FindAll(t => t.isValid);
        if (validToggles.Count == toggles.Count)
        {
            _isValid = true;
        }
        else
        {
            _isValid = false;
        }

        if (_isValid != isValid)
        {
            isValid = _isValid;
            if (isValid)
            {
                onValidate.Invoke();
            }
            else
            {
                onInvalidate.Invoke();
            }
        }
    }
}
