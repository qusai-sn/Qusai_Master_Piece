// Local storage keys
const STORAGE_KEYS = {
    EVENT_ID: 'current_event_id',
    OVERVIEW_COMPLETED: 'overview_completed',
    SPEAKERS_ADDED: 'speakers_added',
    SCHEDULE_ADDED: 'schedule_added',
    TICKETS_ADDED: 'tickets_added'
};

// Event progress tracking
const eventProgress = {
    initialize() {
        localStorage.removeItem(STORAGE_KEYS.SPEAKERS_ADDED);
        localStorage.removeItem(STORAGE_KEYS.SCHEDULE_ADDED);
        localStorage.removeItem(STORAGE_KEYS.TICKETS_ADDED);
    },

    setEventId(eventId) {
        localStorage.setItem(STORAGE_KEYS.EVENT_ID, eventId);
    },

    getEventId() {
        return localStorage.getItem(STORAGE_KEYS.EVENT_ID);
    },

    markStepComplete(step) {
        localStorage.setItem(step, 'true');
    },

    isStepCompleted(step) {
        return localStorage.getItem(step) === 'true';
    },

    isEventComplete() {
        return this.isStepCompleted(STORAGE_KEYS.OVERVIEW_COMPLETED) &&
               this.isStepCompleted(STORAGE_KEYS.SPEAKERS_ADDED) &&
               this.isStepCompleted(STORAGE_KEYS.SCHEDULE_ADDED) &&
               this.isStepCompleted(STORAGE_KEYS.TICKETS_ADDED);
    }
};

// Form Submissions
// async function submitOverview(event) {
//     event.preventDefault();
    
//     const overviewData = {
//         title: document.getElementById('eventTitle').value,
//         description: document.getElementById('overview').value,
//         eventDate: document.getElementById('eventDate').value,
//         startTime: document.getElementById('startTime').value,
//         endTime: document.getElementById('endTime').value,
//         location: document.getElementById('Location').value,
//         locationUrl: document.getElementById('Location-url').value, // Add proper map URL input
//         eventTypeId: parseInt(document.getElementById('eventType').value),
//         totalSeats: parseInt(document.getElementById('total-seats').value),
//         eventCategoryId: parseInt(document.getElementById('eventCategory').value),
//         organizerId: 2, // Replace with actual organizer ID
//         thumbnailUrl: "https://img.freepik.com/free-vector/gradient-business-banner-template_23-2149717823.jpg", // Add proper image handling
//         bannerUrl: "https://img.freepik.com/free-vector/gradient-business-banner-template_23-2149717823.jpg" // Add proper image handling
//     };

//     try {
//         const response = await fetch('https://localhost:7293/api/CreateEvent/overview', {
//             method: 'POST',
//             headers: { 'Content-Type': 'application/json' },
//             body: JSON.stringify(overviewData)
//         });

//         if (!response.ok) throw new Error('Failed to submit overview');
        
//         const result = await response.json();
//         if (result.success) {
//             eventProgress.setEventId(result.eventId);
//             eventProgress.markStepComplete(STORAGE_KEYS.OVERVIEW_COMPLETED);
            
//             showSuccess('Event overview saved successfully');
//             enableNextTab('itemTwo-tab');
//         }
//     } catch (error) {
//         showError('Failed to save event overview');
//         console.error(error);
//     }
// }


// async function submitOverview(event) {
//     event.preventDefault();
    
//     const formData = new FormData();
//     formData.append('title', document.getElementById('eventTitle').value);
//     formData.append('description', document.getElementById('overview').value);
//     formData.append('eventDate', document.getElementById('eventDate').value);
//     formData.append('startTime', document.getElementById('startTime').value);
//     formData.append('endTime', document.getElementById('endTime').value);
//     formData.append('location', document.getElementById('Location').value);
//     formData.append('locationUrl', document.getElementById('Location-url').value);
//     formData.append('eventTypeId', document.getElementById('eventType').value);
//     formData.append('totalSeats', document.getElementById('total-seats').value);
//     formData.append('eventCategoryId', document.getElementById('eventCategory').value);
//     formData.append('organizerId', '2');

