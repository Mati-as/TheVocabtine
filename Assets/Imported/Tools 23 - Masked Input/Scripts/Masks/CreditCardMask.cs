using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class CreditCardMask : BaseInputMask
{
    private int previousLength = 0;
    private string currencySymbol;
    public CreditCardMask()
    {

        MaskName = "xxxx-xxxx-xxxx-xxxx";
        contentType = TMP_InputField.ContentType.DecimalNumber;
        CharacterLimit = 19;
        
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
            if (input.Length > 4)
            {
                formattedInput = $"{input[..4]}-{input[4..]}";

            }
            if(input.Length > 8)
            {
                formattedInput = $"{input[..4]}-{input[4..8]}-{input[8..]}";
            }
            if (input.Length > 12)
            {
                formattedInput = $"{input[..4]}-{input[4..8]}-{input[8..12]}-{input[12..]}";
            }


            data = formattedInput;
        }
        return data;
    }
}
