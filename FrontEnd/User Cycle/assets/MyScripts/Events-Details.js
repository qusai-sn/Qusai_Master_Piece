


// Function to get a query parameter by name
function getQueryParam(param) {

    const urlParams = new URLSearchParams(window.location.search);

     return urlParams.get(param);
}

// Example usage: Get the 'id' parameter from the URL
const eventId = getQueryParam('id');

 
const apiUrl = `https://localhost:7293/api/EventDetails/${eventId}`;

fetch(apiUrl)
  .then(response => {
      if (!response.ok) {
          throw new Error('Network response was not ok');
      }
      return response.json();
  })
  .then(data => {
      // Basic Event Information
      document.getElementById('event_title').textContent = data.title;
      document.getElementById('event_description').textContent = data.description;

      // Date and Time
      document.getElementById('Event_Date').textContent = new Date(data.eventDate).toLocaleDateString();
      document.getElementById('Event_start_time').textContent = new Date(`1970-01-01T${data.startTime}`).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
      document.getElementById('Event_end_time').textContent = new Date(`1970-01-01T${data.endTime}`).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });

      // Location Information
      document.getElementById('Event_Location').textContent = data.location;
      document.getElementById('location_url').src = data.locationUrl;

      // Tickets and Pricing
      document.getElementById('Ticket_price').textContent = `$${data.ticketPrice.toFixed(2)}`;
      document.getElementById('available_and_total_seats').textContent = `${data.totalSeats}`;

      // Images
      document.getElementById('event_banner').src = data.bannerUrl;
 
      // Additional Information
      document.getElementById('what_to_expect').textContent = data.whatToExpect;
 
      // Category and Type
      document.getElementById('event_category').textContent = data.categoryName;
      document.getElementById('event_type').textContent = data.typeName;

      // Organizer Information
      if (data.organizer) {
          document.getElementById('organizer_name').textContent = data.organizer.name || 'N/A';
          document.getElementById('organizer_email').textContent = data.organizer.email || 'N/A';
          document.getElementById('organizer_phone').textContent = data.organizer.phoneNumber || 'N/A';
      }

      // Ticket Summary
      if (data.ticketsSummary) {
          document.getElementById('total_tickets').textContent = data.ticketsSummary.totalTickets;
          document.getElementById('sold_tickets').textContent = data.ticketsSummary.soldTickets;
          document.getElementById('total_revenue').textContent = `$${data.ticketsSummary.totalRevenue.toFixed(2)}`;
      }
  })
  .catch(error => {
      console.error('There was a problem with the fetch operation:', error);
      // Show error message to user
      document.getElementById('error-message').textContent = 'Failed to load event details';
  });
// get the catregory and type dynamiclly later 

  




  async function fetchEventSchedule(eventId) {
    try {
        const response = await fetch(`https://localhost:7293/api/EventDetails/${eventId}/schedule`);
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const schedule = await response.json();
        populateSchedule(schedule);
    } catch (error) {
        console.error('Error fetching schedule:', error);
    }
}



function populateSchedule(schedule) {
    const accordionExample = document.getElementById("accordionExample");
    
    // Group sessions by session type
    const groupedSessions = schedule.reduce((acc, session) => {
        if (!acc[session.sessionType]) {
            acc[session.sessionType] = [];
        }
        acc[session.sessionType].push(session);
        return acc;
    }, {});

    // Create accordion items for each session type
    for (const [sessionType, sessions] of Object.entries(groupedSessions)) {
        const accordionItem = document.createElement('div');
        accordionItem.classList.add('accordion-item');
        
        // Create the header for this session type
        const headerId = `heading${sessionType}`;
        const collapseId = `collapse${sessionType}`;
        accordionItem.innerHTML = `
            <h2 class="accordion-header" id="${headerId}">
                <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#${collapseId}" aria-expanded="true" aria-controls="${collapseId}">
                    ${sessionType} Sessions
                </button>
            </h2>
            <div id="${collapseId}" class="accordion-collapse collapse" aria-labelledby="${headerId}" data-bs-parent="#accordionExample">
                <div class="accordion-body">
                    <ul class="list-wrap">
                        ${sessions.map(session => `
                            <li class="course-item">
                                <span class="item-name">${session.sessionTime}: ${session.sessionTitle}</span>
                            </li>
                        `).join('')}
                    </ul>
                </div>
            </div>
        `;
        accordionExample.appendChild(accordionItem);
    }
}

