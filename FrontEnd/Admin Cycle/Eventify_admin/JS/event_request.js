// event_request.js

$(document).ready(function() {
    // Initialize DataTable
    $('#requests-datatable').DataTable({
        lengthChange: false,
        buttons: ['copy', 'excel', 'pdf']
    });

    // Status filter
    $('#statusFilter').change(function() {
        var status = $(this).val();
        table.column(4).search(status).draw();
    });

    // Category filter
    $('#categoryFilter').change(function() {
        var category = $(this).val();
        table.column(2).search(category).draw();
    });
});

function approveEvent(eventId) {
    // API call to approve event
    if(confirm('Are you sure you want to approve this event?')) {
        console.log('Approving event:', eventId);
    }
}

function rejectEvent(eventId) {
    // API call to reject event
    if(confirm('Are you sure you want to reject this event? This will send a notification to the organizer.')) {
        console.log('Rejecting event:', eventId);
    }
}