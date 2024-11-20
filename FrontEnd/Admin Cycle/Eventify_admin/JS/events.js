// Function to load all events
async function loadEvents() {
    try {
        const response = await fetch('https://localhost:7293/api/Events');
        const events = await response.json();
        
        const table = $('#datatable').DataTable({
            data: events,
            columns: [
                { data: 'title' },
                { data: 'description' },
                { data: 'eventDate', render: data => new Date(data).toLocaleDateString() },
                { data: 'location' },
                { 
                    data: 'status',
                    render: data => `<span class="badge badge-${getBadgeClass(data)}">${data}</span>`
                },
                {
                    data: 'eventId',
                    render: function(data) {
                        return `
                            <button class="btn btn-sm btn-info" onclick="editEvent(${data})" data-toggle="modal" data-target="#eventModal">
                                <i class="mdi mdi-pencil"></i>
                            </button>
                            <button class="btn btn-sm btn-danger" onclick="showDeleteModal(${data})" data-toggle="modal" data-target="#deleteModal">
                                <i class="mdi mdi-trash-can"></i>
                            </button>
                        `;
                    }
                }
            ],
            lengthChange: false,
            buttons: ['copy', 'excel', 'pdf']
        });

        table.buttons().container().appendTo('#datatable_wrapper .col-md-6:eq(0)');
    } catch (error) {
        console.error('Error loading events:', error);
        showAlert('Error loading events', 'error');
    }
}

// Load categories and types for dropdowns
async function loadFormData() {
    try {
        const [categoriesRes, typesRes, plansRes] = await Promise.all([
            fetch('https://localhost:7293/api/Categories'),
            fetch('https://localhost:7293/api/EventTypes'),
            fetch('https://localhost:7293/api/Plans')
        ]);

        const categories = await categoriesRes.json();
        const types = await typesRes.json();
        const plans = await plansRes.json();

        const categorySelect = document.querySelector('select[name="categoryId"]');
        const typeSelect = document.querySelector('select[name="typeId"]');
        const planSelect = document.querySelector('select[name="planId"]');

        categories.forEach(category => {
            categorySelect.add(new Option(category.categoryName, category.categoryId));
        });

        types.forEach(type => {
            typeSelect.add(new Option(type.typeName, type.typeId));
        });

        plans.forEach(plan => {
            planSelect.add(new Option(plan.planName, plan.planId));
        });
    } catch (error) {
        console.error('Error loading form data:', error);
    }
}

// Edit event
async function editEvent(eventId) {
    try {
        const response = await fetch(`https://localhost:7293/api/Events/${eventId}`);
        const event = await response.json();

        // Populate form
        const form = document.getElementById('eventForm');
        Object.keys(event).forEach(key => {
            const input = form.elements[key];
            if (input) {
                if (input.type === 'date') {
                    input.value = event[key].split('T')[0];
                } else {
                    input.value = event[key];
                }
            }
        });

        currentEventId = eventId;
    } catch (error) {
        console.error('Error loading event:', error);
        showAlert('Error loading event details', 'error');
    }
}

// Save event
async function saveEvent(formData) {
    const method = currentEventId ? 'PUT' : 'POST';
    const url = currentEventId 
        ? `https://localhost:7293/api/Events/${currentEventId}`
        : 'https://localhost:7293/api/Events';

    try {
        const response = await fetch(url, {
            method: method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(Object.fromEntries(formData))
        });

        if (!response.ok) throw new Error('Failed to save event');

        $('#eventModal').modal('hide');
        $('#datatable').DataTable().ajax.reload();
        showAlert('Event saved successfully', 'success');
    } catch (error) {
        console.error('Error saving event:', error);
        showAlert('Error saving event', 'error');
    }
}

// Delete event
async function deleteEvent(eventId) {
    try {
        const response = await fetch(`https://localhost:7293/api/Events/${eventId}`, {
            method: 'DELETE'
        });

        if (!response.ok) throw new Error('Failed to delete event');

        $('#deleteModal').modal('hide');
        $('#datatable').DataTable().ajax.reload();
        showAlert('Event deleted successfully', 'success');
    } catch (error) {
        console.error('Error deleting event:', error);
        showAlert('Error deleting event', 'error');
    }
}

// Utility functions
function getBadgeClass(status) {
    const classes = {
        'Active': 'success',
        'Draft': 'warning',
        'Cancelled': 'danger',
        'Pending': 'info'
    };
    return classes[status] || 'secondary';
}

function showAlert(message, type) {
    const alertDiv = document.createElement('div');
    alertDiv.className = `alert alert-${type} alert-dismissible fade show`;
    alertDiv.role = 'alert';
    alertDiv.style.position = 'fixed';
    alertDiv.style.top = '20px';
    alertDiv.style.right = '20px';
    alertDiv.style.zIndex = '1000';
    
    alertDiv.innerHTML = `
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    `;

    document.body.appendChild(alertDiv);
    setTimeout(() => alertDiv.remove(), 3000);
}

// Initialize
document.addEventListener('DOMContentLoaded', () => {
    loadEvents();
    loadFormData();

    // Form submission handler
    document.getElementById('eventForm').addEventListener('submit', (e) => {
        e.preventDefault();
        const formData = new FormData(e.target);
        saveEvent(formData);
    });

    // Delete confirmation handler
    document.getElementById('confirmDelete').addEventListener('click', () => {
        if (currentEventId) {
            deleteEvent(currentEventId);
        }
    });
});