// Function to fetch and display profile data
async function fetchUserProfile(userId) {
    try {
        const response = await fetch(`https://localhost:7293/api/UserProfile/MyProfile/${userId}`, {
            method: 'GET',
            headers: {
                'Accept': 'application/json'
            }
        });

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const data = await response.json();

        // Update the HTML elements with the profile data
        document.getElementById('firstname').innerHTML = `<span>First Name</span> ${data.firstName}`;
        document.getElementById('lastname').innerHTML = `<span>Last Name</span> ${data.lastName}`;
        document.getElementById('username').innerHTML = `<span>Username</span> ${data.username}`;
        document.getElementById('email').innerHTML = `<span>Email</span> ${data.email}`;
        document.getElementById('phone').innerHTML = `<span>Phone Number</span> ${data.phoneNumber}`;
        document.getElementById('biography').innerHTML = `<span>Biography</span> ${data.biography}`;
    } catch (error) {
        console.error('Error fetching profile data:', error);
    }
}

// Call the function with the userId
fetchUserProfile(localStorage.getItem('userId')); // Replace 1 with the actual userId if needed
