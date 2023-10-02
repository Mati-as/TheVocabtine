using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class DateMaskB : BaseInputMask
{
    private int previousLength = 0;
    public DateMaskB()
    {

        MaskName = "mm-dd-yyyy";
        contentType = TMP_InputField.ContentType.IntegerNumber;
        CharacterLimit = 10;
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
            if (input.Length == 2)
            {
                int month = int.Parse(input);
                month = Mathf.Clamp(month, 1, 12);
                formattedInput = $"{month}";


            }

            if (input.Length > 2 && input.Length < 5)
            {
                int day = int.Parse($"{input[2..]}");
                day = Mathf.Clamp(day, 1, 31);
                formattedInput = $"{input[..2]}-{day}";

            }

            if (input.Length >= 5)
            {
                formattedInput = $"{input[..2]}-{input[2..4]}-{input[4..]}";
            }


            data = formattedInput;
        }
        return data;
    }


}
