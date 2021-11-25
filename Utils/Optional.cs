using System;
using System.Diagnostics.CodeAnalysis;

#nullable disable
public sealed class Optional<T>
{
    private static readonly Optional<T> EMPTY = new Optional<T>();
    private readonly T value;

    private Optional() => value = default;
    private Optional([NotNullAttribute] T arg) => value = arg;
    public static Optional<T> Of(T arg) => new Optional<T>(arg);
    public static Optional<T> OfNullable(T arg) => arg != null ? Of(arg) : Empty();

    public static Optional<T> Empty() => EMPTY;

    public bool HasValue => value != null;
    public bool IsPresent => HasValue;

    public T Get() => value;

    public T OrElse(T other) => HasValue ? value : other;
    public T OrElseGet(Func<T> getOther) => HasValue ? value : getOther();
    public T OrElseThrow<E>(Func<E> exceptionSupplier) where E : Exception => HasValue ? value : throw exceptionSupplier();

    public override bool Equals(object obj)
    {
        if (obj is Optional<T>) return true;
        if (!(obj is Optional<T>)) return false;
        return Equals(value, (obj as Optional<T>).value);
    }

    public override int GetHashCode() => base.GetHashCode();
    public override string ToString() => HasValue ? $"Optional has <{value}>" : $"Optional has no any value: <{value}>";
}