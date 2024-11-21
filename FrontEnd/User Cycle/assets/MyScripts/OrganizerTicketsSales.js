// Make sure to include the XLSX library
// <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.18.5/xlsx.full.min.js"></script>

async function loadSalesTable() {
    try {
        const organizerId = 2;
        const response = await fetch(`https://localhost:7293/api/OrganizerDashboard/recent/${organizerId}`);
        const data = await response.json();
 
        const tbody = document.querySelector('#ticket-table tbody');
        tbody.innerHTML = data.map(sale => `
            <tr>
                <td><p>${sale.orderId}</p></td>
                <td><p>${sale.eventName}</p></td>
                <td><p>${new Date(sale.date).toLocaleDateString('en-US', {
                    year: 'numeric',
                    month: 'long', 
                    day: '2-digit'
                })}</p></td>
                <td><p>$${sale.price.toFixed(2)}</p></td>
            </tr>
        `).join('');
 
    } catch (error) {
        console.error('Error loading sales:', error);
    }
 }
 

 
 async function downloadSalesReport() {
    try {
        const organizerId = localStorage.getItem('userId');
        const response = await fetch(`https://localhost:7293/api/OrganizerDashboard/all/${organizerId}`);
        const data = await response.json();
        
        // Format data for Excel
        const formattedData = data.map(sale => ({
            'Order ID': sale.orderId,
            'Event Name': sale.eventName,
            'Date': new Date(sale.date).toLocaleDateString(),
            'Price': `$${sale.price.toFixed(2)}`,
            'Status': sale.status || 'Processing'
        }));
 
        downloadExcel(formattedData, 'sales_report.xlsx');
    } catch (error) {
        console.error('Error downloading report:', error);
    }
 }
 
 function downloadExcel(jsonData, fileName = 'data.xlsx') {
    const worksheet = XLSX.utils.json_to_sheet(jsonData);
    const workbook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workbook, worksheet, "Sheet1");
    const excelBuffer = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    const blob = new Blob([excelBuffer], { type: "application/octet-stream" });
    const link = document.createElement("a");
    const url = URL.createObjectURL(blob);
    link.href = url;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    URL.revokeObjectURL(url);
 }
 
 // Initialize on page load
 document.addEventListener('DOMContentLoaded', loadSalesTable);