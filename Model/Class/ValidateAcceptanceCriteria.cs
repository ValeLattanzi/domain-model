using ApiArquitect.Domain.Interface;

namespace DomainModel.Model.Class;

internal class ValidateAcceptanceCriteria<T> : IValidateAcceptanceCriteria<T>
{
    public ValidateAcceptanceCriteria() { }

    public async Task<IEnumerable<ValidationResult<T>>> ValidateAcceptanceCriterias(IEnumerable<AcceptanceCriteria<T>> acceptanceCriterias, T entity)
    {
        var validationResults = new List<ValidationResult<T>>();

        foreach (var criteria in acceptanceCriterias)
        {
            try
            {
                bool result = await criteria.Validation(entity);
                validationResults.Add(new ValidationResult<T>(entity, result, criteria.Name));
            }
            catch (Exception ex)
            {
                validationResults.Add(new ValidationResult<T>(entity, false, criteria.Name, ex));
            }
        }

        return validationResults;
    }
}
