
using UnityEngine;
using TMPro;
using System.Collections.Generic;

[RequireComponent(typeof(TMP_InputField))]
[ExecuteInEditMode]
public class MaskedInput_TMP : MonoBehaviour
{
    public enum MaskTypes
    {
        PhoneNumber, //0
        Date,        //1
        Currency,    //2
        Time,       //3
        CreditCard  //4
    }

    private TMP_InputField inputField;
    [SerializeField]
    private BaseInputMask previousMask;
    public MaskTypes maskType;
    public string dateFormat;
    
    public bool OverridePlaceHolder = true;

    [HideInInspector]
    public int selectedInputMask = 0;
    [HideInInspector]
    public int dateFormatselected = 0;
    [HideInInspector]
    public int currencyFormatselected = 0;
    [HideInInspector]
    public int timeFormatselected = 0;
    [HideInInspector]
    public bool toggleValue;

    public string[] currencyFormats = new string[]
   {
        "$",//0
        "€",//1
        "£",//2
        "¥"//3
   };

    public void SetUpInputField(BaseInputMask mask)
    {
        
        if (previousMask != null || mask == previousMask)
            return;
        previousMask = mask;
        inputField = GetComponent<TMP_InputField>();
            inputField.text = string.Empty;
            inputField.contentType = mask.contentType;
            inputField.characterLimit = mask.CharacterLimit;
        inputField.onValueChanged.RemoveAllListeners();
        inputField.onValueChanged.AddListener((e) =>
        {
            string maskedString = mask.MaskInput(e);
            inputField.text = maskedString;
            inputField.stringPosition = maskedString.Length;
        });
        



    }

    public void SetMask()
    {
        switch (maskType)
        {
            case MaskTypes.PhoneNumber:
                PhoneMask phoneMaskObject = new PhoneMask();
                SetUpInputField(phoneMaskObject);
                break;
            case MaskTypes.Date:
                if(dateFormatselected == 0)
                {
                    DateMaskA dateMaskObject = new DateMaskA();
                    SetUpInputField(dateMaskObject);
                }
                else
                {
                    DateMaskB dateMaskObject = new DateMaskB();
                    SetUpInputField(dateMaskObject);
                }
                break;
            case MaskTypes.Currency:
                CurrencyMask currencyMaskObject = new CurrencyMask(currencyFormats[currencyFormatselected]);
                SetUpInputField(currencyMaskObject);
                break;
            case MaskTypes.Time:
                if(timeFormatselected == 0)
                {
                    TimeMaskA timeMaskObject = new TimeMaskA();
                    SetUpInputField(timeMaskObject);
                }
                else
                {
                    TimeMaskB timeMaskObject = new TimeMaskB();
                    SetUpInputField(timeMaskObject);
                }
                break;
            case MaskTypes.CreditCard:
                CreditCardMask creditCardMaskObject = new CreditCardMask();
                SetUpInputField(creditCardMaskObject);
                break;
        }
    }

    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    private void Start()
    {
        SetMask();
    }

    public void UpdatePlaceHolder()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.placeholder.GetComponent<TextMeshProUGUI>().text = OverridePlaceHolder ? previousMask.MaskName : string.Empty;
    }

}



