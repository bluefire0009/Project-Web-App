import Api_url from "./Api_url";

// return the userId
export const fetchUserId = async (): Promise<number | null> => {
    try {
        const response = await fetch(`${Api_url}/api/getUserId`, {
            method: "GET",
            credentials: "include", // Include cookies for the session
        });
        const userId = await response.json();

        if (userId === -1) {
            // alert("No user is currently logged in.");
            return null;
        } else {
            // alert(`Your User ID is: ${userId}`);
            return userId; // Return the userId
        }
    } catch (error) {
        console.error("Error fetching UserId:", error);
        // alert("An error occurred while fetching the User ID.");
        return null;
    }
};

// retrun the username of admin or user
export const fetchUserName = async (): Promise<string> => {
    try {
        const response = await fetch(`${Api_url}/api/GetUserName`, {
            method: "GET",
            credentials: "include", // Include cookies for the session
        });

        if (response.ok) {
            const content = await response.text();
            return `${content}`;
        } else {
            throw new Error("Failed to fetch user name");
        }
    } catch (error) {
        console.error("Error fetching user name:", error);
        return "Error fetching name";
    }
};
