
using System;
using System.Text.RegularExpressions;
using TMPro;

[Serializable]
public class PhoneMask : BaseInputMask
{
    private int previousLength = 0;


    public PhoneMask() {

        MaskName = "(xxx) xxx-xxxx";
        contentType = TMP_InputField.ContentType.IntegerNumber;
        CharacterLimit = 14;
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
            
            if (input.Length > 3 && input.Length < 7)
            {
                formattedInput = $"({input[..3]}) {input[3..]}";
            }

            if (input.Length >= 7)
            {
                formattedInput = $"({input[..3]}) {input[3..6] }-{input[6..]}";
            }


            data = formattedInput;
            
        }

        return data;
    }
}
