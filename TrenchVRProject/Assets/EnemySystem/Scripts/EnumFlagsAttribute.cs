using UnityEngine;

public class EnumFlagsAttribute : PropertyAttribute
{
    public string enumName;

    public EnumFlagsAttribute() { }

    public EnumFlagsAttribute(string name)
    {
        enumName = name;
    }
}