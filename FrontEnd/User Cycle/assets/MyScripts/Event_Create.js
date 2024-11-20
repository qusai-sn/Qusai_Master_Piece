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
        localStorage.removeItem(STORAGE_KEYS.EVENT_ID);
        localStorage.removeItem(STORAGE_KEYS.OVERVIEW_COMPLETED);
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

// Form handlers
async function submitOverview(event) {
    event.preventDefault();
    
    const overviewData = {
        title: document.getElementById('eventTitle').value,
        description: document.getElementById('eventOverview').value,
        eventDate: document.getElementById('eventDate').value,
        startTime: document.getElementById('startTime').value,
        endTime: document.getElementById('endTime').value,
        city: document.getElementById('city').value,
        location: document.getElementById('location').value,
        eventType: document.getElementById('eventType').value,
        eventCategory: document.getElementById('eventCategory').value
    };

    try {
        const response = await fetch('api/Event/overview', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(overviewData)
        });

        if (!response.ok) throw new Error('Failed to submit overview');
        
        const { eventId } = await response.json();
        
        // Store event ID and mark overview as complete
        eventProgress.setEventId(eventId);
        eventProgress.markStepComplete(STORAGE_KEYS.OVERVIEW_COMPLETED);
        
        // Show success and enable next tab
        showSuccess('Event overview saved successfully');
        enableTab('speakers-tab');
        switchTab('speakers');
    } catch (error) {
        showError('Failed to save event overview');
        console.error(error);
    }
}

async function submitSpeaker(event) {
    event.preventDefault();
    
    const eventId = eventProgress.getEventId();
    if (!eventId) {
        showError('Please complete event overview first');
        return;
    }

    const speakerData = {
        eventId: eventId,
        name: document.getElementById('speakerName').value,
        role: document.getElementById('speakerRole').value,
        biography: document.getElementById('speakerBiography').value
    };

    try {
        const response = await fetch('api/Event/speaker', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(speakerData)
        });

        if (!response.ok) throw new Error('Failed to add speaker');

        // Update speakers list display
        await updateSpeakersList(eventId);
        
        // Clear form and mark speakers as added
        clearSpeakerForm();
        eventProgress.markStepComplete(STORAGE_KEYS.SPEAKERS_ADDED);
        
        showSuccess('Speaker added successfully');
        enableTab('schedule-tab');
    } catch (error) {
        showError('Failed to add speaker');
        console.error(error);
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
        eventId: eventId,
        activityTitle: document.getElementById('activityTitle').value,
        activityDescription: document.getElementById('activityDescription').value,
        startTime: document.getElementById('sessionStartTime').value,
        endTime: document.getElementById('sessionEndTime').value
    };

    try {
        const response = await fetch('api/Event/session', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(sessionData)
        });

        if (!response.ok) throw new Error('Failed to add session');

        // Update sessions list display
        await updateSessionsList(eventId);
        
        // Clear form and mark schedule as added
        clearSessionForm();
        eventProgress.markStepComplete(STORAGE_KEYS.SCHEDULE_ADDED);
        
        showSuccess('Session added successfully');
        enableTab('tickets-tab');
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
        eventId: eventId,
        ticketType: document.getElementById('ticketType').value,
        price: document.getElementById('price').value,
        donationLimit: document.getElementById('donationLimit').value
    };

    try {
        const response = await fetch('api/Event/tickets', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(ticketData)
        });

        if (!response.ok) throw new Error('Failed to add tickets');

        // Mark tickets as added and event as complete
        eventProgress.markStepComplete(STORAGE_KEYS.TICKETS_ADDED);
        
        showSuccess('Tickets added successfully');
        enableCreateEventButton();
    } catch (error) {
        showError('Failed to add tickets');
        console.error(error);
    }
}

async function finalizeEvent() {
    const eventId = eventProgress.getEventId();
    if (!eventId || !eventProgress.isEventComplete()) {
        showError('Please complete all steps first');
        return;
    }

    try {
        const response = await fetch(`api/Event/${eventId}/finalize`, {
            method: 'POST'
        });

        if (!response.ok) throw new Error('Failed to finalize event');

        showSuccess('Event created successfully!');
        eventProgress.initialize(); // Clear local storage
        window.location.href = `/event/${eventId}`; // Redirect to event page
    } catch (error) {
        showError('Failed to create event');
        console.error(error);
    }
}

// UI Helpers
function showSuccess(message) {
    // Implement success message display
}

function showError(message) {
    // Implement error message display
}

function enableTab(tabId) {
    const tab = document.getElementById(tabId);
    if (tab) {
        tab.classList.remove('disabled');
    }
}

function switchTab(tabName) {
    // Implement tab switching logic
}

function enableCreateEventButton() {
    const createBtn = document.getElementById('createEventBtn');
    if (createBtn) {
        createBtn.disabled = false;
    }
}

// Initialize on page load
document.addEventListener('DOMContentLoaded', () => {
    // Initialize new event creation
    eventProgress.initialize();
    
    // Add form submit handlers
    document.getElementById('eventOverviewForm')?.addEventListener('submit', submitOverview);
    document.getElementById('speakerForm')?.addEventListener('submit', submitSpeaker);
    document.getElementById('scheduleForm')?.addEventListener('submit', submitSchedule);
    document.getElementById('ticketsForm')?.addEventListener('submit', submitTickets);
    document.getElementById('createEventBtn')?.addEventListener('click', finalizeEvent);
}); 