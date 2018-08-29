using UnityEngine;

public class CSharpWrapper : MonoBehaviour
{
    private void Start()
    {
        var addition = new CSharpManagedPlugin.Addition();
        var add = addition.Addify(5, 2);
        print(add);
    }
}
