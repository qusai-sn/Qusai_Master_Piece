document.addEventListener("DOMContentLoaded", function() {
    // Replace '4' with the actual userId you want to fetch
    const userId = 1;
    const apiUrl = `https://localhost:7293/api/OrganizerProfile/${userId}`;

    fetch(apiUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            // Update the HTML elements with the fetched data
            document.getElementById("f-name").innerHTML = `<span>First Name</span> ${data.firstName}`;
            document.getElementById("l-name").innerHTML = `<span>Last Name</span> ${data.lastName}`;
            document.getElementById("username").innerHTML = `<span>Username</span> ${data.username}`;
            document.getElementById("email").innerHTML = `<span>Email</span> ${data.email}`;
            document.getElementById("phone").innerHTML = `<span>Phone Number</span> ${data.phoneNumber}`;
            document.getElementById("bio").innerHTML = `<span>Biography</span> ${data.biography}`;
        })
        .catch(error => {
            console.error('Error fetching profile data:', error);
        });
});
