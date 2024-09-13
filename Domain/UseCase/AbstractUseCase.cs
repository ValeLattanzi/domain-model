using ApiArquitect.Domain.Interface;
using DomainModel.Domain.Contract;
using DomainModel.Model.Class;
using ExceptionManager.Model;

namespace DomainModel.Domain.UseCase;

public abstract class AbstractUseCase<TEntity, TDTO> : IUseCase<TEntity, TDTO>
{
    public AbstractUseCase()
    {
        AcceptanceCriteria = [];
        UseCaseValidator = new ValidateAcceptanceCriteria<TEntity>();
    }

    #region Properties
    private IValidateAcceptanceCriteria<TEntity> UseCaseValidator { get; set; }

    protected abstract TEntity Entity { get; set; }
    protected abstract IEnumerable<AcceptanceCriteria<TEntity>> AcceptanceCriteria { get; set; }
    #endregion

    public async Task<TEntity?> Execute(TDTO dto, string useCase)
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

    protected abstract Task<TEntity?> Start(TDTO dto);
    private async Task<IEnumerable<ValidationResult<TEntity>>> ValidateAcceptanceCriteria()
    {
        return await UseCaseValidator.ValidateAcceptanceCriterias(AcceptanceCriteria, Entity);
    }
    #endregion
}
