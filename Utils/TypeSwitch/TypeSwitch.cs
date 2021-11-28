using System;

public class TypeSwitch<T>
{

    private T Value;

    private Boolean hit;

    private TypeSwitch(T value)
    {
        Value = value;
    }

    public static TypeSwitch<T> Of(T value)
    {
        return new TypeSwitch<T>(value);
    }


    public TypeSwitch<T> Case<C>(Action<T> caseFun) where C : T
    {
        if (Value is C)
        {
            hit = true;
            caseFun(Value);
        }

        return this;
    }


    public void Default(Action defaultFun)
    {
        if (!hit)
        {
            defaultFun();
        }
    }
}