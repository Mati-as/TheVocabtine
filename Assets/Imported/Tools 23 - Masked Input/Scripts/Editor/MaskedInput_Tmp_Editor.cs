using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CustomEditor(typeof(MaskedInput_TMP))]
[CanEditMultipleObjects]
public class MaskedInput_Tmp_Editor : Editor
{
    

    MaskedInput_TMP targetObject;
    TMP_InputField inputTargetObject;
    SerializedObject GetTarget;
    public int selectedInputMask;
    int dateFormatselected;
    int currencyFormatselected;
    int timeFormatselected;
    bool toggleValue;
    private SerializedProperty enumProperty;
    TMP_InputField inputField;
    private SerializedProperty m_OnValueChangedProperty;

    private void OnEnable()
    { 
        enumProperty = serializedObject.FindProperty("maskType");
        m_OnValueChangedProperty = serializedObject.FindProperty("m_OnValueChanged");
        SerializedProperty selectedCurrencyMaskProperty = serializedObject.FindProperty("currencyFormatselected");
       // selectedCurrencyMaskProperty.intValue = 0;


    }

    public string[] dateFormats =new string[]
    {
        "dd-mm-yyyy",//0
        "mm-dd-yyyy"//1
    };
    public string[] currencyFormats = new string[]
    {
        "$",//0
        "€",//1
        "£",//2
        "¥"//3
    };

    public string[] timeFormats = new string[]
    {
        "HH:MM",//0
        "HH:MM:SS"//1
    };

    public override void OnInspectorGUI()
    {



        serializedObject.Update();
        string[] enumNames = enumProperty.enumNames;

        SerializedProperty selectedInputMaskProperty = serializedObject.FindProperty("selectedInputMask");
        selectedInputMaskProperty.intValue = EditorGUILayout.Popup("Mask Type", selectedInputMaskProperty.intValue, enumNames);

        //EditorPrefs.SetInt("selectedInputMask", (int)enumProperty.enumValueIndex);
        enumProperty.enumValueIndex = selectedInputMaskProperty.intValue;
        inputField = target as TMP_InputField;

        EditorGUILayout.Space();




        switch (selectedInputMaskProperty.intValue)
        {
            case 0:
                DisplayPhoneNumberFields();
                targetObject = (MaskedInput_TMP)target;
                //PhoneMask phoneMaskObject = new PhoneMask();
                //targetObject.SetUpInputField(phoneMaskObject);
                
                break;

            case 1:
                DisplayDateFields();
                targetObject = (MaskedInput_TMP)target;
                break;

            case 2:
                DisplayCurrencyFields();
                targetObject = (MaskedInput_TMP)target;
                break;

            case 3:
                DisplayTimeFields();
                targetObject = (MaskedInput_TMP)target;
                break;
            case 4:
                DisplayCreditCardFields();
                targetObject = (MaskedInput_TMP)target;
                break;
        }

        

        toggleValue = EditorGUILayout.Toggle("Override Placeholder?", targetObject.OverridePlaceHolder);
        targetObject.UpdatePlaceHolder();
        if (toggleValue != targetObject.OverridePlaceHolder) {
            targetObject.OverridePlaceHolder = toggleValue;
            targetObject.UpdatePlaceHolder();
        }

        GameObject gO = target as GameObject;
        if(inputField != null) 
            inputField = gO.GetComponent<TMP_InputField>();

        
        serializedObject.ApplyModifiedProperties();
    }

    void DisplayPhoneNumberFields()
    {
        
    }

    void DisplayDateFields()
    {

        SerializedProperty selectedDateMaskProperty = serializedObject.FindProperty("dateFormatselected");
        selectedDateMaskProperty.intValue = EditorGUILayout.Popup("Date Format", selectedDateMaskProperty.intValue, dateFormats);
        //EditorPrefs.SetInt("dateFormatSelected", dateFormatselected);
        if (selectedDateMaskProperty.intValue == 0)
        {
            targetObject = (MaskedInput_TMP)target;
            DateMaskA dateMaskObject = new DateMaskA();
            targetObject.SetUpInputField(dateMaskObject);

        }

        if (selectedDateMaskProperty.intValue == 1)
        {
            targetObject = (MaskedInput_TMP)target;
            DateMaskB dateMaskObject = new DateMaskB();
            targetObject.SetUpInputField(dateMaskObject);
        }
    }

    void DisplayCurrencyFields()
    {
        SerializedProperty selectedCurrencyMaskProperty = serializedObject.FindProperty("currencyFormatselected");
        selectedCurrencyMaskProperty.intValue = EditorGUILayout.Popup("Currency Format", selectedCurrencyMaskProperty.intValue, currencyFormats);
       

        CurrencyMask currencyMaskObject = new CurrencyMask(currencyFormats[selectedCurrencyMaskProperty.intValue]);
        targetObject = (MaskedInput_TMP)target;
        targetObject.SetUpInputField(currencyMaskObject);
    }

    void DisplayTimeFields()
    {

        SerializedProperty selectedtimeMaskProperty = serializedObject.FindProperty("timeFormatselected");
        selectedtimeMaskProperty.intValue = EditorGUILayout.Popup("Time Format", selectedtimeMaskProperty.intValue, timeFormats);
        
    

        if (selectedtimeMaskProperty.intValue == 0)
        {
            targetObject = (MaskedInput_TMP)target;
            TimeMaskA timeMaskObject = new TimeMaskA();
            targetObject.SetUpInputField(timeMaskObject);
        }
        if (selectedtimeMaskProperty.intValue == 1)
        {
            targetObject = (MaskedInput_TMP)target;
            TimeMaskB timeMaskObject = new TimeMaskB();
            targetObject.SetUpInputField(timeMaskObject);
        }
    }

    void DisplayCreditCardFields()
    {
        targetObject = (MaskedInput_TMP)target;
        CreditCardMask creditCardMaskObject = new CreditCardMask();
        targetObject.SetUpInputField(creditCardMaskObject);
    }

}
