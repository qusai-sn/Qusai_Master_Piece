// Function to load and display user profile data
async function loadUserProfile(userId) {
    try {
        const response = await fetch(`https://localhost:7293/api/UserProfile/MyProfile/${userId}`);
        if (!response.ok) {
            throw new Error('Failed to fetch profile data');
        }

        const userData = await response.json();

        // Populate form fields with user data
        document.getElementById('firstname').value = userData.firstName || '';
        document.getElementById('lastname').value = userData.lastName || '';
        document.getElementById('username').value = userData.username || '';
        document.getElementById('phonenumber').value = userData.phoneNumber || '';
        document.getElementById('email').value = userData.email || '';
        document.getElementById('bio').value = userData.biography || '';

    } catch (error) {
        console.error('Error loading profile:', error);
        showAlert('Error loading profile data', 'error');
    }
}

// Function to update user profile
async function updateUserProfile(event) {
    event.preventDefault();

    const userId = localStorage.getItem('userId'); // Fixed user ID for testing
    const updateData = {
        userId: userId,
        firstName: document.getElementById('firstname').value,
        lastName: document.getElementById('lastname').value,
        username: document.getElementById('username').value,
        phoneNumber: document.getElementById('phonenumber').value,
        email: document.getElementById('email').value,
        biography: document.getElementById('bio').value
    };

    try {
        const response = await fetch('https://localhost:7293/api/UserProfile/update', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(updateData)
        });

        if (!response.ok) {
            throw new Error('Failed to update profile');
        }

        const result = await response.json();
        showAlert('Profile updated successfully!', 'success');
        
    } catch (error) {
        console.error('Error updating profile:', error);
        showAlert('Error updating profile', 'error');
    }
}

// Function to show alerts
function showAlert(message, type) {
    // Create alert element
    const alertDiv = document.createElement('div');
    alertDiv.className = `alert alert-${type === 'success' ? 'success' : 'danger'} alert-dismissible fade show`;
    alertDiv.role = 'alert';
    alertDiv.style.position = 'fixed';
    alertDiv.style.top = '20px';
    alertDiv.style.right = '20px';
    alertDiv.style.zIndex = '1000';
    
    alertDiv.innerHTML = `
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    `;

    // Add alert to document
    document.body.appendChild(alertDiv);

    // Remove alert after 3 seconds
    setTimeout(() => {
        alertDiv.remove();
    }, 3000);
}

// Initialize when document is ready
document.addEventListener('DOMContentLoaded', function() {
    // Load initial profile data
    loadUserProfile(localStorage.getItem('userId')); // Fixed user ID for testing

    // Add form submit handler
    const form = document.querySelector('.instructor__profile-form');
    if (form) {
        form.addEventListener('submit', updateUserProfile);
    }
});