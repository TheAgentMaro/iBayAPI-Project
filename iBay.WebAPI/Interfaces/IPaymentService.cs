using iBay.Entities.Models;
using System.Threading.Tasks;

namespace iBay.WebAPI.Interfaces
{
    public interface IPaymentService
    {
        Task<int> CreatePaymentAsync(Payment payment);
        Task<Payment> GetPaymentByIdAsync(int id);
        Task<bool> UpdatePaymentAsync(int id, Payment updatedPayment);
        Task<bool> DeletePaymentAsync(int id);
    }
}
