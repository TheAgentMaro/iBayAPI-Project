using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using iBay.WebAPI.Services;
//---- à créer ----//
using System;
//-----------------------//
namespace iBay.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(PaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // POST: api/Payment
        // Crée un nouvel enregistrement de transaction de paiement.
        [Authorize]
        [HttpPost]
        public IActionResult CreatePayment([FromBody] PaymentDetails paymentDetails)
        {
            try
            {
                var paymentId = _paymentService.CreatePayment(paymentDetails);
                return CreatedAtAction(nameof(GetPayment), new { id = paymentId }, paymentDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la création du paiement.");
                return StatusCode(500, "Une erreur est survenue lors de la création du paiement.");
            }
        }

        // GET: api/Payment/{id}
        // Récupère une transaction de paiement spécifique par son ID.
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetPayment(int id)
        {
            try
            {
                var payment = _paymentService.GetPaymentById(id);
                if (payment == null)
                {
                    return NotFound();
                }

                return Ok(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération du paiement.");
                return StatusCode(500, "Une erreur est survenue lors de la récupération du paiement.");
            }
        }

        // PUT: api/Payment/{id}
        // Met à jour une transaction de paiement existante, comme la capture ou le remboursement d'un paiement.
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdatePayment(int id, [FromBody] PaymentDetails updatedPayment)
        {
            try
            {
                var success = _paymentService.UpdatePayment(id, updatedPayment);
                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la mise à jour du paiement.");
                return StatusCode(500, "Une erreur est survenue lors de la mise à jour du paiement.");
            }
        }

        // DELETE: api/Payment/{id}
        // Supprime une transaction de paiement, possiblement pour des raisons de conformité.
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeletePayment(int id)
        {
            try
            {
                var success = _paymentService.DeletePayment(id);
                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la suppression du paiement.");
                return StatusCode(500, "Une erreur est survenue lors de la suppression du paiement.");
            }
        }
    }
}
