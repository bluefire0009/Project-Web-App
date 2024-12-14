import React from "react"
import '../Styling/UserPage.css'
import UserPageBanner from '../assets/UserPageBanner.jpg'
import UserTable from './UserTable'

export type schedule = {title:string, place:string, date:string, time:string, description:string}

type UserPageProps = {UserName:string, workSchedules:schedule[], eventSchedules:schedule[]}

class UserPageMain extends React.Component<UserPageProps, {}> {
    
    constructor(props:UserPageProps){
        super(props)

        this.state = {}
    }

    render(): React.ReactNode {
        return <div className="card userPage">
            <img className="card__banner" src={UserPageBanner} alt="" />
            <h1>{this.props.UserName}</h1>
            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vitae nisi ut ante pharetra faucibus sit amet eget ante. Proin in ultrices nulla. Donec condimentum leo nulla. Vestibulum id varius metus, sit amet accumsan ipsum. Ut tincidunt massa sed aliquam tempus. Phasellus sed tempus sem, feugiat ultrices nibh. Donec laoreet rutrum sagittis. Nullam pulvinar velit vitae ligula auctor tincidunt. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed in tristique ex. Aliquam viverra ipsum a ultrices venenatis. Pellentesque pulvinar elit et elementum auctor.
            </p>
            <UserTable title="Your schedule for this week" schedules={this.props.workSchedules}/>
            <UserTable title="Your upcoming events" schedules={this.props.eventSchedules}/>
        </div>
    }
}

export default UserPageMain