// Form validation and submission handler
document.addEventListener('DOMContentLoaded', function() {
    const form = document.querySelector('.account__form');
    const inputs = {
        firstName: document.getElementById('fast-name'),
        lastName: document.getElementById('last-name'),
        username: document.getElementById('username'),
        email: document.getElementById('email'),
        phone: document.getElementById('phone'),
        password: document.getElementById('password'),
        confirmPassword: document.getElementById('confirm-password')
    };

    // Add validation styles
    const style = document.createElement('style');
    style.textContent = `
        .form-grp.error input {
            border-color: #dc3545;
        }
        .form-grp.success input {
            border-color: #198754;
        }
        .error-message {
            color: #dc3545;
            font-size: 0.875rem;
            margin-top: 0.25rem;
        }
        .alert {
            position: fixed;
            top: 20px;
            right: 20px;
            padding: 15px;
            border-radius: 4px;
            z-index: 1000;
            animation: slideIn 0.5s ease-in-out;
        }
        .alert-success {
            background-color: #d4edda;
            border-color: #c3e6cb;
            color: #155724;
        }
        .alert-danger {
            background-color: #f8d7da;
            border-color: #f5c6cb;
            color: #721c24;
        }
        @keyframes slideIn {
            from { transform: translateX(100%); }
            to { transform: translateX(0); }
        }
    `;
    document.head.appendChild(style);

    // Validation functions
    const validators = {
        firstName: (value) => value.length >= 2 || 'First name must be at least 2 characters',
        lastName: (value) => value.length >= 2 || 'Last name must be at least 2 characters',
        username: (value) => value.length >= 4 || 'Username must be at least 4 characters',
        email: (value) => /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value) || 'Please enter a valid email',
        phone: (value) => /^[0-9]{10}$/.test(value) || 'Please enter a valid 10-digit phone number',
        password: (value) => value.length >= 8 || 'Password must be at least 8 characters',
        confirmPassword: (value) => value === inputs.password.value || 'Passwords do not match'
    };

    // Show error message
    function showError(input, message) {
        const formGroup = input.closest('.form-grp');
        formGroup.classList.add('error');
        formGroup.classList.remove('success');
        
        // Remove existing error message if any
        const existingError = formGroup.querySelector('.error-message');
        if (existingError) {
            existingError.remove();
        }
        
        // Add new error message
        const errorDiv = document.createElement('div');
        errorDiv.className = 'error-message';
        errorDiv.textContent = message;
        formGroup.appendChild(errorDiv);
    }

    // Show success
    function showSuccess(input) {
        const formGroup = input.closest('.form-grp');
        formGroup.classList.add('success');
        formGroup.classList.remove('error');
        
        // Remove error message if exists
        const errorMessage = formGroup.querySelector('.error-message');
        if (errorMessage) {
            errorMessage.remove();
        }
    }

    // Show alert message
    function showAlert(message, type) {
        const alertDiv = document.createElement('div');
        alertDiv.className = `alert alert-${type}`;
        alertDiv.textContent = message;
        document.body.appendChild(alertDiv);

        setTimeout(() => {
            alertDiv.remove();
        }, 3000);
    }

    // Handle form submission
    form.addEventListener('submit', async function(e) {
        e.preventDefault();

        let isValid = true;
        // Validate all inputs
        Object.entries(inputs).forEach(([key, input]) => {
            const validationResult = validators[key](input.value);
            if (validationResult !== true) {
                showError(input, validationResult);
                isValid = false;
            } else {
                showSuccess(input);
            }
        });

        if (!isValid) return;

        // Prepare data for API
        const registrationData = {
            firstName: inputs.firstName.value,
            lastName: inputs.lastName.value,
            username: inputs.username.value,
            email: inputs.email.value,
            phoneNumber: inputs.phone.value,
            password: inputs.password.value,
            confirmPassword: inputs.confirmPassword.value
        };

        try {
            const response = await fetch('https://localhost:7293/api/Auth/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(registrationData)
            });

            const data = await response.json();

            if (response.ok) {
                showAlert('Registration successful!', 'success');
                // Store token in localStorage
                localStorage.setItem('authToken', data.token);
                localStorage.setItem('userId', data.userId);
                // Redirect after successful registration
                setTimeout(() => {
                    window.location.href = '/dashboard.html';
                }, 1500);
            } else {
                showAlert(data.message || 'Registration failed', 'danger');
            }
        } catch (error) {
            console.error('Registration error:', error);
            showAlert('An error occurred during registration', 'danger');
        }
    });

    // Real-time validation
    Object.entries(inputs).forEach(([key, input]) => {
        input.addEventListener('blur', function() {
            const validationResult = validators[key](input.value);
            if (validationResult !== true) {
                showError(input, validationResult);
            } else {
                showSuccess(input);
            }
        });
    });
});