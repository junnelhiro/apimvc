public interface IDevice
{
    void VolumeUp();
    void VolumeDown();
}
public class clientA
{
    public clientA()
    {
        var z = new C();
        XS x = new XS(z);
    }
}
public class XS
{
    private IA _xx;
    public XS(IA da)
    {
        _xx = da;
    }
}

public interface I1 { }
public interface IA { }
public class ClassA : IA
{
}
public class B : IA
{
}
public class C : B { }

public class Z1 : I1 { }
public class Z2 : I1 { }
public class Z3 : I1 { }