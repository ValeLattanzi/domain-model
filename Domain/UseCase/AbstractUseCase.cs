using DomainModel.Domain.Contract;
using DomainModel.Model.Class;
using ExceptionManager.Model;

namespace DomainModel.Domain.UseCase;

public abstract class AbstractUseCase<T, Request> : IUseCase<T, Request>
{
    public AbstractUseCase()
    {
        AcceptanceCriteria = [];
        UseCaseValidator = new ValidateAcceptanceCriteria<T>();
    }

    #region Properties
    private IValidateAcceptanceCriteria<T> UseCaseValidator { get; set; }

    protected abstract T Entity { get; set; }
    protected abstract IEnumerable<AcceptanceCriteria<T>> AcceptanceCriteria { get; set; }
    #endregion

    public async Task<T?> Execute(Request dto, string useCase)
    {
        var _result = await Start(dto);

        var _acceptanceCriteriaResults = await ValidateAcceptanceCriteria();

        if (_acceptanceCriteriaResults.Any(acceptanceCriteria => !acceptanceCriteria.IsSuccess || acceptanceCriteria.Entity is null))
        {
            var _acceptanceCriteriaFailed = _acceptanceCriteriaResults.First(acceptanceCriteria => !acceptanceCriteria.IsSuccess || acceptanceCriteria.Entity is null);
            throw new AcceptanceCriteriaNotValidException(useCase, _acceptanceCriteriaFailed.Name);
        }

        return _result;
    }

    #region Functions
    // Implement at the subclasses
    protected abstract void DefineAcceptanceCriteria();
    protected abstract Task<T?> Start(Request createClientRequest);

    private async Task<IEnumerable<ValidationResult<T>>> ValidateAcceptanceCriteria()
    {
        return await UseCaseValidator.ValidateAcceptanceCriterias(AcceptanceCriteria, Entity);
    }
    #endregion
}
