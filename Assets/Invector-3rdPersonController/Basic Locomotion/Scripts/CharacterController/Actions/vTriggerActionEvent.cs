using System;
using System.Collections.Generic;
using Invector;
using Invector.vCharacterController.vActions;
using UnityEngine.Events;

[vClassHeader("Trigger Action Event", helpBoxText = "Use this to filter a specific TriggerAction so you can use Events with the Controller or components attached to the Controller", useHelpBox = true)]
public class vTriggerActionEvent : vMonoBehaviour
{
    public List<ActionEvent> actionFinders;

    public void TriggerEvent(vTriggerGenericAction action)
    {
        var _action = actionFinders.Find(a => a.actionName.Equals(action.gameObject.name));

        if(_action != null)
        {
            _action.onTriggerEvent.Invoke();
        }
    }

    [Serializable]
    public class ActionEvent
    {
        public string actionName;
        public UnityEvent onTriggerEvent;
    }
}
