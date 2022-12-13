
delegate string D1(object o);
delegate object D2(string s);
delegate object D3();
delegate string D4(object o, params object[] a);
delegate string D5(int i);
class Test
{
    static string F(object o) { return ""; }

    static void G()
    {
        D1 d1 = F;         // Ok
        D2 d2 = F;         // Ok
        // D3 d3 = F;         // Error – not applicable
        // D4 d4 = F;         // Error – not applicable in normal form
        // D5 d5 = F;         // Error – applicable but not compatible
    }
}

delegate int D(string s, int i);
delegate int E();

class TestX
{
    // public static T F<T>(string s, T t) { return default(T); }
    // public static T G<T>() { return default(T); }

    static void Run()
    {
        // D d1 = F<int>;        // Ok, type argument given explicitly
        // D d2 = F;             // Ok, int inferred as type argument
        // E e1 = G<int>;        // Ok, type argument given explicitly
        // E e2 = G;             // Error, cannot infer from return type
    }
}