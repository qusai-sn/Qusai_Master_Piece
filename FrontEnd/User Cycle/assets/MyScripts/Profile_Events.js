// Function to create event card
function createEventCard(event) {
    return `
        <div class="col-md-4">
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
                    <h5 class="title"><a href="events-details.html?id=${event.eventId}">${event.title}</a></h5>
                    
                </div>
            </div>
        </div>
    `;
}

// Function to populate events
async function fetchAndPopulateEvents(userId) {
    try {
        // Fetch data from all endpoints
        const [latestEvents, endedEvents, activeEvents] = await Promise.all([
            fetch(`https://localhost:7293/api/EventProfile/${userId}/latest`).then(res => res.json()),
            fetch(`https://localhost:7293/api/EventProfile/${userId}/ended`).then(res => res.json()),
            fetch(`https://localhost:7293/api/EventProfile/${userId}/active`).then(res => res.json())
        ]);

        // Replace slider containers with row containers
        document.getElementById('events-slider-1').className = 'row';
        document.getElementById('events-slider-2').className = 'row';
        document.getElementById('events-slider-3').className = 'row';

        // Remove swiper classes from parent containers
        const swiperContainers = document.querySelectorAll('.dashboard-courses-active');
        swiperContainers.forEach(container => {
            container.className = 'events-container';
        });

        // Populate Latest Events (max 3)
        const latestContainer = document.getElementById('events-slider-1');
        latestContainer.innerHTML = latestEvents
            .slice(0, 3)
            .map(event => createEventCard(event))
            .join('');

        // Populate Ended Events (max 3)
        const endedContainer = document.getElementById('events-slider-2');
        endedContainer.innerHTML = endedEvents
            .slice(0, 3)
            .map(event => createEventCard(event))
            .join('');

        // Populate Active Events (max 3)
        const activeContainer = document.getElementById('events-slider-3');
        activeContainer.innerHTML = activeEvents
            .slice(0, 3)
            .map(event => createEventCard(event))
            .join('');

    } catch (error) {
        console.error('Error fetching events:', error);
    }
}

// Add some CSS to maintain styling
const style = document.createElement('style');
style.textContent = `
    .events-container {
        margin: 0 -15px;
    }
    .events-container .row {
        display: flex;
        flex-wrap: wrap;
    }
    .courses__item {
        margin-bottom: 30px;
        height: 100%;
    }
`;
document.head.appendChild(style);

// Initialize when document is ready
document.addEventListener('DOMContentLoaded', function() {
    const userId = 1; // Replace with actual user ID
    fetchAndPopulateEvents(userId);
});