// Function to load and display organizer profile data
async function loadOrganizerProfile(organizerId) {
    try {
        const response = await fetch(`https://localhost:7293/api/UserProfile/MyProfile/${organizerId}`);
        if (!response.ok) {
            throw new Error('Failed to fetch profile data');
        }

        const userData = await response.json();

        // Populate form fields
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

// Function to update organizer profile
async function updateOrganizerProfile(event) {
    event.preventDefault();

    const organizerId =localStorage.getItem('userId'); // Replace with actual organizer ID
    const updateData = {
        userId: organizerId, // Using userId as it matches the existing API
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

// Alert function for success/error messages
function showAlert(message, type) {
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

    document.body.appendChild(alertDiv);

    setTimeout(() => {
        alertDiv.remove();
    }, 3000);
}

// Initialize when document is loaded
document.addEventListener('DOMContentLoaded', function() {
    // Load initial profile data
    const organizerId = localStorage.getItem('userId'); // Replace with actual organizer ID
    loadOrganizerProfile(organizerId);

    // Add form submit handler
    const form = document.querySelector('.instructor__profile-form');
    if (form) {
        form.addEventListener('submit', updateOrganizerProfile);
    }
});