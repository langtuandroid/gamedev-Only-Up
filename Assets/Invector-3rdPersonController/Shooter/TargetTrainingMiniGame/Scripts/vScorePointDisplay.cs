using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class vScorePointDisplay : MonoBehaviour
{
    [SerializeField] protected Text _display;
    public string stringFormat;
    public InputField.OnChangeEvent onChangeDisplayText;

    bool withOutDisplay;
    public Text display
    {
        get
        {
            if (withOutDisplay) return null;
            if (_display == null && !withOutDisplay) _display = GetComponent<Text>();

            if (_display == null) withOutDisplay = true;
            return _display;
        }
    }

    const string StringFormatDefault = "{0}";

    public void ShowValue(float value)
    {
        if (string.IsNullOrEmpty(stringFormat)) stringFormat = StringFormatDefault;
        string text = string.Format(stringFormat, value.ToString());
       if(display) display.text = text;
        onChangeDisplayText.Invoke(text);
    }

    public void ShowValue(int value)
    {
        ShowValue((float)value);
    }
}
