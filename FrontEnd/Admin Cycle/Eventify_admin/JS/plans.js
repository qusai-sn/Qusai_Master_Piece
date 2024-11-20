$(document).ready(function() {
    // Initialize DataTable
    $('#plans-datatable').DataTable({
        lengthChange: false,
        buttons: ['copy', 'excel', 'pdf'],
        ajax: {
            url: 'https://localhost:7293/api/Plans',
            dataSrc: ''
        },
        columns: [
            { data: 'planId' },
            { data: 'planName' },
            { data: 'durationInDays' },
            { data: 'commissionAmount' },
            { 
                data: 'planId',
                render: function(data) {
                    return `
                        <button class="btn btn-sm btn-info" onclick="editPlan(${data})">
                            <i class="mdi mdi-pencil"></i>
                        </button>
                        <button class="btn btn-sm btn-danger" onclick="deletePlan(${data})">
                            <i class="mdi mdi-close"></i>
                        </button>`;
                }
            }
        ]
    });
});

function editPlan(planId) {
    if(confirm('Are you sure you want to edit this plan?')) {
        $.ajax({
            url: `https://localhost:7293/api/Plans/${planId}`,
            method: 'GET',
            success: function(response) {
                $('#planModal').modal('show');
                populatePlanForm(response);
            },
            error: function() {
                showAlert('Error loading plan details', 'error');
            }
        });
    }
}

function deletePlan(planId) {
    if(confirm('Are you sure you want to delete this plan? This action cannot be undone.')) {
        $.ajax({
            url: `https://localhost:7293/api/Plans/${planId}`,
            method: 'DELETE',
            success: function() {
                $('#plans-datatable').DataTable().ajax.reload();
                showAlert('Plan deleted successfully', 'success');
            },
            error: function() {
                showAlert('Error deleting plan', 'error');
            }
        });
    }
}

function populatePlanForm(plan) {
    $('#planForm input[name="planName"]').val(plan.planName);
    $('#planForm input[name="durationInDays"]').val(plan.durationInDays);
    $('#planForm input[name="commissionAmount"]').val(plan.commissionAmount);
}

$('#savePlan').click(function() {
    const planData = {
        planName: $('#planForm input[name="planName"]').val(),
        durationInDays: parseInt($('#planForm input[name="durationInDays"]').val()),
        commissionAmount: parseFloat($('#planForm input[name="commissionAmount"]').val())
    };

    $.ajax({
        url: 'https://localhost:7293/api/Plans',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(planData),
        success: function() {
            $('#planModal').modal('hide');
            $('#plans-datatable').DataTable().ajax.reload();
            showAlert('Plan saved successfully', 'success');
        },
        error: function() {
            showAlert('Error saving plan', 'error');
        }
    });
});

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