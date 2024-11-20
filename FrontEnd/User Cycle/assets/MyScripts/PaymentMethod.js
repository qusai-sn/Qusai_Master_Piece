async function loadPaymentMethod(userId) {
    try {
        const response = await fetch(`https://localhost:7293/api/PaymentMethod/${userId}`);
        if (!response.ok) {
            throw new Error('Failed to fetch payment method');
        }

        const data = await response.json();

        // Populate form fields
        document.getElementById('cardholderName').value = data.cardholderName || '';
        document.getElementById('cardNumber').value = data.cardNumber || '';
        document.getElementById('expirationDate').value = data.expirationDate || '';
        document.getElementById('cvv').value = data.cvv || '';
        document.getElementById('isDefault').checked = data.isDefault;

    } catch (error) {
        console.error('Error loading payment method:', error);
        showAlert('Error loading payment method', 'error');
    }
}

async function updatePaymentMethod() {

    const userId = 2; // Replace with actual user ID
    const paymentData = {
        cardholderName: document.getElementById('cardholderName').value,
        cardNumber: document.getElementById('cardNumber').value,
        expirationDate: document.getElementById('expirationDate').value,
        cvv: document.getElementById('cvv').value,
        isDefault: document.getElementById('isDefault').checked
    };

    try {
        const response = await fetch(`https://localhost:7293/api/PaymentMethod/${userId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(paymentData)
        });

        if (!response.ok) {
            throw new Error('Failed to update payment method');
            showAlert('Failed to update payment method!', 'success');
        }

        showAlert('Payment method updated successfully!', 'success');
        
    } catch (error) {
        console.error('Error updating payment method:', error);
        showAlert('Error updating payment method', 'error');
    }
}

document.addEventListener('DOMContentLoaded', function() {
    const userId = 2; // Replace with actual user ID
    loadPaymentMethod(userId);

    const form = document.querySelector('.instructor__profile-form');
    if (form) {
        form.addEventListener('submit', updatePaymentMethod);
    }
});  