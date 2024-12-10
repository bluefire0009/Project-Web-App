import React from "react";
import {schedule} from './UserPageMain';
import '../Styling/UserPage.css';

type UserTableProps = {title:string, schedules:schedule[]}
class UserWorkSchedule extends React.Component<UserTableProps, {}> {
    constructor(props:UserTableProps){
        super(props)

        this.state = {}
    }

    render(): React.ReactNode {
        return <div className="card">
            <h1>{this.props.title}</h1>
            <table>
                <tr>
                    <th>Title</th>
                    <th>Place</th>
                    <th>Date</th>
                    <th>Time</th>
                    <th>Description</th>
                </tr>
                {this.props.schedules.map(schedule => {
                    return <tr>
                        {Object.values(schedule).map(data => {
                            return <td>{data}</td>
                        })}
                    </tr>
                })}
            </table>
        </div>
    }
}

export default UserWorkSchedule;