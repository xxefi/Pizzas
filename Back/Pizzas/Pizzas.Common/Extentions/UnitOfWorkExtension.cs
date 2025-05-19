using Pizzas.Core.Abstractions.UOW;

namespace Pizzas.Common.Extentions;


public static class UnitOfWorkExtension
{
    public static async Task<TResult> StartTransactionAsync<TResult>(this IUnitOfWork unitOfWork, Func<Task<TResult>> action)
    {
        await unitOfWork.BeginTransactionAsync();
        try
        {
            var result = await action();
            await unitOfWork.CommitTransactionAsync();
            
            return result;
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}