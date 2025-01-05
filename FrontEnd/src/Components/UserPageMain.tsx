import React from "react"
import '../Styling/UserPage.css'
import UserPageBanner from '../assets/UserPageBanner.jpg'

type schedule = {title:string, location:string, date:string, time:string, description:string}

type UserPageState = {UserName:string, workSchedules:schedule[], eventSchedules:schedule[]}
type UserPageProps = {UserId:number|null}

class UserPageMain extends React.Component<UserPageProps, UserPageState> {
    constructor(props:UserPageProps, state:UserPageState){
        super(props)

        this.state = state
    }

    async componentDidMount() {
        const UserName:string = await fetch(`http://localhost:5097/api/user/read?userId=${this.props.UserId}`)
        .then(response => response.json())
        .then(content => `${content.firstName} ${content.lastName}`)

        this.setState(prev => ({...prev, UserName:UserName}))

        var eventSchedules = await fetch(`http://localhost:5097/api/user/getEvents?userId=${this.props.UserId}`)
        .then(response => response.json())
        .then(content => 
            content.map((event: { title: any; location: any; eventDate: any; startTime: any; description: any }) => ({
                title: event.title,
                location: event.location,
                date: event.eventDate,
                time: event.startTime,
                description: event.description,
            }))
        )
        
        this.setState(prev => ({...prev, eventSchedules:eventSchedules}))

        var workSchedules = await fetch(`http://localhost:5097/api/user/GetWorkAttendances?userId=${this.props.UserId}`)
        .then(response => response.json())
        .then(content => 
            content.map((work: { attendanceDate: any; userId: any; time:any; location:any}) => ({
                location: work.location ?? "in office",
                date: work.attendanceDate,
                time: work.time ?? "all day",
            }))
        )
        
        this.setState(prev => ({...prev, workSchedules:workSchedules}))

    }

    render(): React.ReactNode {
        return <div className="card userPage">
            <img className="card__banner" src={UserPageBanner} alt="" />
            <h1>{this.state.UserName}</h1>
            <p>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vitae nisi ut ante pharetra faucibus sit amet eget ante. Proin in ultrices nulla. Donec condimentum leo nulla. Vestibulum id varius metus, sit amet accumsan ipsum. Ut tincidunt massa sed aliquam tempus. Phasellus sed tempus sem, feugiat ultrices nibh. Donec laoreet rutrum sagittis. Nullam pulvinar velit vitae ligula auctor tincidunt. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed in tristique ex. Aliquam viverra ipsum a ultrices venenatis. Pellentesque pulvinar elit et elementum auctor.
            </p>

            <div>
                <h1>Your work schedule for this week</h1>
                <table>
                    <thead>
                        <tr>
                            <th>Place</th>
                            <th>Date</th>
                            <th>Time</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            !!this.state.workSchedules && this.state.workSchedules.length > 0
                            ? this.state.workSchedules.map(schedule => {
                                return <tr key={schedule.title + schedule.date}>
                                    <td>{schedule.location}</td>
                                    <td>{schedule.date}</td>
                                    <td>{schedule.time}</td>
                                </tr>
                            })
                            : <></>
                        }
                    </tbody>
                </table>
            </div>
            
            <div>
                <h1>Your upcoming events</h1>
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
                            !!this.state.eventSchedules && this.state.eventSchedules.length > 0
                            ? this.state.eventSchedules.map(schedule => {
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
        </div>
    }
}

export default UserPageMain