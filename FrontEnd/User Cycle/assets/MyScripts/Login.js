document.addEventListener('DOMContentLoaded', function() {
    // Get form and convert anchor to button
    const form = document.querySelector('.account__form');
    const signInLink = form.querySelector('.btn.btn-two.arrow-btn');
    
    // Create button to replace anchor
    const signInButton = document.createElement('button');
    signInButton.type = 'submit';
    signInButton.className = 'btn btn-two arrow-btn';
    signInButton.innerHTML = signInLink.innerHTML;
    signInLink.parentNode.replaceChild(signInButton, signInLink);

    // Add validation styles
    const style = document.createElement('style');
    style.textContent = `
        .form-grp.error input {
            border-color: #dc3545;
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
            min-width: 250px;
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

    // Show error message function
    function showError(input, message) {
        const formGroup = input.closest('.form-grp');
        formGroup.classList.add('error');
        
        // Remove existing error message if any
        const existingError = formGroup.querySelector('.error-message');
        if (existingError) {
            existingError.remove();
        }
        
        const errorDiv = document.createElement('div');
        errorDiv.className = 'error-message';
        errorDiv.textContent = message;
        formGroup.appendChild(errorDiv);
    }

    // Show alert message function
    function showAlert(message, type) {
        const alertDiv = document.createElement('div');
        alertDiv.className = `alert alert-${type}`;
        alertDiv.textContent = message;
        document.body.appendChild(alertDiv);

        setTimeout(() => alertDiv.remove(), 3000);
    }

    // Handle form submission
    form.addEventListener('submit', async function(e) {
        e.preventDefault();

        const email = document.getElementById('email').value;
        const password = document.getElementById('password').value;
        const rememberMe = document.getElementById('terms-check').checked;

        // Basic validation
        let isValid = true;
        if (!email) {
            showError(document.getElementById('email'), 'Email is required');
            isValid = false;
        }
        if (!password) {
            showError(document.getElementById('password'), 'Password is required');
            isValid = false;
        }

        if (!isValid) return;

        try {
            const response = await fetch('https://localhost:7293/api/Auth/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ email, password })
            });

            const data = await response.json();

            if (response.ok) {
                // Store auth data
                localStorage.setItem('authToken', data.token);
                localStorage.setItem('userId', data.userId);
                localStorage.setItem('username', data.username);
                
                // If remember me is checked, store email
                if (rememberMe) {
                    localStorage.setItem('rememberedEmail', email);
                } else {
                    localStorage.removeItem('rememberedEmail');
                }

                showAlert('Login successful!', 'success');
                
                // Redirect based on user type
                setTimeout(() => {
                    if (data.isOrganizer) {
                        window.location.href = 'instructor-dashboard.html';
                    } else {
                        window.location.href = 'student-dashboard.html';
                    }
                }, 1000);
            } else {
                showAlert(data.message || 'Invalid email or password', 'danger');
            }
        } catch (error) {
            console.error('Login error:', error);
            showAlert('An error occurred during login', 'danger');
        }
    });

    // Clear error message when input changes
    const inputs = form.querySelectorAll('input');
    inputs.forEach(input => {
        input.addEventListener('input', function() {
            const formGroup = this.closest('.form-grp');
            formGroup.classList.remove('error');
            const errorMessage = formGroup.querySelector('.error-message');
            if (errorMessage) {
                errorMessage.remove();
            }
        });
    });

    // Auto-fill email if remembered
    const rememberedEmail = localStorage.getItem('rememberedEmail');
    if (rememberedEmail) {
        document.getElementById('email').value = rememberedEmail;
        document.getElementById('terms-check').checked = true;
    }
});