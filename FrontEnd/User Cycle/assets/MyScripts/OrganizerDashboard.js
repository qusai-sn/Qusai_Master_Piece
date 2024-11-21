async function loadDashboardData() {
    try {
        const userId = localStorage.getItem('userId');
        const response = await fetch(`https://localhost:7293/api/OrganizerDashboard/${userId}`);
        const data = await response.json();
        console.log('Received data:', data);
 
        // Update counters
        const counters = {
            'my-events': data.myEventsCount,
            'active-events': data.activeEventsCount,
            'ended-events': data.endedEventsCount,
            'total-attendance': data.totalAttendees,
            'total-tickets': data.totalTickets,
            'total-earning': data.totalEarnings
        };
 
        Object.entries(counters).forEach(([id, value]) => {
            const element = document.getElementById(id);
            if (element) {
                element.setAttribute('data-count', value);
                element.textContent = value;
                
                if (window.Odometer) {
                    const od = new Odometer({
                        el: element,
                        value: 0,
                        format: '',
                    });
                    od.update(value);
                }
            } else {
                console.error(`Element with id ${id} not found`);
            }
        });
 
        // Update event cards
        const eventsContainer = document.getElementById('events-cards-container');
        eventsContainer.innerHTML = '';
 
        data.latestEvents.forEach(event => {
            const eventCard = `
                <div class="col">
                    <div class="courses__item shine__animate-item">
                        <div class="courses__item-thumb">
                            <a href="events-details.html?id=${event.eventId}" class="shine__animate-link">
                                <img src="${event.thumbnailUrl}" alt="${event.title}">
                            </a>
                        </div>
                        <div class="courses__item-content">
                            <ul class="courses__item-meta list-wrap">
                                <li class="courses__item-tag">
                                    <a href="event.html">${event.category}</a>
                                </li>
                            </ul>
                            <h5 class="title"><a href="events-details.html?id=${event.eventId}">${event.title}</a></h5>
                            <p class="author">${event.description}</p>
                        </div>
                    </div>
                </div>`;
            eventsContainer.insertAdjacentHTML('beforeend', eventCard);
        });
 
        // Update ticket sales
        const ticketsTableBody = document.querySelector('#tickets-table tbody');
        ticketsTableBody.innerHTML = data.latestTicketSales.map(sale => `
            <tr>
                <td>
                    <a href="events-details.html?id=${sale.eventId}">${sale.eventName}</a>
                </td>
                <td>
                    <p class="color-black">$${sale.earnings.toFixed(2)}</p>
                </td>
                <td>
                    ${new Date(sale.date).toLocaleDateString()}
                </td>
            </tr>
        `).join('');
 
    } catch (error) {
        console.error('Error loading dashboard data:', error);
    }
 }
 
 document.addEventListener('DOMContentLoaded', () => {
    loadDashboardData().catch(err => console.error('Failed to load dashboard:', err));
 });