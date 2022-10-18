using Microsoft.AspNetCore.Mvc;
using NLayerApp.API.Controllers.Base;
using NLayerApp.Core.Dtos.ResponseTypes.Concrete;
using NLayerApp.Core.Repositories;

namespace NLayerApp.API.Controllers
{
    public class ExampleController : CustomBaseController
    {
        private readonly ITransactionExampleRepository _transactionExampleRepository;

        public ExampleController(ITransactionExampleRepository transactionExampleRepository)
        {
            _transactionExampleRepository = transactionExampleRepository;
        }

        [HttpPost]
        public async Task<IActionResult> TransactionExample()
        {
            await _transactionExampleRepository.TransactionExample();
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(201, isSuccess: true));

        }

        [HttpPost("UnitOfWorkTransactionExampleClosed")]
        public async Task<IActionResult> UnitOfWorkTransactionExampleClosed()
        {
            await _transactionExampleRepository.UnitOfWorkTransactionExampleClosed();
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(201, isSuccess: true));

        }
        [HttpPost("UnitOfWorkTransactionExampleOpened")]
        public async Task<IActionResult> UnitOfWorkTransactionExampleOpened()
        {
            await _transactionExampleRepository.UnitOfWorkTransactionExampleOpened();
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(201, isSuccess: true));

        }
    }
}
