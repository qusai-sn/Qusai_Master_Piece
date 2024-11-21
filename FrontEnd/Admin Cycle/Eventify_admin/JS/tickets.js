async function initializeTicketsTable() {
    try {
        const userId = getUserIdFromSession(); // Implement this based on your auth system
        const response = await fetch(`https://localhost:7293/api/Tickets/user/${userId}`);
        if (!response.ok) throw new Error('Failed to fetch tickets');
        const tickets = await response.json();

        $('#datatable').DataTable({
            data: tickets,
            columns: [
                {
                    data: 'eventTitle',
                    render: data => data || 'N/A'
                },
                {
                    data: 'category',
                    render: data => data || 'N/A'
                },
                {
                    data: 'eventDate',
                    render: data => formatDate(data)
                },
                {
                    data: 'startTime',
                    render: data => formatTime(data)
                },
                {
                    data: 'purchaseDate',
                    render: data => new Date(data).toLocaleDateString()
                },
                {
                    data: 'price',
                    render: data => `$${parseFloat(data).toFixed(2)}`
                },
                {
                    data: 'status',
                    render: (data) => {
                        const statusClass = {
                            'Active': 'success',
                            'Upcoming': 'info',
                            'Expired': 'warning'
                        }[data] || 'secondary';
                        return `<span class="badge badge-${statusClass}">${data}</span>`;
                    }
                }
            ],
            responsive: true,
            order: [[2, 'asc']], // Sort by event date
            dom: 'Bfrtip',
            buttons: [
                'copy', 'csv', 'excel', 'pdf', 'print'
            ]
        });

        updateStats(tickets);
    } catch (error) {
        console.error('Error:', error);
        showAlert('Failed to load tickets data', 'danger');
    }
}

function updateStats(tickets) {
    const today = DateOnly.FromDateTime(new Date());

    const stats = {
        total: tickets.length,
        upcoming: tickets.filter(t => t.status === 'Upcoming').length,
        attended: tickets.filter(t => t.status === 'Expired').length
    };

    document.getElementById('totalTickets').textContent = stats.total;
    document.getElementById('upcomingTickets').textContent = stats.upcoming;
    document.getElementById('attendedEvents').textContent = stats.attended;
}

// Helper function to format date from DateOnly
function formatDate(dateString) {
    if (!dateString) return 'N/A';
    const [year, month, day] = dateString.split('-');
    return new Date(year, month - 1, day).toLocaleDateString();
}

// Helper function to format time from TimeOnly
function formatTime(timeString) {
    if (!timeString) return 'N/A';
    return timeString;
}

function showAlert(message, type) {
    $.toast({
        heading: type === 'success' ? 'Success' : 'Error',
        text: message,
        showHideTransition: 'slide',
        icon: type,
        position: 'top-right',
        hideAfter: 3000
    });
}

// Function to get user ID from session/local storage
function getUserIdFromSession() {
    // Implement based on your authentication system
    return localStorage.getItem('userId') || sessionStorage.getItem('userId');
}

document.addEventListener('DOMContentLoaded', () => {
    initializeTicketsTable();
});