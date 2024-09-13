using DomainModel.Model.Class;

namespace DomainModel.Domain.Contract;

internal interface IValidateAcceptanceCriteria<T>
{
    Task<IEnumerable<ValidationResult<T>>> ValidateAcceptanceCriterias(IEnumerable<AcceptanceCriteria<T>> acceptanceCriterias, T entity);
}
