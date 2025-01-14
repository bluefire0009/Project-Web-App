const Api_url = "your_api_url"; // Replace with the actual API URL

// Function to check if user is logged in
export const UserLoggedIn = async (): Promise<boolean> => {
    try {
        const response = await fetch(`${Api_url}/api/IsUserLoggedIn`, {
            method: 'GET',
            credentials: 'include', // Include cookies for the session
        });

        if (response.ok) {
            return true; // User is logged in
        } else {
            return false; // User is not logged in
        }
    } catch (error) {
        console.error("Error checking if user is logged in:", error);
        return false; // Return false in case of error
    }
};

// Function to check if admin is logged in
export const AdminLoggedIn = async (): Promise<boolean> => {
    try {
        const response = await fetch(`${Api_url}/api/IsAdminLoggedIn`, {
            method: 'GET',
            credentials: 'include', // Include cookies for the session
        });

        if (response.ok) {
            return true; // Admin is logged in
        } else {
            return false; // Admin is not logged in
        }
    } catch (error) {
        console.error("Error checking if admin is logged in:", error);
        return false; // Return false in case of error
    }
};
