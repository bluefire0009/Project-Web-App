import Api_url from "./Api_url";

// Modify fetchAndAlertUserId to return the userId
export const fetchAndAlertUserId = async (): Promise<number | null> => {
    try {
        const response = await fetch(`${Api_url}/api/getUserId`, {
            method: "GET",
            credentials: "include", // Include cookies for the session
        });
        const userId:number = await response.json();
        if (userId === -1) {
            // alert("No user is currently logged in.");
            return null;
        } else {
            // alert(`Your User ID is: ${userId}`);
            return userId; // Return the userId
        }
    } catch (error) {
        console.error("Error fetching UserId:", error);
        alert("An error occurred while fetching the User ID.");
        return null;
    }
};

// Modify fetchUserName to use the returned userId
export const fetchUserName = async (): Promise<string> => {
    try {
        const userId = await fetchAndAlertUserId(); // Get the userId

        if (userId === null) {
            return "No user logged in"; // If no user is logged in, return an appropriate message
        }

        const response = await fetch(`${Api_url}/api/user/read?userId=${userId}`, {
            method: "GET",
            credentials: "include", // Include cookies for the session
        });

        if (response.ok) {
            const content = await response.json();
            return `${content.firstName} ${content.lastName}`;
        } else {
            throw new Error("Failed to fetch user data");
        }
    } catch (error) {
        console.error("Error fetching user name:", error);
        return "Error fetching name";
    }
};
