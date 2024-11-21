// Function to fetch and display categories and types
async function initializeTables() {
    try {
        const categoryResponse = await fetch('https://localhost:44324/api/CategoryType/categories', {
            method: 'GET',
            headers: {
                'Accept': 'application/json'
            }
        });

        const typeResponse = await fetch('https://localhost:44324/api/CategoryType/types', {
            method: 'GET',
            headers: {
                'Accept': 'application/json'
            }
        });

        if (!categoryResponse.ok || !typeResponse.ok) {
            throw new Error('Failed to fetch data');
        }

        // Parse JSON and handle $values property
        const categories = (await categoryResponse.json()).$values || [];
        const types = (await typeResponse.json()).$values || [];

        if (!Array.isArray(categories) || !Array.isArray(types)) {
            throw new Error('Invalid response format');
        }

        populateCategoryTable(categories);
        populateTypeTable(types);

        // Initialize DataTables
        $('#categoryTable').DataTable({
            lengthChange: false,
            buttons: ['copy', 'excel', 'pdf']
        });

        $('#typeTable').DataTable({
            lengthChange: false,
            buttons: ['copy', 'excel', 'pdf']
        });

    } catch (error) {
        console.error('Error:', error.message);
        showAlert('Error loading data', 'error');
    }
}

// Function to populate category table
function populateCategoryTable(categories) {
    const tbody = document.querySelector('#categoryTable tbody');
    tbody.innerHTML = '';

    categories.forEach(category => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
            <td>${category.categoryId}</td>
            <td>${category.categoryName}</td>
            <td>
                <button class="btn btn-sm btn-info" onclick="editCategory(${category.categoryId})" data-toggle="modal" data-target="#categoryModal">
                    <i class="mdi mdi-pencil"></i>
                </button>
                <button class="btn btn-sm btn-danger" onclick="deleteCategory(${category.categoryId})">
                    <i class="mdi mdi-trash-can-outline"></i>
                </button>
            </td>
        `;
        tbody.appendChild(tr);
    });
}

// Function to populate type table
function populateTypeTable(types) {
    const tbody = document.querySelector('#typeTable tbody');
    tbody.innerHTML = '';

    types.forEach(type => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
            <td>${type.typeId}</td>
            <td>${type.typeName}</td>
            <td>
                <button class="btn btn-sm btn-info" onclick="editType(${type.typeId})" data-toggle="modal" data-target="#typeModal">
                    <i class="mdi mdi-pencil"></i>
                </button>
                <button class="btn btn-sm btn-danger" onclick="deleteType(${type.typeId})">
                    <i class="mdi mdi-trash-can-outline"></i>
                </button>
            </td>
        `;
        tbody.appendChild(tr);
    });
}

// Category CRUD Functions
async function editCategory(categoryId) {
    try {
        const response = await fetch(`https://localhost:44324/api/CategoryType/categories/${categoryId}`);
        if (!response.ok) throw new Error('Failed to fetch category details');
        
        const category = await response.json();
        
        document.querySelector('#categoryForm input[name="categoryId"]').value = category.categoryId;
        document.querySelector('#categoryForm input[name="categoryName"]').value = category.categoryName;
    } catch (error) {
        console.error('Error:', error);
        showAlert('Error loading category details', 'error');
    }
}

async function saveCategory() {
    const form = document.getElementById('categoryForm');
    const formData = new FormData(form);
    const categoryData = {
        categoryId: formData.get('categoryId'),
        categoryName: formData.get('categoryName')
    };

    try {
        const url = categoryData.categoryId ? 
            `https://localhost:44324/api/CategoryType/categories/${categoryData.categoryId}` :
            'https://localhost:44324/api/CategoryType/categories';

        const response = await fetch(url, {
            method: categoryData.categoryId ? 'PUT' : 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(categoryData)
        });

        if (!response.ok) throw new Error('Failed to save category');

        $('#categoryModal').modal('hide');
        showAlert('Category saved successfully', 'success');
        initializeTables();
    } catch (error) {
        console.error('Error:', error);
        showAlert('Error saving category', 'error');
    }
}

async function deleteCategory(categoryId) {
    if (!confirm('Are you sure you want to delete this category?')) return;

    try {
        const response = await fetch(`https://localhost:44324/api/CategoryType/categories/${categoryId}`, {
            method: 'DELETE'
        });

        if (!response.ok) throw new Error('Failed to delete category');
        
        showAlert('Category deleted successfully', 'success');
        initializeTables();
    } catch (error) {
        console.error('Error:', error);
        showAlert('Error deleting category', 'error');
    }
}

// Type CRUD Functions
async function editType(typeId) {
    try {
        const response = await fetch(`https://localhost:44324/api/CategoryType/types/${typeId}`);
        if (!response.ok) throw new Error('Failed to fetch type details');
        
        const type = await response.json();
        
        document.querySelector('#typeForm input[name="typeId"]').value = type.typeId;
        document.querySelector('#typeForm input[name="typeName"]').value = type.typeName;
    } catch (error) {
        console.error('Error:', error);
        showAlert('Error loading type details', 'error');
    }
}

async function saveType() {
    const form = document.getElementById('typeForm');
    const formData = new FormData(form);
    const typeData = {
        typeId: formData.get('typeId'),
        typeName: formData.get('typeName')
    };

    try {
        const url = typeData.typeId ? 
            `https://localhost:44324/api/CategoryType/types/${typeData.typeId}` :
            'https://localhost:44324/api/CategoryType/types';
        


        const response = await fetch(url, {
            method: typeData.typeId ? 'PUT' : 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(typeData)
        });

        if (!response.ok) throw new Error('Failed to save type');

        $('#typeModal').modal('hide');
        showAlert('Type saved successfully', 'success');
        initializeTables();
    } catch (error) {
        console.error('Error:', error);
        showAlert('Error saving type', 'error');
    }
}

async function deleteType(typeId) {
    if (!confirm('Are you sure you want to delete this type?')) return;

    try {
        const response = await fetch(`https://localhost:44324/api/CategoryType/types/${typeId}`, {
            method: 'DELETE'
        });

        if (!response.ok) throw new Error('Failed to delete type');
        
        showAlert('Type deleted successfully', 'success');
        initializeTables();
    } catch (error) {
        console.error('Error:', error);
        showAlert('Error deleting type', 'error');
    }
}

// Alert function matching your pattern
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

// Initialize when document is ready
document.addEventListener('DOMContentLoaded', function() {
    initializeTables();

    // Add form submit handlers
    document.getElementById('saveCategory').addEventListener('click', saveCategory);
    document.getElementById('saveType').addEventListener('click', saveType);

    // Reset forms when modals are closed
    $('#categoryModal, #typeModal').on('hidden.bs.modal', function() {
        this.querySelector('form').reset();
    });
});