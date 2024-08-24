namespace DomainModel.Model.Class;

internal class ValidationResult<T>
{
    public T? Entity { get; set; }
    public string Name { get; }
    public bool IsSuccess { get; }
    public Exception? Exception { get; }

    public ValidationResult(T entity, bool isSuccess, string name, Exception? exception = null)
    {
        Entity = entity;
        Name = name;
        IsSuccess = isSuccess;
        Exception = exception;
    }
}