import React, { useEffect, useState } from "react";
import "../Styling/UserPage.css";
import UserPageBanner from "../assets/UserPageBanner.jpg";
import Api_url from "./Api_url";
import { useNavigate, useParams } from "react-router";

type schedule = { title: string; location: string; date: string; time: string; description: string };

type UserPageProps = {};

const syncToGoogle = (userId: number) => { 
    fetch(`${Api_url}/api/google/syncAll/${userId}`);
};

const UserPageMain: React.FC<UserPageProps> = () => {
  const [userId, setUserId] = useState<number | null>(null);
  const [userName, setUserName] = useState<string>("");
  const [workSchedules, setWorkSchedules] = useState<schedule[]>([]);
  const [eventSchedules, setEventSchedules] = useState<schedule[]>([]);
  const [viewingOwnProfile, setViewingOwnProfile] = useState<boolean>(false);

  const { UserId } = useParams<{ UserId: string }>();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchUserData = async () => {
      let id = UserId !== undefined ? parseInt(UserId) : null;
      if (id !== null) {
        setUserId(id);
        setViewingOwnProfile(false);
      } else {
        const userIdFromApi = await fetch(`${Api_url}/api/getUserId`, {
          method: 'GET',
          credentials: 'include', // Include cookies for the session
        }).then(response => response.json());

        if (userIdFromApi === -1) {
          navigate("/");
          return;
        } else {
          setUserId(parseInt(userIdFromApi));
          id = userIdFromApi;
          setViewingOwnProfile(true);
        }
      }

      const userNameResponse: string = await fetch(`${Api_url}/api/user/read?userId=${id}`)
        .then(response => response.json())
        .then(content => `${content.firstName} ${content.lastName}`);

      setUserName(userNameResponse);

      const eventSchedulesResponse = await fetch(`${Api_url}/api/user/getUpcomingEvents?userId=${id}`)
        .then(response => response.json())
        .then(content =>
          content.map((event: { title: string; location: string; eventDate: string; startTime: string; description: string }) => ({
            title: event.title,
            location: event.location,
            date: event.eventDate,
            time: event.startTime,
            description: event.description,
          }))
        );
      setEventSchedules(eventSchedulesResponse);

      const workSchedulesResponse = await fetch(`${Api_url}/api/user/GetUpcomingWorkAttendances?userId=${id}`)
        .then(response => response.json())
        .then(content =>
          content.map((work: { attendanceDate: string; userId: number; time: string; location: string }) => ({
            location: work.location ?? "in office",
            date: work.attendanceDate,
            time: work.time ?? "all day",
          }))
        );
      setWorkSchedules(workSchedulesResponse);
    };

    fetchUserData();
  }, [UserId, navigate]);

  return (
    <div className="card userPage">
      <img className="card__banner" src={UserPageBanner} alt="" />
      {viewingOwnProfile && userId !== null ? (
        <button onClick={() => syncToGoogle(userId)}>Sync to Google</button>
      ) : null}
      <h1>{userName}</h1>
      <p>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vitae nisi ut ante pharetra faucibus sit amet
        eget ante. Proin in ultrices nulla. Donec condimentum leo nulla. Vestibulum id varius metus, sit amet accumsan
        ipsum. Ut tincidunt massa sed aliquam tempus. Phasellus sed tempus sem, feugiat ultrices nibh. Donec laoreet
        rutrum sagittis. Nullam pulvinar velit vitae ligula auctor tincidunt. Pellentesque habitant morbi tristique
        senectus et netus et malesuada fames ac turpis egestas. Sed in tristique ex. Aliquam viverra ipsum a ultrices
        venenatis. Pellentesque pulvinar elit et elementum auctor.
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
            {workSchedules.length > 0 ? (
              workSchedules.map((schedule, index) => (
                <tr key={index}>
                  <td>{schedule.location}</td>
                  <td>{schedule.date}</td>
                  <td>{schedule.time}</td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan={3}>No work schedules available.</td>
              </tr>
            )}
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
            {eventSchedules.length > 0 ? (
              eventSchedules.map((schedule, index) => (
                <tr key={index}>
                  <td>{schedule.title}</td>
                  <td>{schedule.location}</td>
                  <td>{schedule.date}</td>
                  <td>{schedule.time}</td>
                  <td>{schedule.description}</td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan={5}>No event schedules available.</td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default UserPageMain;
