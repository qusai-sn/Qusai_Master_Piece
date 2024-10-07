// Function to display tickets in the cart
function displayTicketsInCart() {
    const ticketData = localStorage.getItem('eventTickets'); // Retrieve the tickets from local storage
    const cartTableBody = document.querySelector('.cart__table tbody'); // Select the cart table body

    // Clear existing rows in the table body
    cartTableBody.innerHTML = '';

    if (ticketData) {
        // Parse the ticket data as JSON
        const tickets = JSON.parse(ticketData);

        // Loop through each ticket and create a row in the cart table
        tickets.forEach(ticket => {
            const row = document.createElement('tr'); // Create a new table row

            // Populate the row with ticket details
            row.innerHTML = `
                <td class="product__thumb">
                    <a href="shop-details.html"><img src="assets/img/shop/shop_img01.jpg" alt=""></a>
                </td>
                <td class="product__name">
                    <a href="shop-details.html">${ticket.title}</a> <!-- Ticket Title -->
                </td>
                <td class="product__price">$${ticket.price.toFixed(2)}</td> <!-- Price -->
                <td class="product__quantity">
                    <div class="cart-plus-minus">
                        <input type="text" value="${ticket.quantity}" readonly> <!-- Quantity -->
                    </div>
                </td>
                <td class="product__subtotal">$${ticket.subtotal.toFixed(2)}</td> <!-- Subtotal -->
                <td class="product__remove">
                    <a href="#" onclick="removeTicket('${ticket.title}')">×</a> <!-- Add remove functionality -->
                </td>
            `;

            // Append the row to the table body
            cartTableBody.appendChild(row);
        });
    } else {
        // Optional: Display a message if no tickets are found
        const row = document.createElement('tr');
        row.innerHTML = `
            <td colspan="6" class="text-center">No tickets in cart</td>
        `;
        cartTableBody.appendChild(row);
    }
}

// Call the function to display tickets when the page loads
window.onload = displayTicketsInCart;

// Function to remove a ticket from the cart
function removeTicket(ticketTitle) {
    // Retrieve the current tickets from local storage
    const ticketData = JSON.parse(localStorage.getItem('eventTickets'));

    // Filter out the ticket to be removed
    const updatedTickets = ticketData.filter(ticket => ticket.title !== ticketTitle);

    // Update local storage with the remaining tickets
    localStorage.setItem('eventTickets', JSON.stringify(updatedTickets));

    // Refresh the cart display
    displayTicketsInCart();
}
