namespace DomainModel.Model.Class;

public class AcceptanceCriteria<T>
{
    public string Name { get; }
    public Func<T, Task<bool>> Validation { get; }

    public AcceptanceCriteria(string name, Func<T, Task<bool>> validation)
    {
        Name = name;
        Validation = validation;
    }
}
