using MasterPiece.DTO;
using MasterPiece.Models;
using Microsoft.EntityFrameworkCore;


namespace MasterPiece.Services
{
    // Services/IPaymentMethodService.cs
    public interface IPaymentMethodService
    {
        Task<PaymentMethodDTO> GetUserPaymentMethodAsync(int userId);
        Task<bool> UpdatePaymentMethodAsync(int userId, PaymentMethodDTO paymentMethod);
    }


    // Services/PaymentMethodService.cs
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly MasterPieceContext _context;

        public PaymentMethodService(MasterPieceContext context)
        {
            _context = context;
        }

        public async Task<PaymentMethodDTO> GetUserPaymentMethodAsync(int userId)
        {
            var paymentMethod = await _context.PaymentMethods
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (paymentMethod == null) return null;

            return new PaymentMethodDTO
            {
                CardholderName = paymentMethod.CardholderName,
                CardNumber = paymentMethod.CardNumber,  // Implement decryption
                ExpirationDate = paymentMethod.ExpirationDate,
                CVV = paymentMethod.Cvv,  // Implement decryption
                IsDefault = paymentMethod.IsDefault
            };
        }

        public async Task<bool> UpdatePaymentMethodAsync(int userId, PaymentMethodDTO dto)
        {
            var paymentMethod = await _context.PaymentMethods
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (paymentMethod == null)
            {
                paymentMethod = new PaymentMethod
                {
                    UserId = userId,
                    CreatedAt = DateTime.Now
                };
                _context.PaymentMethods.Add(paymentMethod);
            }

            paymentMethod.CardholderName = dto.CardholderName;
            paymentMethod.CardNumber = dto.CardNumber;  // Implement encryption
            paymentMethod.ExpirationDate = dto.ExpirationDate;
            paymentMethod.Cvv = dto.CVV;  // Implement encryption
            paymentMethod.IsDefault = dto.IsDefault;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
