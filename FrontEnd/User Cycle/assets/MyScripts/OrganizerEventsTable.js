async function loadOrganizerEventsTable() {
    try {
        const organizerId = 2; // Replace with actual ID
        const response = await fetch(`https://localhost:7293/table/${organizerId}`);
        const data = await response.json();
 
        const tbody = document.querySelector('#event-table tbody');
        tbody.innerHTML = data.map(event => `
            <tr>
                <td><p>${event.eventId}</p></td>
                <td><p>${event.eventName}</p></td>
                <td><p>${new Date(event.eventDate).toLocaleDateString('en-US', {
                    year: 'numeric',
                    month: 'long',
                    day: '2-digit'
                })}</p></td>
                <td><p>$${event.price.toFixed(2)}</p></td>
            </tr>
        `).join('');
 
    } catch (error) {
        console.error('Error loading organizer events:', error);
    }
 }
 
 document.addEventListener('DOMContentLoaded', loadOrganizerEventsTable);