// Checkout page JavaScript
document.addEventListener('DOMContentLoaded', function() {
    // Get event ID from URL
    const urlParams = new URLSearchParams(window.location.search);
    const eventId = urlParams.get('eventId');

    // Fetch event details
    fetch(`https://localhost:7293/api/EventDetails/${eventId}`)
        .then(response => response.json())
        .then(data => {
            // Display event details
            document.getElementById('event-name').textContent = data.title;
            document.getElementById('ticket-price').textContent = `$${data.ticketPrice.toFixed(2)}`;
        })
        .catch(error => console.error('Error fetching event details:', error));

    // Payment form validation
    const form = document.querySelector('.customer__form-wrap');
    form.addEventListener('submit', function(e) {
        e.preventDefault();
        if (validatePaymentForm()) {
            savePaymentMethod();
        }
    });

    // Place order button click handler
    document.querySelector('.order__info-wrap .btn').addEventListener('click', async function(e) {
        e.preventDefault();
        if (!validatePaymentForm()) {
            alert('Please fill in valid payment details before placing the order.');
            return;
        }
        await createTicket(eventId);
    });
});

// Payment form validation functions
function validatePaymentForm() {
    const cardholderName = document.getElementById('cardholderName').value;
    const cardNumber = document.getElementById('cardNumber').value;
    const expirationDate = document.getElementById('expirationDate').value;
    const cvv = document.getElementById('cvv').value;

    // Basic validation rules
    if (cardholderName.length < 3) {
        alert('Please enter a valid cardholder name');
        return false;
    }

    if (!validateCardNumber(cardNumber)) {
        alert('Please enter a valid card number');
        return false;
    }

    if (!validateExpirationDate(expirationDate)) {
        alert('Please enter a valid expiration date (MM/YY)');
        return false;
    }

    if (!validateCVV(cvv)) {
        alert('Please enter a valid CVV');
        return false;
    }

    return true;
}

function validateCardNumber(cardNumber) {
    // Remove spaces and check if it's 16 digits
    const cleaned = cardNumber.replace(/\s/g, '');
    return /^\d{16}$/.test(cleaned);
}

function validateExpirationDate(expDate) {
    // Check MM/YY format
    return /^(0[1-9]|1[0-2])\/([0-9]{2})$/.test(expDate);
}

function validateCVV(cvv) {
    // Check if it's 3 or 4 digits
    return /^\d{3,4}$/.test(cvv);
}

// Function to generate random QR code
function generateQRCode() {
    const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
    let result = '';
    for (let i = 0; i < 6; i++) {
        result += characters.charAt(Math.floor(Math.random() * characters.length));
    }
    return result;
}
// Function to create ticket
async function createTicket(eventId) {
    const ticket = {
        eventId: parseInt(eventId),
        userId: localStorage.getItem('userId'), // Assuming user 1 is logged in the logged in user
        qrcode: generateQRCode(),
        price: parseFloat(document.getElementById('ticket-price').textContent.replace('$', '')),
        purchaseDate: new Date().toISOString()
    };

    try {
        const response = await fetch('https://localhost:7293/api/Tickets', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(ticket)
        });

        if (response.ok) {
            alert('Ticket purchased successfully!');
            window.location.href = 'student-review.html';
        } else {
            throw new Error('Failed to create ticket');
        }
    } catch (error) {
        console.error('Error creating ticket:', error);
        alert('Failed to process ticket purchase. Please try again.');
    }
}

// Function to save payment method
async function savePaymentMethod() {
    const paymentMethod = {
        userId: 1, // Assuming user 1 is logged in
        cardholderName: document.getElementById('cardholderName').value,
        cardNumber: document.getElementById('cardNumber').value,
        expirationDate: document.getElementById('expirationDate').value,
        cvv: document.getElementById('cvv').value,
        isDefault: document.getElementById('isDefault').checked
    };

    try {
        const response = await fetch('https://localhost:7293/api/PaymentMethod', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(paymentMethod)
        });

        if (!response.ok) {
            throw new Error('Failed to save payment method');
        }
    } catch (error) {
        console.error('Error saving payment method:', error);
        alert('Failed to save payment method. Please try again.');
    }
}

// Add input formatters for better user experience
document.getElementById('cardNumber').addEventListener('input', function(e) {
    // Format card number with spaces every 4 digits
    let value = e.target.value.replace(/\s/g, '').replace(/[^\d]/g, '');
    let formatted = '';
    for(let i = 0; i < value.length; i++) {
        if(i > 0 && i % 4 === 0) {
            formatted += ' ';
        }
        formatted += value[i];
    }
    e.target.value = formatted.substring(0, 19); // Limit to 16 digits + 3 spaces
});

document.getElementById('expirationDate').addEventListener('input', function(e) {
    // Format expiration date as MM/YY
    let value = e.target.value.replace(/\D/g, '');
    if (value.length >= 2) {
        value = value.substring(0, 2) + '/' + value.substring(2, 4);
    }
    e.target.value = value;
});

document.getElementById('cvv').addEventListener('input', function(e) {
    // Limit CVV to 3-4 digits
    e.target.value = e.target.value.replace(/\D/g, '').substring(0, 4);
});