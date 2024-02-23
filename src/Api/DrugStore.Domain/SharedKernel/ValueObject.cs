namespace DrugStore.Domain.SharedKernel;

[Serializable]
public abstract class ValueObject
{
    protected static bool EqualOperator(ValueObject? left, ValueObject? right) 
        => !(left is null ^ right is null) && left?.Equals(right!) != false;

    protected static bool NotEqualOperator(ValueObject left, ValueObject right) => !(EqualOperator(left, right));

    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;

        var other = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();

        foreach (var component in GetEqualityComponents())
            hash.Add(component);

        return hash.ToHashCode();
    }
}