//     const thumbnailFile = document.getElementById('Thumbnail').files[0];
//     const bannerFile = document.getElementById('banner').files[0];
//     formData.append('thumbnailFile', thumbnailFile);
//     formData.append('bannerFile', bannerFile);

//     try {
//         const response = await fetch('https://localhost:7293/api/CreateEvent/overview', {
//             method: 'POST',
//             body: formData
//         });

//         if (!response.ok) throw new Error('Failed to submit overview');
        
//         const result = await response.json();
//         if (result.success) {
//             eventProgress.setEventId(result.eventId);
//             eventProgress.markStepComplete(STORAGE_KEYS.OVERVIEW_COMPLETED);
//             showSuccess('Event overview saved successfully');
//             enableNextTab('itemTwo-tab');
//         }
//     } catch (error) {
//         showError('Failed to save event overview');
//         console.error(error);
//     }
// }

async function submitOverview(event) {
    event.preventDefault();
    
    const formData = new FormData();
    formData.append('Title', document.getElementById('eventTitle').value);
    formData.append('Description', document.getElementById('overview').value);
    formData.append('Highlights', document.getElementById('Highlights').value);
    formData.append('WhatToExpect', document.getElementById('What-to-Expect').value);
    formData.append('EventDate', document.getElementById('eventDate').value);
    formData.append('StartTime', document.getElementById('startTime').value);
    formData.append('EndTime', document.getElementById('endTime').value);
    formData.append('Location', document.getElementById('Location').value);
    formData.append('LocationUrl', document.getElementById('Location-url').value);
    formData.append('EventTypeId', document.getElementById('eventType').value);
    formData.append('TotalSeats', document.getElementById('total-seats').value);
    formData.append('EventCategoryId', document.getElementById('eventCategory').value);
    formData.append('OrganizerId', '2');
    formData.append('ThumbnailFile', document.getElementById('Thumbnail').files[0]);
    formData.append('BannerFile', document.getElementById('banner').files[0]);

    try {
        const response = await fetch('https://localhost:7293/api/CreateEvent/overview', {
            method: 'POST',
            body: formData
        });

        if (!response.ok) throw new Error('Failed to submit overview');
        
        const result = await response.json();
        if (result.success) {
            eventProgress.setEventId(result.eventId);
            eventProgress.markStepComplete(STORAGE_KEYS.OVERVIEW_COMPLETED);
            showSuccess('Event overview saved successfully');
            enableNextTab('itemTwo-tab');
        }
    } catch (error) {
        showError('Failed to save event overview');
        console.error(error);
    }
}
 

// Single form submission handler
async function handleSpeakerSubmit(event) {
    event.preventDefault();
    
    // Check for event ID first
    const eventId = eventProgress.getEventId();
    if (!eventId) {
        showError('Please complete event overview first');
        return;
    }

    // Create FormData object
    const formData = new FormData();
 
    
    // Add all form fields to FormData
    formData.append('EventId', eventId);
    formData.append('Name', document.getElementById('speakerName').value);
    formData.append('Role', document.getElementById('speakerRole').value);
    formData.append('Bio', document.getElementById('speakerBio').value);
   

    try {
        const response = await fetch('https://localhost:7293/api/CreateEvent/speaker', {
            method: 'POST',
            body: formData
        });

        const result = await response.json();
        
        if (!response.ok) {
            throw new Error(result.message || 'Failed to add speaker');
        }
        
        if (result.success) {
            eventProgress.markStepComplete(STORAGE_KEYS.SPEAKERS_ADDED);
            showSuccess('Speaker added successfully');
            enableNextTab('itemThree-tab');
            clearSpeakerForm();
        } else {
            throw new Error(result.message || 'Failed to add speaker');
        }
    } catch (error) {
        showError(error.message || 'Failed to add speaker');
        console.error('Error:', error);
    }
}

