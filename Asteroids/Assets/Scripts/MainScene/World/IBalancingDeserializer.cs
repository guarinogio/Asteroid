using SimpleJSON;

public interface IBalancingDeserializable
{
    string BalancingKey { get; }
    void BalancingFillData(JSONNode jsonNode);

}

public static class IBalancingDeserializableExtentions
{
    public static T[] Deserialize<T>(this IBalancingDeserializable filleable, JSONNode jsonNode) where T : IBalancingDeserializable, new()
    {
        #region Variables
        //the array to return;
        T[] array = null;
        //the current fillable element to be referenced in the array
        T curFilleable = default(T);
        //the current node inside the iteration;
        JSONNode curNode = null;
        //key to search for in the jsonNode;
        string balancingKey = null;
        //the length of the node;
        int jsonNodeLength = 0;
        #endregion

        //get the balancing key from a disposable new T(child);
        balancingKey = new T().BalancingKey;
        //get the JSON node length to avoid recalculations;
        jsonNodeLength = jsonNode[balancingKey].Count;
        //create the array to be returned;
        array = new T[jsonNodeLength];

        //iterate through the JSON node to fill the curFilleable;
        for (int _i = jsonNodeLength - 1; _i >= 0; _i--)
        {
            //make a new fillable;
            curFilleable = new T();
            //get the current node of this iteration to avoid recalculations;
            curNode = jsonNode[balancingKey][_i];
            //fill;
            curFilleable.BalancingFillData(curNode);
            //reference it in the returned array;
            array[_i] = curFilleable;
        }
        //return dat array Ramon!;
        return array;
    }
}
