// Function to fetch and display event requests
async function fetchEventRequests() {
    try {
        const response = await fetch('https://localhost:7293/api/Events/pending');
        if (!response.ok) throw new Error('Failed to fetch requests');
        const requests = await response.json();

        const tbody = document.querySelector('#datatable tbody');
        tbody.innerHTML = '';

        requests.forEach(request => {
            const tr = document.createElement('tr');
            tr.innerHTML = `
                <td>${request.title}</td>
                <td>${request.organizer}</td>
                <td>${new Date(request.submissionDate).toLocaleDateString()}</td>
                <td>${request.category}</td>
                <td><span class="badge badge-warning">Pending</span></td>
                <td>
                    <button class="btn btn-sm btn-info" onclick="showEventDetails(${request.eventId})" data-toggle="modal" data-target="#reviewModal">
                        <i class="mdi mdi-eye"></i>
                    </button>
                    <button class="btn btn-sm btn-success" onclick="approveEvent(${request.eventId})">
                        <i class="mdi mdi-check"></i>
                    </button>
                    <button class="btn btn-sm btn-danger" onclick="rejectEvent(${request.eventId})">
                        <i class="mdi mdi-close"></i>
                    </button>
                </td>
            `;
            tbody.appendChild(tr);
        });

        // Initialize DataTable
        $('#datatable').DataTable({
            lengthChange: false,
            buttons: ['copy', 'excel', 'pdf']
        });

    } catch (error) {
        console.error('Error:', error);
        showAlert('Error loading event requests', 'error');
    }
}

async function showEventDetails(eventId) {
    try {
        const response = await fetch(`https://localhost:44324/api/EventDetails/${eventId}`);
        if (!response.ok) throw new Error('Failed to fetch event details');

        const event = await response.json();

        // Update modal content
        document.querySelector('#reviewModal .modal-body').innerHTML = `
            <div class="row">
                <div class="col-md-6">
                    <h5>Event Details</h5>
                    <div class="form-group">
                        <label>Title:</label>
                        <p>${event.title}</p>
                    </div>
                    <div class="form-group">
                        <label>Description:</label>
                        <p>${event.description}</p>
                    </div>
                    <div class="form-group">
                        <label>Date:</label>
                        <p>${new Date(event.eventDate).toLocaleDateString()}</p>
                    </div>
                    <div class="form-group">
                        <label>Location:</label>
                        <p>${event.location}</p>
                    </div>
                    <div class="form-group">
                        <label>Ticket Price:</label>
                        <p>$${event.ticketPrice}</p>
                    </div>
                </div>
                <div class="col-md-6">
                    <h5>Organizer Details</h5>
                    <div class="form-group">
                        <label>Name:</label>
                        <p>${event.organizer}</p>
                    </div>
                    <div class="form-group">
                        <label>Email:</label>
                        <p>${event.organizerEmail}</p>
                    </div>
                </div>
            </div>
            <div class="form-group mt-4">
                <label>Admin Notes</label>
                <textarea class="form-control" id="adminNotes" rows="3" placeholder="Add notes or feedback for the organizer..."></textarea>
            </div>
        `;
    } catch (error) {
        console.error('Error:', error);
        showAlert('Error loading event details', 'error');
    }
}

async function approveEvent(eventId) {
    if (!confirm('Are you sure you want to approve this event?')) return;

    try {
        const notes = document.getElementById('adminNotes')?.value || '';
        const response = await fetch(`https://localhost:7293/api/Events/approve/${eventId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ notes })
        });

        if (!response.ok) throw new Error('Failed to approve event');

        showAlert('Event approved successfully', 'success');
        $('#reviewModal').modal('hide');
        fetchEventRequests();

    } catch (error) {
        console.error('Error:', error);
        showAlert('Error approving event', 'error');
    }
}

async function rejectEvent(eventId) {
    const notes = document.getElementById('adminNotes')?.value;
    if (!notes) {
        showAlert('Please provide rejection reason in notes', 'error');
        return;
    }

    if (!confirm('Are you sure you want to reject this event?')) return;

    try {
        const response = await fetch(`https://localhost:7293/api/Events/reject/${eventId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ notes })
        });

        if (!response.ok) throw new Error('Failed to reject event');

        showAlert('Event rejected successfully', 'success');
        $('#reviewModal').modal('hide');
        fetchEventRequests();

    } catch (error) {
        console.error('Error:', error);
        showAlert('Error rejecting event', 'error');
    }
}

function showAlert(message, type) {
    const alertDiv = document.createElement('div');
    alertDiv.className = `alert alert-${type === 'success' ? 'success' : 'danger'} alert-dismissible fade show`;
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
document.addEventListener('DOMContentLoaded', fetchEventRequests);