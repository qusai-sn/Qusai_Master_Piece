
let userId = localStorage.getItem('userId');

async function loadDashboardData(userId) {
    try {
        const response = await fetch(`https://localhost:7293/api/Dashboard/stats/${userId}`);
        if (!response.ok) {
            throw new Error('Failed to fetch dashboard data');
        }
        
        const data = await response.json();

        // Update counter statistics - directly update the text content
        const waitingElement = document.getElementById('in-waiting');
        const activeElement = document.getElementById('active-events');
        const attendedElement = document.getElementById('attended-events');

        if (waitingElement) {
            waitingElement.textContent = data.eventsInWaiting;
            waitingElement.setAttribute('data-count', data.eventsInWaiting);
        }

        if (activeElement) {
            activeElement.textContent = data.activeEvents;
            activeElement.setAttribute('data-count', data.activeEvents);
        }

        if (attendedElement) {
            attendedElement.textContent = data.attendedEvents;
            attendedElement.setAttribute('data-count', data.attendedEvents);
        }

        // Update latest events
        const eventsContainer = document.querySelector('.progress__courses-wrap .row');
        if (eventsContainer && data.latestEvents.length > 0) {
            eventsContainer.innerHTML = data.latestEvents.slice(0, 3).map(event => `
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
                                <li class="courses__item-tag">
                                    <a href="event.html">${event.type}</a>
                                </li>
                            </ul>
                            <h5 class="title">
                                <a href="events-details.html?id=${event.eventId}">${event.title}</a>
                            </h5>
                        </div>
                    </div>
                </div>
            `).join('');
        } else if (eventsContainer) {
            eventsContainer.innerHTML = `
                <div class="col">
                    <p>No events found</p>
                </div>
            `;
        }

        // If using odometer, update it
        if (typeof Odometer !== 'undefined') {
            new Odometer({
                el: waitingElement,
                value: data.eventsInWaiting,
                format: 'd'
            });
            
            new Odometer({
                el: activeElement,
                value: data.activeEvents,
                format: 'd'
            });
            
            new Odometer({
                el: attendedElement,
                value: data.attendedEvents,
                format: 'd'
            });
        }

    } catch (error) {
        console.error('Error loading dashboard data:', error);
    }
}

// Load dashboard data when page loads
document.addEventListener('DOMContentLoaded', () => {
    const userId = localStorage.getItem('userId'); // Replace with actual user ID
    loadDashboardData(userId);
});