async function submitSchedule(event) {
    event.preventDefault();
    
    const eventId = eventProgress.getEventId();
    if (!eventId) {
        showError('Please complete previous steps first');
        return;
    }

    const sessionData = {
        eventId: parseInt(eventId),
        sessionTitle: document.getElementById('activityTitle').value,
        sessionDescription: document.getElementById('Description').value,
        sessionTime: `${document.getElementById('startTime').value} - ${document.getElementById('endTime').value}`,
        sessionType: "Morning" // Add proper session type selection
    };

    try {
        const response = await fetch('https://localhost:7293/api/CreateEvent/session', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(sessionData)
        });

        if (!response.ok) throw new Error('Failed to add session');

        const result = await response.json();
        if (result.success) {
            eventProgress.markStepComplete(STORAGE_KEYS.SCHEDULE_ADDED);
            showSuccess('Session added successfully');
            enableNextTab('itemFour-tab');
            clearSessionForm();
        }
    } catch (error) {
        showError('Failed to add session');
        console.error(error);
    }
}

async function submitTickets(event) {
    event.preventDefault();
    
    const eventId = eventProgress.getEventId();
    if (!eventId) {
        showError('Please complete previous steps first');
        return;
    }

    const ticketData = {
        eventId: parseInt(eventId),
        ticketType: "Paid",
        price: parseFloat(document.getElementById('price').value),
        donationLimit: parseFloat(100),
        totalSeats: parseInt(document.getElementById('total-seats').value)
    };

    try {
        const response = await fetch('https://localhost:7293/api/CreateEvent/tickets', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(ticketData)
        });

        if (!response.ok) throw new Error('Failed to add tickets');

        const result = await response.json();
        if (result.success) {
            eventProgress.markStepComplete(STORAGE_KEYS.TICKETS_ADDED);
            showSuccess('Tickets added successfully');
            enableCreateEventButton();
        }
    } catch (error) {
        showError('Failed to add tickets');
        console.error(error);
    }
}

async function finalizeEvent() {
    const eventId = localStorage.getItem('current_event_id');
    
    if (!eventId || !eventProgress.isEventComplete()) {
        showError('Please complete all steps first');
        return;
    }

    try {
        const response = await fetch(`https://localhost:7293/api/CreateEvent/finalize/${eventId}`, {
            method: 'POST'
        });

        if (!response.ok) throw new Error('Failed to finalize event');

        const result = await response.json();
        if (result.success) {
            showSuccess('Event created successfully!');
            eventProgress.initialize();
            window.location.href = `instructor-courses.html`;
        }
    } catch (error) {
        showError('Failed to create event');
        console.error(error);
    }
}

// UI Helper Functions
function showSuccess(message) {
    // Implement using your preferred notification system
    alert(message); // Replace with better UI
}

function showError(message) {
    // Implement using your preferred notification system
    alert(message); // Replace with better UI
}

function enableNextTab(tabId) {
    const nextTab = document.getElementById(tabId);
    if (nextTab) {
        nextTab.classList.remove('disabled');
        // Using Bootstrap's tab API
        const tab = new bootstrap.Tab(nextTab);
        tab.show();
    }
}

function clearSpeakerForm() {
    document.getElementById('speakerName').value = '';
    document.getElementById('speakerRole').value = '';
    document.getElementById('speakerBio').value = '';
}

function clearSessionForm() {
    document.getElementById('activityTitle').value = '';
    document.getElementById('Description').value = '';
    document.getElementById('startTime').value = '';
    document.getElementById('endTime').value = '';
}

function enableCreateEventButton() {
    const createBtn = document.querySelector('.btn-two.arrow-btn');
    if (createBtn) {
        createBtn.disabled = false;
    }
}

// Initialize on page load
document.addEventListener('DOMContentLoaded', () => {
    eventProgress.initialize();
    
    // Add form submit handlers
    document.querySelector('.instructor__profile-form')?.addEventListener('submit', submitOverview);
    document.getElementById('speakerForm')?.addEventListener('submit', handleSpeakerSubmit);
    document.getElementById('scheduleForm')?.addEventListener('submit', submitSchedule);
    document.getElementById('ticketsForm')?.addEventListener('submit', submitTickets);
    document.getElementById('final-submit')?.addEventListener('click', finalizeEvent);
});