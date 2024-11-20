async function initializeTicketsTable() {
    try {
        const response = await fetch('https://localhost:7293/api/Tickets/admin');
        if (!response.ok) throw new Error('Failed to fetch tickets');
        const tickets = await response.json();

        $('#datatable').DataTable({
            data: tickets,
            columns: [
                { data: 'ticketId' },
                { 
                    data: 'eventId',
                    render: (data, type, row) => `<a href="event-details.html?id=${data}">${row.eventTitle || 'Event ' + data}</a>`
                },
                { 
                    data: 'userId',
                    render: (data, type, row) => `${row.userName || 'User ' + data}`
                },
                { data: 'ticketType' },
                { 
                    data: 'purchaseDate',
                    render: data => new Date(data).toLocaleDateString()
                },
                { 
                    data: 'price',
                    render: data => `$${parseFloat(data).toFixed(2)}`
                },
                {
                    data: null,
                    render: (data) => {
                        const statusClass = {
                            'Active': 'success',
                            'Used': 'info',
                            'Cancelled': 'danger'
                        }[data.status] || 'secondary';
                        return `<span class="badge badge-${statusClass}">${data.status}</span>`;
                    }
                },
                {
                    data: null,
                    render: (data) => `
                        <div class="btn-group">
                            <button class="btn btn-sm btn-info waves-effect waves-light" onclick="editTicket(${data.ticketId})">
                                <i class="mdi mdi-pencil"></i>
                            </button>
                            <button class="btn btn-sm btn-danger waves-effect waves-light" onclick="deleteTicket(${data.ticketId})">
                                <i class="mdi mdi-trash-can-outline"></i>
                            </button>
                            <button class="btn btn-sm btn-success waves-effect waves-light" onclick="viewQRCode('${data.qrCode}')">
                                <i class="mdi mdi-qrcode"></i>
                            </button>
                        </div>`
                }
            ],
            responsive: true,
            order: [[0, 'desc']],
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
    const today = new Date().toDateString();

    const stats = {
        total: tickets.length,
        active: tickets.filter(t => t.status === 'Active').length,
        revenue: tickets.reduce((sum, t) => sum + (t.price || 0), 0),
        todaySales: tickets.filter(t => new Date(t.purchaseDate).toDateString() === today).length
    };

    document.getElementById('totalTickets').textContent = stats.total;
    document.getElementById('activeTickets').textContent = stats.active;
    document.getElementById('totalRevenue').textContent = `$${stats.revenue.toFixed(2)}`;
    document.getElementById('todaySales').textContent = stats.todaySales;
}

async function saveTicket() {
    const form = document.getElementById('ticketForm');
    const data = new FormData(form);
    const ticket = Object.fromEntries(data.entries());

    try {
        const url = ticket.id ? 
            `https://localhost:7293/api/Tickets/${ticket.id}` : 
            'https://localhost:7293/api/Tickets';
            
        const response = await fetch(url, {
            method: ticket.id ? 'PUT' : 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(ticket)
        });

        if (!response.ok) throw new Error('Failed to save ticket');

        $('#ticketModal').modal('hide');
        $('#datatable').DataTable().ajax.reload();
        showAlert(`Ticket ${ticket.id ? 'updated' : 'created'} successfully`, 'success');
    } catch (error) {
        console.error('Error:', error);
        showAlert('Failed to save ticket', 'danger');
    }
}

async function deleteTicket(id) {
    if (!confirm('Are you sure you want to delete this ticket?')) return;

    try {
        const response = await fetch(`https://localhost:7293/api/Tickets/${id}`, {
            method: 'DELETE'
        });

        if (!response.ok) throw new Error('Failed to delete ticket');

        $('#datatable').DataTable().ajax.reload();
        showAlert('Ticket deleted successfully', 'success');
    } catch (error) {
        console.error('Error:', error);
        showAlert('Failed to delete ticket', 'danger');
    }
}

async function editTicket(id) {
    try {
        const response = await fetch(`https://localhost:7293/api/Tickets/${id}`);
        if (!response.ok) throw new Error('Failed to fetch ticket details');
        const ticket = await response.json();

        const form = document.getElementById('ticketForm');
        Object.keys(ticket).forEach(key => {
            const input = form.querySelector(`[name="${key}"]`);
            if (input) input.value = ticket[key];
        });

        $('#ticketModal').modal('show');
    } catch (error) {
        console.error('Error:', error);
        showAlert('Failed to load ticket details', 'danger');
    }
}

function viewQRCode(qrCode) {
    document.getElementById('qrImage').src = qrCode;
    $('#qrModal').modal('show');
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

document.addEventListener('DOMContentLoaded', () => {
    initializeTicketsTable();
    
    document.getElementById('ticketForm').addEventListener('submit', (e) => {
        e.preventDefault();
        saveTicket();
    });

    $('#ticketModal').on('hidden.bs.modal', () => {
        document.getElementById('ticketForm').reset();
    });
});