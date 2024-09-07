using InventoryManagement.Contracts.Service;
using InventoryManagement.Dto.Response;
using InventoryManagement.Mapper;
using InventoryManagement.Repository;
using InventoryManagement.Settings.Validations;

namespace InventoryManagement.Service
{
    public class InventoryMovementService : IInventoryMovementService
    {
        private readonly InventoryMovementRepository _repository;

        public InventoryMovementService(InventoryMovementRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InventoryMovementResponse>> GetInventoryMovements()
        {
            var response = new List<InventoryMovementResponse>();

            var result = await _repository.GetAllAsync();

            foreach (var item in result)
            {
                response.Add(InventoryMovementMapper.InventoryManagementMapperView(item));
            }

            return response;
        }

        public async Task<InventoryMovementResponse> GetInventoryMovement(int id)
        {
            var response = new InventoryMovementResponse();

            var result = await _repository.GetByIdAsync(id);

            if (result == null)
            {
                response.AddNotification(new Notification("Movimentação não encotrada!"));
                return response;
            };

            response = InventoryMovementMapper.InventoryManagementMapperView(result);

            return response;
        }
    }
}