// Call this function with the specific event ID you want to fetch the schedule for
// You can change the event ID or call this function dynamically based on your application's logic
fetchEventSchedule(eventId); // Example event ID



 


function populateEventDetails(eventDetails) {
    const whatToExpect = document.getElementById("What-to-Expect");
    const highlightsList = document.getElementById("Event-HighLights");

    // Update "What to Expect" section
    whatToExpect.innerText = eventDetails.whatToExpect;

    // Clear current highlights
    highlightsList.innerHTML = '';

    // Populate "Event Highlights" section
    const highlights = eventDetails.highlights.split(';').map(item => item.trim());
    highlights.forEach(highlight => {
        const listItem = document.createElement('li');
        listItem.classList.add('about__info-list-item');
        listItem.innerHTML = `
            <i class="flaticon-angle-right"></i>
            <p class="content">${highlight}</p>
        `;
        highlightsList.appendChild(listItem);
    });
}
 
 // In event-details.js
document.getElementById('joinEventButton').addEventListener('click', function(e) {
    e.preventDefault();
    
    const eventData = {
        title: encodeURIComponent(document.getElementById('event_title').textContent),
        price: encodeURIComponent(document.getElementById('Ticket_price').textContent.replace('$', '')),
        date: encodeURIComponent(document.getElementById('Event_Date').textContent)
    };

    // Build URL with all parameters
    const checkoutUrl = `check-out.html?eventId=${eventId}&title=${eventData.title}&price=${eventData.price}&date=${eventData.date}`;
    
    window.location.href = checkoutUrl;
});
 
// Define the API URL for speakers
const apiUrl2 = `https://localhost:7293/api/EventDetails/${eventId}/speakers`;

// Fetch the speakers data
fetch(apiUrl2)
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();
    })
    .then(data => {
        const tableBody = document.getElementById('speakers-table-body');
        tableBody.innerHTML = ''; // Clear existing content

        if (data.length > 0) {
            data.forEach(speaker => {
                const row = document.createElement('tr');
                row.innerHTML = `
                    <td>${speaker.name}</td>
                    <td>${speaker.role}</td>
                    <td>${speaker.bio}</td>
                `;
                tableBody.appendChild(row);
            });
        } else {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td colspan="4" class="text-center">No speakers available for this event.</td>
            `;
            tableBody.appendChild(row);
        }
    })
    .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
        const tableBody = document.getElementById('speakers-table-body');
        tableBody.innerHTML = `
            <tr>
                <td colspan="4" class="text-center text-danger">
                    Failed to load speakers data.
                </td>
            </tr>
        `;
    });



    // Function to fetch and populate highlights
async function fetchAndPopulateHighlights(eventId) {
    try {
        const response = await fetch(`https://localhost:7293/api/EventDetails/${eventId}/highlights`);
        if (!response.ok) {
            throw new Error('Failed to fetch highlights');
        }
        const highlightsText = await response.text(); // Get response as text since it's plain text
        populateHighlights(highlightsText);
    } catch (error) {
        console.error('Error fetching highlights:', error);
    }
}

// Function to populate highlights into the HTML
function populateHighlights(highlightsText) {
    const highlightsList = document.getElementById('Event-HighLights');
    highlightsList.innerHTML = ''; // Clear existing highlights

    // Split the highlights text by semicolon and trim each item
    const highlights = highlightsText.split(';').map(item => item.trim());

    // Create and append list items for each highlight
    highlights.forEach(highlight => {
        if (highlight) { // Only create element if highlight is not empty
            const li = document.createElement('li');
            li.className = 'about__info-list-item';
            li.innerHTML = `
                <i class="flaticon-angle-right"></i>
                <p class="content">${highlight}</p>
            `;
            highlightsList.appendChild(li);
        }
    });
}

fetchAndPopulateHighlights(eventId);