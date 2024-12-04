import React from "react";
import '../Styling/UserPage.css';
import UserPageBanner from '../assets/UserPageBanner.jpg'

class Banner extends React.Component<{}, {}> {
    constructor(props:{}){
        super(props)

        this.state = {}
    }

    render(): React.ReactNode {
        return <img className="card__banner" src={UserPageBanner} alt="" />
    }
}

export default Banner;