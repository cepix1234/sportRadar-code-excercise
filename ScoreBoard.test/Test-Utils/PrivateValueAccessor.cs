using System.Reflection;

namespace ScoreBoard.test.Test_Utils;

public class PrivateValueAccessor
{
    public static BindingFlags Flags = BindingFlags.Instance
                                            | BindingFlags.GetProperty
                                            | BindingFlags.SetProperty
                                            | BindingFlags.GetField
                                            | BindingFlags.SetField
                                            | BindingFlags.NonPublic;

    public static FieldInfo GetPrivateFieldInfo(Type type, string fieldName)
    {
        var fields = type.GetFields(Flags);
        return fields.FirstOrDefault(feildInfo => feildInfo.Name == fieldName);
    }

    public static object GetPrivateFieldValue(Type type, string fieldName, object o)
    {
        return GetPrivateFieldInfo(type, fieldName).GetValue(o);
    }

    public static void SetPrivateFieldValue(Type type, string fieldName, object o, object value)
    {
        GetPrivateFieldInfo(type, fieldName).SetValue(o, value);
    }
}
