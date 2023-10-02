using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class TimeMaskA : BaseInputMask
{
    private int previousLength = 0;
    public TimeMaskA()
    {

        MaskName = "HH:MM";
        contentType = TMP_InputField.ContentType.DecimalNumber;
        CharacterLimit = 5;
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

            if (input.Length > 2)
            {
                int firstPart = int.Parse(input[..2]);
                firstPart = Mathf.Clamp(firstPart, 0, 23);
                formattedInput = $"{firstPart}:{input[2..]}";

            }
            if (input.Length > 3)
            {
                int secondPart = int.Parse(input[2..]);
                secondPart = Mathf.Clamp(secondPart, 0, 59);

                formattedInput = $"{input[..2]}:{secondPart}";

            }


            data = formattedInput;
        }
        return data;
    }
}
