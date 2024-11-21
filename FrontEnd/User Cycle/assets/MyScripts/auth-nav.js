// auth-nav.js
document.addEventListener('DOMContentLoaded', function() {
    const isLoggedIn = localStorage.getItem('authToken') && localStorage.getItem('userId');
    
    // Get all nav elements
    const loginBtn = document.querySelector('.login-btn');
    const dashboardNav = document.querySelector('.dashboard-nav');
    const userMenus = document.querySelectorAll('.user-menu');

    if (isLoggedIn) {
        // User is logged in
        if (loginBtn) {
            loginBtn.innerHTML = `
                <a href="#" onclick="logout()">Log out</a>
            `;
        }

        // Show dashboard options
        if (dashboardNav) {
            dashboardNav.style.display = 'block';
        }

        // Show user menus
        userMenus.forEach(menu => {
            menu.style.display = 'block';
        });
    } else {
        // User is logged out
        if (loginBtn) {
            loginBtn.innerHTML = `
                <a href="login.html">Log in</a>
            `;
        }

        // Hide dashboard options
        if (dashboardNav) {
            dashboardNav.style.display = 'none';
        }

        // Hide user menus
        userMenus.forEach(menu => {
            menu.style.display = 'none';
        });
    }
});

// Logout function
function logout() {
    // Clear localStorage
    localStorage.removeItem('authToken');
    localStorage.removeItem('userId');
    localStorage.removeItem('username');

    // Redirect to home page
    window.location.href = 'index.html';
}

// Make logout function global
window.logout = logout;