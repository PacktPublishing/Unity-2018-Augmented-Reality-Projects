using System.Runtime.InteropServices;
using UnityEngine;

public class CPlusPlusWrapper : MonoBehaviour {

    [DllImport("CPlusPlusUnManagedPlugin")]
    public static extern int Addify(int a, int b);

    private void Start()
    {
        var add = Addify(2, 4);
        print(add);
    }
}
