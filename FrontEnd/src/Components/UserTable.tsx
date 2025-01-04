import React from "react";
import {schedule} from './UserPageMain';
import '../Styling/UserPage.css';

type UserTableProps = {title:string, schedules:schedule[]}
class UserSchedule extends React.Component<UserTableProps, {}> {
    constructor(props:UserTableProps){
        super(props)

        this.state = {}
    }

    render(): React.ReactNode {
        return <div>
            <h1>{this.props.title}</h1>
            <table>
                <thead>
                    <tr>
                        <th>Title</th>
                        <th>Place</th>
                        <th>Date</th>
                        <th>Time</th>
                        <th>Description</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        !!this.props.schedules && this.props.schedules.length > 0
                        ? this.props.schedules.map(schedule => {
                            return <tr key={schedule.title + schedule.date}>
                                <td>{schedule.title}</td>
                                <td>{schedule.location}</td>
                                <td>{schedule.date}</td>
                                <td>{schedule.time}</td>
                                <td>{schedule.description}</td>
                            </tr>
                        })
                        : <></>
                    }
                </tbody>
            </table>
        </div>
    }
}

export default UserSchedule ;