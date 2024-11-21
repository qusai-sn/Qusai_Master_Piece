// Function to fetch and display ticket data
async function fetchTickets(userId) {
    try {
        const response = await fetch(`https://localhost:7293/api/Tickets/user/${userId}`, {
            method: 'GET',
            headers: {
                'Accept': 'application/json'
            }
        });

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const tickets = await response.json();

        // Get the tbody element
        const tbody = document.querySelector('tbody');
        tbody.innerHTML = ''; // Clear any existing rows

        // Iterate over the tickets and create table rows
        tickets.forEach(ticket => {
            const tr = document.createElement('tr');
            tr.innerHTML = `
                <td><a  href="events-details.html?id=${ticket.eventId}">${ticket.eventTitle}</a></td>
                <td>${new Date(ticket.purchaseDate).toLocaleDateString()}</td>
                <td>$${ticket.price.toFixed(2)}</td>
            `;
            tbody.appendChild(tr);
        });
    } catch (error) {
        console.error('Error fetching tickets:', error);
    }
}

// Call the function with the userId
fetchTickets(localStorage.getItem('userId')); // Replace 1 with the actual userId if needed
