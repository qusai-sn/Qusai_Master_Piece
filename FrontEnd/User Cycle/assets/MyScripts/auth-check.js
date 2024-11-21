// auth-check.js
(function() {
    function checkAuth() {
        const token = localStorage.getItem('authToken');
        const userId = localStorage.getItem('userId');

        if (!token || !userId) {
            // No token or userId found, redirect to login
            window.location.href = 'login.html';
            return false;
        }
        return true;
    }

    // Run check when page loads
    document.addEventListener('DOMContentLoaded', checkAuth);

    // Expose function globally if needed
    window.checkAuth = checkAuth;
})();