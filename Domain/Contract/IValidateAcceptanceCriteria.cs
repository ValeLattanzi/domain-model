using DomainModel.Model.Class;

namespace ApiArquitect.Domain.Interface;

internal interface IValidateAcceptanceCriteria<T>
{
    Task<IEnumerable<ValidationResult<T>>> ValidateAcceptanceCriterias(IEnumerable<AcceptanceCriteria<T>> acceptanceCriterias, T entity);
}
