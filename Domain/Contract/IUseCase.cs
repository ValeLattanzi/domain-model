namespace DomainModel.Domain.Contract;

public interface IUseCase<TEntity, TDTO>
{
    /// <summary>
    /// Get the DTO from parameters and returns the entity after process
    /// </summary>
    /// <param name="dto">Data transfer object from frontend</param>
    /// <param name="useCase">Name of use case</param>
    /// <returns>Object from database</returns>
    Task<TEntity?> Execute(TDTO dto, string useCase);
}
