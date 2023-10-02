using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class DummyInp : MonoBehaviour
{
    public int previousLength;

    public TMP_InputField thisText;

    private void Awake()
    {
       // thisText = GetComponent<TextMeshProUGUI>();
    }
    public void MaskInput(string inputString)
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
                formattedInput = $"({input[..3]}) {input[3..6]}-{input[6..]}";
            }


            thisText.text = formattedInput;

        }
    }
}
