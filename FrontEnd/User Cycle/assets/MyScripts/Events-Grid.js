// Define the API URL
const apiUrl = 'https://localhost:7293/api/EventDetails/summaries';

// Fetch the event details
fetch(apiUrl)
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json(); // Parse JSON response
    })
    .then(data => {
        // Clear existing content (if any)
        const container = document.getElementById('event-container');
        container.innerHTML = ''; // Clear any existing events

        // Check if event data is available
        if (data.length > 0) {
            data.forEach(event => {
                // Create a new div for each event
                const eventDiv = document.createElement('div');
                eventDiv.classList.add('col');
                eventDiv.innerHTML = `
                    <div class="courses__item shine__animate-item">
                        <div class="courses__item-thumb">
                            <a href="events-details.html" class="shine__animate-link">
                                <img src="${event.thumbnailUrl}" alt="${event.title} Poster">
                            </a>
                        </div>
                        <div class="courses__item-content">
                            <ul class="courses__item-meta list-wrap">
                                <li class="courses__item-tag">
                                    <a href="event.html">${event.category}</a>
                                </li>
                              
                            </ul>
                            <h5 class="title"><a href="events-details.html?id=${event.eventId}">${event.title}</a></h5>
                            <p class="author">Hosted by Event Organizer</p>
                            <div class="courses__item-bottom">
                                <div class="button">
                                    <a href="events-details.html">
                                        <span class="text">Register Now</span>
                                        <i class="flaticon-arrow-right"></i>
                                    </a>
                                </div>
                                <h5 class="price">$${event.price.toFixed(2)}</h5>
                            </div>
                        </div>
                    </div>
                `;

                // Append the new event div to the container
                container.appendChild(eventDiv);
            });
        } else {
            console.error('No event data available.');
        }
    })
    .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
    });
