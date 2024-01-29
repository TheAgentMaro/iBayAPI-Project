using iBay.Entities.Repositories;
using iBay.Entities.Models;
using iBay.WebAPI.Interfaces;
using System;
using System.Threading.Tasks;

namespace iBay.WebAPI.Services
{
public class PaymentService : IPaymentService
    {
        private readonly IBasicRepository<Payment> _paymentRepository;

        public PaymentService(IBasicRepository<Payment> paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<int> CreatePaymentAsync(Payment paymentDetails)
        {
            try
            {
                var payment = new Payment
                {
                    UserId = paymentDetails.UserId,
                    Amount = paymentDetails.Amount,
                    PaymentDate = DateTime.Now
                };

                await _paymentRepository.AddAsync(payment);
                return payment.PaymentId;
            }
            catch (Exception ex)
            {
                // Gérer les exceptions selon les besoins
                throw new ApplicationException("Une erreur s'est produite lors de la création du paiement.", ex);
            }
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            try
            {
                var payment = await _paymentRepository.GetByIdAsync(id);
                if (payment != null)
                {
                    return new Payment
                    {
                        UserId = payment.UserId,
                        Amount = payment.Amount,
                        PaymentDate = payment.PaymentDate
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                // Gérer les exceptions selon les besoins
                throw new ApplicationException("Une erreur s'est produite lors de la récupération du paiement.", ex);
            }
        }

        public async Task<bool> UpdatePaymentAsync(int id, Payment updatedPayment)
        {
            try
            {
                var payment = await _paymentRepository.GetByIdAsync(id);
                if (payment != null)
                {
                    payment.Amount = updatedPayment.Amount;
                    payment.PaymentDate = DateTime.Now;
                    await _paymentRepository.UpdateAsync(payment);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Gérer les exceptions selon les besoins
                throw new ApplicationException("Une erreur s'est produite lors de la mise à jour du paiement.", ex);
            }
        }

        public async Task<bool> DeletePaymentAsync(int id)
        {
            try
            {
                var payment = await _paymentRepository.GetByIdAsync(id);
                if (payment != null)
                {
                    await _paymentRepository.DeleteAsync(id);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Gérer les exceptions selon les besoins
                throw new ApplicationException("Une erreur s'est produite lors de la suppression du paiement.", ex);
            }
        }
    }
}
