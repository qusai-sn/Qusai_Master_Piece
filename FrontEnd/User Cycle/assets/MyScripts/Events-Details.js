// Tags for use with id's : 

//  <h2 class="title" id="Ticket_price">$19.00</h2>

//  <span id="Event_Date">26/10/2024</span>

//  <span id="Event_start_time">10.00am</span>

// <span id="Event_end_time">3.00pm</span>

// <span id="Event_Location">Amman</span>

// <span id="available_and_total_seats">43/80</span>

// <iframe id="location_url" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d216663.6087361063!2d35.689091839469256!3d31.950887765402538!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x151ca01f4813fa53%3A0xb305654d2c2663b2!2sMAP%20Architects%20%26%20Engineers!5e0!3m2!1sen!2sjo!4v1726659967578!5m2!1sen!2sjo" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>

// <a id="event_category" href="course.html">Development</a>

// <a id="event_type" href="course.html">Conference</a>

// <h2 class="title" id="event_title">Woman Business Conference 2019</h2>

// <a id="organizer_name" href="#">Andrew Tate</a>

// <p id="event_description">The Annual Business & Entrepreneurship Summit brings together innovators, entrepreneurs, and industry leaders to discuss the latest trends, challenges, and opportunities in the business world. This two-day event will offer a blend of keynote speeches, panel discussions, and interactive workshops designed to inspire and equip attendees with actionable insights and strategies for success.</p>


// this is the api respone from swagger when you request event by id : 

// {
//     "eventId": 1,
//     "title": "Annual Business Conference",
//     "description": "A comprehensive conference covering all aspects of business management and innovation.",
//     "date": "2024-10-05",
//     "startTime": "09:00:00",
//     "endTime": "17:00:00",
//     "location": "Convention Center, Downtown",
//     "totalSeats": 300,
//     "availableSeats": 300,
//     "ticketPrice": 200,
//     "categoryId": 1,
//     "typeId": 1,
//     "organizerId": 1,
//     "bannerUrl": "http://example.com/banner.jpg",
//     "category": {
//       "categoryId": 1,
//       "categoryName": "Business"
//     },
//     "organizer": {
//       "userId": 1,
//       "firstName": "Qusai",
//       "lastName": "Omar",
//       "email": "qusayomar20@gmail.com",
//       "phoneNumber": "1234567890",
//       "username": "qusaiOmar",
//       "biography": "Passionate about event planning and management."
//     },
//     "type": {
//       "typeId": 1,
//       "typeName": "Conference"
//     }
//   }

// the api url is in the format : https://localhost:7293/api/EventDetails/{id}
// the id is the id of the event you want to get details of

// make a fetch for an event with id=1 , then map the api response json data to the html tags above 
// change the content of the tag to the response data , by using js . 



// Fetch event details by ID
const eventId = 1;  // Event ID you want to fetch
const apiUrl = `https://localhost:7293/api/EventDetails/${eventId}`;

// Fetch the event details
fetch(apiUrl)
  .then(response => {
      if (!response.ok) {
          throw new Error('Network response was not ok');
      }
      return response.json();
  })
  .then(data => {
      // Map the API response to the HTML elements
      document.getElementById('Ticket_price').textContent = `$${data.ticketPrice}`;
      document.getElementById('Event_Date').textContent = new Date(data.date).toLocaleDateString();
      document.getElementById('Event_start_time').textContent = new Date(`1970-01-01T${data.startTime}`).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
      document.getElementById('Event_end_time').textContent = new Date(`1970-01-01T${data.endTime}`).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
      document.getElementById('Event_Location').textContent = data.location;
      document.getElementById('available_and_total_seats').textContent = `${data.availableSeats}/${data.totalSeats}`;
      document.getElementById('location_url').src = data.locationUrl;
      document.getElementById('event_category').textContent = data.category.categoryName;
      document.getElementById('event_type').textContent = data.type.typeName;
      document.getElementById('event_title').textContent = data.title;
      document.getElementById('organizer_name').textContent = `${data.organizer.firstName} ${data.organizer.lastName}`;
      document.getElementById('event_description').textContent = data.description;
      document.getElementById('event_banner').src = data.bannerUrl;
  })
  .catch(error => {
      console.error('There was a problem with the fetch operation:', error);
  });
