using UnityEngine.UIElements;

public class UIBaseTextField
{
    public TextField TextFieldRoot;

    public UIBaseTextField(TextField textField)
    {
        TextFieldRoot = textField;
    }

    public string GetTextFieldValue()
    {
        return TextFieldRoot.text;
    }
}