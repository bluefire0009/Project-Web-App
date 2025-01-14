import React from "react";
import Api_url from "./Api_url";

type User = { name: string, email: string, id: number }


export class ListAllUsers extends React.Component<{}, { Users: User[], Message: string }> {
    constructor(props: {}) {
        super(props)

        this.state = {
            Users: [],
            Message: ""
        };
    }
    // Fetch users from the API
    GetallUsers = async () => {
        try {
            const response = await fetch(`${Api_url}/api/User/All`, {
                method: `GET`
            });

            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }

            const data: User[] = await response.json();
            this.setState({ Users: data });
        } catch (error) {
            this.setState({ Message: `${error}` })
        }
    };

    componentDidMount() {
        this.GetallUsers(); // Fetch users when the component mounts
    }



    render(): JSX.Element {
        return (
            <>
                <table>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Email</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.Users.length > 0 ? (
                            this.state.Users.map((user, Id) => (
                                <tr key={Id}>
                                    <td>{user.name}</td>
                                    <td>{user.email}</td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan={2}>No Users available.</td>
                            </tr>
                        )}
                    </tbody>
                </table>
                {this.state.Message}
            </>
        );
    }
}