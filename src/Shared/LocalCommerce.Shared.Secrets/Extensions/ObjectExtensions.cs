namespace LocalCommerce.Shared.Secrets.Extensions;

public static class ObjectExtensions
{
    public static T ToObject<T>(this IDictionary<string, object> source) where T : new()
    {
        var someObject = new T();
        var someObjectType = someObject.GetType();

        foreach (var item in source)
        {
            var theProperty = someObjectType.GetProperty(item.Key)!;
            
            theProperty?.SetValue(someObject, ((System.Text.Json.JsonElement)item.Value).GetString(), null);//,null);
        }
        return someObject;
    }
}