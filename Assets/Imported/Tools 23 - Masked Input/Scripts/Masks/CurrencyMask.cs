using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class CurrencyMask : BaseInputMask
{
    private int previousLength = 0;
    private string currencySymbol;
    public CurrencyMask(string currencySymbolRef)
    {

        MaskName = currencySymbolRef;
        contentType = TMP_InputField.ContentType.DecimalNumber;
        CharacterLimit = 5;
        currencySymbol = currencySymbolRef;
    }

    public override string MaskInput(string inputString)
    {
        string data = new string(inputString);
        if (string.IsNullOrEmpty(data))
        {
            data = string.Empty;
        }
        else if (data.Length > previousLength)
        {
            string input = data;
            input = new Regex(@"\D").Replace(input, string.Empty);
            string formattedInput = input;
            if (input.Length > 0)
            {
                formattedInput = currencySymbol+input;


            }


            data = formattedInput;
        }
        return data;
    }
}
