using System;
using TMPro;


public  class BaseInputMask

{
    public string MaskName;

    public TMP_InputField.ContentType contentType;
    public int CharacterLimit = 10;
    public virtual string MaskInput(string inputString) { return ""; }
}
